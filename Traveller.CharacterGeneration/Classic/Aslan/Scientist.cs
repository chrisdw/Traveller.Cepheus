using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Aslan
{
    public class Scientist : Career
    {
        public Scientist()
        {
            Name = "Scientist";
            hasRanks = false;

            enlistment = 6;

            survival = 9;
            survival2attr = string.Empty;
            survival3attr = "INT";
            survival3val = 8;

            reenlist = 5;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = "Personal Development";
            var skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Edu;
            skills[1] = CharacterGeneration.SkillLibrary.Str;
            skills[2] = CharacterGeneration.SkillLibrary.Dex;
            skills[3] = SkillLibrary.GunCombat;
            skills[4] = CharacterGeneration.SkillLibrary.Int;
            skills[5] = CharacterGeneration.SkillLibrary.Edu;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = "Service Skills";
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Electronics;
            skills[1] = CharacterGeneration.SkillLibrary.Mechanical;
            skills[2] = CharacterGeneration.SkillLibrary.Gravitics;
            skills[3] = CharacterGeneration.SkillLibrary.Computer;
            skills[4] = CharacterGeneration.SkillLibrary.Admin;
            skills[5] = CharacterGeneration.SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = "Advanced Service Skills";
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Electronics;
            skills[1] = CharacterGeneration.SkillLibrary.Electronics;
            skills[2] = CharacterGeneration.SkillLibrary.Mechanical;
            skills[3] = CharacterGeneration.SkillLibrary.Mechanical;
            skills[4] = CharacterGeneration.SkillLibrary.Gravitics;
            skills[5] = CharacterGeneration.SkillLibrary.Gravitics;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = "Experience";
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.JackOfTrades;
            skills[1] = CharacterGeneration.SkillLibrary.Medic;
            skills[2] = CharacterGeneration.SkillLibrary.Computer;
            skills[3] = CharacterGeneration.SkillLibrary.Tolerance;
            skills[4] = CharacterGeneration.SkillLibrary.VaccSuit;
            skills[5] = CharacterGeneration.SkillLibrary.JackOfTrades;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.MidPsg);
            Material.Add(BenefitLibrary.MidPsg);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.Soc);
            Material.Add(BenefitLibrary.Researcher);

            Cash[0] = 1000;
            Cash[1] = 5000;
            Cash[2] = 5000;
            Cash[3] = 10000;
            Cash[4] = 20000;
            Cash[5] = 40000;
            Cash[6] = 70000;

            Ranks[0] = "Scientist";
        }
        protected override void CommsionSkill()
        {
            
        }

        protected override void EnlistSkill()
        {
            Owner.AddSkill(CharacterGeneration.SkillLibrary.Computer);
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
