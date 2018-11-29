using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Aslan
{
    public class Envoy : Career
    {
        protected override void CommsionSkill()
        {
            Name = Resources.Career_Envoy;
            hasRanks = false;

            enlistment = 11;

            survival = 6;
            survival2attr = string.Empty;
            survival3attr = "INT";
            survival3val = 8;

            reenlist = 8;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Independance;
            skills[1] = CharacterGeneration.SkillLibrary.Str;
            skills[2] = CharacterGeneration.SkillLibrary.Dex;
            skills[3] = CharacterGeneration.SkillLibrary.End;
            skills[4] = CharacterGeneration.SkillLibrary.Independance;
            skills[5] = CharacterGeneration.SkillLibrary.DewClaw;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Int;
            skills[1] = CharacterGeneration.SkillLibrary.Liason;
            skills[2] = CharacterGeneration.SkillLibrary.Tolerance;
            skills[3] = CharacterGeneration.SkillLibrary.Carousing;
            skills[4] = CharacterGeneration.SkillLibrary.Hunting;
            skills[5] = CharacterGeneration.SkillLibrary.Leader;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Resources.Table_AdvancedServiceSkills;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Liason;
            skills[1] = CharacterGeneration.SkillLibrary.Liason;
            skills[2] = CharacterGeneration.SkillLibrary.Tolerance;
            skills[3] = CharacterGeneration.SkillLibrary.Tolerance;
            skills[4] = SkillLibrary.GunCombat;
            skills[5] = CharacterGeneration.SkillLibrary.JackOfTrades;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Resources.Table_Experience;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Vehicle;
            skills[1] = CharacterGeneration.SkillLibrary.Hunting;
            skills[2] = SkillLibrary.GunCombat;
            skills[3] = SkillLibrary.PersonalWeapons;
            skills[4] = CharacterGeneration.SkillLibrary.Leader;
            skills[5] = CharacterGeneration.SkillLibrary.Soc;

            Material.Add(BenefitLibrary.Nothing);
            Material.Add(BenefitLibrary.Nothing);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Edu2);
            Material.Add(BenefitLibrary.Independance);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.Courier);
            Material.Add(BenefitLibrary.Land);

            Cash[0] = 0;
            Cash[1] = 0;
            Cash[2] = 0;
            Cash[3] = 5000;
            Cash[4] = 5000;
            Cash[5] = 10000;
            Cash[6] = 20000;

            Ranks[0] = Resources.Rank_Envoy;
        }

        /// <summary>
        /// Need to override as survival based on tolerance skill not
        /// any attribute
        /// </summary>
        /// <returns></returns>
        public override SurvivalResult Survival()
        {
            var survive = SurvivalResult.Died;

            var target = survival;

            if (Owner.Skills.ContainsKey(CharacterGeneration.SkillLibrary.Tolerance.Name))
            {
                target -= Owner.Skills[CharacterGeneration.SkillLibrary.Tolerance.Name].Level;
            }

            if (dice.roll(2) >= target)
            {
                survive = SurvivalResult.Survived;
            }
            return survive;
        }

        protected override void EnlistSkill()
        {
            Owner.AddSkill(CharacterGeneration.SkillLibrary.Tolerance);
        }

        protected override void RankSkill()
        {
            
        }

        public override void CheckTableAvailablity()
        {

        }
    }
}
