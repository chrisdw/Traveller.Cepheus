namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Hostile
{
    public class CorporateAgent : Cepheus.Career
    {
        public CorporateAgent()
        {
            Name = Resources.Career_CorporateAgent;
            hasRanks = true;

            enlistment = 6;
            enlistmentattr = "INT";
            survival = 5;
            survivalattr = "DEX";
            position = 5;
            positionattr = "SOC";
            promotion = 7;
            promotionattr = "INT";
            reenlist = 5;
            medicalBand = 2;

            Ranks[0] = Resources.Rank_Agent;
            Ranks[1] = Resources.Rank_SeniorAgent;
            Ranks[2] = Resources.Rank_Supervisor;
            Ranks[3] = Resources.Rank_AssistantProjectLeader;
            Ranks[4] = Resources.Rank_ProjectLeader;
            Ranks[5] = Resources.Rank_AssistantDivisionChief;
            Ranks[6] = Resources.Rank_DivisionChief;

            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.StandardTicket);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(CharacterGeneration.BenefitLibrary.Int);
            Material.Add(CharacterGeneration.BenefitLibrary.Edu);
            Material.Add(CharacterGeneration.BenefitLibrary.Soc);
            Material.Add(CharacterGeneration.BenefitLibrary.Soc);

            Hostile.Culture.InitCashBenefits(this);

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Cepheus.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Str;
            skills[1] = CharacterGeneration.SkillLibrary.Dex;
            skills[2] = CharacterGeneration.SkillLibrary.Bribery;
            skills[3] = CharacterGeneration.SkillLibrary.Int;
            skills[4] = CharacterGeneration.SkillLibrary.Edu;
            skills[5] = CharacterGeneration.SkillLibrary.Soc;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Cepheus.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.GunCombat;
            skills[1] = CharacterGeneration.SkillLibrary.VaccSuit;
            skills[2] = SkillLibrary.Vehicle;
            skills[3] = CharacterGeneration.SkillLibrary.Streetwise;
            skills[4] = SkillLibrary.GunCombat;
            skills[5] = CharacterGeneration.SkillLibrary.Recon;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Cepheus.Resources.Table_Specialist;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Forgery;
            skills[1] = SkillLibrary.Investigate;
            skills[2] = CharacterGeneration.SkillLibrary.Computer;
            skills[3] = CharacterGeneration.SkillLibrary.Carousing;
            skills[4] = CharacterGeneration.SkillLibrary.Communications;
            skills[5] = CharacterGeneration.SkillLibrary.JackOfTrades;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Cepheus.Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Medic;
            skills[1] = SkillLibrary.Security;
            skills[2] = CharacterGeneration.SkillLibrary.Admin;
            skills[3] = CharacterGeneration.SkillLibrary.Computer;
            skills[4] = CharacterGeneration.SkillLibrary.Leader;
            skills[5] = CharacterGeneration.SkillLibrary.Tactics;
        }
        protected override void EnlistSkill()
        {

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
                    Owner.Journal.Add(Resources.Mishap_Agent1);
                    ResolveInjury(0);
                    break;
                case 2:
                    Owner.Journal.Add(Resources.Mishap_Agent2);
                    survive = SurvivalResult.Discharged;
                    break;
                case 3:
                    Owner.Journal.Add(Resources.Mishap_Agent3);
                    ResolveInjury(0);
                    break;
                case 4:
                    Owner.Journal.Add(Resources.Mishap_Agent4);
                    survive = SurvivalResult.Discharged;
                    break;
                case 5:
                    Owner.Journal.Add(Resources.Mishap_Agent5);
                    survive = SurvivalResult.Discharged;
                    break;
                case 6:
                    Owner.Journal.Add(Resources.Mishap_Agent6);
                    survive = SurvivalResult.Discharged;
                    break;
            }
            return survive;
        }
    }
}
