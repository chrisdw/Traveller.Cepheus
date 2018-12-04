using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus
{
    public class Diplomat : Career
    {
        public Diplomat()
        {
            Name = Resources.Career_Diplomat;
            hasRanks = true;

            enlistment = 6;
            enlistmentattr = "SOC";
            survival = 5;
            survivalattr = "EDU";
            position = 7;
            positionattr = "INT";
            promotion = 7;
            promotionattr = "SOC";
            reenlist = 5;
            medicalBand = 2;

            Ranks[0] = Resources.Rank_Attache;
            Ranks[1] = Resources.Rank_ThirdSecretary;
            Ranks[2] = Resources.Rank_SecondSecretary;
            Ranks[3] = Resources.Rank_FirstSecretary;
            Ranks[4] = Resources.Rank_Counselor;
            Ranks[5] = Resources.Rank_Minister;
            Ranks[6] = Resources.Rank_Ambassador;

            Material.Add(CharacterGeneration.BenefitLibrary.LowPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.Edu);
            Material.Add(CharacterGeneration.BenefitLibrary.MidPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.HighPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.Soc);
            Material.Add(CharacterGeneration.BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.ExplorersSociety);

            Cash[0] = 1000;
            Cash[1] = 5000;
            Cash[2] = 10000;
            Cash[3] = 20000;
            Cash[4] = 20000;
            Cash[5] = 50000;
            Cash[6] = 100000;

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
            skills[2] = CharacterGeneration.SkillLibrary.Carousing;
            skills[3] = CharacterGeneration.SkillLibrary.Bribery;
            skills[4] = CharacterGeneration.SkillLibrary.Liason;
            skills[5] = SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Resources.Table_Specialist;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Carousing;
            skills[1] = SkillLibrary.Linguistics;
            skills[2] = CharacterGeneration.SkillLibrary.Bribery;
            skills[3] = CharacterGeneration.SkillLibrary.Liason;
            skills[4] = CharacterGeneration.SkillLibrary.Steward;
            skills[5] = SkillLibrary.Vehicle;

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
        protected override void CommsionSkill()
        {
            
        }

        protected override void EnlistSkill()
        {
            Owner.AddSkill(CharacterGeneration.SkillLibrary.Liason);
        }

        protected override void RankSkill()
        {
            if (RankNumber == 3)
            {
                Owner.AddSkill(CharacterGeneration.SkillLibrary.Admin);
            }
        }
    }
}
