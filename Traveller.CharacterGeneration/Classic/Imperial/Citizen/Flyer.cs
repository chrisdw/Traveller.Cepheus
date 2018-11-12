using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Imperial.Citizen
{
    public class Flyer : Career
    {
        public Flyer()
        {
            Name = "Flyer";
            RankNumber = 0;
            TermSkills = 2;

            enlistment = 6;
            enlistment1attr = "STR";
            enlistment1val =7;
            enlistment2attr = "DEX";
            enlistment2val = 9;
            survival = 5;
            survival2attr = "DEX";
            survival2val = 8;
            position = 5;
            position1attr = "EDU";
            position1val = 6;
            promotion = 8;
            promotion1attr = "EDU";
            promotion1val = 8;
            reenlist = 6;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = "Personal Development";
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Dex;
            skills[2] = SkillLibrary.Int;
            skills[3] = SkillLibrary.Gambling;
            skills[4] = SkillLibrary.Brawling;
            skills[5] = SkillLibrary.Carousing;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = "Service Skills";
            skills = table.Skills;
            skills[0] = SkillLibrary.Brawling;
            skills[1] = SkillLibrary.VaccSuit;
            skills[2] = SkillLibrary.GunCombat;
            skills[3] = SkillLibrary.Vehicle;
            skills[4] = SkillLibrary.Vehicle;
            skills[5] = SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = "Education";
            skills = table.Skills;
            skills[0] = SkillLibrary.Aircraft;
            skills[1] = SkillLibrary.Mechanical;
            skills[2] = SkillLibrary.Electronics;
            skills[3] = SkillLibrary.Gravitics;
            skills[4] = SkillLibrary.GunCombat;
            skills[5] = SkillLibrary.Survival;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = "Advanced Education";
            skills = table.Skills;
            skills[0] = SkillLibrary.Medic;
            skills[1] = SkillLibrary.Leader;
            skills[2] = SkillLibrary.Pilot;
            skills[3] = SkillLibrary.Computer;
            skills[4] = SkillLibrary.Admin;
            skills[5] = SkillLibrary.JackOfTrades;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.MidPsg);
            Material.Add(BenefitLibrary.Soc);

            Cash[0] = 2000;
            Cash[1] = 5000;
            Cash[2] = 10000;
            Cash[3] = 10000;
            Cash[4] = 10000;
            Cash[5] = 20000;
            Cash[6] = 30000;

            Ranks[0] = "Flyer";
            Ranks[1] = "Pilot";
            Ranks[2] = "Flight Leader";
            Ranks[3] = "Squadron Leader";
            Ranks[4] = "Staff Major";
            Ranks[5] = "Group Leader";
            Ranks[6] = "Air Marshall";

        }
        protected override void CommsionSkill()
        {
            // Nothing to do
        }

        protected override int EnlistFactor()
        {
            var target = 0;
            if (Owner.CharacterSpecies == Character.Species.AelYael)
            {
                target += 1;
            }
            else if (Owner.CharacterSpecies == Character.Species.Aslan && Owner.Sex.Equals("Female"))
            {
                target += 4;
            }
            return target;
        }

        protected override void EnlistSkill()
        {
            OnSkillOffered(SkillLibrary.Aircraft);
        }

        protected override void RankSkill()
        {
            // Nothing to do
        }
    }
}
