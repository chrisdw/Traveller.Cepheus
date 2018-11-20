using org.DownesWard.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace org.DownesWard.Traveller.CharacterGeneration
{
    public abstract class Career
    {
        public class SkillOfferedEventArgs
        {
            public Skill OfferedSkill { get; set; }
            public Character Owner { get; set; }
        }

        public event EventHandler<SkillOfferedEventArgs> SkillOffered;

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

        abstract public string RankName { get; }

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

        public virtual int MaxCashRolls()
        {
            return 3;
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
                    Owner.AddBenefit(new Benefit()
                    {
                        Name = Properties.Resources.Benefit_RetirementPay,
                        Value = pension,
                        TypeOfBenefit = Benefit.BenefitType.Material
                    });
                }
            }
        }

        public void ResolveCashBenefit()
        {
            var roll = dice.roll();

            if (Owner.Skills.ContainsKey(SkillLibrary.Gambling.Name) || Owner.Skills.ContainsKey(SkillLibrary.Prospecting.Name) || Retired)
            {
                roll++;
            }
            roll = roll.Clamp(1, Cash.Length);
            Owner.AddBenefit(new Benefit()
            {
                Name = Properties.Resources.Benefit_Cash,
                Value = Cash[roll - 1],
                TypeOfBenefit = Benefit.BenefitType.Cash
            });
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
                if (RankNumber >= 5 || Owner.Skills.ContainsKey(SkillLibrary.Prospecting.Name))
                {
                    roll++;
                }
            }
            roll = roll.Clamp(1, Material.Count);
            var benefit = Material[roll - 1];
            result.benefit = benefit;
            if (Culture.BenefitAllowed(Owner, benefit))
            {
                if (benefit.TypeOfBenefit == Benefit.BenefitType.Material)
                {
                    Owner.AddBenefit(benefit);
                }
                else if (benefit.TypeOfBenefit == Benefit.BenefitType.Skill)
                {
                    Owner.AddSkill(new Skill()
                    {
                        Class = Skill.SkillClass.None,
                        Name = benefit.Name,
                        Level = benefit.Value
                    });
                }
                else if (benefit.TypeOfBenefit == Benefit.BenefitType.AttributeModification)
                {
                    Owner.AddAttribute(benefit.Name, benefit.Value);
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
                Owner.Journal.Add(string.Format(Properties.Resources.Msg_BenefitNotAllowed, benefit.Name, benefit.Value));
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

        protected virtual void OnSkillOffered(Skill skill)
        {
            var e = new SkillOfferedEventArgs() { OfferedSkill = skill, Owner = Owner };
            SkillOffered?.Invoke(this, e);
        }

        public void SaveXML(XmlElement doc)
        {
            var career = doc.OwnerDocument.CreateElement("Career");
            career.SetAttribute("Name", Name);
            career.SetAttribute("TermsServed", TermsServed.ToString());
            career.SetAttribute("Rank", RankNumber.ToString());
            career.SetAttribute("RankName", RankName);
            career.SetAttribute("Implmentation", GetType().AssemblyQualifiedName);
            doc.AppendChild(career);
        }

        public void LoadXML(XmlElement doc)
        {
            Name = doc.GetAttribute("Name");
            RankNumber = int.Parse(doc.GetAttribute("Rank"));
            TermsServed = int.Parse(doc.GetAttribute("TermsServed"));
        }

        public static Career Load(XmlElement element)
        {
            var name = element.GetAttribute("Implmentation");

            var type = Type.GetType(name);

            var career = Activator.CreateInstance(type) as Career;
            career.LoadXML(element);

            return career;
        }
    }
}
