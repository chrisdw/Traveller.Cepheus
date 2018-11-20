using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.SwordWorlds
{
    public class Patrol : Imperial.Citizen.Career
    {
        public Patrol()
        {
            Name = "Patrol";
            RankNumber = 0;
            TermSkills = 2;

            enlistment = 7;
            enlistment1attr = "INT";
            enlistment1val = 6;
            survival = 5;
            survival2attr = "END";
            survival2val = 8;
            position = 6;
            position1attr = "INT";
            position1val = 8;
            promotion = 7;
            promotion1attr = "INT";
            promotion1val = 6;
            reenlist = 7;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Properties.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Gambling;
            skills[2] = SkillLibrary.End;
            skills[3] = SkillLibrary.Int;
            skills[4] = SkillLibrary.Edu;
            skills[5] = SkillLibrary.GunCombat;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Properties.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.AirRaft;
            skills[1] = SkillLibrary.VaccSuit;
            skills[2] = SkillLibrary.Mechanical;
            skills[3] = SkillLibrary.Navigation;
            skills[4] = SkillLibrary.Electronics;
            skills[5] = SkillLibrary.Admin;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Properties.Resources.Table_Education;
            skills = table.Skills;
            skills[0] = SkillLibrary.Pilot;
            skills[1] = SkillLibrary.VaccSuit;
            skills[2] = SkillLibrary.Mechanical;
            skills[3] = SkillLibrary.Navigation;
            skills[4] = SkillLibrary.Electronics;
            skills[5] = SkillLibrary.Admin;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Properties.Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = SkillLibrary.Tactics;
            skills[1] = SkillLibrary.Navigation;
            skills[2] = SkillLibrary.Engineering;
            skills[3] = SkillLibrary.Computer;
            skills[4] = SkillLibrary.Medic;
            skills[5] = SkillLibrary.JackOfTrades;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.Blade);
            Material.Add(BenefitLibrary.Gun);
            Material.Add(BenefitLibrary.HighPsg);

            Cash[0] = 20000;
            Cash[1] = 20000;
            Cash[2] = 30000;
            Cash[3] = 30000;
            Cash[4] = 50000;
            Cash[5] = 60000;
            Cash[6] = 70000;

            Ranks[0] = "Patrolman";
            Ranks[1] = "Konstabel";
            Ranks[2] = "Overhode";
            Ranks[3] = "Leutnant";
            Ranks[4] = "Overleutnant";
            Ranks[5] = "Kapiten";
            Ranks[6] = "Oberst";
        }
        protected override void CommsionSkill()
        {
            
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
            
        }
    }
}
