using org.DownesWard.Utilities;
using System;
using System.Collections.Generic;
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

        public string Name { get; set; }
        public int TermsServed { get; set; }
        public List<Benefit> Material { get; } = new List<Benefit>();
        public long[] Cash { get; } = new long[7];
        public virtual Character Owner { get; set; }
        public int RankNumber { get; set; }
        public bool Retired { get; set; }

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
                    Owner.Benefits.Add("Retirement Pay", new Benefit() { Name = "Retirement Pay", Value = pension, TypeOfBenefit = Benefit.BenefitType.Material } );
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
            if (Owner.Benefits.ContainsKey("Cash"))
            {
                Owner.Benefits["Cash"].Value += Cash[roll];
            }
            else
            {
                Owner.Benefits.Add("Cash", new Benefit() { Name = "Cash", Value = Cash[roll], TypeOfBenefit = Benefit.BenefitType.Cash });
            }
        }
    }
}
