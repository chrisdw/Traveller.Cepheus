namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Darrian
{
    public class Army : Imperial.Citizen.Career
    {
        public Army()
        {
            Name = "Army";
            RankNumber = 0;
            TermSkills = 2;

            enlistment = 6;
            enlistment1attr = "STR";
            enlistment1val = 8;
            enlistment2attr = "DEX";
            enlistment2val = 8;
            enlistment3attr = "END";
            enlistment3val = 8;
            survival = 5;
            survival2attr = "EDU";
            survival2val = 6;
            position = 8;
            position1attr = string.Empty;
            position1val = 0;
            position2attr = string.Empty;
            position2val = 0;
            promotion = 8;
            promotion1attr = "STR";
            promotion1val = 9;
            reenlist = 6;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Properties.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Dex;
            skills[2] = SkillLibrary.End;
            skills[3] = SkillLibrary.GunCombat;
            skills[4] = SkillLibrary.Edu;
            skills[5] = SkillLibrary.Brawling;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Properties.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.Vehicle;
            skills[1] = SkillLibrary.FowardObserver;
            skills[2] = SkillLibrary.Mechanical;
            skills[3] = SkillLibrary.Electronics;
            skills[4] = SkillLibrary.BladeCombat;
            skills[5] = SkillLibrary.GunCombat;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Properties.Resources.Table_Education;
            skills = table.Skills;
            skills[0] = SkillLibrary.Medic;
            skills[1] = SkillLibrary.Tactics;
            skills[2] = SkillLibrary.Tactics;
            skills[3] = SkillLibrary.Computer;
            skills[4] = SkillLibrary.Leader;
            skills[5] = SkillLibrary.Admin;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Properties.Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = SkillLibrary.Medic;
            skills[1] = SkillLibrary.Tactics;
            skills[2] = SkillLibrary.Tactics;
            skills[3] = SkillLibrary.Computer;
            skills[4] = SkillLibrary.Leader;
            skills[5] = SkillLibrary.Admin;

            Cash[0] = 1000;
            Cash[1] = 2000;
            Cash[2] = 5000;
            Cash[3] = 5000;
            Cash[4] = 5000;
            Cash[5] = 10000;
            Cash[6] = 20000;

            Ranks[0] = "Trooper";
            Ranks[1] = "Lieutenant";
            Ranks[2] = "Captain";
            Ranks[3] = "Major";
            Ranks[4] = "Lt. Colonel";
            Ranks[5] = "Colonel";
            Ranks[6] = "General";
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