
namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Commonwealth
{
    public class LicencedWitch : Cepheus.Career
    {
        public LicencedWitch()
        {
            Name = Resources.Career_LicencedWitch;
            psionicTrained = true;

            enlistment = 6;
            enlistmentattr = "PSI";
            survival = 4;
            survivalattr = "EDU";
            position = 5;
            positionattr = "PSI";
            promotion = 8;
            promotionattr = "INT";
            reenlist = 5;
            medicalBand = 2;

            Ranks[0] = Resources.Rank_TraineeWitch;
            Ranks[1] = Resources.Rank_Witch;
            Ranks[2] = Resources.Rank_SeniorWitch;
            Ranks[3] = Resources.Rank_AssistantCovenLeader;
            Ranks[4] = Resources.Rank_CovenLeader;
            Ranks[5] = Resources.Rank_AreaLeader;
            Ranks[6] = Resources.Rank_SectorLeader;

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
            table.Name = Cepheus.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Dex;
            skills[1] = CharacterGeneration.SkillLibrary.End;
            skills[2] = CharacterGeneration.SkillLibrary.Int;
            skills[3] = CharacterGeneration.SkillLibrary.Edu;
            skills[4] = SkillLibrary.Psionics;
            skills[5] = CharacterGeneration.SkillLibrary.Psi;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Cepheus.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Admin;
            skills[1] = CharacterGeneration.SkillLibrary.Computer;
            skills[2] = SkillLibrary.Psionics;
            skills[3] = CharacterGeneration.SkillLibrary.Bribery;
            skills[4] = CharacterGeneration.SkillLibrary.Leader;
            skills[5] = Cepheus.SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Cepheus.Resources.Table_Specialist;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Admin;
            skills[1] = CharacterGeneration.SkillLibrary.Computer;
            skills[2] = SkillLibrary.Psionics;
            skills[3] = CharacterGeneration.SkillLibrary.Leader;
            skills[4] = CharacterGeneration.SkillLibrary.Psi;
            skills[5] = Cepheus.SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Cepheus.Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = SkillLibrary.Psionics;
            skills[1] = CharacterGeneration.SkillLibrary.Computer;
            skills[2] = CharacterGeneration.SkillLibrary.Liason;
            skills[3] = SkillLibrary.Psionics;
            skills[4] = CharacterGeneration.SkillLibrary.Medic;
            skills[5] = CharacterGeneration.SkillLibrary.Admin;
        }
        protected override void EnlistSkill()
        {
            Owner.AddSkill(Cepheus.SkillLibrary.Awareness);
        }

        protected override void RankSkill()
        {
            if (RankNumber == 5)
            {
                Owner.AddSkill(CharacterGeneration.SkillLibrary.Admin);
            }
        }
    }
}
