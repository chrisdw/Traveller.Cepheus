using org.DownesWard.Utilities;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Hostile
{
    public class Marine : Cepheus.Career
    {
        public string[] NCORanks { get; } = new string[7];

        public Marine()
        {
            Name = "Marine";
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

            Ranks[0] = "Private";
            Ranks[1] = "Lieutenant";
            Ranks[2] = "Captain";
            Ranks[3] = "Major";
            Ranks[4] = "Lt. Colonel";
            Ranks[5] = "Colonel";
            Ranks[6] = "General";

            NCORanks[0] = "Private";
            NCORanks[1] = "Lance Corporal";
            NCORanks[2] = "Corporal";
            NCORanks[3] = "Sergeant";
            NCORanks[4] = "Staff Sergeant";
            NCORanks[5] = "Gunnery Sergeant";
            NCORanks[6] = "Master Sergeant";

            Material.Add(BenefitLibrary.StandardTicket);
            Material.Add(BenefitLibrary.SilverStar);
            Material.Add(CharacterGeneration.BenefitLibrary.Edu);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.StarEnvoyClubMember);
            Material.Add(BenefitLibrary.EliteTicket);
            Material.Add(CharacterGeneration.BenefitLibrary.Edu2);

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
                    Owner.Journal.Add("During a mission you are captured and injured, when released you are honourably discharged.");
                    ResolveInjury(0);
                    survive = SurvivalResult.Discharged;
                    break;
                case 2:
                    Owner.Journal.Add("You argue with a senior officer who drives you out of the Marines.");
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
                    Owner.Journal.Add("You were involved in an illegal operation, your conscience forces you to resign.");
                    survive = SurvivalResult.Discharged;
                    break;
                case 6:
                    Owner.Journal.Add("Fighting hostile alien exomorphs, you save colonists, but are injured.");
                    ResolveInjury(0);
                    break;
            }
            return survive;
        }
    }
}
