using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Imperial.Citizen
{
    public class Hunter : Career
    {
        public Hunter()
        {
            CurrentRank = 0;
            TermSkills = 2;

            enlistment = 9;
            enlistment1attr = "DEX";
            enlistment1val = 10;
            enlistment2attr = "END";
            enlistment2val = 9;
            survival = 6;
            survival2attr = "STR";
            survival2val = 10;

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
            skills[4] = SkillLibrary.GunCombat;
            skills[5] = SkillLibrary.BladeCombat;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = "Service Skills";
            skills = table.Skills;
            skills[0] = SkillLibrary.GunCombat;
            skills[1] = SkillLibrary.BladeCombat;
            skills[2] = SkillLibrary.Survival;
            skills[3] = SkillLibrary.Hunting;
            skills[4] = SkillLibrary.GroundVehicle;
            skills[5] = SkillLibrary.Hunting;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = "Education";
            skills = table.Skills;
            skills[0] = SkillLibrary.Mechanical;
            skills[1] = SkillLibrary.Electronics;
            skills[2] = SkillLibrary.Gravitics;
            skills[3] = SkillLibrary.Computer;
            skills[4] = SkillLibrary.Hunting;
            skills[5] = SkillLibrary.Admin;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = "Advanced Education";
            skills = table.Skills;
            skills[0] = SkillLibrary.Medic;
            skills[1] = SkillLibrary.Computer;
            skills[2] = SkillLibrary.Hunting;
            skills[3] = SkillLibrary.Leader;
            skills[4] = SkillLibrary.Survival;
            skills[5] = SkillLibrary.Admin;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.SafariShip);

            Cash[0] = 1000;
            Cash[1] = 1000;
            Cash[2] = 5000;
            Cash[3] = 5000;
            Cash[4] = 10000;
            Cash[5] = 100000;
            Cash[6] = 100000;

            Ranks[0] = "Hunter";
        }

        protected override void CommsionSkill()
        {
            // Nothing to do here
        }

        protected override int EnlistFactor()
        {
            var target = 0;
            if (Owner.CharacterSpecies == Character.Species.AelYael)
            {
                target--;
            }
            return target;
        }

        protected override void EnlistSkill()
        {
            Owner.AddSkill(SkillLibrary.Hunting);
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
