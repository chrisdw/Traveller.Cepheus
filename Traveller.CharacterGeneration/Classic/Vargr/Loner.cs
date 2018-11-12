using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Vargr
{
    public class Loner : Career
    {
        public Loner()
        {
            Name = "Loner";

            enlistment = 7;
            enlistment1attr = "STR";
            enlistment1val = 6;
            enlistment2attr = "DEX";
            enlistment2val = 8;
            survival = 6;
            survival2attr = "DEX";
            survival2val = 9;

            reenlist = 5;

            hasRanks = false;

            RankNumber = 0;
            TermSkills = 2;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = "Personal Development";
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Dex;
            skills[2] = SkillLibrary.End;
            skills[3] = SkillLibrary.Infighting;
            skills[4] = SkillLibrary.Gambling;
            skills[5] = SkillLibrary.ChrDrop;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = "Service Skills";
            skills = table.Skills;
            skills[0] = SkillLibrary.Vehicle;
            skills[1] = SkillLibrary.VaccSuit;
            skills[2] = SkillLibrary.Electronics;
            skills[3] = SkillLibrary.JackOfTrades;
            skills[4] = SkillLibrary.Prospecting;
            skills[5] = SkillLibrary.Prospecting;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = "Education";
            skills = table.Skills;
            skills[0] = SkillLibrary.Streetwise;
            skills[1] = SkillLibrary.Forgery;
            skills[2] = SkillLibrary.Bribery;
            skills[3] = SkillLibrary.JackOfTrades;
            skills[4] = SkillLibrary.Computer;
            skills[5] = SkillLibrary.Medic;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = "High Charisma";
            skills = table.Skills;
            skills[0] = SkillLibrary.Medic;
            skills[1] = SkillLibrary.JackOfTrades;
            skills[2] = SkillLibrary.Computer;
            skills[3] = SkillLibrary.Computer;
            skills[4] = SkillLibrary.Pilot;
            skills[5] = SkillLibrary.Streetwise;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Gun);
            Material.Add(BenefitLibrary.Blade);
            Material.Add(BenefitLibrary.MidPsg);
            Material.Add(BenefitLibrary.MidPsg);
            Material.Add(BenefitLibrary.Seeker);

            Cash[0] = 0;
            Cash[1] = 0;
            Cash[2] = 10000;
            Cash[3] = 10000;
            Cash[4] = 30000;
            Cash[5] = 40000;
            Cash[6] = 100000;

            Ranks[0] = "Loner";
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

        public override void EndTerm()
        {
            base.EndTerm();
            TermSkills = 2;
        }

    }
}
