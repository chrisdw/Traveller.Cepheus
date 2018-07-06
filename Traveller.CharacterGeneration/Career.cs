﻿using org.DownesWard.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration
{
    public class Career
    {
        public enum CareerType
        {
            Imperial_Navy,
            Imperial_Marines,
            Imperial_Army,
            Imperial_Scouts,
            Imperial_Merchants,
            Imperial_Other,
            Citizen_Pirate,
            Citizen_Belter,
            Citizen_Sailor,
            Citizen_Doctor,
            Citizen_Diplomat,
            Citizen_Flyer,
            Citizen_Barbarian,
            Citizen_Bureaucrat,
            Citizen_Rogue,
            Citizen_Noble,
            Citizen_Scientist,
            Citizen_Hunter,
            SwordWorlds_Patrol,
            Vargr_Corsair,
            Vargr_Army,
            Vargr_Navy,
            Vargr_Emissary,
            Vargr_Merchant,
            Vargr_Loner,
            Dolphin_Military,
            Dolphin_Civilian,
            Zhodani_Navy,
            Zhodani_ConsularGuard,
            Zhodani_Army,
            Zhodani_Merchant,
            Zhodani_Government,
            Zhodani_Prole,
            Darrian_Navy,
            Darrian_SpecialArm,
            Darrian_Army,
            Darrian_Merchant,
            Darrian_Noble,
            Darrian_Academic
        }

        [Flags]
        public enum BenefitPick
        {
            None = 0,
            Skill = 1,
            Weapon = 2
        }

        public struct BenefitResolution
        {
            public BenefitPick pick;
            public Benefit benefit;
        }

        public string Name { get; set; }
        public int TermsServed { get; set; }
        public List<Benefit> Material { get; } = new List<Benefit>();
        public int[] Cash { get; } = new int[7];
        public virtual Character Owner { get; set; }
        public int RankNumber { get; set; }
        public bool Retired { get; set; }
        public ICulture Culture { get; set; }

        protected Dice dice = new Dice(6);

        public virtual int MusterOutRolls()
        {
            var rolls = TermsServed;
            switch (RankNumber)
            {
                case 0:
                    break;
                case 1:
                case 2:
                    rolls++;
                    break;
                default:
                    rolls += 2;
                    break;
            }
            return rolls;
        }

        public void MusterOut()
        {
            if (Retired)
            {
                if (TermsServed > 4)
                {
                    int pension = ((TermsServed - 4) * 2000) + 2000;
                    if (Owner.Culture == Constants.CultureType.Zhodani && Owner.Profile.Soc.Value > 10)
                    {
                        pension *= 2;
                    }
                    Owner.AddBenefit(new Benefit() { Name = "Retirement Pay", Value = pension, TypeOfBenefit = Benefit.BenefitType.Material });
                }
            }
        }

        public void ResolveCashBenefit()
        {
            var roll = dice.roll();

            if (Owner.Skills.ContainsKey("Gambling") || Owner.Skills.ContainsKey("Prospecting") || Retired)
            {
                roll++;
            }
            Owner.AddBenefit(new Benefit() { Name = "Cash", Value = Cash[roll-1], TypeOfBenefit = Benefit.BenefitType.Cash });
        }

        public BenefitResolution ResolveMaterialBenefit()
        {
            var result = new BenefitResolution() { pick = BenefitPick.None };
            
            var roll = dice.roll();
            if (Owner.Culture == Constants.CultureType.Darrian)
            {
                if (RankNumber >= 5 || Owner.Profile.Soc.Value >= 11)
                {
                    roll++;
                }
            }
            else
            {
                if (RankNumber >= 5 || Owner.Skills.ContainsKey("Prospecting"))
                {
                    roll++;
                }
            }
            var benefit = Material[roll];
            result.benefit = benefit;
            if (Culture.BenefitAllowed(Owner, benefit))
            {
                Owner.Journal.Add(string.Format("Received {0} value {1} as a benefit", benefit.Name, benefit.Value));
                if (benefit.TypeOfBenefit == Benefit.BenefitType.Material)
                {
                    Owner.AddBenefit(benefit);
                }
                else if (benefit.TypeOfBenefit == Benefit.BenefitType.Skill)
                {
                    Owner.AddSkill(new Skill() { Class = Skill.SkillClass.None, Name = benefit.Name, Level = benefit.Value });
                }
                else if (benefit.TypeOfBenefit == Benefit.BenefitType.AttributeModification)
                {
                    Owner.Profile[benefit.Name].Value += benefit.Value;
                }
                else if (benefit.TypeOfBenefit == Benefit.BenefitType.Weapon)
                {
                    result.pick = ResolveWeaponBenefit();
                }
                else if (benefit.TypeOfBenefit != Benefit.BenefitType.None)
                {
                    Owner.AddBenefit(benefit);
                }
            }
            else
            {
                Owner.Journal.Add(string.Format("Not allowed to receive {0} value {1} as a benefit", benefit.Name, benefit.Value));
            }
            return result;
        }

        private BenefitPick ResolveWeaponBenefit()
        {
            var result = BenefitPick.None;

            if (Owner.Benefits.Values.Any(b => b.TypeOfBenefit == Benefit.BenefitType.Weapon))
            {
                // has to be a weapon or a skill
                result = BenefitPick.Skill | BenefitPick.Weapon;
            }
            else
            {
                // has to be taken as a weapon
                result = BenefitPick.Weapon;
            }
            return result;
        }
    }
}