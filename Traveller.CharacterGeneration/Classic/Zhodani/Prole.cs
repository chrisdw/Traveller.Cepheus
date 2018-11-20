using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Zhodani
{
    public class Prole : Career
    {
        public Prole()
        {
            Name = "Prole";
            RankNumber = 0;
            TermSkills = 2;

            enlistment = 3;
            enlistment1attr = "DEX";
            enlistment1val = 8;
            enlistment2attr = "EDU";
            enlistment2val = 7;
            enlistment3attr = string.Empty;
            enlistment3val = 0;
            survival = 5;
            survival2attr = "INT";
            survival2val = 9;
            position = 8;
            position1attr = "INT";
            position1val = 7;
            position3attr = string.Empty;
            position3val = 0;
            promotion = 8;
            promotion1attr = "EDU";
            promotion1val = 8;
            reenlist = 5;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Properties.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Dex;
            skills[2] = SkillLibrary.End;
            skills[3] = SkillLibrary.Brawling;
            skills[4] = SkillLibrary.Edu;
            skills[5] = SkillLibrary.Prole;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Properties.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.Vehicle;
            skills[1] = SkillLibrary.Mechanical;
            skills[2] = SkillLibrary.Brawling;
            skills[3] = SkillLibrary.Prole;
            skills[4] = SkillLibrary.Prole;
            skills[5] = SkillLibrary.Prole;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Properties.Resources.Table_Education;
            skills = table.Skills;
            skills[0] = SkillLibrary.Prole;
            skills[1] = SkillLibrary.Prole;
            skills[2] = SkillLibrary.Prole;
            skills[3] = SkillLibrary.Computer;
            skills[4] = SkillLibrary.Brawling;
            skills[5] = SkillLibrary.JackOfTrades;

            table = new SkillTable();
            SkillTables[3] = table;

            Material.Add(BenefitLibrary.Soc);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.MidPsg);
            Material.Add(BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.Legion);

            Cash[0] = 1000;
            Cash[1] = 2000;
            Cash[2] = 5000;
            Cash[3] = 5000;
            Cash[4] = 10000;
            Cash[5] = 30000;

            Ranks[0] = "Worker";
            Ranks[1] = "Assistant Supervisor";
            Ranks[2] = "Supervisor";
            Ranks[3] = "Manager";
            Ranks[4] = "Executive";
            Ranks[5] = "Senior Executive";
            Ranks[6] = "Director";
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

        public override void CheckTableAvailablity()
        {
            SkillTables[3].Available = false;
        }
    }
}
