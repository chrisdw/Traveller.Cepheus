using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Imperial
{
    public class BasicOther : BasicCareer
    {
        public BasicOther()
        {
            CurrentRank = 0;
            TermSkills = 2;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = "Personal Development";
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Dex;
            skills[2] = SkillLibrary.End;
            skills[3] = SkillLibrary.BladeCombat;
            skills[4] = SkillLibrary.Brawling;
            skills[5] = SkillLibrary.Soc;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = "Service Skills";
            skills = table.Skills;
            skills[0] = SkillLibrary.Forgery;
            skills[1] = SkillLibrary.Gambling;
            skills[2] = SkillLibrary.Brawling;
            skills[3] = SkillLibrary.BladeCombat;
            skills[4] = SkillLibrary.GunCombat;
            skills[5] = SkillLibrary.Bribery;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = "Education";
            skills = table.Skills;
            skills[0] = SkillLibrary.Streetwise;
            skills[1] = SkillLibrary.Mechanical;
            skills[2] = SkillLibrary.Electronics;
            skills[3] = SkillLibrary.Gambling;
            skills[4] = SkillLibrary.Brawling;
            skills[5] = SkillLibrary.Forgery;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = "Advanced Education";
            skills = table.Skills;
            skills[0] = SkillLibrary.Medic;
            skills[1] = SkillLibrary.Forgery;
            skills[2] = SkillLibrary.Electronics;
            skills[3] = SkillLibrary.Computer;
            skills[4] = SkillLibrary.Streetwise;
            skills[5] = SkillLibrary.JackOfTrades;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.Gun);
            Material.Add(BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.Nothing);

            Cash[0] = 1000;
            Cash[1] = 5000;
            Cash[2] = 10000;
            Cash[3] = 10000;
            Cash[4] = 10000;
            Cash[5] = 50000;
            Cash[6] = 100000;

            Ranks[0] = string.Empty;
        }
        public override Renlistment CanRenlist()
        {
            var renlist = Renlistment.Cant;
            var target = 5;

            renlist = BaseCanRenlist(renlist, target);
            return renlist; throw new NotImplementedException();
        }

        public override bool Commission()
        {
            return false;
        }

        public override bool Enlist()
        {
            var target = 3;
            var enlist = false;
            if (Owner.CharacterSpecies == Character.Species.Aslan)
            {
                if (Owner.Sex.Equals("Male"))
                {
                    target +=2;
                }
                else
                {
                    target++;
                }
            }

            if (dice.roll(2) >= target)
            {
                enlist = true;
                Owner.Journal.Add(string.Format("Enlisted in Others at age {0}", Owner.Age));
            }
            else
            {
                Owner.Journal.Add(string.Format("Enlisted in Others refused at age {0}", Owner.Age));
            }
            return enlist;
        }

        public override void HandleRenlist(bool renlisted)
        {
            // Other has no renlistment bonus
            if (renlisted)
            {
                Owner.Journal.Add(string.Format("Remain in service at end of term {0}", Term));
            }
            else
            {
                Retired = false;
                Owner.Journal.Add(string.Format("Left service at end of term {0}", Term));
            }
        }

        public override bool Promotion()
        {
            return false;
        }

        public override bool Survival()
        {
            var survive = true;

            var target = 5;

            if (Owner.Profile.Int.Value >= 9)
            {
                target -= 2;
            }
            if (dice.roll(2) >= target)
            {
                survive = true;
            }
            return survive;
        }

        public override void EndTerm()
        {
            base.EndTerm();
            TermSkills = 2;
        }
    }
}
