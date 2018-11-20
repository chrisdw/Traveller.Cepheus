using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Zhodani
{
    public class Merchant : Career
    {
        public Merchant()
        {
            Name = "Merchant";
            RankNumber = 0;
            TermSkills = 2;

            enlistment = 8;
            enlistment1attr = "STR";
            enlistment1val = 7;
            enlistment2attr = "INT";
            enlistment2val = 6;
            enlistment3attr = "EDU";
            enlistment3val = 8;
            survival = 5;
            survival2attr = "INT";
            survival2val = 7;
            position = 6;
            position1attr = "INT";
            position1val = 9;
            position3attr = string.Empty;
            position3val = 0;
            promotion = 9;
            promotion1attr = "INT";
            promotion1val = 8;
            reenlist = 4;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Properties.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Dex;
            skills[2] = SkillLibrary.End;
            skills[3] = SkillLibrary.Edu;
            skills[4] = SkillLibrary.BladeCombat;
            skills[5] = SkillLibrary.Steward;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Properties.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.Vehicle;
            skills[1] = SkillLibrary.VaccSuit;
            skills[2] = SkillLibrary.JackOfTrades;
            skills[3] = SkillLibrary.Medic;
            skills[4] = SkillLibrary.Electronics;
            skills[5] = SkillLibrary.Mechanical;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Properties.Resources.Table_Education;
            skills = table.Skills;
            skills[0] = SkillLibrary.Navigation;
            skills[1] = SkillLibrary.Engineering;
            skills[2] = SkillLibrary.Pilot;
            skills[3] = SkillLibrary.Computer;
            skills[4] = SkillLibrary.Gunnery;
            skills[5] = SkillLibrary.GunCombat;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Properties.Resources.Table_NobleIntendant;
            skills = table.Skills;
            skills[0] = SkillLibrary.Clairvoyance;
            skills[1] = SkillLibrary.Psi;
            skills[2] = SkillLibrary.NonVerbalComms;
            skills[3] = SkillLibrary.Psychology;
            skills[4] = SkillLibrary.Liason;
            skills[5] = SkillLibrary.Trader;

            Material.Add(BenefitLibrary.Soc);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.Blade);
            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Trader);

            Cash[0] = 1000;
            Cash[1] = 2000;
            Cash[2] = 5000;
            Cash[3] = 10000;
            Cash[4] = 10000;
            Cash[5] = 20000;
            Cash[6] = 30000;

            Ranks[0] = "Deck Hand";
            Ranks[1] = "4th Officer";
            Ranks[2] = "3rd Officer";
            Ranks[3] = "2nd Officer";
            Ranks[4] = "1st Officer";
            Ranks[5] = "Captain";
            Ranks[6] = "Senior Captain";
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
            // nothing to do
        }

        protected override void RankSkill()
        {
            if (RankNumber == 3)
            {
                Owner.AddSkill(SkillLibrary.Engineering);
            }
            else if (RankNumber == 4)
            {
                Owner.AddSkill(SkillLibrary.Pilot);
            }
        }
    }
}
