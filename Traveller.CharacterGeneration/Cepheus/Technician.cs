namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus
{
    public class Technician : Career
    {
        public Technician()
        {
            Name = Resources.Career_Technician;
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

            Ranks[0] = Resources.Rank_Technician;
            Ranks[1] = Resources.Rank_TeamLead;
            Ranks[2] = Resources.Rank_Supervisor;
            Ranks[3] = Resources.Rank_Manager;
            Ranks[4] = Resources.Rank_Director;
            Ranks[5] = Resources.Rank_VicePresident;
            Ranks[6] = Resources.Rank_ExecutiveOfficer;

            Material.Add(CharacterGeneration.BenefitLibrary.LowPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.Edu);
            Material.Add(CharacterGeneration.BenefitLibrary.Int);
            Material.Add(CharacterGeneration.BenefitLibrary.MidPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.MidPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.HighPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.Soc);

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
            skills[2] = CharacterGeneration.SkillLibrary.Mechanical;
            skills[3] = CharacterGeneration.SkillLibrary.Medic;
            skills[4] = CharacterGeneration.SkillLibrary.Electronics;
            skills[5] = SkillLibrary.Sciences;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Resources.Table_Specialist;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Computer;
            skills[1] = CharacterGeneration.SkillLibrary.Electronics;
            skills[2] = CharacterGeneration.SkillLibrary.Gravitics;
            skills[3] = SkillLibrary.Linguistics;
            skills[4] = CharacterGeneration.SkillLibrary.Engineering;
            skills[5] = SkillLibrary.Animals;

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
            Owner.AddSkill(CharacterGeneration.SkillLibrary.Computer);
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
