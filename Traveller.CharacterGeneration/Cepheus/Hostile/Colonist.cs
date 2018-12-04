using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Hostile
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

            Cash[0] = 500;
            Cash[1] = 1000;
            Cash[2] = 1000;
            Cash[3] = 5000;
            Cash[4] = 8000;
            Cash[5] = 10000;
            Cash[6] = 20000;

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
                    Owner.Journal.Add("A colony accident left you injured.");
                    ResolveInjury(0);
                    break;
                case 2:
                    Owner.Journal.Add("Your colony is struck by disaster and you took some of the blame.");
                    survive = SurvivalResult.Discharged;
                    break;
                case 3:
                    Owner.Journal.Add("New colonial leaders decided you and your crew were troublemakers.");
                    survive = SurvivalResult.Discharged;
                    break;
                case 4:
                    Owner.Journal.Add("A civil disturbance was put down by the corporation.");
                    ResolveInjury(0);
                    break;
                case 5:
                    Owner.Journal.Add("You outgrew the colony. You didn’t fit in anymore.");
                    survive = SurvivalResult.Discharged;
                    break;
                case 6:
                    Owner.Journal.Add("Your ideas are revolutionary and unpopular - colonists attacked you and your settlement.");
                    survive = SurvivalResult.Discharged;
                    break;
            }
            return survive;
        }
    }
}
