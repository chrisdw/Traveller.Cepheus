namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Hostile
{
    public class Ranger : Cepheus.Career
    {
        public Ranger()
        {
            Name = Resources.Career_Ranger;
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

            Ranks[0] = Resources.Rank_Ranger;
            Ranks[1] = Resources.Rank_AssistantTeamLeader;
            Ranks[2] = Resources.Rank_TeamLeader;
            Ranks[3] = Resources.Rank_DeputyChiefRanger;
            Ranks[4] = Resources.Rank_ChiefRanger;
            Ranks[5] = Resources.Rank_AreaCommander;
            Ranks[6] = Resources.Rank_DistrictCommander;

            Material.Add(BenefitLibrary.StandardTicket);
            Material.Add(BenefitLibrary.StandardTicket);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.Dex);
            Material.Add(BenefitLibrary.Str2);
            Material.Add(BenefitLibrary.EliteTicket);

            Hostile.Culture.InitCashBenefits(this);

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Cepheus.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Str;
            skills[1] = CharacterGeneration.SkillLibrary.Dex;
            skills[2] = CharacterGeneration.SkillLibrary.End;
            skills[3] = CharacterGeneration.SkillLibrary.Int;
            skills[4] = SkillLibrary.GunCombat;
            skills[5] = SkillLibrary.BladeCombat;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Cepheus.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.GunCombat;
            skills[1] = SkillLibrary.GunCombat;
            skills[2] = CharacterGeneration.SkillLibrary.Survival;
            skills[3] = CharacterGeneration.SkillLibrary.Recon;
            skills[4] = SkillLibrary.GroundVehicle;
            skills[5] = CharacterGeneration.SkillLibrary.Survival;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Cepheus.Resources.Table_Specialist;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Mechanical;
            skills[1] = CharacterGeneration.SkillLibrary.Electronics;
            skills[2] = CharacterGeneration.SkillLibrary.Communications;
            skills[3] = CharacterGeneration.SkillLibrary.Recon;
            skills[4] = SkillLibrary.GroundVehicle;
            skills[5] = CharacterGeneration.SkillLibrary.Survival;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Cepheus.Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Medic;
            skills[1] = CharacterGeneration.SkillLibrary.Computer;
            skills[2] = CharacterGeneration.SkillLibrary.JackOfTrades;
            skills[3] = CharacterGeneration.SkillLibrary.Leader;
            skills[4] = CharacterGeneration.SkillLibrary.Medic;
            skills[5] = CharacterGeneration.SkillLibrary.Mechanical;
        }
        protected override void EnlistSkill()
        {
            Owner.AddSkill(CharacterGeneration.SkillLibrary.Survival);
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
                    Owner.Journal.Add(Resources.Mishap_InjuredOnDuty);
                    ResolveInjury(0);
                    break;
                case 2:
                    Owner.Journal.Add(Resources.Mishap_RangerRivalGroup);
                    survive = SurvivalResult.Discharged;
                    break;
                case 3:
                    Owner.Journal.Add(Resources.Mishap_RangerDisaster);
                    survive = SurvivalResult.Discharged;
                    break;
                case 4:
                    Owner.Journal.Add(Resources.Mishap_RangerAttacked);
                    ResolveInjury(0);
                    break;
                case 5:
                    Owner.Journal.Add(Resources.Mishap_RangerLost);
                    survive = SurvivalResult.Discharged;
                    break;
                case 6:
                    Owner.Journal.Add(Resources.Mishap_RangerRescueFailed);
                    survive = SurvivalResult.Discharged;
                    break;
            }
            return survive;
        }
    }
}
