namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Bughunters
{
    public class AerospaceDefence : Cepheus.AerospaceDefence
    {
        public override CharacterGeneration.Character Owner
        {
            get => base.Owner;
            set
            {
                base.Owner = value;
                if (Owner.CharacterSpecies == CharacterGeneration.Character.Species.Synner)
                {
                    maxRank = 3;
                }
            }
        }
    }
}
