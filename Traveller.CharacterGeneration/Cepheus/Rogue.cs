namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus
{
    public class Rogue : Career
    {
        public Rogue()
        {
            Name = Resources.Career_Rogue;
            hasRanks = true;

            enlistment = 5;
            enlistmentattr = "DEX";
            survival = 4;
            survivalattr = "DEX";
            position = 6;
            positionattr = "STR";
            promotion = 7;
            promotionattr = "INT";
            reenlist = 4;
            medicalBand = 3;

            Ranks[0] = Resources.Rank_Independant;
            Ranks[1] = Resources.Rank_Associate;
            Ranks[2] = Resources.Rank_Soldier;
            Ranks[3] = Resources.Rank_Lieutenant;
            Ranks[4] = Resources.Rank_Underboss;
            Ranks[5] = Resources.Rank_Consigliere;
            Ranks[6] = Resources.Rank_Boss;

            Material.Add(CharacterGeneration.BenefitLibrary.LowPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.Int);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(CharacterGeneration.BenefitLibrary.HighPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.Soc);
            Material.Add(CharacterGeneration.BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.ShipShares);

            Cash[0] = 1000;
            Cash[1] = 5000;
            Cash[2] = 5000;
            Cash[3] = 5000;
            Cash[4] = 10000;
            Cash[5] = 20000;
            Cash[6] = 50000;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Str;
            skills[1] = CharacterGeneration.SkillLibrary.Dex;
            skills[2] = CharacterGeneration.SkillLibrary.End;
            skills[3] = SkillLibrary.MeleeCombat;
            skills[4] = CharacterGeneration.SkillLibrary.Bribery;
            skills[5] = CharacterGeneration.SkillLibrary.Gambling;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Streetwise;
            skills[1] = CharacterGeneration.SkillLibrary.Mechanical;
            skills[2] = SkillLibrary.GunCombat;
            skills[3] = SkillLibrary.MeleeCombat;
            skills[4] = CharacterGeneration.SkillLibrary.Recon;
            skills[5] = SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Resources.Table_Specialist;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Computer;
            skills[1] = CharacterGeneration.SkillLibrary.Electronics;
            skills[2] = CharacterGeneration.SkillLibrary.Brawling;
            skills[3] = CharacterGeneration.SkillLibrary.Broker;
            skills[4] = CharacterGeneration.SkillLibrary.Recon;
            skills[5] = SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Computer;
            skills[1] = CharacterGeneration.SkillLibrary.Gravitics;
            skills[2] = CharacterGeneration.SkillLibrary.JackOfTrades;
            skills[3] = CharacterGeneration.SkillLibrary.Medic;
            skills[4] = SkillLibrary.Advocate;
            skills[5] = CharacterGeneration.SkillLibrary.Tactics;
        }

        protected override void EnlistSkill()
        {
            Owner.AddSkill(CharacterGeneration.SkillLibrary.Streetwise);
        }

        protected override void RankSkill()
        {
            if (RankNumber == 2)
            {
                OnSkillOffered(SkillLibrary.GunCombat);
            }
        }
    }
}
