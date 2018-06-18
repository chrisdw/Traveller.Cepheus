using System;
using System.Collections.Generic;
using System.Text;

namespace Traveller.CharacterGeneration
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
        public Character Owner { get; set; }
        public int RankNumber { get; set; }
        public bool Retired { get; set; }

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
    }
}
