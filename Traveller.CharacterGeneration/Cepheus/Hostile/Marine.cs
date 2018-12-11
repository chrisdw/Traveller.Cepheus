using org.DownesWard.Utilities;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Hostile
{
    public class Marine : Cepheus.Career
    {
        public string[] NCORanks { get; } = new string[7];

        public Marine()
        {
            Name = Resources.Career_Marine;
            hasRanks = true;

            enlistment = 4;
            enlistmentattr = "INT";
            survival = 6;
            survivalattr = "END";
            position = 9;
            positionattr = "EDU";
            promotion = 6;
            promotionattr = "EDU";
            reenlist = 6;
            medicalBand = 1;

            Ranks[0] = Resources.Rank_Private;
            Ranks[1] = Resources.Rank_Lieutenant;
            Ranks[2] = Resources.Rank_Captain;
            Ranks[3] = Resources.Rank_Major;
            Ranks[4] = Resources.Rank_LtColonel;
            Ranks[5] = Resources.Rank_Colonel;
            Ranks[6] = Resources.Rank_General;

            NCORanks[0] = Resources.Rank_Private;
            NCORanks[1] = Resources.Rank_LanceCorporal;
            NCORanks[2] = Resources.Rank_Corporal;
            NCORanks[3] = Resources.Rank_Sergeant;
            NCORanks[4] = Resources.Rank_StaffSergeant;
            NCORanks[5] = Resources.Rank_GunnerySergeant;
            NCORanks[6] = Resources.Rank_MasterSergeant;

            Material.Add(BenefitLibrary.StandardTicket);
            Material.Add(BenefitLibrary.SilverStar);
            Material.Add(CharacterGeneration.BenefitLibrary.Edu);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.StarEnvoyClubMember);
            Material.Add(BenefitLibrary.EliteTicket);
            Material.Add(CharacterGeneration.BenefitLibrary.Edu2);

            Hostile.Culture.InitCashBenefits(this);

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Cepheus.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Str;
            skills[1] = CharacterGeneration.SkillLibrary.Dex;
            skills[2] = CharacterGeneration.SkillLibrary.End;
            skills[3] = CharacterGeneration.SkillLibrary.Gambling;
            skills[4] = CharacterGeneration.SkillLibrary.Brawling;
            skills[5] = SkillLibrary.BladeCombat;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Cepheus.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.Vehicle;
            skills[1] = CharacterGeneration.SkillLibrary.VaccSuit;
            skills[2] = SkillLibrary.GunCombat;
            skills[3] = CharacterGeneration.SkillLibrary.Survival;
            skills[4] = SkillLibrary.GunCombat;
            skills[5] = CharacterGeneration.SkillLibrary.VaccSuit;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Cepheus.Resources.Table_Specialist;
            skills = table.Skills;
            skills[0] = SkillLibrary.Vehicle;
            skills[1] = CharacterGeneration.SkillLibrary.Mechanical;
            skills[2] = CharacterGeneration.SkillLibrary.Electronics;
            skills[3] = CharacterGeneration.SkillLibrary.Demolitions;
            skills[4] = CharacterGeneration.SkillLibrary.Recon;
            skills[5] = Cepheus.SkillLibrary.HeavyWeapons;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Cepheus.Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Medic;
            skills[1] = CharacterGeneration.SkillLibrary.Tactics;
            skills[2] = CharacterGeneration.SkillLibrary.Tactics;
            skills[3] = CharacterGeneration.SkillLibrary.Computer;
            skills[4] = CharacterGeneration.SkillLibrary.Leader;
            skills[5] = CharacterGeneration.SkillLibrary.Admin;
        }

        protected override void EnlistSkill()
        {
            Owner.AddSkill(SkillLibrary.GunCombat);
        }

        protected override void RankSkill()
        {
            
        }

        public override string RankName
        {
            get
            {
                if (RankNumber == 0 && TermsServed > 0)
                {
                    var ncoRank = TermsServed.Clamp(0, NCORanks.Length - 1);
                    return NCORanks[ncoRank];
                }
                return Ranks[RankNumber];
            }
        }

        protected override SurvivalResult ResolveMishap()
        {
            SurvivalResult survive = SurvivalResult.Survived;
            switch (dice.roll(1))
            {
                case 1:
                    Owner.Journal.Add(Resources.Mishap_Marine1);
                    ResolveInjury(0);
                    survive = SurvivalResult.Discharged;
                    break;
                case 2:
                    Owner.Journal.Add(Resources.Mishap_Marine2);
                    survive = SurvivalResult.Discharged;
                    break;
                case 3:
                    Owner.Journal.Add(Resources.Mishap_InjuredOnDuty);
                    ResolveInjury(0);
                    break;
                case 4:
                    Owner.Journal.Add(Resources.Mishap_InjuredOnDuty);
                    var roll = System.Math.Min(dice.roll(), dice.roll());
                    ResolveInjury(roll);
                    survive = SurvivalResult.Discharged;
                    break;
                case 5:
                    Owner.Journal.Add(Resources.Mishap_Marine5);
                    survive = SurvivalResult.Discharged;
                    break;
                case 6:
                    Owner.Journal.Add(Resources.Mishap_Marine6);
                    ResolveInjury(0);
                    break;
            }
            return survive;
        }
    }
}
