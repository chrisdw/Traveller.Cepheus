namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus
{
    public class Merchant : Career
    {
        public Merchant()
        {
            Name = Resources.Career_Merchant;
            hasRanks = true;

            enlistment = 4;
            enlistmentattr = "INT";
            survival = 5;
            survivalattr = "INT";
            position = 5;
            positionattr = "INT";
            promotion = 8;
            promotionattr = "EDU";
            reenlist = 4;
            medicalBand = 2;

            Ranks[0] = Resources.Rank_Crewman;
            Ranks[1] = Resources.Rank_DeckCadet;
            Ranks[2] = Resources.Rank_4thOfficer;
            Ranks[3] = Resources.Rank_3rdOfficer;
            Ranks[4] = Resources.Rank_2ndOfficer;
            Ranks[5] = Resources.Rank_1stOfficer;
            Ranks[6] = Resources.Rank_Captain;

            Material.Add(CharacterGeneration.BenefitLibrary.LowPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.Edu);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(CharacterGeneration.BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.ShipShares);
            Material.Add(CharacterGeneration.BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.ExplorersSociety);

            Cash[0] = 1000;
            Cash[1] = 5000;
            Cash[2] = 10000;
            Cash[3] = 20000;
            Cash[4] = 20000;
            Cash[5] = 50000;
            Cash[6] = 100000;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Str;
            skills[1] = CharacterGeneration.SkillLibrary.Dex;
            skills[2] = CharacterGeneration.SkillLibrary.End;
            skills[3] = CharacterGeneration.SkillLibrary.ZeroGCombat;
            skills[4] = SkillLibrary.MeleeCombat;
            skills[5] = CharacterGeneration.SkillLibrary.Steward;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Communications;
            skills[1] = CharacterGeneration.SkillLibrary.Engineering;
            skills[2] = SkillLibrary.GunCombat;
            skills[3] = SkillLibrary.MeleeCombat;
            skills[4] = CharacterGeneration.SkillLibrary.Broker;
            skills[5] = SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Resources.Table_Specialist;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Gravitics;
            skills[1] = SkillLibrary.GunCombat;
            skills[2] = SkillLibrary.Gunnery;
            skills[3] = SkillLibrary.MeleeCombat;
            skills[4] = CharacterGeneration.SkillLibrary.Recon;
            skills[5] = SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = SkillLibrary.Advocate;
            skills[1] = CharacterGeneration.SkillLibrary.Engineering;
            skills[2] = CharacterGeneration.SkillLibrary.Medic;
            skills[3] = CharacterGeneration.SkillLibrary.Navigation;
            skills[4] = SkillLibrary.Sciences;
            skills[5] = CharacterGeneration.SkillLibrary.Tactics;
        }

        protected override void EnlistSkill()
        {
            Owner.AddSkill(CharacterGeneration.SkillLibrary.Steward);
        }

        protected override void RankSkill()
        {
            if (RankNumber == 3)
            {
                Owner.AddSkill(CharacterGeneration.SkillLibrary.Pilot);
            }
        }
    }
}
