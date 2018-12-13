namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Commonwealth.Lucerne
{
    public class Technician : Cepheus.Technician
    {
        public override CharacterGeneration.Character Owner
        {
            get => base.Owner;
            set
            {
                base.Owner = value;
                if (Owner.Sex.Equals(Properties.Resources.Sex_Male))
                {
                    maxRank = 3;
                }
            }
        }
    }
}
