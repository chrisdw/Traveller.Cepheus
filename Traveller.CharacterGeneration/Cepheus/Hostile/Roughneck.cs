namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Hostile
{
    public class Roughneck : Career
    {
        public Roughneck()
        {
            Name = Resources.Career_Roughneck;
            hasRanks = true;

            enlistment = 9;
            enlistmentattr = "END";
            survival = 6;
            survivalattr = "STR";
            position = 5;
            positionattr = "INT";
            promotion = 6;
            promotionattr = "END";
            reenlist = 5;
            medicalBand = 2;

            Ranks[0] = Resources.Rank_Roustabout;
            Ranks[1] = Resources.Rank_Floorhand;
            Ranks[2] = Resources.Rank_AssistantDriller;
            Ranks[3] = Resources.Rank_Driller;
            Ranks[4] = Resources.Rank_Toolpusher;
            Ranks[5] = Resources.Rank_Superintendant;
            Ranks[6] = Resources.Rank_GeneralManager;

            Material.Add(BenefitLibrary.StandardTicket);
            Material.Add(BenefitLibrary.StandardTicket);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.EliteTicket);
            Material.Add(CharacterGeneration.BenefitLibrary.Edu);
            Material.Add(CharacterGeneration.BenefitLibrary.Int);
            Material.Add(BenefitLibrary.EliteTicket);

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
            skills[0] = CharacterGeneration.SkillLibrary.Str;
            skills[1] = CharacterGeneration.SkillLibrary.Dex;
            skills[2] = CharacterGeneration.SkillLibrary.End;
            skills[3] = CharacterGeneration.SkillLibrary.Gambling;
            skills[4] = CharacterGeneration.SkillLibrary.VaccSuit;
            skills[5] = CharacterGeneration.SkillLibrary.Brawling;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Cepheus.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.VaccSuit;
            skills[1] = SkillLibrary.Mining;
            skills[2] = CharacterGeneration.SkillLibrary.VaccSuit;
            skills[3] = CharacterGeneration.SkillLibrary.Demolitions;
            skills[4] = CharacterGeneration.SkillLibrary.Communications;
            skills[5] = SkillLibrary.GroundVehicle;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Cepheus.Resources.Table_Specialist;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Streetwise;
            skills[1] = CharacterGeneration.SkillLibrary.Electronics;
            skills[2] = SkillLibrary.GroundVehicle;
            skills[3] = CharacterGeneration.SkillLibrary.Mechanical;
            skills[4] = SkillLibrary.Mining;
            skills[5] = CharacterGeneration.SkillLibrary.Mechanical;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Cepheus.Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Navigation;
            skills[1] = CharacterGeneration.SkillLibrary.Medic;
            skills[2] = CharacterGeneration.SkillLibrary.Electronics;
            skills[3] = CharacterGeneration.SkillLibrary.Computer;
            skills[4] = CharacterGeneration.SkillLibrary.Engineering;
            skills[5] = CharacterGeneration.SkillLibrary.JackOfTrades;
        }

        protected override void EnlistSkill()
        {
            Owner.AddSkill(CharacterGeneration.SkillLibrary.VaccSuit);
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
                    Owner.Journal.Add(Resources.Mishap_RoughneckIllness);
                    break;
                case 2:
                    Owner.Journal.Add(Resources.Mishap_RoughneckFired);
                    survive = SurvivalResult.Discharged;
                    lostBenefits = true;
                    break;
                case 3:
                    Owner.Journal.Add(Resources.Mishap_RoughneckAccident);
                    survive = SurvivalResult.Discharged;
                    break;
                case 4:
                    Owner.Journal.Add(Resources.Mishap_InjuredAtWork);
                    ResolveInjury(0);
                    break;
                case 5:
                    Owner.Journal.Add(Resources.Mishap_RoughneckBadBoss);
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
