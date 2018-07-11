using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Imperial
{
    public class BasicNavy : BasicCareer
    {
        public BasicNavy()
        {
            Name = "Navy";
            CurrentRank = 0;
            TermSkills = 2;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = "Personal Development";
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Dex;
            skills[2] = SkillLibrary.End;
            skills[3] = SkillLibrary.Soc;
            skills[4] = SkillLibrary.Int;
            skills[5] = SkillLibrary.Edu;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.Blade);
            Material.Add(BenefitLibrary.Travellers);
            Material.Add(BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.Soc);

            Cash[0] = 1000;
            Cash[1] = 5000;
            Cash[2] = 5000;
            Cash[3] = 10000;
            Cash[4] = 20000;
            Cash[5] = 50000;
            Cash[6] = 50000;

            Ranks[0] = "Spacer";
            Ranks[1] = "Ensign";
            Ranks[2] = "Lieutenant";
            Ranks[3] = "Lt Cmdr";
            Ranks[4] = "Commander";
            Ranks[5] = "Captain";
            Ranks[6] = "Admiral";
        }

        public override Renlistment CanRenlist()
        {
            var renlist = Renlistment.Cant;
            var target = 6;
            if (Owner.CharacterSpecies == Character.Species.AelYael)
            {
                target++;
            }
            var roll = dice.roll(2);
            if (roll == 12)
            {
                renlist = Renlistment.Must;
                Owner.Journal.Add(string.Format("Forced to remain in Navy at end of term {0}", Term));
            }
            else if (Term >= 6)
            {
                Retired = true;
                Owner.Journal.Add(string.Format("Re-enlistment refused due to age at end of term {0}", Term));
            }
            else if (roll == 2)
            {
                Retired = true;
                Owner.Journal.Add(string.Format("Dismissed from service at end of term {0}", Term));
            }
            else if (roll < target)
            {
                Retired = true;
                Owner.Journal.Add(string.Format("Re-enlistment refused at end of term {0}", Term));
            }
            else
            {
                renlist = Renlistment.Can;
            }
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
                    var target = 10;
                    if (Owner.Profile.Soc.Value >= 9)
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
            var target = 8;
            var enlist = false;
            if (Owner.CharacterSpecies == Character.Species.AelYael)
            {
                target++;
            }

            if (Owner.Profile.Int.Value >= 8)
            {
                target -= 1;
            }
            if (Owner.Profile.Edu.Value >= 9)
            {
                target -= 2;
            }
            if (dice.roll(2) >= target)
            {
                enlist = true;
                Owner.Journal.Add(string.Format("Enlisted in Navy at age {0}", Owner.Age));
            }
            else
            {
                Owner.Journal.Add(string.Format("Enlisted in Navy refused at age {0}", Owner.Age));
            }
            return enlist;
        }

        public override void HandleRenlist(bool renlisted)
        {
            // Army has no renlistment bonus
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

            if (CurrentRank > 0 && CurrentRank < 6)
            {
                var target = 8;
                if (Owner.Profile.Edu.Value >= 8)
                {
                    target--;
                }
                if (dice.roll(2) >= target)
                {
                    promote = true;
                    CurrentRank++;
                    TermSkills++;
                    Owner.Journal.Add(string.Format("Promoted to {0}", Ranks[CurrentRank]));
                    if (CurrentRank == 5 || CurrentRank == 6)
                    {
                        Owner.Journal.Add("Social standing increased by one due to rank.");
                        Owner.Profile.Soc.Value++;
                    }
                }
            }
            return promote;
        }

        public override bool Survival()
        {
            var survive = true;

            var target = 5;
            if (Owner.CharacterSpecies == Character.Species.Aslan)
            {
                if (Owner.Sex.Equals("Male"))
                {
                    target++;
                }
                else
                {
                    target--;
                }
            }

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
