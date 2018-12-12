namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Commonwealth.Covenant
{
    public class Agent : Cepheus.Agent
    {
        public override CharacterGeneration.Character Owner
        {
            get => base.Owner;
            set
            {
                base.Owner = value;
                if (Owner.Sex.Equals(Properties.Resources.Sex_Female))
                {
                    maxRank = 2;
                }
            }
        }
    }
}
