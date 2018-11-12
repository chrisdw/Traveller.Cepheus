using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Imperial.Citizen
{
    public class Scientist : Career
    {
        public Scientist()
        {
            Name = "Scientist";

            RankNumber = 0;
            TermSkills = 2;

            enlistment = 6;
            enlistment1attr = "INT";
            enlistment1val = 9;
            enlistment2attr = "EDU";
            enlistment2val = 10;

            survival = 5;
            survival2attr = "EDU";
            survival2val = 9;

            reenlist = 5;
            hasRanks = false;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = "Personal Development";
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Dex;
            skills[2] = SkillLibrary.End;
            skills[3] = SkillLibrary.Int;
            skills[4] = SkillLibrary.Edu;
            skills[5] = SkillLibrary.Carousing;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = "Service Skills";
            skills = table.Skills;
            skills[0] = SkillLibrary.GunCombat;
            skills[1] = SkillLibrary.BladeCombat;
            skills[2] = SkillLibrary.Vehicle;
            skills[3] = SkillLibrary.JackOfTrades;
            skills[4] = SkillLibrary.Navigation;
            skills[5] = SkillLibrary.Survival;
            table = new SkillTable();

            SkillTables[2] = table;
            table.Name = "Education";
            skills = table.Skills;
            skills[0] = SkillLibrary.Mechanical;
            skills[1] = SkillLibrary.Electronics;
            skills[2] = SkillLibrary.Gravitics;
            skills[3] = SkillLibrary.Computer;
            skills[4] = SkillLibrary.Int;
            skills[5] = SkillLibrary.Edu;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = "Advanced Education";
            skills = table.Skills;
            skills[0] = SkillLibrary.Medic;
            skills[1] = SkillLibrary.Computer;
            skills[2] = SkillLibrary.Admin;
            skills[3] = SkillLibrary.Leader;
            skills[4] = SkillLibrary.Int;
            skills[5] = SkillLibrary.JackOfTrades;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.MidPsg);
            Material.Add(BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.Soc);
            Material.Add(BenefitLibrary.Gun);
            Material.Add(BenefitLibrary.LabShip);

            Cash[0] = 1000;
            Cash[1] = 2000;
            Cash[2] = 5000;
            Cash[3] = 10000;
            Cash[4] = 20000;
            Cash[5] = 30000;
            Cash[6] = 40000;

            Ranks[0] = "Scientist";
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
                if (Owner.Sex.Equals("Male"))
                {
                    target += 4;
                }
            }
            else if (Owner.CharacterSpecies == Character.Species.Virushi)
            {
                target--;
            }

            return target;
        }

        protected override void EnlistSkill()
        {
            Owner.AddSkill(SkillLibrary.Computer);
        }

        protected override void RankSkill()
        {
            // Nothing to do
        }

        public override void EndTerm()
        {
            base.EndTerm();
            TermSkills = 2;
        }
    }
}
