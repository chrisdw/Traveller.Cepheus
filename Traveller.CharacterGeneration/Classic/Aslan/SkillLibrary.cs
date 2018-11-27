namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Aslan
{
    public static class SkillLibrary
    {
        public static Skill Uealikhe = new Skill(Resources.Skill_UealikheCarbine, Skill.SkillClass.None, 1);

        public static Skill PersonalWeapons = new Skill(Resources.Skill_PersonalWeapons, Skill.SkillClass.None, 1)
        {
            Cascade =
            {
                new Skill(Resources.Skill_Fierah, Skill.SkillClass.None, 1),
                new Skill(Resources.Skill_Yurletya, Skill.SkillClass.None, 1),
                new Skill(Resources.Skill_Akhaeh, Skill.SkillClass.None, 1),
                new Skill(Resources.Skill_Spear, Skill.SkillClass.None, 1),
                CharacterGeneration.SkillLibrary.Pike,
                CharacterGeneration.SkillLibrary.Cudgel
            }
        };

        public static Skill GunCombat = new Skill(Resources.Skill_GunCombat, Skill.SkillClass.None, 1)
        {
            Cascade =
            {
                new Skill(Resources.Skill_KhaihtePistol, Skill.SkillClass.None, 1),
                new Skill(Resources.Skill_TakhestahLongPistol, Skill.SkillClass.None, 1),
                new Skill(Resources.Skill_KhaifealateMachinePistol, Skill.SkillClass.None, 1),
                Uealikhe,
                new Skill(Resources.Skill_TakhealLongRifle, Skill.SkillClass.None, 1),
                new Skill(Resources.Skill_YehealAutomaticRifle, Skill.SkillClass.None, 1),
                new Skill(Resources.Skill_EakhyasearHuntersRifle, Skill.SkillClass.None, 1),
                new Skill(Resources.Skill_TriluealikheLaserCarbine, Skill.SkillClass.None, 1),
                new Skill(Resources.Skill_TrolitakhealLaserRifle, Skill.SkillClass.None, 1)
            }
        };
    }
}
