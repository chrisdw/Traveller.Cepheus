namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus
{
    public class Barbarian : Career
    {
        public Barbarian()
        {
            Name = Resources.Career_Barbarian;
            hasRanks = false;

            enlistment = 5;
            enlistmentattr = "END";
            survival = 6;
            survivalattr = "STR";
            reenlist = 5;
            medicalBand = 3;

            Ranks[0] = Resources.Rank_Barbarian;

            Material.Add(CharacterGeneration.BenefitLibrary.LowPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.Int);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.End);
            Material.Add(CharacterGeneration.BenefitLibrary.MidPsg);

            Cash[0] = 0;
            Cash[1] = 1000;
            Cash[2] = 2000;
            Cash[3] = 5000;
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
            skills[3] = CharacterGeneration.SkillLibrary.Int;
            skills[4] = SkillLibrary.Athletics;
            skills[5] = SkillLibrary.GunCombat;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Mechanical;
            skills[1] = SkillLibrary.GunCombat;
            skills[2] = SkillLibrary.MeleeCombat;
            skills[3] = CharacterGeneration.SkillLibrary.Recon;
            skills[4] = CharacterGeneration.SkillLibrary.Survival;
            skills[5] = SkillLibrary.Animals;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Resources.Table_Specialist;
            skills = table.Skills;
            skills[0] = SkillLibrary.GunCombat;
            skills[1] = CharacterGeneration.SkillLibrary.JackOfTrades;
            skills[2] = SkillLibrary.MeleeCombat;
            skills[3] = CharacterGeneration.SkillLibrary.Recon;
            skills[4] = SkillLibrary.Animals;
            skills[5] = CharacterGeneration.SkillLibrary.Tactics;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = SkillLibrary.Advocate;
            skills[1] = SkillLibrary.Linguistics;
            skills[2] = CharacterGeneration.SkillLibrary.Medic;
            skills[3] = CharacterGeneration.SkillLibrary.Leader;
            skills[4] = CharacterGeneration.SkillLibrary.Tactics;
            skills[5] = CharacterGeneration.SkillLibrary.Broker;
        }

        protected override void EnlistSkill()
        {
            OnSkillOffered(SkillLibrary.MeleeCombat);
        }

        protected override void RankSkill()
        {
            
        }
    }
}
