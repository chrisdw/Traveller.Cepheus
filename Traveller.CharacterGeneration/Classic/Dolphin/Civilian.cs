namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Dolphin
{
    public class Civilian : Career
    {
        public Civilian() : base(false)
        {
            RankNumber = 0;
            TermSkills = 1;
            terms = dice.roll(2);
            Name = Resources.Career_Civilian;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Resources.Table_BasicSkills;
            var skills = table.Skills;
            skills[0] = SkillLibrary.HitsU;
            skills[1] = SkillLibrary.HitsU;
            skills[2] = SkillLibrary.Brawling;
            skills[3] = SkillLibrary.Int;
            skills[4] = SkillLibrary.Int;
            skills[5] = SkillLibrary.WaldoOps;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Resources.Table_CivilianSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.Herding;
            skills[1] = SkillLibrary.Herding;
            skills[2] = SkillLibrary.Hunting;
            skills[3] = SkillLibrary.Brawling;
            skills[4] = SkillLibrary.Survival;
            skills[5] = SkillLibrary.WaldoOps;

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

            Ranks[0] = Resources.Rank_Dolphin;
        }

        public override void CheckTableAvailablity()
        {
            base.CheckTableAvailablity();
            if (Owner.Profile.Int.Value >= 9)
            {
                SkillTables[1].Skills[0].Name = SkillLibrary.Liason.Name;
                SkillTables[1].Skills[0].Class = Skill.SkillClass.Civilian;
            }
            else
            {
                SkillTables[1].Skills[0].Name = SkillLibrary.Herding.Name;
                SkillTables[1].Skills[0].Class = Skill.SkillClass.Civilian;
            }
        }
    }
}
