using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Imperial.Citizen
{
    public class Doctor : Career
    {
        public Doctor()
        {
            Name = "Doctor";

            CurrentRank = 0;
            TermSkills = 3;

            enlistment = 8;
            enlistment1attr = "INT";
            enlistment1val = 8;
            enlistment2attr = "DEX";
            enlistment2val = 9;

            reenlist = 4;
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
            skills[5] = SkillLibrary.Soc;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = "Service Skills";
            skills = table.Skills;
            skills[0] = SkillLibrary.Dex;
            skills[1] = SkillLibrary.Electronics;
            skills[2] = SkillLibrary.Medic;
            skills[3] = SkillLibrary.Streetwise;
            skills[4] = SkillLibrary.Medic;
            skills[5] = SkillLibrary.BladeCombat;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = "Education";
            skills = table.Skills;
            skills[0] = SkillLibrary.Medic;
            skills[1] = SkillLibrary.Medic;
            skills[2] = SkillLibrary.Mechanical;
            skills[3] = SkillLibrary.Electronics;
            skills[4] = SkillLibrary.Computer;
            skills[5] = SkillLibrary.Admin;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = "Advanced Education";
            skills = table.Skills;
            skills[0] = SkillLibrary.Medic;
            skills[1] = SkillLibrary.Medic;
            skills[2] = SkillLibrary.Admin;
            skills[3] = SkillLibrary.Computer;
            skills[4] = SkillLibrary.Int;
            skills[5] = SkillLibrary.Edu;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.Instruments);
            Material.Add(BenefitLibrary.MidPsg);

            Cash[0] = 20000;
            Cash[1] = 20000;
            Cash[2] = 20000;
            Cash[3] = 30000;
            Cash[4] = 40000;
            Cash[5] = 60000;
            Cash[6] = 100000;

            Ranks[0] = "Doctor";
        }

        protected override void CommsionSkill()
        {
            // nothing to do
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
            Owner.AddSkill(SkillLibrary.Medic);
        }

        protected override void RankSkill()
        {
            // Nothing to do here
        }

        public override void EndTerm()
        {
            base.EndTerm();
            TermSkills = 2;
        }
    }
}
