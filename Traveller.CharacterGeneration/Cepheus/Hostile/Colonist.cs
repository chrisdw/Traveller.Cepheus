﻿namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Hostile
{
    public class Colonist : Cepheus.Colonist
    {
        public Colonist()
        {
            Name = Resources.Career_Colonist;
            hasRanks = true;

            Ranks[0] = Resources.Rank_Colonist;
            Ranks[1] = Resources.Rank_TeamLeader;
            Ranks[2] = Resources.Rank_Supervisor;
            Ranks[3] = Resources.Rank_DepartmentChief;
            Ranks[4] = Resources.Rank_AssistantOperationsManager;
            Ranks[5] = Resources.Rank_OperationsManager;
            Ranks[6] = Resources.Rank_ColonialAdministrator;

            Hostile.Culture.InitCashBenefits(this);

            Material[0] = BenefitLibrary.StandardTicket;
            Material[3] = BenefitLibrary.StandardTicket;
            Material[4] = BenefitLibrary.StandardTicket;
            Material[5] = BenefitLibrary.EliteTicket;

            SkillTables[0].Skills[4] = CharacterGeneration.SkillLibrary.Brawling;
            SkillTables[1].Skills[2] = SkillLibrary.Agriculture;
            SkillTables[1].Skills[5] = SkillLibrary.Vehicle;
            SkillTables[2].Skills[0] = SkillLibrary.Loader;
            SkillTables[2].Skills[1] = CharacterGeneration.SkillLibrary.Carousing;
            SkillTables[2].Skills[2] = CharacterGeneration.SkillLibrary.JackOfTrades;
            SkillTables[2].Skills[3] = CharacterGeneration.SkillLibrary.Engineering;
            SkillTables[2].Skills[4] = SkillLibrary.Agriculture;
            SkillTables[2].Skills[5] = SkillLibrary.Vehicle;
            SkillTables[3].Skills[0] = CharacterGeneration.SkillLibrary.Medic;
            SkillTables[3].Skills[1] = SkillLibrary.Agriculture;
            SkillTables[3].Skills[2] = CharacterGeneration.SkillLibrary.JackOfTrades;
            SkillTables[3].Skills[3] = CharacterGeneration.SkillLibrary.Liason;
            SkillTables[3].Skills[4] = CharacterGeneration.SkillLibrary.Admin;
            SkillTables[3].Skills[5] = CharacterGeneration.SkillLibrary.Leader;
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
                    Owner.Journal.Add(Resources.Mishap_ColonyAccident);
                    ResolveInjury(0);
                    break;
                case 2:
                    Owner.Journal.Add(Resources.Mishap_ColonyDisaster);
                    survive = SurvivalResult.Discharged;
                    break;
                case 3:
                    Owner.Journal.Add(Resources.Mishap_NewColonyLeaders);
                    survive = SurvivalResult.Discharged;
                    break;
                case 4:
                    Owner.Journal.Add(Resources.Mishap_ColonyDisturbance);
                    ResolveInjury(0);
                    break;
                case 5:
                    Owner.Journal.Add(Resources.Mishap_ColonyOutgrown);
                    survive = SurvivalResult.Discharged;
                    break;
                case 6:
                    Owner.Journal.Add(Resources.Mishap_ColonyUnpopular);
                    survive = SurvivalResult.Discharged;
                    break;
            }
            return survive;
        }
    }
}
