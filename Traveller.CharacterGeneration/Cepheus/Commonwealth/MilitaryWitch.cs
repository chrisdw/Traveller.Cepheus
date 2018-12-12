using org.DownesWard.Utilities;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Commonwealth
{
    public class MilitaryWitch : Cepheus.Career
    {
        public string[] NCORanks { get; } = new string[7];

        public MilitaryWitch()
        {
            Name = Resources.Career_MilitaryWitch;
            hasRanks = true;
            psionicTrained = true;

            enlistment = 5;
            enlistmentattr = "PSI";
            survival = 5;
            survivalattr = "END";
            position = 6;
            positionattr = "PSI";
            promotion = 7;
            promotionattr = "EDU";
            reenlist = 5;
            medicalBand = 1;

            Ranks[0] = Resources.Rank_Private;
            Ranks[1] = Cepheus.Resources.Rank_Lieutenant;
            Ranks[2] = Cepheus.Resources.Rank_Captain;
            Ranks[3] = Cepheus.Resources.Rank_Major;
            Ranks[4] = Cepheus.Resources.Rank_LtColonel;
            Ranks[5] = Cepheus.Resources.Rank_Colonel;
            Ranks[6] = Cepheus.Resources.Rank_General;

            Material.Add(CharacterGeneration.BenefitLibrary.LowPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.Int);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(CharacterGeneration.BenefitLibrary.MidPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(CharacterGeneration.BenefitLibrary.HighPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.Soc);

            Cash[0] = 1000;
            Cash[1] = 5000;
            Cash[2] = 10000;
            Cash[3] = 10000;
            Cash[4] = 20000;
            Cash[5] = 50000;
            Cash[6] = 50000;

            NCORanks[0] = Resources.Rank_Private;
            NCORanks[1] = Resources.Rank_LanceCorporal;
            NCORanks[2] = Resources.Rank_Corporal;
            NCORanks[3] = Resources.Rank_Sergeant;
            NCORanks[4] = Resources.Rank_StaffSergeant;
            NCORanks[5] = Resources.Rank_CompanySergeantMajor;
            NCORanks[6] = Resources.Rank_RegimentalSergeantMajor;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Cepheus.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Str;
            skills[1] = CharacterGeneration.SkillLibrary.Dex;
            skills[2] = CharacterGeneration.SkillLibrary.End;
            skills[3] = CharacterGeneration.SkillLibrary.Psi;
            skills[4] = Cepheus.SkillLibrary.MeleeCombat;
            skills[5] = Cepheus.SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Cepheus.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.Psionics;
            skills[1] = Cepheus.SkillLibrary.GunCombat;
            skills[2] = Cepheus.SkillLibrary.Awareness;
            skills[3] = Cepheus.SkillLibrary.MeleeCombat;
            skills[4] = CharacterGeneration.SkillLibrary.Recon;
            skills[5] = CharacterGeneration.SkillLibrary.BattleDress;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Cepheus.Resources.Table_Specialist;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Communications;
            skills[1] = SkillLibrary.Psionics;
            skills[2] = Cepheus.SkillLibrary.GunCombat;
            skills[3] = Cepheus.SkillLibrary.MeleeCombat;
            skills[4] = CharacterGeneration.SkillLibrary.Survival;
            skills[5] = CharacterGeneration.SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Cepheus.Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = SkillLibrary.Psionics; 
            skills[1] = CharacterGeneration.SkillLibrary.Computer;
            skills[2] = CharacterGeneration.SkillLibrary.JackOfTrades;
            skills[3] = CharacterGeneration.SkillLibrary.Medic;
            skills[4] = CharacterGeneration.SkillLibrary.Leader;
            skills[5] = CharacterGeneration.SkillLibrary.Tactics;
        }
        protected override void EnlistSkill()
        {
            Owner.AddSkill(Cepheus.SkillLibrary.Awareness);
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
    }
}
