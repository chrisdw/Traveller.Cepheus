using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Hostile
{
    public class CorporateAgent : Cepheus.Career
    {
        public CorporateAgent()
        {
            Name = "Corporate Agent";
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

            Ranks[0] = "Agent";
            Ranks[1] = "Senior Agent";
            Ranks[2] = Resources.Rank_Supervisor;
            Ranks[3] = "Assistant Project Leader";
            Ranks[4] = "Project Leader";
            Ranks[5] = "Assistant Division Chief";
            Ranks[6] = "Division Chief";

            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.StandardTicket);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(CharacterGeneration.BenefitLibrary.Int);
            Material.Add(CharacterGeneration.BenefitLibrary.Edu);
            Material.Add(CharacterGeneration.BenefitLibrary.Soc);
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
                    Owner.Journal.Add("A corporate defection went wrong and you were wounded.");
                    ResolveInjury(0);
                    break;
                case 2:
                    Owner.Journal.Add("Your team was annihilated in a double-cross.");
                    survive = SurvivalResult.Discharged;
                    break;
                case 3:
                    Owner.Journal.Add("A rival corporation killed members of your family and wounded you.");
                    ResolveInjury(0);
                    break;
                case 4:
                    Owner.Journal.Add("You learnt a secret about your employer and had to get out – fast.");
                    survive = SurvivalResult.Discharged;
                    break;
                case 5:
                    Owner.Journal.Add("An investigation uncovered government corruption, but you were forced to step down.");
                    survive = SurvivalResult.Discharged;
                    break;
                case 6:
                    Owner.Journal.Add("You failed to stop damage/disease/worker unrest at an Off-World outpost");
                    survive = SurvivalResult.Discharged;
                    break;
            }
            return survive;
        }
    }
}
