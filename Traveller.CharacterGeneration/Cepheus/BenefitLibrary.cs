namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus
{
    public static class BenefitLibrary
    {
        public static Benefit End = new Benefit("END", 1, Benefit.BenefitType.AttributeModification);

        public static Benefit ExplorersSociety = new Benefit(Resources.Benefit_ExplorersSociety, 1, Benefit.BenefitType.Material);
        public static Benefit CourierVessel = new Benefit(Resources.Benefit_CourierVessel, 1, Benefit.BenefitType.Material);

        public static Benefit ShipShares = new Benefit("1D6 Ship Shares", 1, Benefit.BenefitType.Material);
    }
}
