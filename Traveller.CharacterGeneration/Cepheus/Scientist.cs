namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus
{
    public class Scientist : Career
    {
        public Scientist()
        {
            Name = Resources.Career_Scientist;
            hasRanks = true;

            enlistment = 6;
            enlistmentattr = "EDU";
            survival = 5;
            survivalattr = "EDU";
            position = 7;
            positionattr = "INT";
            promotion = 6;
            promotionattr = "INT";
            reenlist = 5;
            medicalBand = 2;


            Ranks[0] = Resources.Rank_Instructor;
            Ranks[1] = Resources.Rank_AdjunctProfessor;
            Ranks[2] = Resources.Rank_ResearchProfessor;
            Ranks[3] = Resources.Rank_AssistantProfessor;
            Ranks[4] = Resources.Rank_AssociateProfessor;
            Ranks[5] = Resources.Rank_Professor;
            Ranks[6] = Resources.Rank_DistinguishedProfessor;

            Material.Add(CharacterGeneration.BenefitLibrary.LowPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.Edu);
            Material.Add(CharacterGeneration.BenefitLibrary.Int);
            Material.Add(CharacterGeneration.BenefitLibrary.MidPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.Soc);
            Material.Add(CharacterGeneration.BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.ResearchVessel);

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
            skills[0] = CharacterGeneration.SkillLibrary.Str;
            skills[1] = CharacterGeneration.SkillLibrary.Dex;
            skills[2] = CharacterGeneration.SkillLibrary.End;
            skills[3] = CharacterGeneration.SkillLibrary.Int;
            skills[4] = CharacterGeneration.SkillLibrary.Edu;
            skills[5] = SkillLibrary.GunCombat;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Admin;
            skills[1] = CharacterGeneration.SkillLibrary.Computer;
            skills[2] = CharacterGeneration.SkillLibrary.Electronics;
            skills[3] = CharacterGeneration.SkillLibrary.Medic;
            skills[4] = CharacterGeneration.SkillLibrary.Bribery;
            skills[5] = SkillLibrary.Sciences;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Resources.Table_Specialist;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Navigation;
            skills[1] = CharacterGeneration.SkillLibrary.Admin;
            skills[2] = SkillLibrary.Sciences;
            skills[3] = SkillLibrary.Sciences;
            skills[4] = SkillLibrary.Animals;
            skills[5] = SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = SkillLibrary.Advocate;
            skills[1] = CharacterGeneration.SkillLibrary.Computer;
            skills[2] = CharacterGeneration.SkillLibrary.JackOfTrades;
            skills[3] = SkillLibrary.Linguistics;
            skills[4] = CharacterGeneration.SkillLibrary.Medic;
            skills[5] = SkillLibrary.Sciences;
        }
        protected override void EnlistSkill()
        {
            OnSkillOffered(SkillLibrary.Sciences);
        }

        protected override void RankSkill()
        {
            if (RankNumber == 3)
            {
                Owner.AddSkill(CharacterGeneration.SkillLibrary.Computer);
            }
        }
    }
}
