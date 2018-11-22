namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Zhodani
{
    public class Government : Career
    {
        public Government()
        {
            Name = Resources.Career_Government;
            RankNumber = 0;
            TermSkills = 2;

            enlistment = 7;
            enlistment1attr = "INT";
            enlistment1val = 7;
            enlistment2attr = "EDU";
            enlistment2val = 8;
            enlistment3attr = "SOC";
            enlistment3val = 10;
            survival = 5;
            survival2attr = "EDU";
            survival2val = 8;
            position = 9;
            position1attr = "EDU";
            position1val = 10;
            position3attr = "SOC";
            position3val = 10;
            promotion = 9;
            promotion1attr = "SOC";
            promotion1val = 11;
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
            skills[3] = SkillLibrary.Psychology;
            skills[4] = SkillLibrary.NonVerbalComms;
            skills[5] = SkillLibrary.Liason;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Properties.Resources.Table_NobleIntendant;
            skills = table.Skills;
            skills[0] = SkillLibrary.Clairvoyance;
            skills[1] = SkillLibrary.Psi;
            skills[2] = SkillLibrary.NonVerbalComms;
            skills[3] = SkillLibrary.Psychology;
            skills[4] = SkillLibrary.Liason;
            skills[5] = SkillLibrary.Awareness;

            Cash[0] = 5000;
            Cash[1] = 5000;
            Cash[2] = 10000;
            Cash[3] = 10000;
            Cash[4] = 20000;
            Cash[5] = 20000;
            Cash[6] = 40000;

            Ranks[0] = Resources.Rank_Clerk;
            Ranks[1] = Resources.Rank_AssistantSupervisor;
            Ranks[2] = Resources.Rank_Supervisor;
            Ranks[3] = Resources.Rank_Executive;
            Ranks[4] = Resources.Rank_Consul;
            Ranks[5] = Resources.Rank_WorldConsul;
            Ranks[6] = Resources.Rank_HighConsul;
        }

        protected override void CommsionSkill()
        {
            // Nothing to do here
        }

        protected override int EnlistFactor()
        {
            return 0;
        }

        protected override void EnlistSkill()
        {
            Owner.AddSkill(SkillLibrary.Admin);
        }

        protected override void RankSkill()
        {
            if (RankNumber == 4)
            {
                Owner.Profile.Soc.Value += 1;
            }
        }
    }
}
