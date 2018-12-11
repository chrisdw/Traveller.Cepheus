namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Hostile
{
    public class CommericalSpacer : Cepheus.Career
    {
        public CommericalSpacer()
        {
            Name = Resources.Career_CommercialSpacer;
            hasRanks = true;

            enlistment = 4;
            enlistmentattr = "INT";
            survival = 5;
            survivalattr = "INT";
            position = 5;
            positionattr = "INT";
            promotion = 8;
            promotionattr = "EDU";
            reenlist = 4;
            medicalBand = 2;

            Ranks[0] = Resources.Rank_Crewman;
            Ranks[1] = Resources.Rank_4thOfficer;
            Ranks[2] = Resources.Rank_3rdOfficer;
            Ranks[3] = Resources.Rank_2ndOfficer;
            Ranks[4] = Resources.Rank_1stOfficer;
            Ranks[5] = Resources.Rank_Captain;
            Ranks[6] = Resources.Rank_SeniorCaptain;

            Material.Add(BenefitLibrary.StandardTicket);
            Material.Add(CharacterGeneration.BenefitLibrary.Edu);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.EliteTicket);
            Material.Add(CharacterGeneration.BenefitLibrary.Int);
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
            skills[3] = CharacterGeneration.SkillLibrary.Str;
            skills[4] = CharacterGeneration.SkillLibrary.Brawling;
            skills[5] = CharacterGeneration.SkillLibrary.VaccSuit;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Cepheus.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Communications;
            skills[1] = CharacterGeneration.SkillLibrary.Bribery;
            skills[2] = SkillLibrary.GunCombat;
            skills[3] = SkillLibrary.Loader;
            skills[4] = CharacterGeneration.SkillLibrary.Broker;
            skills[5] = SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Cepheus.Resources.Table_Specialist;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.VaccSuit;
            skills[1] = CharacterGeneration.SkillLibrary.Mechanical;
            skills[2] = SkillLibrary.Loader;
            skills[3] = CharacterGeneration.SkillLibrary.Electronics;
            skills[4] = CharacterGeneration.SkillLibrary.Steward;
            skills[5] = CharacterGeneration.SkillLibrary.Navigation;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Cepheus.Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Medic;
            skills[1] = CharacterGeneration.SkillLibrary.Computer;
            skills[2] = CharacterGeneration.SkillLibrary.Broker;
            skills[3] = CharacterGeneration.SkillLibrary.Pilot;
            skills[4] = CharacterGeneration.SkillLibrary.Engineering;
            skills[5] = CharacterGeneration.SkillLibrary.Navigation;
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
                    Owner.Journal.Add(Resources.Mishap_CommSpace1);
                    survive = SurvivalResult.Discharged;
                    break;
                case 2:
                    Owner.Journal.Add(Resources.Mishap_CommSpace2);
                    survive = SurvivalResult.Discharged;
                    break;
                case 3:
                    Owner.Journal.Add(Resources.Mishap_CommSpace3);
                    survive = SurvivalResult.Discharged;
                    break;
                case 4:
                    Owner.Journal.Add(Resources.Mishap_CommSpace4);
                    survive = SurvivalResult.Discharged;
                    break;
                case 5:
                    Owner.Journal.Add(Resources.Mishap_CommSpace5);
                    ResolveInjury(0);
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
