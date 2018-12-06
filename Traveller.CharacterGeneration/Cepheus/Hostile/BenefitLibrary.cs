namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Hostile
{
    public static class BenefitLibrary
    {
        public static Benefit Dex = new Benefit("DEX", 1, Benefit.BenefitType.AttributeModification);
        public static Benefit Str2 = new Benefit("STR", 2, Benefit.BenefitType.AttributeModification);

        // Tickets
        public static Benefit StandardTicket = new Benefit(Resources.Benefit_StandardTicket, 1, Benefit.BenefitType.Material);
        public static Benefit EliteTicket = new Benefit(Resources.Benefit_EliteTicket, 1, Benefit.BenefitType.Material);

        public static Benefit SilverStar = new Benefit("Silver Star", 1, Benefit.BenefitType.Material);
        public static Benefit StarEnvoyClubMember = new Benefit(Resources.Benefit_StarEnvoyClubMember, 1, Benefit.BenefitType.Material);
    }
}
