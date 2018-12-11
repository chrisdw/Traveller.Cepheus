namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Hostile
{
    public class Technician : Cepheus.Career
    {
        public Technician()
        {
            Name = Resources.Career_Technician;
            hasRanks = true;

            enlistment = 7;
            enlistmentattr = "EDU";
            survival = 4;
            survivalattr = "INT";
            position = 4;
            positionattr = "EDU";
            promotion = 8;
            promotionattr = "EDU";
            reenlist = 5;
            medicalBand = 2;

            Ranks[0] = Resources.Rank_Technician;
            Ranks[1] = Resources.Rank_TeamLeader;
            Ranks[2] = Resources.Rank_Supervisor;
            Ranks[3] = Resources.Rank_DepartmentChief;
            Ranks[4] = Resources.Rank_AssistantTechnicalManager;
            Ranks[5] = Resources.Rank_TechnicalManager;
            Ranks[6] = Resources.Rank_Administrator;

            Material.Add(BenefitLibrary.StandardTicket);
            Material.Add(BenefitLibrary.EliteTicket);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(CharacterGeneration.BenefitLibrary.Int);
            Material.Add(CharacterGeneration.BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.Dex);
            Material.Add(CharacterGeneration.BenefitLibrary.Edu2);

            Hostile.Culture.InitCashBenefits(this);

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Cepheus.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Str;
            skills[1] = CharacterGeneration.SkillLibrary.Dex;
            skills[2] = CharacterGeneration.SkillLibrary.End;
            skills[3] = CharacterGeneration.SkillLibrary.Int;
            skills[4] = CharacterGeneration.SkillLibrary.Edu;
            skills[5] = CharacterGeneration.SkillLibrary.Edu;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Cepheus.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.Vehicle;
            skills[1] = CharacterGeneration.SkillLibrary.Electronics;
            skills[2] = CharacterGeneration.SkillLibrary.Mechanical;
            skills[3] = CharacterGeneration.SkillLibrary.Communications;
            skills[4] = CharacterGeneration.SkillLibrary.VaccSuit;
            skills[5] = SkillLibrary.Loader;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Cepheus.Resources.Table_Specialist;
            skills = table.Skills;
            skills[0] = SkillLibrary.Investigate;
            skills[1] = SkillLibrary.Vehicle;
            skills[2] = CharacterGeneration.SkillLibrary.Computer;
            skills[3] = SkillLibrary.Security;
            skills[4] = CharacterGeneration.SkillLibrary.Engineering;
            skills[5] = CharacterGeneration.SkillLibrary.JackOfTrades;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Cepheus.Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Mechanical;
            skills[1] = CharacterGeneration.SkillLibrary.Computer;
            skills[2] = CharacterGeneration.SkillLibrary.Admin;
            skills[3] = CharacterGeneration.SkillLibrary.Electronics;
            skills[4] = CharacterGeneration.SkillLibrary.Engineering;
            skills[5] = CharacterGeneration.SkillLibrary.JackOfTrades;
        }
        protected override void EnlistSkill()
        {
            var basic = new Skill()
            {
                Name = Cepheus.Resources.Skill_BasicTraining,
                Cascade =
                {
                    CharacterGeneration.SkillLibrary.Electronics,
                    CharacterGeneration.SkillLibrary.Mechanical
                }
            };
            OnSkillOffered(basic);
        }

        protected override void RankSkill()
        {
            
        }

        protected override SurvivalResult ResolveMishap()
        {
            SurvivalResult survive = SurvivalResult.Survived;
            switch (dice.roll(1))
            {
                case 1:
                    Owner.Journal.Add(Resources.Mishap_InjuredOnDuty);
                    ResolveInjury(0);
                    break;
                case 2:
                    Owner.Journal.Add(Resources.Mishap_Tech2);
                    survive = SurvivalResult.Discharged;
                    break;
                case 3:
                    Owner.Journal.Add(Resources.Mishap_Tech3);
                    survive = SurvivalResult.Discharged;
                    break;
                case 4:
                    Owner.Journal.Add(Resources.Mishap_Tech4);
                    ResolveInjury(0);
                    break;
                case 5:
                    Owner.Journal.Add(Resources.Mishap_Tech5);
                    survive = SurvivalResult.Discharged;
                    break;
                case 6:
                    Owner.Journal.Add(Resources.Mishap_Tech6);
                    survive = SurvivalResult.Discharged;
                    break;
            }
            return survive;
        }
    }
}
