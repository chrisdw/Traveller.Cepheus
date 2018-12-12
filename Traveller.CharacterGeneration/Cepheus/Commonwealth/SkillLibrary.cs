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
    }
}
