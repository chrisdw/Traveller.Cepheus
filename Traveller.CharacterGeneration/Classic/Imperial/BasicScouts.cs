namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Imperial
{
    public class BasicScouts : BasicCareer
    {
        public BasicScouts()
        {
            RankNumber = 0;
            TermSkills = 2;
            Name = Properties.Resources.Career_Scouts;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Properties.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Dex;
            skills[2] = SkillLibrary.End;
            skills[3] = SkillLibrary.GunCombat;
            skills[4] = SkillLibrary.Int;
            skills[5] = SkillLibrary.Edu;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Properties.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.AirRaft;
            skills[1] = SkillLibrary.VaccSuit;
            skills[2] = SkillLibrary.Navigation;
            skills[3] = SkillLibrary.Mechanical;
            skills[4] = SkillLibrary.Electronics;
            skills[5] = SkillLibrary.JackOfTrades;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Properties.Resources.Table_Education;
            skills = table.Skills;
            skills[0] = SkillLibrary.AirRaft;
            skills[1] = SkillLibrary.Mechanical;
            skills[2] = SkillLibrary.Electronics;
            skills[3] = SkillLibrary.JackOfTrades;
            skills[4] = SkillLibrary.Gunnery;
            skills[5] = SkillLibrary.Medic;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Properties.Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = SkillLibrary.Medic;
            skills[1] = SkillLibrary.Navigation;
            skills[2] = SkillLibrary.Engineering;
            skills[3] = SkillLibrary.Computer;
            skills[4] = SkillLibrary.Pilot;
            skills[5] = SkillLibrary.JackOfTrades;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.Blade);
            Material.Add(BenefitLibrary.Gun);
            Material.Add(BenefitLibrary.Scout);

            Cash[0] = 20000;
            Cash[1] = 20000;
            Cash[2] = 30000;
            Cash[3] = 30000;
            Cash[4] = 50000;
            Cash[5] = 50000;
            Cash[6] = 50000;

            Ranks[0] = Resources.Rank_Scout;
        }

        public override Renlistment CanRenlist()
        {
            var renlist = Renlistment.Cant;
            var target = 3;
            if (Owner.CharacterSpecies == Character.Species.AelYael)
            {
                target += 1;
            }

            renlist = BaseCanRenlist(renlist, target);
            return renlist;
        }

        public override bool Commission()
        {
            return false;
        }

        public override bool Enlist()
        {
            var target = 5;

            if (Owner.CharacterSpecies == Character.Species.Aslan)
            {
                if (Owner.Sex.Equals(Properties.Resources.Sex_Male))
                {
                    target += 4;
                }
            }
            else if (Owner.CharacterSpecies == Character.Species.Virushi || Owner.CharacterSpecies == Character.Species.Bwap)
            {
                target--;
            }
            else if (Owner.CharacterSpecies == Character.Species.AelYael)
            {
                target -= 2;
            }

            if (Owner.Profile.Dex.Value >= 6)
            {
                target -= 1;
            }
            if (Owner.Profile.End.Value >= 5)
            {
                target -= 2;
            }

            var enlist = BaseEnlist(target);

            if (enlist)
            {
                Owner.AddSkill(SkillLibrary.Pilot);
            }
            return enlist;
        }

        public override void HandleRenlist(bool renlisted)
        {
            // Scouts has no renlistment bonus
            BaseRenlist(renlisted);
        }

        public override bool Promotion()
        {
           return false;
        }

        public override SurvivalResult Survival()
        {
            var survive = SurvivalResult.Died;

            var target = 7;

            if (Owner.Profile.End.Value >= 9)
            {
                target -= 2;
            }
            if (dice.roll(2) >= target)
            {
                survive = SurvivalResult.Survived;
            }
            return survive;
        }

        public override void EndTerm()
        {
            base.EndTerm();
            TermSkills = 2;
        }

        public override bool Drafted
        {
            get => base.Drafted;
            set
            {
                base.Drafted = value;
                if (value)
                {
                    Owner.AddSkill(SkillLibrary.Pilot);
                }
            }
        }
    }
}
