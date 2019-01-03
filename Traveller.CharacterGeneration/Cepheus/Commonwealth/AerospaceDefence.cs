namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Commonwealth
{
    public class AerospaceDefence : Cepheus.AerospaceDefence
    {
        public AerospaceDefence()
        {
            // Replace vehcile skill with one that does not have "grav"
            SkillTables[0].Skills[5] = SkillLibrary.Vehicle;
            SkillTables[1].Skills[5] = SkillLibrary.Vehicle;

            // Replace Gravitics with engineering
            SkillTables[3].Skills[1] = CharacterGeneration.SkillLibrary.Engineering;
        }
    }
}
