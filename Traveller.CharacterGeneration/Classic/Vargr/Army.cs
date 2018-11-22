namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Vargr
{
    public class Army : Career
    {
        
 
        public Army()
        {
            Name = Resources.Career_Army;

            enlistment = 5;
            enlistment1attr = "STR";
            enlistment1val = 10;
            enlistment2attr = "END";
            enlistment2val = 6;
            survival = 5;
            survival2attr = "STR";
            survival2val = 5;
            position = 7;
            position1attr = "END";
            position1val = 6;
            promotion1attr = "EDU";
            promotion1val = 9;
            promotion2attr = "CHR";
            promotion2val = 6;
            reenlist = 6;

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
            skills[4] = SkillLibrary.Infighting;
            skills[5] = SkillLibrary.Chr;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Properties.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.Vehicle;
            skills[1] = SkillLibrary.VaccSuit;
            skills[2] = SkillLibrary.Infighting;
            skills[3] = SkillLibrary.BladeCombat;
            skills[4] = SkillLibrary.GunCombat;
            skills[5] = SkillLibrary.GunCombat;
            
            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Properties.Resources.Table_Education;
            skills = table.Skills;
            skills[0] = SkillLibrary.Vehicle;
            skills[1] = SkillLibrary.Tactics;
            skills[2] = SkillLibrary.Electronics;
            skills[3] = SkillLibrary.Mechanical;
            skills[4] = SkillLibrary.Computer;
            skills[5] = SkillLibrary.Medic;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Resources.Table_HighCharisma;
            skills = table.Skills;
            skills[0] = SkillLibrary.Medic;
            skills[1] = SkillLibrary.Tactics;
            skills[2] = SkillLibrary.Leader;
            skills[3] = SkillLibrary.FowardObserver;
            skills[4] = SkillLibrary.Chr;
            skills[5] = SkillLibrary.Tactics;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.Gun);
            Material.Add(BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.Chr);

            Cash[0] = 5000;
            Cash[1] = 5000;
            Cash[2] = 10000;
            Cash[3] = 10000;
            Cash[4] = 20000;
            Cash[5] = 20000;
            Cash[6] = 20000;

            Ranks[0] = Resources.Rank_Trooper;
            Ranks[1] = Resources.Rank_Lieutenant;
            Ranks[2] = Resources.Rank_Captain;
            Ranks[3] = Resources.Rank_Major;
            Ranks[4] = Resources.Rank_LtColonel;
            Ranks[5] = Resources.Rank_Colonel;
            Ranks[6] = Resources.Rank_General;
        }
        protected override void CommsionSkill()
        {
            if (!doneOnce && RankNumber == 1)
            {
                Owner.AddSkill(SkillLibrary.SubmachineGun);
                doneOnce = true;
            }
        }

        protected override int EnlistFactor()
        {
            return 0;
        }

        protected override void EnlistSkill()
        {
            Owner.AddSkill(SkillLibrary.Rifle);
        }

        protected override void RankSkill()
        {
            // Nothing to do
        }

        public override bool Drafted
        {
            get => base.Drafted;
            set {
                base.Drafted = value;
                if (value)
                {
                    Owner.AddSkill(SkillLibrary.Rifle);
                }
            }
        }
    }
}
