namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Hostile
{
    public class CorporateExec : Cepheus.Career
    {
        public CorporateExec()
        {
            Name = Resources.Career_CorporateExecutive;
            hasRanks = true;

            enlistment = 8;
            enlistmentattr = "SOC";
            survival = 4;
            survivalattr = "SOC";
            position = 5;
            positionattr = "SOC";
            promotion = 7;
            promotionattr = "INT";
            reenlist = 5;
            medicalBand = 2;

            Ranks[0] = Resources.Rank_ExecutiveConsultant;
            Ranks[1] = Resources.Rank_VicePresident;
            Ranks[2] = Resources.Rank_SeniorVicePresident;
            Ranks[3] = Resources.Rank_ExecutiveSeniorVicePresident;
            Ranks[4] = Resources.Rank_SeniorExecutiveVicePresident;
            Ranks[5] = Resources.Rank_Director;
            Ranks[6] = Resources.Rank_President;

            Material.Add(BenefitLibrary.EliteTicket);
            Material.Add(CharacterGeneration.BenefitLibrary.Edu);
            Material.Add(CharacterGeneration.BenefitLibrary.Int);
            Material.Add(BenefitLibrary.EliteTicket);
            Material.Add(BenefitLibrary.StarEnvoyClubMember);
            Material.Add(BenefitLibrary.EliteTicket);
            Material.Add(CharacterGeneration.BenefitLibrary.Soc);

            Cash[0] = 500;
            Cash[1] = 1000;
            Cash[2] = 1000;
            Cash[3] = 5000;
            Cash[4] = 8000;
            Cash[5] = 10000;
            Cash[6] = 20000;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Cepheus.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Streetwise;
            skills[1] = CharacterGeneration.SkillLibrary.Int;
            skills[2] = CharacterGeneration.SkillLibrary.Edu;
            skills[3] = CharacterGeneration.SkillLibrary.Soc;
            skills[4] = CharacterGeneration.SkillLibrary.Carousing;
            skills[5] = CharacterGeneration.SkillLibrary.Bribery;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Cepheus.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Gambling;
            skills[1] = CharacterGeneration.SkillLibrary.Admin;
            skills[2] = CharacterGeneration.SkillLibrary.Carousing;
            skills[3] = CharacterGeneration.SkillLibrary.Leader;
            skills[4] = CharacterGeneration.SkillLibrary.Bribery;
            skills[5] = CharacterGeneration.SkillLibrary.Forgery;

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
            skills[1] = CharacterGeneration.SkillLibrary.Computer;
            skills[2] = CharacterGeneration.SkillLibrary.Admin;
            skills[3] = CharacterGeneration.SkillLibrary.Liason;
            skills[4] = CharacterGeneration.SkillLibrary.Leader;
            skills[5] = CharacterGeneration.SkillLibrary.JackOfTrades;
        }

        protected override void EnlistSkill()
        {

        }

        protected override void RankSkill()
        {
            if (RankNumber == 1)
            {
                Owner.AddSkill(CharacterGeneration.SkillLibrary.Survival);
            }
        }

        protected override SurvivalResult ResolveMishap()
        {
            SurvivalResult survive = SurvivalResult.Survived;
            switch (dice.roll(1))
            {
                case 1:
                    Owner.Journal.Add(Resources.Mishap_Exec1);
                    survive = SurvivalResult.Discharged;
                    break;
                case 2:
                    Owner.Journal.Add(Resources.Mishap_Exec2);
                    survive = SurvivalResult.Discharged;
                    break;
                case 3:
                    Owner.Journal.Add(Resources.Mishap_Exec3);
                    survive = SurvivalResult.Discharged;
                    break;
                case 4:
                    Owner.Journal.Add(Resources.Mishap_Exec4);
                    survive = SurvivalResult.Discharged;
                    break;
                case 5:
                    Owner.Journal.Add(Resources.Mishap_Exec5);
                    survive = SurvivalResult.Discharged;
                    break;
                case 6:
                    Owner.Journal.Add(Resources.Mishap_InjuredAtWork);
                    ResolveInjury(0);
                    break;
            }
            return survive;
        }
    }
}
