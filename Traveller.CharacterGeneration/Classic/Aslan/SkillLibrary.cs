namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Aslan
{
    public static class SkillLibrary
    {
        public static Skill Uealikhe = new Skill("Uealikhe (Carbine)", Skill.SkillClass.None, 1);

        public static Skill PersonalWeapons = new Skill("Peronal Weapons", Skill.SkillClass.None, 1)
        {
            Cascade =
            {
                new Skill("Fierah", Skill.SkillClass.None, 1),
                new Skill("Yurletya", Skill.SkillClass.None, 1),
                new Skill("Akhaeh", Skill.SkillClass.None, 1),
                new Skill("Spear", Skill.SkillClass.None, 1),
                CharacterGeneration.SkillLibrary.Pike,
                CharacterGeneration.SkillLibrary.Cudgel
            }
        };

        public static Skill GunCombat = new Skill("Gun Combat", Skill.SkillClass.None, 1)
        {
            Cascade =
            {
                new Skill("Khaihte (Pistol)", Skill.SkillClass.None, 1),
                new Skill("Takhestah (Long Pistol)", Skill.SkillClass.None, 1),
                new Skill("Khaifealate (Machine Pistol)", Skill.SkillClass.None, 1),
                Uealikhe,
                new Skill("Takheal (Long Rifle)", Skill.SkillClass.None, 1),
                new Skill("Yeheal (Automatic Rifle)", Skill.SkillClass.None, 1),
                new Skill("Eakhyasear (Hunters Rifle)", Skill.SkillClass.None, 1),
                new Skill("Triluealikhe (Laser Carbine)", Skill.SkillClass.None, 1),
                new Skill("Trolitakheal (Laser Rifle)", Skill.SkillClass.None, 1)
            }
        };
    }
}
