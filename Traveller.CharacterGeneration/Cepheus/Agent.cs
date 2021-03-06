﻿namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus
{
    public class Agent : Career
    {
        public Agent()
        {
            Name = Resources.Career_Agent;
            hasRanks = true;

            enlistment = 6;
            enlistmentattr = "SOC";
            survival = 6;
            survivalattr = "INT";
            position = 7;
            positionattr = "EDU";
            promotion = 6;
            promotionattr = "EDU";
            reenlist = 6;
            medicalBand = 2;

            Ranks[0] = Resources.Rank_Agent;
            Ranks[1] = Resources.Rank_SpecialAgent;
            Ranks[2] = Resources.Rank_SpecialAgentInCharge;
            Ranks[3] = Resources.Rank_UnitChief;
            Ranks[4] = Resources.Rank_SectionChief;
            Ranks[5] = Resources.Rank_AssistantDirector;
            Ranks[6] = Resources.Rank_Director;

            Material.Add(CharacterGeneration.BenefitLibrary.LowPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.Int);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(CharacterGeneration.BenefitLibrary.MidPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.Soc);
            Material.Add(CharacterGeneration.BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.ExplorersSociety);

            Cash[0] = 1000;
            Cash[1] = 5000;
            Cash[2] = 10000;
            Cash[3] = 10000;
            Cash[4] = 20000;
            Cash[5] = 50000;
            Cash[6] = 50000;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Dex;
            skills[1] = CharacterGeneration.SkillLibrary.End;
            skills[2] = CharacterGeneration.SkillLibrary.Int;
            skills[3] = CharacterGeneration.SkillLibrary.Edu;
            skills[4] = SkillLibrary.Athletics;
            skills[5] = CharacterGeneration.SkillLibrary.Carousing;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Admin;
            skills[1] = CharacterGeneration.SkillLibrary.Computer;
            skills[2] = CharacterGeneration.SkillLibrary.Streetwise;
            skills[3] = CharacterGeneration.SkillLibrary.Bribery;
            skills[4] = CharacterGeneration.SkillLibrary.Leader;
            skills[5] = SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Resources.Table_Specialist;
            skills = table.Skills;
            skills[0] = SkillLibrary.GunCombat;
            skills[1] = SkillLibrary.MeleeCombat;
            skills[2] = CharacterGeneration.SkillLibrary.Bribery;
            skills[3] = CharacterGeneration.SkillLibrary.Leader;
            skills[4] = CharacterGeneration.SkillLibrary.Recon;
            skills[5] = CharacterGeneration.SkillLibrary.Survival;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = SkillLibrary.Advocate;
            skills[1] = CharacterGeneration.SkillLibrary.Computer;
            skills[2] = CharacterGeneration.SkillLibrary.Liason;
            skills[3] = SkillLibrary.Linguistics;
            skills[4] = CharacterGeneration.SkillLibrary.Medic;
            skills[5] = CharacterGeneration.SkillLibrary.Leader;
        }

        protected override void EnlistSkill()
        {
            Owner.AddSkill(CharacterGeneration.SkillLibrary.Streetwise);
        }

        protected override void RankSkill()
        {
            if (RankNumber == 4)
            {
                Owner.AddSkill(CharacterGeneration.SkillLibrary.Admin);
            }
        }
    }
}
