using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Dolphin
{
    public class Military : Career
    {
        private int minTL;
        public Military(bool isSolomani) : base(isSolomani)
        {
            Intialise();
        }

        public Military() : base(false)
        {
            Intialise();
        }

        private void Intialise()
        {
            RankNumber = 0;
            TermSkills = 1;
            terms = dice.roll(2) + 4;
            Name = "Military";

            if (solomani)
            {
                minTL = dice.roll(2) + 3;
            }
            else
            {
                minTL = dice.roll(2) + 4;
            }
            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = "Basic Skills";
            var skills = table.Skills;
            skills[0] = SkillLibrary.HitsU;
            skills[1] = SkillLibrary.HitsU;
            skills[2] = SkillLibrary.Brawling;
            skills[3] = SkillLibrary.Int;
            skills[4] = SkillLibrary.Int;
            skills[5] = SkillLibrary.WaldoOps;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = "Military Skills";
            skills = table.Skills;
            skills[0] = SkillLibrary.Brawling;
            skills[1] = SkillLibrary.Brawling;
            skills[2] = SkillLibrary.Recon;
            skills[3] = SkillLibrary.WaldoOps;
            skills[4] = SkillLibrary.Tactics;
            skills[5] = SkillLibrary.CombatEngineering;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Available = false;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Available = false;

            Material.Add(BenefitLibrary.Nothing);
            Material.Add(BenefitLibrary.Nothing);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Verbalization);
            Material.Add(BenefitLibrary.WaldoSet);

            Ranks[0] = "Dolphin";
        }

        public override void CheckTableAvailablity()
        {
            base.CheckTableAvailablity();
            if (minTL >= 12)
            {
                SkillTables[1].Skills[0].Name = "Battle Dress";
                SkillTables[1].Skills[0].Class = Skill.SkillClass.Military;
            }
            else
            {
                SkillTables[1].Skills[0].Name = "Brawling";
                SkillTables[1].Skills[0].Class = Skill.SkillClass.Military;
            }
            SkillTables[0].Available = (Term <= 4);
        }
    }
}
