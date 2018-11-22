using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Imperial
{
    public class BasicMarines : BasicCareer
    {
        public BasicMarines()
        {
            Name = Properties.Resources.Career_Marines;
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
            skills[5] = SkillLibrary.BladeCombat;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Properties.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.ATV;
            skills[1] = SkillLibrary.VaccSuit;
            skills[2] = SkillLibrary.BladeCombat;
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
            Material.Add(BenefitLibrary.Blade);
            Material.Add(BenefitLibrary.Travellers);
            Material.Add(BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.Soc);

            Cash[0] = 2000;
            Cash[1] = 5000;
            Cash[2] = 5000;
            Cash[3] = 10000;
            Cash[4] = 20000;
            Cash[5] = 30000;
            Cash[6] = 40000;

            Ranks[0] = Resources.Rank_Marine;
            Ranks[1] = Resources.Rank_Lieutenant;
            Ranks[2] = Resources.Rank_Captain;
            Ranks[3] = Resources.Rank_ForceCmdr;
            Ranks[4] = Resources.Rank_LtColonel;
            Ranks[5] = Resources.Rank_Colonel;
            Ranks[6] = Resources.Rank_Brigadier;
        }

        public override Renlistment CanRenlist()
        {
            var renlist = Renlistment.Cant;
            var target = 6;
            if (Owner.CharacterSpecies == Character.Species.AelYael)
            {
                target++;
            }

            renlist = BaseCanRenlist(renlist, target);
            return renlist;
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
                    if (Owner.Profile.Edu.Value >= 7)
                    {
                        target -= 1;
                    }
                    if (dice.roll(2) >= target)
                    {
                        commision = true;
                        RankNumber = 1;
                        TermSkills += 1;
                        Owner.Journal.Add(string.Format(Resources.Prompt_Commisioned, Ranks[RankNumber]));
                        Owner.AddSkill(SkillLibrary.Revolver);
                    }
                }
            }
            else
            {
                commision = true;
            }
            return commision;
        }

        public override bool Enlist()
        {
            var target = 8;
            if (Owner.CharacterSpecies == Character.Species.Aslan)
            {
                if (Owner.Sex.Equals(Properties.Resources.Sex_Male))
                {
                    target -= 1;
                }
            }
            else if (Owner.CharacterSpecies == Character.Species.AelYael)
            {
                target += 1;
            }
            else if (Owner.CharacterSpecies == Character.Species.Bwap)
            {
                target += 3;
            }
            if (Owner.Profile.Int.Value >= 8)
            {
                target -= 1;
            }
            if (Owner.Profile.Str.Value >= 8)
            {
                target -= 2;
            }

            var enlist = BaseEnlist(target);

            if (enlist)
            {
                Owner.AddSkill(SkillLibrary.Cutlass);
            }
            return enlist;
        }

        public override void HandleRenlist(bool renlisted)
        {
            // Marines have no renlistment bonus
            BaseRenlist(renlisted);
        }

        public override bool Promotion()
        {
            var promote = false;

            if (RankNumber > 0 && RankNumber < 6)
            {
                var target = 9;
                if (Owner.Profile.Soc.Value >= 8)
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

        public override bool Survival()
        {
            var survive = false;

            var target = 6;
            if (Owner.CharacterSpecies == Character.Species.Aslan)
            {
                if (Owner.Sex.Equals(Properties.Resources.Sex_Male))
                {
                    target++;
                }
                else
                {
                    target--;
                }
            }
            if (Owner.Profile.End.Value >= 8)
            {
                target -= 2;
            }
            if (dice.roll(2) >= target)
            {
                survive = true;
            }
            return survive;
        }

        public override bool Drafted
        {
            get => base.Drafted;
            set
            {
                base.Drafted = value;
                if (value)
                {
                    Owner.AddSkill(SkillLibrary.Cutlass);
                }
            }
        }
    }
}
