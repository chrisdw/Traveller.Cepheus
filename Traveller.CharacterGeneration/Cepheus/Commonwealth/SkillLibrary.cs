namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Commonwealth
{
    public class SkillLibrary
    {
        // Override psionics to remove teleportation
        public static Skill Psionics = new Skill(Cepheus.Resources.Skill_Psionics, Skill.SkillClass.Psionic, 1)
        {
            Cascade =
            {
                Cepheus.SkillLibrary.Awareness,
                Cepheus.SkillLibrary.Clairvoyance,
                Cepheus.SkillLibrary.Telekinesis,
                Cepheus.SkillLibrary.Telepathy
            }
        };

        public static Skill Aircraft = new Skill(Resources.Skill_Aircraft, Skill.SkillClass.None, 1)
        {
            Cascade =
            {
                 new Skill(Resources.Skill_RotorAircraft, Skill.SkillClass.None, 1),
                 new Skill(Resources.Skill_WingedAircraft, Skill.SkillClass.None, 1)
            }
        };

        public static Skill Vehicle = new Skill(Resources.Skill_Vehicle, Skill.SkillClass.None, 1)
        {
            Cascade =
            {
                Aircraft,
                Cepheus.SkillLibrary.GroundVehicle,
                Cepheus.SkillLibrary.Watercraft
            }
        };
    }
}
