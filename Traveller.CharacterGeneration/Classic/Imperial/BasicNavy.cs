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

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = "Service Skills";
            skills = table.Skills;
            skills[0] = SkillLibrary.ShipsBoat;
            skills[1] = SkillLibrary.VaccSuit;
            skills[2] = SkillLibrary.FowardObserver;
            skills[3] = SkillLibrary.BladeCombat;
            skills[4] = SkillLibrary.GunCombat;
            skills[5] = SkillLibrary.Gunnery;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = "Education";
            skills = table.Skills;
            skills[0] = SkillLibrary.VaccSuit;
            skills[1] = SkillLibrary.Mechanical;
            skills[2] = SkillLibrary.Electronics;
            skills[3] = SkillLibrary.Engineering;
            skills[4] = SkillLibrary.Gunnery;
            skills[5] = SkillLibrary.JackOfTrades;

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
            return BaseEnlist(target);
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
