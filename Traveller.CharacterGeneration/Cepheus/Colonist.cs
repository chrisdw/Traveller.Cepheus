using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus
{
    public class Colonist : Career
    {
        public Colonist()
        {
            Name = "Colonist";
            hasRanks = true;

            enlistment = 5;
            enlistmentattr = "END";
            survival = 6;
            survivalattr = "END";
            position = 7;
            positionattr = "INT";
            promotion = 6;
            promotionattr = "EDU";
            reenlist = 5;
            medicalBand = 3;

            Ranks[0] = "Citizen";
            Ranks[1] = "District Leader";
            Ranks[2] = "District Delegate";
            Ranks[3] = "Council Advisor";
            Ranks[4] = "Councilor";
            Ranks[5] = "Lieutenant Governor";
            Ranks[6] = "Governor";

            Material.Add(CharacterGeneration.BenefitLibrary.LowPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.Int);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(CharacterGeneration.BenefitLibrary.MidPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.MidPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.HighPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.Soc);

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
            skills[3] = CharacterGeneration.SkillLibrary.Int;
            skills[4] = SkillLibrary.Athletics;
            skills[5] = SkillLibrary.GunCombat;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Mechanical;
            skills[1] = SkillLibrary.GunCombat;
            skills[2] = SkillLibrary.Animals;
            skills[3] = SkillLibrary.MeleeCombat;
            skills[4] = CharacterGeneration.SkillLibrary.Survival;
            skills[5] = SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Resources.Table_Specialist;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Survival;
            skills[1] = CharacterGeneration.SkillLibrary.Electronics;
            skills[2] = CharacterGeneration.SkillLibrary.JackOfTrades;
            skills[3] = CharacterGeneration.SkillLibrary.Medic;
            skills[4] = SkillLibrary.Animals;
            skills[5] = SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = SkillLibrary.Advocate;
            skills[1] = SkillLibrary.Linguistics;
            skills[2] = CharacterGeneration.SkillLibrary.Medic;
            skills[3] = CharacterGeneration.SkillLibrary.Liason;
            skills[4] = CharacterGeneration.SkillLibrary.Admin;
            skills[5] = SkillLibrary.VetinaryMedicine;
        }

        protected override void CommsionSkill()
        {
            Owner.AddSkill(CharacterGeneration.SkillLibrary.Survival);
        }

        protected override void EnlistSkill()
        {

        }

        protected override void RankSkill()
        {
            if (RankNumber == 4)
            {
                Owner.AddSkill(CharacterGeneration.SkillLibrary.Liason);
            }
        }
    }
}
