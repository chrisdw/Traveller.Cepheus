namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Imperial
{
    public class BasicArmy : BasicCareer
    {
        public BasicArmy()
        {
            Name = Properties.Resources.Career_Army;
            RankNumber = 0;
            TermSkills = 2;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Properties.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Dex;
            skills[2] = SkillLibrary.End;
            skills[3] = SkillLibrary.Gambling;
            skills[4] = SkillLibrary.Brawling;
            skills[5] = SkillLibrary.Edu;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Properties.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.ATV;
            skills[1] = SkillLibrary.AirRaft;
            skills[2] = SkillLibrary.FowardObserver;
            skills[3] = SkillLibrary.BladeCombat;
            skills[4] = SkillLibrary.GunCombat;
            skills[5] = SkillLibrary.GunCombat;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Properties.Resources.Table_Education;
            skills = table.Skills;
            skills[0] = SkillLibrary.ATV;
            skills[1] = SkillLibrary.Mechanical;
            skills[2] = SkillLibrary.Electronics;
            skills[3] = SkillLibrary.Tactics;
            skills[4] = SkillLibrary.BladeCombat;
            skills[5] = SkillLibrary.GunCombat;

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

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.Gun);
            Material.Add(BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.MidPsg);
            Material.Add(BenefitLibrary.Soc);

            Cash[0] = 2000;
            Cash[1] = 5000;
            Cash[2] = 10000;
            Cash[3] = 10000;
            Cash[4] = 10000;
            Cash[5] = 20000;
            Cash[6] = 30000;

            Ranks[0] = Resources.Rank_Trooper;
            Ranks[1] = Resources.Rank_Lieutenant;
            Ranks[2] = Resources.Rank_Captain;
            Ranks[3] = Resources.Rank_Major;
            Ranks[4] = Resources.Rank_LtColonel;
            Ranks[5] = Resources.Rank_Colonel;
            Ranks[6] = Resources.Rank_General;
        }
        public override bool Commission()
        {
            var commision = false;
            if (RankNumber == 0)
            {
                if (Term == 0 && Drafted)
                {
                    commision = false;
                }
                else
                {
                    var target = 5;
                    if (Owner.Profile.End.Value >= 7)
                    {
                        target -= 1;
                    }
                    if (dice.roll(2) >= target)
                    {
                        commision = true;
                        RankNumber = 1;
                        TermSkills += 1;
                        Owner.Journal.Add(string.Format(Resources.Prompt_Commisioned, Ranks[RankNumber]));
                        Owner.AddSkill(SkillLibrary.SubmachineGun);
                    }
                }
            }
            else
            {
                commision = true;
            }
            return commision;
        }

        public override bool Drafted
        {
            get => base.Drafted;
            set
            {
                base.Drafted = value;
                if (value)
                {
                    Owner.AddSkill(SkillLibrary.Rifle);
                }
            }
        }

        public override bool Enlist()
        {
            var target = 5;

            if (Owner.CharacterSpecies == Character.Species.Aslan)
            {
                if (Owner.Sex.Equals(Properties.Resources.Sex_Male))
                {
                    target -= 1;
                }
            }
            else if (Owner.CharacterSpecies == Character.Species.AelYael || Owner.CharacterSpecies == Character.Species.Bwap)
            {
                target += 1;
            }

            if (Owner.Profile.Dex.Value >= 6)
            {
                target -= 1;
            }
            if (Owner.Profile.End.Value >= 6)
            {
                target -= 2;
            }
            var enlist = BaseEnlist(target);
            if (enlist)
            {
                Owner.AddSkill(SkillLibrary.Rifle);
            }
            return enlist;
        }

        public override bool Promotion()
        {
            var promote = false;

            if (RankNumber > 0 && RankNumber < 6)
            {
                var target = 6;
                if (Owner.Profile.Edu.Value >= 7)
                {
                    target--;
                }
                if (dice.roll(2) >= target)
                {
                    promote = true;
                    RankNumber++;
                    TermSkills++;
                    Owner.Journal.Add(string.Format(Resources.Prompt_Promoted, Ranks[RankNumber]));
                }
            }
            return promote;
        }

        public override Renlistment CanRenlist()
        {
            var renlist = Renlistment.Cant;
            var target = 7;
            if (Owner.CharacterSpecies == Character.Species.AelYael)
            {
                target++;
            }

            renlist = BaseCanRenlist(renlist, target);
            return renlist;
        }

        public override void HandleRenlist(bool renlisted)
        {
            // Army has no renlistment bonus
            BaseRenlist(renlisted);
        }

        public override SurvivalResult Survival()
        {
            var survive = SurvivalResult.Died;

            var target = 5;
            if (Owner.CharacterSpecies == Character.Species.Aslan && Owner.Sex.Equals(Properties.Resources.Sex_Male))
            {
                target++;
            }
            if (Owner.Culture == Constants.CultureType.Dynchia)
            {
                target++;
            }
            if (Owner.Profile.Edu.Value >= 6)
            {
                target -= 2;
            }
            if (dice.roll(2) >= target)
            {
                survive = SurvivalResult.Survived;
            }
            return survive;
        }
    }
}
