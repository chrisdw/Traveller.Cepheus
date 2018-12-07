namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Hostile
{
    public static class SkillLibrary
    {
        public static Skill Agriculture = new Skill(Resources.Skill_Agriculture, Skill.SkillClass.None, 1);
        public static Skill Aircraft = new Skill(Cepheus.Resources.Skill_Aircraft, Skill.SkillClass.None, 1);
        public static Skill BladeCombat = new Skill(Resources.Skill_BladeCombat, Skill.SkillClass.None, 1);
        public static Skill GroundVehicle = new Skill(Cepheus.Resources.Skill_GroundVehicle, Skill.SkillClass.None, 1);
        public static Skill GunCombat = new Skill(Resources.Skill_GunCombat, Skill.SkillClass.None, 1);
        public static Skill Investigate = new Skill(Resources.Skill_Investigate, Skill.SkillClass.None, 1);
        public static Skill Loader = new Skill(Resources.Skill_Loader, Skill.SkillClass.None, 1);
        public static Skill Mining = new Skill(Resources.Skill_Mining, Skill.SkillClass.None, 1);
        public static Skill Security = new Skill(Resources.Skill_Security, Skill.SkillClass.None, 1);
        public static Skill Watercraft = new Skill(Cepheus.Resources.Skill_Watercraft, Skill.SkillClass.None, 1);

        public static Skill Vehicle = new Skill(Properties.Resources.Skill_Vehicle, Skill.SkillClass.None, 1)
        {
            Cascade =
            {
                Aircraft,
                GroundVehicle,
                Watercraft
            }
        };
    }
}
