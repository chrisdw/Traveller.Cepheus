namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Hostile
{
    public class Rogue : Cepheus.Career
    {
        public Rogue()
        {
            Name = Resources.Career_Rogue;
            hasRanks = true;

            enlistment = 6;
            enlistmentattr = "END";
            survival = 6;
            survivalattr = "INT";
            position = 8;
            positionattr = "STR";
            promotion = 6;
            promotionattr = "INT";
            reenlist = 5;
            medicalBand = 3;

            Ranks[0] = Resources.Rank_Cholo;
            Ranks[1] = Resources.Rank_Soldier;
            Ranks[2] = Resources.Rank_Veteran;
            Ranks[3] = Resources.Rank_Lieutenant;
            Ranks[4] = Resources.Rank_Captain;
            Ranks[5] = Resources.Rank_RightHand;
            Ranks[6] = Resources.Rank_General;

            Material.Add(BenefitLibrary.StandardTicket);
            Material.Add(CharacterGeneration.BenefitLibrary.Soc);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.EliteTicket);
            Material.Add(Cepheus.BenefitLibrary.End);
            Material.Add(BenefitLibrary.StarEnvoyClubMember);

            Hostile.Culture.InitCashBenefits(this);

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Cepheus.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Str;
            skills[1] = CharacterGeneration.SkillLibrary.Dex;
            skills[2] = CharacterGeneration.SkillLibrary.End;
            skills[3] = CharacterGeneration.SkillLibrary.Brawling;
            skills[4] = SkillLibrary.BladeCombat;
            skills[5] = CharacterGeneration.SkillLibrary.Carousing;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Cepheus.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.BladeCombat;
            skills[1] = SkillLibrary.GunCombat;
            skills[2] = CharacterGeneration.SkillLibrary.Brawling;
            skills[3] = SkillLibrary.Vehicle;
            skills[4] = CharacterGeneration.SkillLibrary.Recon;
            skills[5] = SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Cepheus.Resources.Table_Specialist;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Streetwise;
            skills[1] = CharacterGeneration.SkillLibrary.Forgery;
            skills[2] = CharacterGeneration.SkillLibrary.Bribery;
            skills[3] = CharacterGeneration.SkillLibrary.Demolitions;
            skills[4] = SkillLibrary.Security;
            skills[5] = SkillLibrary.BladeCombat;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Cepheus.Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Tactics;
            skills[1] = CharacterGeneration.SkillLibrary.Bribery;
            skills[2] = CharacterGeneration.SkillLibrary.Forgery;
            skills[3] = CharacterGeneration.SkillLibrary.Computer;
            skills[4] = CharacterGeneration.SkillLibrary.Leader;
            skills[5] = CharacterGeneration.SkillLibrary.JackOfTrades;
        }

        protected override void EnlistSkill()
        {
            Owner.AddSkill(CharacterGeneration.SkillLibrary.Streetwise);
        }

        protected override void RankSkill()
        {
            if (RankNumber == 1)
            {
                Owner.AddSkill(SkillLibrary.BladeCombat);
            }
        }

        protected override SurvivalResult ResolveMishap()
        {
            SurvivalResult survive = SurvivalResult.Survived;
            switch (dice.roll(1))
            {
                case 1:
                    Owner.Journal.Add(Resources.Mishap_Rogue1);
                    survive = SurvivalResult.Discharged;
                    break;
                case 2:
                    Owner.Journal.Add(Resources.Mishap_Rogue2);
                    survive = SurvivalResult.Discharged;
                    break;
                case 3:
                    Owner.Journal.Add(Resources.Mishap_Rogue3);
                    ResolveInjury(0);
                    break;
                case 4:
                    Owner.Journal.Add(Resources.Mishap_Rogue4);
                    survive = SurvivalResult.Discharged;
                    break;
                case 5:
                    Owner.Journal.Add(Resources.Mishap_Rogue5);
                    survive = SurvivalResult.Discharged;
                    break;
                case 6:
                    Owner.Journal.Add(Resources.Mishap_Rogue6);
                    ResolveInjury(0);
                    break;
            }
            return survive;
        }
    }
}
