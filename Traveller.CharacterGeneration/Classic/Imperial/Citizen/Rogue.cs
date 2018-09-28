using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Imperial.Citizen
{
    public class Rogue : Career
    {
        public Rogue()
        {
            Name = "Rogue";

            CurrentRank = 0;
            TermSkills = 2;

            enlistment = 6;
            enlistment1attr = "SOC";
            enlistment1val = 8;
            enlistment2attr = "END";
            enlistment2val = 7;

            survival = 6;
            survival2attr = "INT";
            survival2val = 9;

            reenlist = 5;
            hasRanks = false;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = "Personal Development";
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Dex;
            skills[2] = SkillLibrary.End;
            skills[3] = SkillLibrary.Int;
            skills[4] = SkillLibrary.Brawling;
            skills[5] = SkillLibrary.Carousing;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = "Service Skills";
            skills = table.Skills;
            skills[0] = SkillLibrary.BladeCombat;
            skills[1] = SkillLibrary.GunCombat;
            skills[2] = SkillLibrary.Demolitions;
            skills[3] = SkillLibrary.Vehicle;
            skills[4] = SkillLibrary.Edu;
            skills[5] = SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = "Education";
            skills = table.Skills;
            skills[0] = SkillLibrary.Streetwise;
            skills[1] = SkillLibrary.Forgery;
            skills[2] = SkillLibrary.Bribery;
            skills[3] = SkillLibrary.Carousing;
            skills[4] = SkillLibrary.Liason;
            skills[5] = SkillLibrary.ShipTactics;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = "Advanced Education";
            skills = table.Skills;
            skills[0] = SkillLibrary.Medic;
            skills[1] = SkillLibrary.Bribery;
            skills[2] = SkillLibrary.Forgery;
            skills[3] = SkillLibrary.Computer;
            skills[4] = SkillLibrary.Leader;
            skills[5] = SkillLibrary.JackOfTrades;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Soc);
            Material.Add(BenefitLibrary.Gun);
            Material.Add(BenefitLibrary.Blade);
            Material.Add(BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.Travellers);

            Cash[0] = 0;
            Cash[1] = 0;
            Cash[2] = 10000;
            Cash[3] = 10000;
            Cash[4] = 50000;
            Cash[5] = 100000;
            Cash[6] = 100000;

            Ranks[0] = "Rogue";
        }
        protected override void CommsionSkill()
        {
           // Nothing to do
        }

        protected override int EnlistFactor()
        {
            throw new NotImplementedException();
        }

        protected override void EnlistSkill()
        {
            Owner.AddSkill(SkillLibrary.Streetwise);
        }

        protected override void RankSkill()
        {
            // Nothing to do
        }

        public override void EndTerm()
        {
            base.EndTerm();
            TermSkills = 2;
        }
    }
}
