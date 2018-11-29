using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Imperial.Citizen
{
    public class Belter : Career
    {
        public Belter()
        {
            Name = Resources.Career_Belter;
            RankNumber = 0;
            TermSkills = 3;

            enlistment = 5;
            enlistment1attr = "DEX";
            enlistment1val = 7;
            enlistment2attr = "INT";
            enlistment2val = 6;

            reenlist = 7;
            hasRanks = false;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Properties.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Dex;
            skills[2] = SkillLibrary.End;
            skills[3] = SkillLibrary.Gambling;
            skills[4] = SkillLibrary.Brawling;
            skills[5] = SkillLibrary.VaccSuit;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Properties.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.VaccSuit;
            skills[1] = SkillLibrary.VaccSuit;
            skills[2] = SkillLibrary.Prospecting;
            skills[3] = SkillLibrary.FowardObserver;
            skills[4] = SkillLibrary.Prospecting;
            skills[5] = SkillLibrary.ShipsBoat;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Properties.Resources.Table_Education;
            skills = table.Skills;
            skills[0] = SkillLibrary.ShipsBoat;
            skills[1] = SkillLibrary.Electronics;
            skills[2] = SkillLibrary.Prospecting;
            skills[3] = SkillLibrary.Mechanical;
            skills[4] = SkillLibrary.Prospecting;
            skills[5] = SkillLibrary.Instruction;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Properties.Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = SkillLibrary.Navigation;
            skills[1] = SkillLibrary.Medic;
            skills[2] = SkillLibrary.Pilot;
            skills[3] = SkillLibrary.Computer;
            skills[4] = SkillLibrary.Engineering;
            skills[5] = SkillLibrary.JackOfTrades;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.Travellers);
            Material.Add(BenefitLibrary.Seeker);

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

            var target = 9;

            target -= Term;

            if (dice.roll(2) >= target)
            {
                survive = SurvivalResult.Survived;
            }
            return survive;
        }

        protected override void CommsionSkill()
        {
            // Nothing to do
        }

        protected override int EnlistFactor()
        {
            var target = 0;

            if (Owner.CharacterSpecies == Character.Species.Aslan)
            {
                if (Owner.Sex.Equals(Properties.Resources.Sex_Male))
                {
                    target += 4;
                }
            }
            else if (Owner.CharacterSpecies == Character.Species.Virushi || Owner.CharacterSpecies == Character.Species.AelYael)
            {
                target--;
            }

            return target;
        }

        protected override void EnlistSkill()
        {
            Owner.AddSkill(SkillLibrary.VaccSuit);
        }

        protected override void RankSkill()
        {
            // nothing to do here
        }

        public override void EndTerm()
        {
            base.EndTerm();
            TermSkills = 2;
        }
    }
}
