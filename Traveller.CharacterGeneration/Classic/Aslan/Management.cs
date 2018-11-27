using System;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Aslan
{
    public class Management : Career
    {
        public Management()
        {
            Name = Resources.Career_Management;
            TermSkills = 2;

            Array.Resize(ref skillTables, 3);

            enlistment = 8;

            survival = 7;
            survival2attr = "INT";
            survival2val = 8;
            position = 9;
            position1attr = "EDU";
            position1val = 8;
            promotion = 8;
            promotion1attr = "INT";
            promotion1val = 8;
            reenlist = 5;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = null;
            skills[1] = CharacterGeneration.SkillLibrary.Str;
            skills[2] = CharacterGeneration.SkillLibrary.Dex;
            skills[3] = CharacterGeneration.SkillLibrary.End;
            skills[4] = CharacterGeneration.SkillLibrary.Tolerance;
            skills[5] = CharacterGeneration.SkillLibrary.Broker;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = null;
            skills[1] = CharacterGeneration.SkillLibrary.Broker;
            skills[2] = CharacterGeneration.SkillLibrary.Trader;
            skills[3] = SkillLibrary.GunCombat;
            skills[4] = CharacterGeneration.SkillLibrary.Tolerance;
            skills[5] = CharacterGeneration.SkillLibrary.Admin;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Resources.Table_ServiceSkillsFemale;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Admin;
            skills[1] = CharacterGeneration.SkillLibrary.Bribery;
            skills[2] = CharacterGeneration.SkillLibrary.Broker;
            skills[3] = CharacterGeneration.SkillLibrary.Trader;
            skills[4] = CharacterGeneration.SkillLibrary.Streetwise;
            skills[5] = CharacterGeneration.SkillLibrary.CombatEngineering;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Edu2);
            Material.Add(BenefitLibrary.MidPsg);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.MidPsg);
            Material.Add(BenefitLibrary.Trader);

            Cash[0] = 5000;
            Cash[1] = 5000;
            Cash[2] = 10000;
            Cash[3] = 20000;
            Cash[4] = 20000;
            Cash[5] = 40000;
            Cash[6] = 100000;

            Ranks[0] = Resources.Rank_Clerk;
            Ranks[1] = Resources.Rank_Adminstrator;
            Ranks[2] = Resources.Rank_Supervisor;
            Ranks[3] = Resources.Rank_Manager;
            Ranks[4] = Resources.Rank_Executive;
            Ranks[5] = Resources.Rank_Officer;
            Ranks[6] = Resources.Rank_Director;
        }

        protected override void CommsionSkill()
        {
            
        }

        protected override void EnlistSkill()
        {
            Owner.AddSkill(CharacterGeneration.SkillLibrary.Admin);
        }

        protected override void RankSkill()
        {
            
        }
    }
}
