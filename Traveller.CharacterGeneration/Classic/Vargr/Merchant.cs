using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Vargr
{
    public class Merchant : Career
    {
        public Merchant()
        {
            Name = "Merchant";

            enlistment = 5;
            enlistment1attr = "DEX";
            enlistment1val = 8;
            enlistment2attr = "INT";
            enlistment2val = 8;
            survival = 3;
            survival2attr = "INT";
            survival2val = 9;
            position = 7;
            position1attr = "INT";
            position1val = 8;
            promotion1attr = "INT";
            promotion1val = 8;
            promotion2attr = "CHR";
            promotion2val = 7;
            reenlist = 4;

            maxRank = 5;

            RankNumber = 0;
            TermSkills = 2;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Properties.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Dex;
            skills[2] = SkillLibrary.End;
            skills[3] = SkillLibrary.Str;
            skills[4] = SkillLibrary.Bribery;
            skills[5] = SkillLibrary.GunCombat;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Properties.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.Vehicle;
            skills[1] = SkillLibrary.VaccSuit;
            skills[2] = SkillLibrary.Steward;
            skills[3] = SkillLibrary.Medic;
            skills[4] = SkillLibrary.Gunnery;
            skills[5] = SkillLibrary.GunCombat;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Properties.Resources.Table_Education;
            skills = table.Skills;
            skills[0] = SkillLibrary.Electronics;
            skills[1] = SkillLibrary.Mechanical;
            skills[2] = SkillLibrary.Navigation;
            skills[3] = SkillLibrary.Engineering;
            skills[4] = SkillLibrary.Computer;
            skills[5] = SkillLibrary.Medic;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = "High Charisma";
            skills = table.Skills;
            skills[0] = SkillLibrary.Medic;
            skills[1] = SkillLibrary.Admin;
            skills[2] = SkillLibrary.Chr;
            skills[3] = SkillLibrary.Admin;
            skills[4] = SkillLibrary.Pilot;
            skills[5] = SkillLibrary.Navigation;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.Blade);
            Material.Add(BenefitLibrary.Gun);
            Material.Add(BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.Trader);

            Cash[0] = 10000;
            Cash[1] = 20000;
            Cash[2] = 30000;
            Cash[3] = 40000;
            Cash[4] = 40000;
            Cash[5] = 50000;
            Cash[6] = 50000;

            Ranks[0] = "Spacehand";
            Ranks[1] = "4th Officer";
            Ranks[2] = "3rd Officer";
            Ranks[3] = "2nd Officer";
            Ranks[4] = "1st Officer";
            Ranks[5] = "Captain";
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
            if (!doneOnce && RankNumber == 4)
            {
                Owner.AddSkill(SkillLibrary.Pilot);
                doneOnce = true;
            }
        }
    }
}
