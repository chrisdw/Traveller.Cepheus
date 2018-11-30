namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus
{
    public class Athlete : Career
    {
        public Athlete()
        {
            Name = Resources.Career_Athlete;
            hasRanks = false;

            enlistment = 8;
            enlistmentattr = "END";
            survival = 5;
            survivalattr = "DEX";
            reenlist = 6;
            medicalBand = 2;

            Material.Add(CharacterGeneration.BenefitLibrary.LowPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.Int);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(CharacterGeneration.BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.ExplorersSociety);
            Material.Add(CharacterGeneration.BenefitLibrary.HighPsg);

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
            skills[2] = CharacterGeneration.SkillLibrary.End;
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
            skills[3] = CharacterGeneration.SkillLibrary.Computer;
            skills[4] = CharacterGeneration.SkillLibrary.Gambling;
            skills[5] = CharacterGeneration.SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Resources.Table_Specialist;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.ZeroGCombat;
            skills[1] = SkillLibrary.Athletics;
            skills[2] = SkillLibrary.Athletics;
            skills[3] = CharacterGeneration.SkillLibrary.Computer;
            skills[4] = CharacterGeneration.SkillLibrary.Leader;
            skills[5] = CharacterGeneration.SkillLibrary.Gambling;

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

            Ranks[0] = Resources.Rank_Athlete;
        }
        protected override void CommsionSkill()
        {
            
        }

        protected override void EnlistSkill()
        {
            Owner.AddSkill(SkillLibrary.Athletics);
        }

        protected override void RankSkill()
        {
            
        }
    }
}
