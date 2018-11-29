namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus
{
    public static class SkillLibrary
    {
        public static Skill Advocate = new Skill("Advocate", Skill.SkillClass.None, 1);
        public static Skill Athletics = new Skill("Athletics", Skill.SkillClass.None, 1);
        public static Skill Linguistics = new Skill("Linguistics", Skill.SkillClass.None, 1);

        public static Skill Aircraft = new Skill("Aircraft", Skill.SkillClass.None, 1)
        {
            Cascade =
            {
                 new Skill("Grav Vehicle", Skill.SkillClass.None, 1),
                 new Skill("Rotor Aircraft", Skill.SkillClass.None, 1),
                 new Skill("Winged Aircraft", Skill.SkillClass.None, 1)
            }
        };
        public static Skill GroundVehicle = new Skill("Ground Vehicle", Skill.SkillClass.None, 1)
        {
            Cascade =
            {
                 new Skill("Mole", Skill.SkillClass.None, 1),
                 new Skill("Tracked Vehicle", Skill.SkillClass.None, 1),
                 new Skill("Wheeled Vehicle", Skill.SkillClass.None, 1)
            }
        };
        public static Skill GunCombat = new Skill("Gun Combat", Skill.SkillClass.None, 1)
        {
            Cascade =
            {
                new Skill("Archery", Skill.SkillClass.None, 1),
                new Skill("Energy Pistol", Skill.SkillClass.None, 1),
                new Skill("Energy Rifle", Skill.SkillClass.None, 1),
                new Skill("Shotgun", Skill.SkillClass.None, 1),
                new Skill("Slug Pistol", Skill.SkillClass.None, 1),
                new Skill("Slug Rifle", Skill.SkillClass.None, 1)
            }
        };
        public static Skill Gunnery = new Skill("Gunnery", Skill.SkillClass.None, 1)
        {
            Cascade =
            {
                new Skill("Bay Weapons", Skill.SkillClass.None, 1),
                new Skill("Heavy Weapons", Skill.SkillClass.None, 1),
                new Skill("Screens", Skill.SkillClass.None, 1),
                new Skill("Spinal Mounts", Skill.SkillClass.None, 1),
                new Skill("Turret Weapons", Skill.SkillClass.None, 1)
            }
        };
        public static Skill MeleeCombat = new Skill("Melee Combat", Skill.SkillClass.None, 1)
        {
            Cascade =
            {
                new Skill("Bludgeoning Weapons", Skill.SkillClass.None, 1),
                new Skill("Natural Weapons", Skill.SkillClass.None, 1),
                new Skill("Piercing Weapons", Skill.SkillClass.None, 1),
                new Skill("Slashing Weapons", Skill.SkillClass.None, 1)
            }
        };
        public static Skill Sciences = new Skill("Sciences", Skill.SkillClass.None, 1)
        {
            Cascade =
            {
                 new Skill("Life Sciences", Skill.SkillClass.None, 1),
                 new Skill("Physical Sciences", Skill.SkillClass.None, 1),
                 new Skill("Social Sciences", Skill.SkillClass.None, 1),
                 new Skill("Space Sciences", Skill.SkillClass.None, 1)
            }
        };
        public static Skill Watercraft = new Skill("Watercraft", Skill.SkillClass.None, 1)
        {
            Cascade =
            {
                 new Skill("Motorboats", Skill.SkillClass.None, 1),
                 new Skill("Ocean Ships", Skill.SkillClass.None, 1),
                 new Skill("Sailing Ships", Skill.SkillClass.None, 1),
                 new Skill("Submarines", Skill.SkillClass.None, 1)
            }
        };
        public static Skill Vehicle = new Skill("Vehicle", Skill.SkillClass.None, 1)
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
