using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Imperial.Citizen
{
    public class Belter : BasicCareer
    {
        public Belter()
        {
            CurrentRank = 0;
            TermSkills = 3;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = "Personal Development";
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Dex;
            skills[2] = SkillLibrary.End;
            skills[3] = SkillLibrary.Gambling;
            skills[4] = SkillLibrary.Brawling;
            skills[5] = SkillLibrary.VaccSuit;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = "Service Skills";
            skills = table.Skills;
            skills[0] = SkillLibrary.VaccSuit;
            skills[1] = SkillLibrary.VaccSuit;
            skills[2] = SkillLibrary.Prospecting;
            skills[3] = SkillLibrary.FowardObserver;
            skills[4] = SkillLibrary.Prospecting;
            skills[5] = SkillLibrary.ShipsBoat;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = "Education";
            skills = table.Skills;
            skills[0] = SkillLibrary.ShipsBoat;
            skills[1] = SkillLibrary.Electronics;
            skills[2] = SkillLibrary.Prospecting;
            skills[3] = SkillLibrary.Mechanical;
            skills[4] = SkillLibrary.Prospecting;
            skills[5] = SkillLibrary.Instruction;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = "Advanced Education";
            skills = table.Skills;
            skills[0] = SkillLibrary.Navigation;
            skills[1] = SkillLibrary.Medic;
            skills[2] = SkillLibrary.Pilot;
            skills[3] = SkillLibrary.Computer;
            skills[4] = SkillLibrary.Engineering;
            skills[5] = SkillLibrary.JackOfTrades;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.Travellers);
            Material.Add(BenefitLibrary.Seeker);

            Cash[0] = 0;
            Cash[1] = 0;
            Cash[2] = 1000;
            Cash[3] = 10000;
            Cash[4] = 100000;
            Cash[5] = 100000;
            Cash[6] = 100000;

            Ranks[0] = "Belter";
        }

        public override Renlistment CanRenlist()
        {
            var renlist = Renlistment.Cant;
            var target = 7;

            renlist = BaseCanRenlist(renlist, target);
            return renlist;
        }

        public override bool Commission()
        {
            return false;
        }

        public override bool Enlist()
        {
            var target = 5;

            if (Owner.CharacterSpecies == Character.Species.Aslan)
            {
                if (Owner.Sex.Equals("Male"))
                {
                    target += 4;
                }
            }
            else if (Owner.CharacterSpecies == Character.Species.Virushi || Owner.CharacterSpecies == Character.Species.AelYael)
            {
                target--;
            }

            if (Owner.Profile.Dex.Value >= 7)
            {
                target -= 1;
            }
            if (Owner.Profile.Int.Value >= 6)
            {
                target -= 2;
            }

            var enlist = BaseEnlist(target);

            if (enlist)
            {
                Owner.AddSkill(SkillLibrary.VaccSuit);
            }
            return enlist;
        }

        public override void HandleRenlist(bool renlisted)
        {
            BaseRenlist(renlisted);
        }

        public override bool Promotion()
        {
            return false;
        }

        public override bool Survival()
        {
            var survive = true;

            var target = 9;

            target -= Term;

            if (dice.roll(2) >= target)
            {
                survive = true;
            }
            return survive;
        }
    }
}
