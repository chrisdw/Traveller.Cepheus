using org.DownesWard.Utilities;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Hostile
{
    public class MilitarySpacer : Cepheus.Career
    {
        public string[] NCORanks { get; } = new string[7];

        public MilitarySpacer()
        {
            Name = Resources.Career_MilitarySpacer;
            hasRanks = true;

            enlistment = 6;
            enlistmentattr = "INT";
            survival = 5;
            survivalattr = "INT";
            position = 7;
            positionattr = "SOC";
            promotion = 6;
            promotionattr = "EDU";
            reenlist = 5;
            medicalBand = 1;

            Ranks[0] = Resources.Rank_Airman;
            Ranks[1] = Resources.Rank_Lieutenant;
            Ranks[2] = Resources.Rank_Captain;
            Ranks[3] = Resources.Rank_Major;
            Ranks[4] = Resources.Rank_LtColonel;
            Ranks[5] = Resources.Rank_Colonel;
            Ranks[6] = Resources.Rank_General;

            NCORanks[0] = Resources.Rank_Airman;
            NCORanks[1] = Resources.Rank_AirmanFirstClass;
            NCORanks[2] = Resources.Rank_SeniorAirman;
            NCORanks[3] = Resources.Rank_Sergeant;
            NCORanks[4] = Resources.Rank_StaffSergeant;
            NCORanks[5] = Resources.Rank_TechnicalSergeant;
            NCORanks[6] = Resources.Rank_MasterSergeant;

            Material.Add(BenefitLibrary.DistinguishedFlyingCross);
            Material.Add(CharacterGeneration.BenefitLibrary.Edu);
            Material.Add(CharacterGeneration.BenefitLibrary.Edu2);
            Material.Add(BenefitLibrary.EliteTicket);
            Material.Add(BenefitLibrary.StandardTicket);
            Material.Add(BenefitLibrary.EliteTicket);
            Material.Add(BenefitLibrary.StarEnvoyClubMember);

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
            skills[3] = CharacterGeneration.SkillLibrary.Int;
            skills[4] = CharacterGeneration.SkillLibrary.Edu;
            skills[5] = CharacterGeneration.SkillLibrary.Soc;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Cepheus.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.VaccSuit;
            skills[1] = CharacterGeneration.SkillLibrary.Computer;
            skills[2] = CharacterGeneration.SkillLibrary.VaccSuit;
            skills[3] = Cepheus.SkillLibrary.Gunnery;
            skills[4] = CharacterGeneration.SkillLibrary.Brawling;
            skills[5] = SkillLibrary.GunCombat;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Cepheus.Resources.Table_Specialist;
            skills = table.Skills;
            skills[0] = Cepheus.SkillLibrary.Gunnery;
            skills[1] = CharacterGeneration.SkillLibrary.Mechanical;
            skills[2] = CharacterGeneration.SkillLibrary.Electronics;
            skills[3] = CharacterGeneration.SkillLibrary.Engineering;
            skills[4] = Cepheus.SkillLibrary.Gunnery;
            skills[5] = CharacterGeneration.SkillLibrary.Communications;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Cepheus.Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Medic;
            skills[1] = CharacterGeneration.SkillLibrary.Navigation;
            skills[2] = CharacterGeneration.SkillLibrary.Engineering;
            skills[3] = CharacterGeneration.SkillLibrary.Computer;
            skills[4] = CharacterGeneration.SkillLibrary.Pilot;
            skills[5] = CharacterGeneration.SkillLibrary.Admin;

        }
        protected override void EnlistSkill()
        {
            Owner.AddSkill(CharacterGeneration.SkillLibrary.VaccSuit);
        }

        protected override void RankSkill()
        {
            if (RankNumber == 5)
            {
                Owner.AddSkill(CharacterGeneration.SkillLibrary.Soc);
            }
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
                    Owner.Journal.Add(Resources.Mishap_MilSpace1);
                    survive = SurvivalResult.Discharged;
                    break;
                case 2:
                    Owner.Journal.Add(Resources.Mishap_MilSpace2);
                    survive = SurvivalResult.Discharged;
                    break;
                case 3:
                    Owner.Journal.Add(Resources.Mishap_MilSpace3);
                    survive = SurvivalResult.Discharged;
                    break;
                case 4:
                    Owner.Journal.Add(Resources.Mishap_InjuredOnDuty);
                    ResolveInjury(0);
                    break;
                case 5:
                    Owner.Journal.Add(Resources.Mishap_MilSpace5);
                    survive = SurvivalResult.Discharged;
                    break;
                case 6:
                    Owner.Journal.Add(Resources.Mishap_MilSpace6);
                    ResolveInjury(0);
                    break;
            }
            return survive;
        }
    }
}
