using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus
{
    public class Scout : Career
    {
        public Scout()
        {
            Name = "Scout";
            hasRanks = false;

            enlistment = 6;
            enlistmentattr = "INT";
            survival = 7;
            survivalattr = "END";
            reenlist = 6;
            medicalBand = 1;

            Material.Add(CharacterGeneration.BenefitLibrary.LowPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.Edu);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(CharacterGeneration.BenefitLibrary.MidPsg);
            Material.Add(BenefitLibrary.ExplorersSociety);
            Material.Add(BenefitLibrary.CourierVessel);

            Cash[0] = 1000;
            Cash[1] = 5000;
            Cash[2] = 10000;
            Cash[3] = 10000;
            Cash[4] = 20000;
            Cash[5] = 50000;
            Cash[6] = 50000;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Str;
            skills[1] = CharacterGeneration.SkillLibrary.Dex;
            skills[2] = CharacterGeneration.SkillLibrary.End;
            skills[3] = CharacterGeneration.SkillLibrary.JackOfTrades;
            skills[4] = CharacterGeneration.SkillLibrary.Edu;
            skills[5] = SkillLibrary.MeleeCombat;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Communications;
            skills[1] = CharacterGeneration.SkillLibrary.Electronics;
            skills[2] = SkillLibrary.GunCombat;
            skills[3] = SkillLibrary.Gunnery;
            skills[4] = CharacterGeneration.SkillLibrary.Recon;
            skills[5] = CharacterGeneration.SkillLibrary.Pilot;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Resources.Table_Education;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Engineering;
            skills[1] = SkillLibrary.Gunnery;
            skills[2] = CharacterGeneration.SkillLibrary.Demolitions;
            skills[3] = CharacterGeneration.SkillLibrary.Navigation;
            skills[4] = CharacterGeneration.SkillLibrary.Medic;
            skills[5] = SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = SkillLibrary.Advocate;
            skills[1] = CharacterGeneration.SkillLibrary.Computer;
            skills[2] = SkillLibrary.Linguistics;
            skills[3] = CharacterGeneration.SkillLibrary.Medic;
            skills[4] = CharacterGeneration.SkillLibrary.Navigation;
            skills[5] = CharacterGeneration.SkillLibrary.Tactics;

            Ranks[0] = "Scout";
        }
        protected override void CommsionSkill()
        {
            
        }

        protected override void EnlistSkill()
        {
            Owner.AddSkill(CharacterGeneration.SkillLibrary.Pilot);
        }

        protected override void RankSkill()
        {
            
        }
    }
}
