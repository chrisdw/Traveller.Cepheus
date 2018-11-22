namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Vargr
{
    public class Navy : Career
    {
        public Navy()
        {
            Name = Resources.Career_Navy;

            enlistment = 7;
            enlistment1attr = "INT";
            enlistment1val = 8;
            enlistment2attr = "CHR";
            enlistment2val = 6;
            survival = 5;
            survival2attr = "EDU";
            survival2val = 7;
            position = 9;
            position1attr = "INT";
            position1val = 7;
            promotion1attr = "EDU";
            promotion1val = 7;
            promotion2attr = "CHR";
            promotion2val = 6;
            reenlist = 5;

            maxRank = 6;

            RankNumber = 0;
            TermSkills = 2;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Properties.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Dex;
            skills[2] = SkillLibrary.End;
            skills[3] = SkillLibrary.Int;
            skills[4] = SkillLibrary.Gambling;
            skills[5] = SkillLibrary.Chr;
            
            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Properties.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.ShipsBoat;
            skills[1] = SkillLibrary.VaccSuit;
            skills[2] = SkillLibrary.Electronics;
            skills[3] = SkillLibrary.Mechanical;
            skills[4] = SkillLibrary.Gunnery;
            skills[5] = SkillLibrary.Gunnery;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Properties.Resources.Table_Education;
            skills = table.Skills;
            skills[0] = SkillLibrary.Pilot;
            skills[1] = SkillLibrary.GunCombat;
            skills[2] = SkillLibrary.Navigation;
            skills[3] = SkillLibrary.Engineering;
            skills[4] = SkillLibrary.Computer;
            skills[5] = SkillLibrary.Medic;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Resources.Table_HighCharisma;
            skills = table.Skills;
            skills[0] = SkillLibrary.Medic;
            skills[1] = SkillLibrary.Tactics;
            skills[2] = SkillLibrary.Leader;
            skills[3] = SkillLibrary.Pilot;
            skills[4] = SkillLibrary.Pilot;
            skills[5] = SkillLibrary.JackOfTrades;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.Gun);
            Material.Add(BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.Chr);

            Cash[0] = 1000;
            Cash[1] = 5000;
            Cash[2] = 10000;
            Cash[3] = 10000;
            Cash[4] = 20000;
            Cash[5] = 30000;
            Cash[6] = 40000;

            Ranks[0] = Resources.Rank_Spacehand;
            Ranks[1] = Resources.Rank_Ensign;
            Ranks[2] = Resources.Rank_Lieutenant;
            Ranks[3] = Resources.Rank_LtCommander;
            Ranks[4] = Resources.Rank_Commander;
            Ranks[5] = Resources.Rank_Captain;
            Ranks[6] = Resources.Rank_Admiral;
        }

        protected override void CommsionSkill()
        {
            // nothing to do
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
            if (!doneOnce && RankNumber == 5)
            {
                Owner.Profile["CHR"].Value++;
                doneOnce = true;
            }
        }
    }
}
