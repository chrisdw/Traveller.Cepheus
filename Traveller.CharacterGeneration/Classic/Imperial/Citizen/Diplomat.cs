using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Imperial.Citizen
{
    public class Diplomat : Career
    {
        public Diplomat()
        {
            Name = "Diplomat";
            RankNumber = 0;
            TermSkills = 2;

            enlistment = 8;
            enlistment1attr = "EDU";
            enlistment1val = 8;
            enlistment2attr = "SOC";
            enlistment2val = 9;
            survival = 3;
            survival2attr = "EDU";
            survival2val = 9;
            position = 5;
            position1attr = "INT";
            position1val = 8;
            promotion = 10;
            promotion1attr = "SOC";
            promotion1val = 10;
            reenlist = 5;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Properties.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Edu;
            skills[2] = SkillLibrary.Int;
            skills[3] = SkillLibrary.BladeCombat;
            skills[4] = SkillLibrary.GunCombat;
            skills[5] = SkillLibrary.Carousing;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Properties.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.Int;
            skills[1] = SkillLibrary.VaccSuit;
            skills[2] = SkillLibrary.GroundVehicle;
            skills[3] = SkillLibrary.GroundVehicle;
            skills[4] = SkillLibrary.Gambling;
            skills[5] = SkillLibrary.Computer;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Properties.Resources.Table_Education;
            skills = table.Skills;
            skills[0] = SkillLibrary.Forgery;
            skills[1] = SkillLibrary.Streetwise;
            skills[2] = SkillLibrary.Interrogation;
            skills[3] = SkillLibrary.Recruiting;
            skills[4] = SkillLibrary.Instruction;
            skills[5] = SkillLibrary.Admin;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Properties.Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = SkillLibrary.Liason;
            skills[1] = SkillLibrary.Liason;
            skills[2] = SkillLibrary.Admin;
            skills[3] = SkillLibrary.Computer;
            skills[4] = SkillLibrary.Soc;
            skills[5] = SkillLibrary.JackOfTrades;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.MidPsg);
            Material.Add(BenefitLibrary.Nothing);
            Material.Add(BenefitLibrary.Watch);
            Material.Add(BenefitLibrary.Nothing);
            Material.Add(BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.Soc);

            Cash[0] = 10000;
            Cash[1] = 10000;
            Cash[2] = 10000;
            Cash[3] = 20000;
            Cash[4] = 50000;
            Cash[5] = 60000;
            Cash[6] = 70000;

            Ranks[0] = string.Empty;
            Ranks[1] = "3rd Secretary";
            Ranks[2] = "2nd Secretary";
            Ranks[3] = "1st Secretary";
            Ranks[4] = "Counselor";
            Ranks[5] = "Minister";
            Ranks[6] = "Ambassador";
        }

        protected override void CommsionSkill()
        {
            // nothing to do here
        }

        protected override int EnlistFactor()
        {
            var target = 0;
            if (Owner.CharacterSpecies == Character.Species.Aslan && Owner.Sex.Equals("Male"))
            {
                target--;
            }
            return target;
        }

        protected override void EnlistSkill()
        {
            Owner.AddSkill(SkillLibrary.Liason);
        }

        protected override void RankSkill()
        {
            // nothing to do here
        }
    }
}
