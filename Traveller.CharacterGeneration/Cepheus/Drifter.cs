namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus
{
    public class Drifter : Career
    {

        public Drifter()
        {
            Name = Resources.Career_Drifter;
            hasRanks = false;

            enlistment = 5;
            enlistmentattr = "DEX";
            survival = 5;
            survivalattr = "END";
            reenlist = 5;
            medicalBand = 3;

            Material.Add(CharacterGeneration.BenefitLibrary.LowPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.Int);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(CharacterGeneration.BenefitLibrary.MidPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.MidPsg);

            Cash[0] = 0;
            Cash[1] = 1000;
            Cash[2] = 2000;
            Cash[3] = 2000;
            Cash[4] = 5000;
            Cash[5] = 10000;
            Cash[6] = 10000;

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
            skills[0] = CharacterGeneration.SkillLibrary.Electronics;
            skills[1] = SkillLibrary.MeleeCombat;
            skills[2] = CharacterGeneration.SkillLibrary.Bribery;
            skills[3] = CharacterGeneration.SkillLibrary.Streetwise;
            skills[4] = CharacterGeneration.SkillLibrary.Gambling;
            skills[5] = CharacterGeneration.SkillLibrary.Recon;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Computer;
            skills[1] = CharacterGeneration.SkillLibrary.Engineering;
            skills[2] = CharacterGeneration.SkillLibrary.JackOfTrades;
            skills[3] = CharacterGeneration.SkillLibrary.Medic;
            skills[4] = CharacterGeneration.SkillLibrary.Liason;
            skills[5] = CharacterGeneration.SkillLibrary.Tactics;

            Ranks[0] = Resources.Rank_Drifter;
        }

        protected override void EnlistSkill()
        {
            
        }

        protected override void RankSkill()
        {
            
        }
    }
}
