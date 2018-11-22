using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Imperial
{
    public class BasicMerchants : BasicCareer
    {
        public BasicMerchants()
        {
            Name = Properties.Resources.Career_Merchants;
            RankNumber = 0;
            TermSkills = 2;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Properties.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Dex;
            skills[2] = SkillLibrary.End;
            skills[3] = SkillLibrary.Str;
            skills[4] = SkillLibrary.BladeCombat;
            skills[5] = SkillLibrary.Bribery;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Properties.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.Steward;
            skills[1] = SkillLibrary.VaccSuit;
            skills[2] = SkillLibrary.Str;
            skills[3] = SkillLibrary.GunCombat;
            skills[4] = SkillLibrary.Electronics;
            skills[5] = SkillLibrary.JackOfTrades;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Properties.Resources.Table_Education;
            skills = table.Skills;
            skills[0] = SkillLibrary.Streetwise;
            skills[1] = SkillLibrary.Mechanical;
            skills[2] = SkillLibrary.Electronics;
            skills[3] = SkillLibrary.Navigation;
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
            skills[5] = SkillLibrary.Admin;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.Gun);
            Material.Add(BenefitLibrary.Blade);
            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Merchant);

            Cash[0] = 1000;
            Cash[1] = 5000;
            Cash[2] = 10000;
            Cash[3] = 20000;
            Cash[4] = 20000;
            Cash[5] = 40000;
            Cash[6] = 40000;

            Ranks[0] = Resources.Rank_SpaceHand;
            Ranks[1] = Resources.Rank_4thOfficer;
            Ranks[2] = Resources.Rank_3rdOfficer;
            Ranks[3] = Resources.Rank_2ndOfficer;
            Ranks[4] = Resources.Rank_1stOfficer;
            Ranks[5] = Resources.Rank_Captain;
        }

        public override Renlistment CanRenlist()
        {
            var renlist = Renlistment.Cant;
            var target = 4;

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
                    var target = 4;
                    if (Owner.Profile.Int.Value >= 6)
                    {
                        target -= 1;
                    }
                    if (dice.roll(2) >= target)
                    {
                        commision = true;
                        RankNumber = 1;
                        TermSkills += 1;
                        Owner.Journal.Add(string.Format(Resources.Prompt_Commisioned, Ranks[RankNumber]));
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
            var target = 7;
            if (Owner.CharacterSpecies == Character.Species.Aslan)
            {
                if (Owner.Sex.Equals(Properties.Resources.Sex_Male))
                {
                    target += 4;
                }
                else
                {
                    target--;
                }
            }
            else if (Owner.CharacterSpecies == Character.Species.Vargr)
            {
                target += 2;
            }
            else if (Owner.CharacterSpecies == Character.Species.Bwap)
            {
                target -= 2;
            }

            if (Owner.Profile.Str.Value >= 7)
            {
                target -= 1;
            }
            if (Owner.Profile.Int.Value >= 6)
            {
                target -= 2;
            }
 
            return BaseEnlist(target);
        }

        public override void HandleRenlist(bool renlisted)
        {
            BaseRenlist(renlisted);
        }

        public override bool Promotion()
        {
            var promote = false;

            if (RankNumber > 0 && RankNumber < 5)
            {
                var target = 10;
                if (Owner.Profile.Int.Value >= 9)
                {
                    target--;
                }
                if (dice.roll(2) >= target)
                {
                    promote = true;
                    RankNumber++;
                    TermSkills++;
                    Owner.Journal.Add(string.Format(Resources.Prompt_Promoted, Ranks[RankNumber]));
                    if (RankNumber == 4)
                    {
                        Owner.AddSkill(SkillLibrary.Pilot);
                    }
                }
            }
            return promote;
        }

        public override bool Survival()
        {
            var survive = false;

            var target = 5;

            if (Owner.Profile.Int.Value >= 7)
            {
                target -= 2;
            }
            if (dice.roll(2) >= target)
            {
                survive = true;
            }
            return survive;
        }
    }
}
