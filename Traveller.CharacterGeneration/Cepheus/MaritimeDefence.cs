using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus
{
    public class MaritimeDefence : Career
    {
        public MaritimeDefence()
        {
            Name = "Maritime Defence";
            hasRanks = true;

            enlistment = 5;
            enlistmentattr = "END";
            survival = 5;
            survivalattr = "END";
            position = 6;
            positionattr = "INT";
            promotion = 7;
            promotionattr = "EDU";
            reenlist = 5;
            medicalBand = 1;

            Material.Add(CharacterGeneration.BenefitLibrary.LowPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.Edu);
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

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Str;
            skills[1] = CharacterGeneration.SkillLibrary.Dex;
            skills[2] = CharacterGeneration.SkillLibrary.End;
            skills[3] = SkillLibrary.Athletics;
            skills[4] = SkillLibrary.MeleeCombat;
            skills[5] = SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Mechanical;
            skills[1] = SkillLibrary.GunCombat;
            skills[2] = SkillLibrary.Gunnery;
            skills[3] = SkillLibrary.MeleeCombat;
            skills[4] = CharacterGeneration.SkillLibrary.Survival;
            skills[5] = SkillLibrary.Watercraft;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Resources.Table_Education;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Communications;
            skills[1] = CharacterGeneration.SkillLibrary.Electronics;
            skills[2] = SkillLibrary.GunCombat;
            skills[3] = CharacterGeneration.SkillLibrary.Demolitions;
            skills[4] = CharacterGeneration.SkillLibrary.Recon;
            skills[5] = SkillLibrary.Watercraft;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = SkillLibrary.Advocate;
            skills[1] = CharacterGeneration.SkillLibrary.Computer;
            skills[2] = CharacterGeneration.SkillLibrary.JackOfTrades;
            skills[3] = CharacterGeneration.SkillLibrary.Medic;
            skills[4] = CharacterGeneration.SkillLibrary.Leader;
            skills[5] = CharacterGeneration.SkillLibrary.Tactics;

            Ranks[0] = "Seaman";
            Ranks[1] = "Ensign";
            Ranks[2] = "Lieutenant";
            Ranks[3] = "Lt. Commander";
            Ranks[4] = "Commander";
            Ranks[5] = "Captain";
            Ranks[6] = "Admiral";
        }
        protected override void CommsionSkill()
        {
            throw new NotImplementedException();
        }

        protected override void EnlistSkill()
        {
            OnSkillOffered(SkillLibrary.Watercraft);
        }

        protected override void RankSkill()
        {
            if (RankNumber == 3)
            {
                Owner.AddSkill(CharacterGeneration.SkillLibrary.Leader);
            }
        }
    }
}
