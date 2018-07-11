using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Imperial
{
    public class BasicArmy : BasicCareer
    {
        public BasicArmy()
        {
            Name = "Army";
            CurrentRank = 0;
            TermSkills = 2;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = "Personal Development";
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Dex;
            skills[2] = SkillLibrary.End;
            skills[3] = SkillLibrary.Gambling;
            skills[4] = SkillLibrary.Brawling;
            skills[5] = SkillLibrary.Edu;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = "Service Skills";
            skills = table.Skills;
            skills[0] = SkillLibrary.ATV;
            skills[1] = SkillLibrary.AirRaft;
            skills[2] = SkillLibrary.FowardObserver;
            skills[3] = SkillLibrary.BladeCombat;
            skills[4] = SkillLibrary.GunCombat;
            skills[5] = SkillLibrary.GunCombat;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = "Education";
            skills = table.Skills;
            skills[0] = SkillLibrary.ATV;
            skills[1] = SkillLibrary.Mechanical;
            skills[2] = SkillLibrary.Electronics;
            skills[3] = SkillLibrary.Tactics;
            skills[4] = SkillLibrary.BladeCombat;
            skills[5] = SkillLibrary.GunCombat;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = "Advanced Education";
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

            Ranks[0] = "Trooper";
            Ranks[1] = "Lieutenant";
            Ranks[2] = "Captain";
            Ranks[3] = "Major";
            Ranks[4] = "Lt. Colonel";
            Ranks[5] = "Colonel";
            Ranks[6] = "General";
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
                    var target = 5;
                    if (Owner.Profile.End.Value >= 7)
                    {
                        target -= 1;
                    }
                    if (dice.roll(2) >= target)
                    {
                        commision = true;
                        CurrentRank = 1;
                        TermSkills += 1;
                        Owner.Journal.Add(string.Format("Commissioned as {0}", Ranks[CurrentRank]));
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
            var enlist = false;
            if (Owner.CharacterSpecies == Character.Species.Aslan)
            {
                if (Owner.Sex.Equals("Male"))
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
            if (dice.roll(2) >= target)
            {
                enlist = true;
                Owner.AddSkill(SkillLibrary.Rifle);
                Owner.Journal.Add(string.Format("Enlisted in Army at age {0}", Owner.Age));
            }
            else
            {
                Owner.Journal.Add(string.Format("Enlisted in Army refused at age {0}", Owner.Age));
            }
            return enlist;
        }

        public override bool Promotion()
        {
            var promote = false;

            if (CurrentRank > 0 && CurrentRank < 6)
            {
                var target = 6;
                if (Owner.Profile.Edu.Value >= 7)
                {
                    target--;
                }
                if (dice.roll(2) >= target)
                {
                    promote = true;
                    CurrentRank++;
                    TermSkills++;
                    Owner.Journal.Add(string.Format("Promoted to {0}", Ranks[CurrentRank]));
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
            var roll = dice.roll(2);
            if (roll == 12)
            {
                renlist = Renlistment.Must;
                Owner.Journal.Add(string.Format("Forced to remain in Army at end of term {0}", Term));
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

        public override bool Survival()
        {
            var survive = true;

            var target = 5;
            if (Owner.CharacterSpecies == Character.Species.Aslan && Owner.Sex.Equals("Male"))
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
                survive = true;
            }
            return survive;
        }
    }
}
