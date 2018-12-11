namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Hostile
{
    public class Scientist : Cepheus.Career
    {
        public Scientist()
        {
            Name = Resources.Career_Scientist;
            hasRanks = true;

            enlistment = 6;
            enlistmentattr = "EDU";
            survival = 3;
            survivalattr = "INT";
            position = 5;
            positionattr = "EDU";
            promotion = 8;
            promotionattr = "EDU";
            reenlist = 5;
            medicalBand = 2;

            Ranks[0] = Resources.Rank_Researcher;
            Ranks[1] = Resources.Rank_Scientist;
            Ranks[2] = Resources.Rank_SeniorScientist;
            Ranks[3] = Resources.Rank_DeputyScienceLeader;
            Ranks[4] = Resources.Rank_ScienceLeader;
            Ranks[5] = Resources.Rank_AssistantDirector;
            Ranks[6] = Resources.Rank_Director;

            Material.Add(BenefitLibrary.StandardTicket);
            Material.Add(BenefitLibrary.EliteTicket);
            Material.Add(BenefitLibrary.EliteTicket);
            Material.Add(Cepheus.BenefitLibrary.End);
            Material.Add(CharacterGeneration.BenefitLibrary.Soc);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
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
            skills[5] = CharacterGeneration.SkillLibrary.Int;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Cepheus.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.GunCombat;
            skills[1] = CharacterGeneration.SkillLibrary.Communications;
            skills[2] = SkillLibrary.Investigate;
            skills[3] = SkillLibrary.Vehicle;
            skills[4] = CharacterGeneration.SkillLibrary.Communications;
            skills[5] = CharacterGeneration.SkillLibrary.Survival;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Cepheus.Resources.Table_Specialist;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Mechanical;
            skills[1] = CharacterGeneration.SkillLibrary.Electronics;
            skills[2] = CharacterGeneration.SkillLibrary.VaccSuit;
            skills[3] = CharacterGeneration.SkillLibrary.Computer;
            skills[4] = SkillLibrary.Investigate;
            skills[5] = SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Cepheus.Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Medic;
            skills[1] = CharacterGeneration.SkillLibrary.Computer;
            skills[2] = CharacterGeneration.SkillLibrary.Admin;
            skills[3] = CharacterGeneration.SkillLibrary.Leader;
            skills[4] = CharacterGeneration.SkillLibrary.Navigation;
            skills[5] = CharacterGeneration.SkillLibrary.JackOfTrades;
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
                    Owner.Journal.Add(Resources.Mishap_Science1);
                    survive = SurvivalResult.Discharged;
                    lostBenefits = true;
                    break;
                case 2:
                    Owner.Journal.Add(Resources.Mishap_Science2);
                    survive = SurvivalResult.Discharged;
                    break;
                case 3:
                    Owner.Journal.Add(Resources.Mishap_Science3);
                    Owner.Profile.Soc.Value--;
                    break;
                case 4:
                    Owner.Journal.Add(Resources.Mishap_Science4);
                    survive = SurvivalResult.Discharged;
                    break;
                case 5:
                    Owner.Journal.Add(Resources.Mishap_Science5);
                    survive = SurvivalResult.Discharged;
                    break;
                case 6:
                    Owner.Journal.Add(Resources.Mishap_Science6);
                    ResolveInjury(0);
                    break;
            }
            return survive;
        }
    }
}
