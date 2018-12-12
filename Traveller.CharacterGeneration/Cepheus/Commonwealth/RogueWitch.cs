namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Commonwealth
{
    public class RogueWitch : Cepheus.Career
    {
        public RogueWitch()
        {
            Name = Resources.Career_RogueWitch;
            hasRanks = false;
            psionicTrained = true;

            enlistment = 5;
            enlistmentattr = "PSI";
            survival = 4;
            survivalattr = "PSI";
            reenlist = 4;
            medicalBand = 3;

            Ranks[0] = string.Empty;

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
            table.Name = Cepheus.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Str;
            skills[1] = CharacterGeneration.SkillLibrary.Dex;
            skills[2] = CharacterGeneration.SkillLibrary.End;
            skills[3] = Cepheus.SkillLibrary.MeleeCombat;
            skills[4] = CharacterGeneration.SkillLibrary.Bribery;
            skills[5] = CharacterGeneration.SkillLibrary.Psi;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Cepheus.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Streetwise;
            skills[1] = SkillLibrary.Psionics;
            skills[2] = Cepheus.SkillLibrary.GunCombat;
            skills[3] = Cepheus.SkillLibrary.MeleeCombat;
            skills[4] = CharacterGeneration.SkillLibrary.Recon;
            skills[5] = Cepheus.SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Cepheus.Resources.Table_Specialist;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Computer;
            skills[1] = CharacterGeneration.SkillLibrary.Electronics;
            skills[2] = CharacterGeneration.SkillLibrary.Brawling;
            skills[3] = SkillLibrary.Psionics;
            skills[4] = CharacterGeneration.SkillLibrary.Recon;
            skills[5] = Cepheus.SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Cepheus.Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Computer;
            skills[1] = CharacterGeneration.SkillLibrary.Gravitics;
            skills[2] = CharacterGeneration.SkillLibrary.JackOfTrades;
            skills[3] = CharacterGeneration.SkillLibrary.Medic;
            skills[4] = SkillLibrary.Psionics;
            skills[5] = CharacterGeneration.SkillLibrary.Tactics;
        }
        protected override void EnlistSkill()
        {
           
        }

        protected override void RankSkill()
        {

        }
    }
}
