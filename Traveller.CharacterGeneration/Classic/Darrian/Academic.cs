namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Darrian
{
    public class Academic : Imperial.Citizen.Career
    {
        public Academic()
        {
            Name = Resources.Career_Academic;
            RankNumber = 0;
            TermSkills = 2;

            enlistment = 8;
            enlistment1attr = string.Empty;
            enlistment1val = 0;
            enlistment2attr = "INT";
            enlistment2val = 9;
            enlistment3attr = "EDU";
            enlistment3val = 10;
            survival = 5;
            survival2attr = "INT";
            survival2val = 9;
            position = 8;
            position1attr = "EDU";
            position1val = 9;
            position2attr = "INT";
            position2val = 9;
            promotion = 8;
            promotion1attr = "EDU";
            promotion1val = 9;
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
            skills[5] = SkillLibrary.Soc;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Properties.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.Vehicle;
            skills[1] = SkillLibrary.Mechanical;
            skills[2] = SkillLibrary.Medic;
            skills[3] = SkillLibrary.Computer;
            skills[4] = SkillLibrary.Electronics;
            skills[5] = SkillLibrary.Liason;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Properties.Resources.Table_Education;
            skills = table.Skills;
            skills[0] = SkillLibrary.Admin;
            skills[1] = SkillLibrary.Edu;
            skills[2] = SkillLibrary.VaccSuit;
            skills[3] = SkillLibrary.Computer;
            skills[4] = SkillLibrary.Leader;
            skills[5] = SkillLibrary.JackOfTrades;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Properties.Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = SkillLibrary.Medic;
            skills[1] = SkillLibrary.Computer;
            skills[2] = SkillLibrary.Admin;
            skills[3] = SkillLibrary.Leader;
            skills[4] = SkillLibrary.Int;
            skills[5] = SkillLibrary.JackOfTrades;

            Material.Add(BenefitLibrary.Soc);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.Voucher);
            Material.Add(BenefitLibrary.Voucher);
            Material.Add(BenefitLibrary.Voucher);
            Material.Add(BenefitLibrary.LabShip);

            Cash[0] = 1000;
            Cash[1] = 2000;
            Cash[2] = 5000;
            Cash[3] = 5000;
            Cash[4] = 5000;
            Cash[5] = 10000;
            Cash[6] = 30000;

            Ranks[0] = Resources.Rank_Academic;
            Ranks[1] = Resources.Rank_Teacher;
            Ranks[2] = Resources.Rank_AsstInstructor;
            Ranks[3] = Resources.Rank_Instructor;
            Ranks[4] = Resources.Rank_AsstProfessor;
            Ranks[5] = Resources.Rank_Professor;
            Ranks[6] = Resources.Rank_Dean;
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
            // Nothing to do
        }

        public override void CheckTableAvailablity()
        {
            SkillTables[3].Available = (Owner.Profile["SOC"].Value >= 10);
        }
    }
}
