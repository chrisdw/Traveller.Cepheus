namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Darrian
{
    public class Noble : Imperial.Citizen.Career
    {
        public Noble()
        {
            Name = Resources.Career_Noble;
            RankNumber = 0;
            TermSkills = 2;

            hasRanks = true;
            maxRank = 5;

            enlistment = 14;
            enlistment1attr = "EDU";
            enlistment1val = 11;
            enlistment2attr = "INT";
            enlistment2val = 10;
            enlistment3attr = "SOC";
            enlistment3val = 11;
            survival = 5;
            survival2attr = "INT";
            survival2val = 8;
            position = 9;
            position1attr = "EDU";
            position1val = 8;
            position2attr = "INT";
            position2val = 9;
            promotion = 9;
            promotion1attr = "INT";
            promotion1val = 8;
            reenlist = 6;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Properties.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Dex;
            skills[2] = SkillLibrary.End;
            skills[3] = SkillLibrary.Int;
            skills[4] = SkillLibrary.Edu;
            skills[5] = SkillLibrary.Edu;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Properties.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.Vehicle;
            skills[1] = SkillLibrary.Admin;
            skills[2] = SkillLibrary.Admin;
            skills[3] = SkillLibrary.Computer;
            skills[4] = SkillLibrary.BladeCombat;
            skills[5] = SkillLibrary.GunCombat;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Properties.Resources.Table_Education;
            skills = table.Skills;
            skills[0] = SkillLibrary.Computer;
            skills[1] = SkillLibrary.Admin;
            skills[2] = SkillLibrary.Liason;
            skills[3] = SkillLibrary.GunCombat;
            skills[4] = SkillLibrary.Streetwise;
            skills[5] = SkillLibrary.Liason;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Properties.Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = SkillLibrary.Medic;
            skills[1] = SkillLibrary.Computer;
            skills[2] = SkillLibrary.Admin;
            skills[3] = SkillLibrary.Liason;
            skills[4] = SkillLibrary.Leader;
            skills[5] = SkillLibrary.JackOfTrades;

            Material.Add(BenefitLibrary.Soc);
            Material.Add(BenefitLibrary.Int2);
            Material.Add(BenefitLibrary.Edu2);
            Material.Add(BenefitLibrary.Soc2);
            Material.Add(BenefitLibrary.Voucher);
            Material.Add(BenefitLibrary.Voucher);
            Material.Add(BenefitLibrary.Yacht);

            Cash[0] = 5000;
            Cash[1] = 5000;
            Cash[2] = 10000;
            Cash[3] = 10000;
            Cash[4] = 20000;
            Cash[5] = 20000;
            Cash[6] = 40000;

            Ranks[0] = Resources.Rank_Noble;
            Ranks[1] = Resources.Rank_Knight;
            Ranks[2] = Resources.Rank_Baron;
            Ranks[3] = Resources.Rank_Marquis;
            Ranks[4] = Resources.Rank_Count;
            Ranks[5] = Resources.Rank_Duke;
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
            SkillTables[3].Available = (Owner.Profile["SOC"].Value >= 10);
        }
    }
}