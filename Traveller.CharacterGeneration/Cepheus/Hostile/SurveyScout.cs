namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Hostile
{
    public class SurveyScout : Cepheus.Career
    {
        public SurveyScout()
        {
            Name = Resources.Career_SurveyScout;
            hasRanks = true;

            enlistment = 7;
            enlistmentattr = "STR";
            survival = 7;
            survivalattr = "END";
            position = 4;
            positionattr = "INT";
            promotion = 8;
            promotionattr = "END";
            reenlist = 3;
            medicalBand = 2;

            Ranks[0] = Resources.Rank_Scout;
            Ranks[1] = Resources.Rank_SeniorScout;
            Ranks[2] = Resources.Rank_Supervisor;
            Ranks[3] = Resources.Rank_MissionSpecialist;
            Ranks[4] = Resources.Rank_SeniorMissionSpecialist;
            Ranks[5] = Resources.Rank_MissionChief;
            Ranks[6] = Resources.Rank_OperationsChief;

            Material.Add(BenefitLibrary.StandardTicket);
            Material.Add(CharacterGeneration.BenefitLibrary.Int2);
            Material.Add(CharacterGeneration.BenefitLibrary.Edu);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.EliteTicket);
            Material.Add(BenefitLibrary.StarEnvoyClubMember);

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
            skills[5] = SkillLibrary.GunCombat;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Cepheus.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.Vehicle;
            skills[1] = CharacterGeneration.SkillLibrary.VaccSuit;
            skills[2] = CharacterGeneration.SkillLibrary.Mechanical;
            skills[3] = SkillLibrary.Mining;
            skills[4] = CharacterGeneration.SkillLibrary.Communications;
            skills[5] = SkillLibrary.Mining;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Cepheus.Resources.Table_Specialist;
            skills = table.Skills;
            skills[0] = SkillLibrary.Vehicle;
            skills[1] = CharacterGeneration.SkillLibrary.Mechanical;
            skills[2] = CharacterGeneration.SkillLibrary.Electronics;
            skills[3] = SkillLibrary.Investigate;
            skills[4] = Cepheus.SkillLibrary.Gunnery;
            skills[5] = CharacterGeneration.SkillLibrary.Medic;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Cepheus.Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Medic;
            skills[1] = CharacterGeneration.SkillLibrary.Navigation;
            skills[2] = CharacterGeneration.SkillLibrary.Engineering;
            skills[3] = CharacterGeneration.SkillLibrary.Computer;
            skills[4] = CharacterGeneration.SkillLibrary.Pilot;
            skills[5] = CharacterGeneration.SkillLibrary.JackOfTrades;
        }
        protected override void EnlistSkill()
        {
            
        }

        protected override void RankSkill()
        {
            if (RankNumber == 1)
            {
                Owner.AddSkill(CharacterGeneration.SkillLibrary.Pilot);
            }
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
                    Owner.Journal.Add(Resources.Mishap_Scout2);
                    ResolveInjury(0);
                    break;
                case 3:
                    Owner.Journal.Add(Resources.Mishap_Scout3);
                    survive = SurvivalResult.Discharged;
                    break;
                case 4:
                    Owner.Journal.Add(Resources.Mishap_Scout4);
                    survive = SurvivalResult.Discharged;
                    break;
                case 5:
                    Owner.Journal.Add(Resources.Mishap_Scout5);
                    ResolveInjury(0);
                    break;
                case 6:
                    Owner.Journal.Add(Resources.Mishap_Scout6);
                    survive = SurvivalResult.Discharged;
                    break;
            }
            return survive;
        }
    }
}
