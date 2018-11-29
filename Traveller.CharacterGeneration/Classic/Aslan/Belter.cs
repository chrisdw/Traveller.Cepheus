using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Aslan
{
    public class Belter : Career
    {
        protected override void CommsionSkill()
        {
            Name = Resources.Career_Belter;
            hasRanks = false;

            RankNumber = 0;
            TermSkills = 3;

            survival = 9;
            survival2attr = string.Empty;
            survival3attr = "EDU";
            survival3val = 8;

            reenlist = 6;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.End;
            skills[1] = CharacterGeneration.SkillLibrary.Str;
            skills[2] = CharacterGeneration.SkillLibrary.Dex;
            skills[3] = CharacterGeneration.SkillLibrary.End;
            skills[4] = CharacterGeneration.SkillLibrary.Int;
            skills[5] = CharacterGeneration.SkillLibrary.Edu;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Prospecting;
            skills[1] = CharacterGeneration.SkillLibrary.Broker;
            skills[2] = CharacterGeneration.SkillLibrary.VaccSuit;
            skills[3] = CharacterGeneration.SkillLibrary.Engineering;
            skills[4] = CharacterGeneration.SkillLibrary.Navigation;
            skills[5] = CharacterGeneration.SkillLibrary.Computer;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Resources.Table_AdvancedServiceSkills;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Prospecting;
            skills[1] = CharacterGeneration.SkillLibrary.Prospecting;
            skills[2] = CharacterGeneration.SkillLibrary.Prospecting;
            skills[3] = CharacterGeneration.SkillLibrary.VaccSuit;
            skills[4] = CharacterGeneration.SkillLibrary.Broker;
            skills[5] = CharacterGeneration.SkillLibrary.Navigation;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Resources.Table_Experience;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Computer;
            skills[1] = CharacterGeneration.SkillLibrary.ShipsBoat;
            skills[2] = SkillLibrary.GunCombat;
            skills[3] = CharacterGeneration.SkillLibrary.Bribery;
            skills[4] = CharacterGeneration.SkillLibrary.ShipsBoat;
            skills[5] = CharacterGeneration.SkillLibrary.Engineering;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.MidPsg);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.Seeker);
            Material.Add(BenefitLibrary.Corporation);

            Cash[0] = 0;
            Cash[1] = 0;
            Cash[2] = 1000;
            Cash[3] = 10000;
            Cash[4] = 100000;
            Cash[5] = 100000;
            Cash[6] = 100000;

            Ranks[0] = Resources.Rank_Belter;
        }

        /// <summary>
        /// Need to override as survival based on terms served not
        /// any attribute
        /// </summary>
        /// <returns></returns>
        public override SurvivalResult Survival()
        {
            var survive = SurvivalResult.Died;

            var target = survival;

            target -= Term;

            if (dice.roll(2) >= target)
            {
                survive = SurvivalResult.Survived;
            }
            return survive;
        }

        protected override void EnlistSkill()
        {
            Owner.AddSkill(CharacterGeneration.SkillLibrary.VaccSuit);
        }

        protected override void RankSkill()
        {
            
        }

        public override void EndTerm()
        {
            base.EndTerm();
            TermSkills = 3;
        }

        protected override int EnlistFactor()
        {
            return 0;
        }

        public override void CheckTableAvailablity()
        {

        }
    }
}
