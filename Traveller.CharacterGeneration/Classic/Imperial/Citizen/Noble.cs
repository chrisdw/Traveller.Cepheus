using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Imperial.Citizen
{
    public class Noble : Career
    {
        public Noble()
        {
            Name = "Noble";
            RankNumber = 0;
            TermSkills = 2;

            enlistment = 7;
            enlistment1attr = "SOC";
            enlistment1val = 7;
            enlistment2attr = "END";
            enlistment2val = 9;
            survival = 3;
            survival2attr = "DEX";
            survival2val = 8;
            position = 3;
            position1attr = "EDU";
            position1val = 9;
            promotion = 12;
            promotion1attr = "INT";
            promotion1val = 10;
            reenlist = 4;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Properties.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Dex;
            skills[2] = SkillLibrary.End;
            skills[3] = SkillLibrary.Int;
            skills[4] = SkillLibrary.Carousing;
            skills[5] = SkillLibrary.Brawling;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Properties.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.GunCombat;
            skills[1] = SkillLibrary.BladeCombat;
            skills[2] = SkillLibrary.Hunting;
            skills[3] = SkillLibrary.GroundVehicle;
            skills[4] = SkillLibrary.Bribery;
            skills[5] = SkillLibrary.Dex;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Properties.Resources.Table_Education;
            skills = table.Skills;
            skills[0] = SkillLibrary.Pilot;
            skills[1] = SkillLibrary.ShipsBoat;
            skills[2] = SkillLibrary.GroundVehicle;
            skills[3] = SkillLibrary.Navigation;
            skills[4] = SkillLibrary.Engineering;
            skills[5] = SkillLibrary.Leader;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Properties.Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = SkillLibrary.Medic;
            skills[1] = SkillLibrary.Computer;
            skills[2] = SkillLibrary.Admin;
            skills[3] = SkillLibrary.Liason;
            skills[4] = SkillLibrary.Leader;
            skills[5] = SkillLibrary.JackOfTrades;

            Material.Add(BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.Gun);
            Material.Add(BenefitLibrary.Blade);
            Material.Add(BenefitLibrary.Travellers);
            Material.Add(BenefitLibrary.Yacht);

            Cash[0] = 10000;
            Cash[1] = 50000;
            Cash[2] = 50000;
            Cash[3] = 100000;
            Cash[4] = 100000;
            Cash[5] = 100000;
            Cash[6] = 200000;

            Ranks[0] = "Noble";
            Ranks[1] = "Knight";
            Ranks[2] = "Baron";
            Ranks[3] = "Marquis";
            Ranks[4] = "Count";
            Ranks[5] = "Duke";
        }

        protected override void CommsionSkill()
        {
            Owner.Profile["SOC"].Value = 11;
        }

        protected override int EnlistFactor()
        {
            return 0;
        }

        protected override void EnlistSkill()
        {
            
        }

        protected override void RankSkill()
        {
            Owner.Profile["SOC"].Value = RankNumber + 10;
        }

        public override bool Enlist()
        {
            var target = 10;
            if (Owner.CharacterSpecies == Character.Species.Aslan && Owner.Sex == "Female")
            {
                target++;
            }

            var enlist = false;
            if (Owner.Profile["SOC"].Value >= target)
            {
                enlist = true;
                Owner.Journal.Add(string.Format("Enlisted in {0} at age {1}", Name, Owner.Age));
            }
            else
            {
                Owner.Journal.Add(string.Format("Enlistment in {0} refused at age {1}", Name, Owner.Age));
            }
 
            return enlist;
        }

        public override bool Survival()
        {
            var target = survival;
            var survive = false;
            if (dice.roll(2) >= target)
            {
                survive = true;
            }
            return survive;
        }
    }
}
