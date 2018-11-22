using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Imperial.Citizen
{
    public class Bureaucrat : Career
    {
        public Bureaucrat()
        {
            Name = "Bureaucrat";
            RankNumber = 0;
            TermSkills = 2;

            enlistment = 5;
            enlistment1attr = "EDU";
            enlistment1val = 8;
            enlistment2attr = "STR";
            enlistment2val = 8;
            survival = 4;
            survival2attr = "EDU";
            survival2val = 10;
            position = 6;
            position1attr = "SOC";
            position1val = 9;
            promotion = 7;
            promotion1attr = "INT";
            promotion1val = 9;
            reenlist = 3;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Properties.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Edu;
            skills[2] = SkillLibrary.Int;
            skills[3] = SkillLibrary.Brawling;
            skills[4] = SkillLibrary.Carousing;
            skills[5] = SkillLibrary.Dex;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Properties.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.GunCombat;
            skills[1] = SkillLibrary.Vehicle;
            skills[2] = SkillLibrary.BladeCombat;
            skills[3] = SkillLibrary.Instruction;
            skills[4] = SkillLibrary.Vehicle;
            skills[5] = SkillLibrary.Edu;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Properties.Resources.Table_Education;
            skills = table.Skills;
            skills[0] = SkillLibrary.Recruiting;
            skills[1] = SkillLibrary.Vehicle;
            skills[2] = SkillLibrary.Liason;
            skills[3] = SkillLibrary.Interrogation;
            skills[4] = SkillLibrary.Admin;
            skills[5] = SkillLibrary.Admin;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Properties.Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = SkillLibrary.Admin;
            skills[1] = SkillLibrary.Admin;
            skills[2] = SkillLibrary.Computer;
            skills[3] = SkillLibrary.Admin;
            skills[4] = SkillLibrary.JackOfTrades;
            skills[5] = SkillLibrary.Leader;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.MidPsg);
            Material.Add(BenefitLibrary.Nothing);
            Material.Add(BenefitLibrary.Watch);
            Material.Add(BenefitLibrary.Nothing);
            Material.Add(BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.Soc);

            Cash[0] = 0;
            Cash[1] = 0;
            Cash[2] = 10000;
            Cash[3] = 10000;
            Cash[4] = 40000;
            Cash[5] = 40000;
            Cash[6] = 80000;

            Ranks[0] = string.Empty;
            Ranks[1] = Resources.Rank_Clerk;
            Ranks[2] = Resources.Rank_Supervisor;
            Ranks[3] = Resources.Rank_AssistantManager;
            Ranks[4] = Resources.Rank_Manager;
            Ranks[5] = Resources.Rank_Executive;
            Ranks[6] = Resources.Rank_Director;
        }
        protected override void CommsionSkill()
        {
            
        }

        protected override int EnlistFactor()
        {
            var target = 0;
            if (Owner.CharacterSpecies == Character.Species.Aslan && Owner.Sex.Equals(Properties.Resources.Sex_Male))
            {
                target += 4;
            }
            return target;
        }

        protected override void EnlistSkill()
        {
            OnSkillOffered(SkillLibrary.Vehicle);
        }
         
        protected override void RankSkill()
        {
            
        }
    }
}
