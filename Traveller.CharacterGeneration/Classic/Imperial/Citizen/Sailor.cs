using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Imperial.Citizen
{
    public class Sailor : Career
    {
        public Sailor()
        {
            Name = "Sailor";
            CurrentRank = 0;
            TermSkills = 2;

            enlistment = 6;
            enlistment1attr = "END";
            enlistment1val = 10;
            enlistment2attr = "STR";
            enlistment2val = 8;
            survival = 5;
            survival2attr = "END";
            survival2val = 8;
            position = 5;
            position1attr = "INT";
            position1val = 9;
            promotion = 6;
            promotion1attr = "EDU";
            promotion1val = 8;
            reenlist = 6;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = "Personal Development";
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Dex;
            skills[2] = SkillLibrary.End;
            skills[3] = SkillLibrary.Gambling;
            skills[4] = SkillLibrary.Brawling;
            skills[5] = SkillLibrary.Carousing;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = "Service Skills";
            skills = table.Skills;
            skills[0] = SkillLibrary.GunCombat;
            skills[1] = SkillLibrary.Communications;
            skills[2] = SkillLibrary.FowardObserver;
            skills[3] = SkillLibrary.Vehicle;
            skills[4] = SkillLibrary.Vehicle;
            skills[5] = SkillLibrary.BattleDress;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = "Education";
            skills = table.Skills;
            skills[0] = SkillLibrary.Watercraft;
            skills[1] = SkillLibrary.Electronics;
            skills[2] = SkillLibrary.Mechanical;
            skills[3] = SkillLibrary.Gravitics;
            skills[4] = SkillLibrary.Navigation;
            skills[5] = SkillLibrary.Demolitions;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = "Advanced Education";
            skills = table.Skills;
            skills[0] = SkillLibrary.Medic;
            skills[1] = SkillLibrary.Vehicle;
            skills[2] = SkillLibrary.Streetwise;
            skills[3] = SkillLibrary.Computer;
            skills[4] = SkillLibrary.Admin;
            skills[5] = SkillLibrary.JackOfTrades;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.Soc);

            Cash[0] = 2000;
            Cash[1] = 5000;
            Cash[2] = 10000;
            Cash[3] = 10000;
            Cash[4] = 10000;
            Cash[5] = 20000;
            Cash[6] = 30000;

            Ranks[0] = "Sailor";
            Ranks[1] = "Ensign";
            Ranks[2] = "Lieutenant";
            Ranks[3] = "Lt. Commander";
            Ranks[4] = "Commander";
            Ranks[5] = "Captain";
            Ranks[6] = "Admiral";
        }
        protected override void CommsionSkill()
        {
            // Nothing to do
        }

        protected override int EnlistFactor()
        {
            return 0;
        }

        protected override void EnlistSkill()
        {
            // Nothing to do
        }

        protected override void RankSkill()
        {
            // Nothing to do
        }
    }
}
