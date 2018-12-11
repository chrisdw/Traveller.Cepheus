namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus
{
    public static class SkillLibrary
    {
        public static Skill Advocate = new Skill(Resources.Skill_Advocate, Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill Athletics = new Skill(Resources.Skill_Athletics, Skill.SkillClass.None, 1);
        public static Skill Linguistics = new Skill(Resources.Skill_Linguistics, Skill.SkillClass.None, 1);
        public static Skill NaturalWeapons = new Skill("Natural Weapons", Skill.SkillClass.None, 1);

        public static Skill Aircraft = new Skill(Resources.Skill_Aircraft, Skill.SkillClass.None, 1)
        {
            Cascade =
            {
                 new Skill(Resources.Skill_GravVehicle, Skill.SkillClass.None, 1),
                 new Skill(Resources.Skill_RotorAircraft, Skill.SkillClass.None, 1),
                 new Skill(Resources.Skill_WingedAircraft, Skill.SkillClass.None, 1)
            }
        };

        public static Skill VetinaryMedicine = new Skill(Resources.Skill_VetinaryMedicine, Skill.SkillClass.None, 1);

        public static Skill Animals = new Skill(Resources.Skill_Animals, Skill.SkillClass.None, 1)
        {
            Cascade =
            {
                 new Skill(Resources.Skill_Farming, Skill.SkillClass.None, 1, Skill.SkillSex.Female),
                 new Skill(Resources.Skill_Riding, Skill.SkillClass.None, 1),
                 CharacterGeneration.SkillLibrary.Survival,
                 VetinaryMedicine
            }
        };

        public static Skill GroundVehicle = new Skill(Resources.Skill_GroundVehicle, Skill.SkillClass.None, 1)
        {
            Cascade =
            {
                 new Skill(Resources.Skill_Mole, Skill.SkillClass.None, 1),
                 new Skill(Resources.Skill_TrackedVehicle, Skill.SkillClass.None, 1),
                 new Skill(Resources.Skill_WheeledVehicle, Skill.SkillClass.None, 1)
            }
        };
        public static Skill GunCombat = new Skill(Resources.Skill_GunCombat, Skill.SkillClass.None, 1)
        {
            Cascade =
            {
                new Skill(Resources.Skill_Archery, Skill.SkillClass.None, 1),
                new Skill(Resources.Skill_EnergyPistol, Skill.SkillClass.None, 1),
                new Skill(Resources.Skill_EnergyRifle, Skill.SkillClass.None, 1),
                new Skill(Resources.Skill_Shotgun, Skill.SkillClass.None, 1),
                new Skill(Resources.Skill_SlugPistol, Skill.SkillClass.None, 1),
                new Skill(Resources.Skill_SlugRifle, Skill.SkillClass.None, 1)
            }
        };

        public static Skill HeavyWeapons = new Skill(Resources.Skill_HeavyWeapons, Skill.SkillClass.None, 1);

        public static Skill Gunnery = new Skill(Resources.Skill_Gunnery, Skill.SkillClass.None, 1)
        {
            Cascade =
            {
                new Skill(Resources.Skill_BayWeapons, Skill.SkillClass.None, 1),
                HeavyWeapons,
                new Skill(Resources.Skill_Screens, Skill.SkillClass.None, 1),
                new Skill(Resources.Skill_SpinalMounts, Skill.SkillClass.None, 1),
                new Skill(Resources.Skill_TurretWeapons, Skill.SkillClass.None, 1)
            }
        };
        public static Skill MeleeCombat = new Skill(Resources.Skill_MeleeCombat, Skill.SkillClass.None, 1)
        {
            Cascade =
            {
                new Skill(Resources.Skill_BludgeoningWeapons, Skill.SkillClass.None, 1),
                new Skill(Resources.Skill_NaturalWeapons, Skill.SkillClass.None, 1),
                new Skill(Resources.Skill_PiercingWeapons, Skill.SkillClass.None, 1),
                new Skill(Resources.Skill_SlashingWeapons, Skill.SkillClass.None, 1)
            }
        };
        public static Skill Sciences = new Skill(Resources.Skill_Sciences, Skill.SkillClass.None, 1)
        {
            Cascade =
            {
                 new Skill(Resources.Skill_LifeSciences, Skill.SkillClass.None, 1),
                 new Skill(Resources.Skill_PhysicalSciences, Skill.SkillClass.None, 1),
                 new Skill(Resources.Skill_SocialSciences, Skill.SkillClass.None, 1),
                 new Skill(Resources.Skill_SpaceSciences, Skill.SkillClass.None, 1)
            }
        };
        public static Skill Watercraft = new Skill(Resources.Skill_Watercraft, Skill.SkillClass.None, 1)
        {
            Cascade =
            {
                 new Skill(Resources.Skill_Motorboats, Skill.SkillClass.None, 1),
                 new Skill(Resources.Skill_OceanShips, Skill.SkillClass.None, 1),
                 new Skill(Resources.Skill_SailingShips, Skill.SkillClass.None, 1),
                 new Skill(Resources.Skill_Submarines, Skill.SkillClass.None, 1)
            }
        };
        public static Skill Vehicle = new Skill(Resources.Skill_Vehicle, Skill.SkillClass.None, 1)
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
