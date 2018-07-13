using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Imperial
{
    public class BasicMerchants : BasicCareer
    {
        public BasicMerchants()
        {
            Name = "Merchants";
            CurrentRank = 0;
            TermSkills = 2;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = "Personal Development";
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Dex;
            skills[2] = SkillLibrary.End;
            skills[3] = SkillLibrary.Str;
            skills[4] = SkillLibrary.BladeCombat;
            skills[5] = SkillLibrary.Bribery;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = "Service Skills";
            skills = table.Skills;
            skills[0] = SkillLibrary.Steward;
            skills[1] = SkillLibrary.VaccSuit;
            skills[2] = SkillLibrary.Str;
            skills[3] = SkillLibrary.GunCombat;
            skills[4] = SkillLibrary.Electronics;
            skills[5] = SkillLibrary.JackOfTrades;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = "Education";
            skills = table.Skills;
            skills[0] = SkillLibrary.Streetwise;
            skills[1] = SkillLibrary.Mechanical;
            skills[2] = SkillLibrary.Electronics;
            skills[3] = SkillLibrary.Navigation;
            skills[4] = SkillLibrary.Gunnery;
            skills[5] = SkillLibrary.Medic;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = "Advanced Education";
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

            Ranks[0] = "Space Hand";
            Ranks[1] = "4th Officer";
            Ranks[2] = "3rd Officer";
            Ranks[3] = "2nd Officer";
            Ranks[4] = "1st Officer";
            Ranks[5] = "Captain";
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
            if (CurrentRank == 0)
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
                        CurrentRank = 1;
                        TermSkills += 1;
                        Owner.Journal.Add(string.Format("Commissioned as {0}", Ranks[CurrentRank]));
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
                if (Owner.Sex.Equals("Male"))
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
            // Merchants has no renlistment bonus
            if (renlisted)
            {
                Owner.Journal.Add(string.Format("Remain in service at end of term {0}", Term));
            }
            else
            {
                Retired = true;
                Owner.Journal.Add(string.Format("Left service at end of term {0}", Term));
            }
        }

        public override bool Promotion()
        {
            var promote = false;

            if (CurrentRank > 0 && CurrentRank < 5)
            {
                var target = 10;
                if (Owner.Profile.Int.Value >= 9)
                {
                    target--;
                }
                if (dice.roll(2) >= target)
                {
                    promote = true;
                    CurrentRank++;
                    TermSkills++;
                    Owner.Journal.Add(string.Format("Promoted to {0}", Ranks[CurrentRank]));
                    if (CurrentRank == 4)
                    {
                        Owner.AddSkill(SkillLibrary.Pilot);
                    }
                }
            }
            return promote;
        }

        public override bool Survival()
        {
            var survive = true;

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
