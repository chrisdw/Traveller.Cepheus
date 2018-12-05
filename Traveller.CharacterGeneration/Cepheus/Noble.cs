namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus
{
    public class Noble : Career
    {
        public Noble()
        {
            Name = Resources.Career_Noble;
            hasRanks = true;

            enlistment = 8;
            enlistmentattr = "SOC";
            survival = 4;
            survivalattr = "SOC";
            position = 5;
            positionattr = "EDU";
            promotion = 8;
            promotionattr = "INT";
            reenlist = 6;
            medicalBand = 2;

            Ranks[0] = Resources.Rank_Courtier;
            Ranks[1] = Resources.Rank_Knight;
            Ranks[2] = Resources.Rank_Baron;
            Ranks[3] = Resources.Rank_Marquis;
            Ranks[4] = Resources.Rank_Count;
            Ranks[5] = Resources.Rank_Duke;
            Ranks[6] = Resources.Rank_Archduke;

            Material.Add(CharacterGeneration.BenefitLibrary.HighPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.Edu);
            Material.Add(CharacterGeneration.BenefitLibrary.Int);
            Material.Add(CharacterGeneration.BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.ExplorersSociety);
            Material.Add(CharacterGeneration.BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.ShipShares);

            Cash[0] = 2000;
            Cash[1] = 10000;
            Cash[2] = 20000;
            Cash[3] = 20000;
            Cash[4] = 50000;
            Cash[5] = 100000;
            Cash[6] = 100000;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Dex;
            skills[1] = CharacterGeneration.SkillLibrary.Int;
            skills[2] = CharacterGeneration.SkillLibrary.Edu;
            skills[3] = CharacterGeneration.SkillLibrary.Soc;
            skills[4] = CharacterGeneration.SkillLibrary.Carousing;
            skills[5] = SkillLibrary.MeleeCombat;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.Athletics;
            skills[1] = CharacterGeneration.SkillLibrary.Admin;
            skills[2] = CharacterGeneration.SkillLibrary.Carousing;
            skills[3] = CharacterGeneration.SkillLibrary.Leader;
            skills[4] = CharacterGeneration.SkillLibrary.Gambling;
            skills[5] = SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Resources.Table_Specialist;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Computer;
            skills[1] = CharacterGeneration.SkillLibrary.Carousing;
            skills[2] = SkillLibrary.GunCombat;
            skills[3] = SkillLibrary.MeleeCombat;
            skills[4] = CharacterGeneration.SkillLibrary.Liason;
            skills[5] = SkillLibrary.Animals;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = SkillLibrary.Advocate;
            skills[1] = CharacterGeneration.SkillLibrary.Computer;
            skills[2] = CharacterGeneration.SkillLibrary.Liason;
            skills[3] = SkillLibrary.Linguistics;
            skills[4] = CharacterGeneration.SkillLibrary.Medic;
            skills[5] = SkillLibrary.Sciences;
        }

        protected override void EnlistSkill()
        {
            Owner.AddSkill(CharacterGeneration.SkillLibrary.Carousing);
        }

        protected override void RankSkill()
        {
            if (RankNumber == 4)
            {
                Owner.AddSkill(SkillLibrary.Advocate);
            }
        }
    }
}
