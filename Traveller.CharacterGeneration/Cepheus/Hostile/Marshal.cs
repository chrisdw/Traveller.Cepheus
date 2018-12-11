namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Hostile
{
    public class Marshal : Cepheus.Career
    {
        public Marshal()
        {
            Name = Resources.Career_Marshal;
            hasRanks = true;

            enlistment = 7;
            enlistmentattr = "INT";
            survival = 6;
            survivalattr = "DEX";
            position = 8;
            positionattr = "EDU";
            promotion = 7;
            promotionattr = "INT";
            reenlist = 5;
            medicalBand = 2;

            Ranks[0] = Resources.Rank_Deputy;
            Ranks[1] = Resources.Rank_SeniorDeputy;
            Ranks[2] = Resources.Rank_SupervisoryDeputy;
            Ranks[3] = Resources.Rank_AssistantChief;
            Ranks[4] = Resources.Rank_ChiefDeputy;
            Ranks[5] = Resources.Rank_Marshal;
            Ranks[6] = Resources.Rank_DivisionChief;

            Material.Add(BenefitLibrary.StandardTicket);
            Material.Add(BenefitLibrary.EliteTicket);
            Material.Add(CharacterGeneration.BenefitLibrary.Int2);
            Material.Add(CharacterGeneration.BenefitLibrary.Edu);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.EliteTicket);
            Material.Add(CharacterGeneration.BenefitLibrary.Soc);

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
            skills[5] = CharacterGeneration.SkillLibrary.Soc;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Cepheus.Resources.Table_Specialist;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Streetwise;
            skills[1] = CharacterGeneration.SkillLibrary.Brawling;
            skills[2] = CharacterGeneration.SkillLibrary.Brawling;
            skills[3] = SkillLibrary.Investigate;
            skills[4] = CharacterGeneration.SkillLibrary.Recon;
            skills[5] = SkillLibrary.GunCombat;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Cepheus.Resources.Table_Specialist;
            skills = table.Skills;
            skills[0] = SkillLibrary.GunCombat;
            skills[1] = CharacterGeneration.SkillLibrary.Communications;
            skills[2] = SkillLibrary.GroundVehicle;
            skills[3] = SkillLibrary.Security;
            skills[4] = CharacterGeneration.SkillLibrary.Computer;
            skills[5] = CharacterGeneration.SkillLibrary.Medic;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Cepheus.Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Medic;
            skills[1] = CharacterGeneration.SkillLibrary.Tactics;
            skills[2] = SkillLibrary.Investigate;
            skills[3] = CharacterGeneration.SkillLibrary.Computer;
            skills[4] = CharacterGeneration.SkillLibrary.Tactics;
            skills[5] = CharacterGeneration.SkillLibrary.Admin;
        }
        protected override void EnlistSkill()
        {
            Owner.AddSkill(SkillLibrary.Investigate);
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
                    Owner.Journal.Add(Resources.Mishap_Marshal1);
                    ResolveInjury(0);
                    break;
                case 2:
                    Owner.Journal.Add(Resources.Mishap_Marshal2);
                    survive = SurvivalResult.Discharged;
                    break;
                case 3:
                    Owner.Journal.Add(Resources.Mishap_Marshal3);
                    ResolveInjury(0);
                    break;
                case 4:
                    Owner.Journal.Add(Resources.Mishap_Marshal4);
                    survive = SurvivalResult.Discharged;
                    break;
                case 5:
                    Owner.Journal.Add(Resources.Mishap_Marshal5);
                    survive = SurvivalResult.Discharged;
                    break;
                case 6:
                    Owner.Journal.Add(Resources.Mishap_InjuredOnDuty);
                    ResolveInjury(0);
                    break;
            }
            return survive;
        }
    }
}
