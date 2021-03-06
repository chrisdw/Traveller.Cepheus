﻿namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Hostile
{
    public static class BenefitLibrary
    {
        public static Benefit Dex = new Benefit("DEX", 1, Benefit.BenefitType.AttributeModification);
        public static Benefit Str2 = new Benefit("STR", 2, Benefit.BenefitType.AttributeModification);

        // Tickets
        public static Benefit StandardTicket = new Benefit(Resources.Benefit_StandardTicket, 1, Benefit.BenefitType.Material);
        public static Benefit EliteTicket = new Benefit(Resources.Benefit_EliteTicket, 1, Benefit.BenefitType.Material);

        public static Benefit DistinguishedFlyingCross = new Benefit(Resources.Benefit_DFC, 1, Benefit.BenefitType.Material);
        public static Benefit SilverStar = new Benefit(Resources.Benefit_SilverStar, 1, Benefit.BenefitType.Material);
        public static Benefit StarEnvoyClubMember = new Benefit(Resources.Benefit_StarEnvoyClubMember, 1, Benefit.BenefitType.Material);
        public static Benefit TraumaKit = new Benefit(Resources.Benefit_TraumaKit, 1, Benefit.BenefitType.Material);

        public static Benefit OneThousandCredits = new Benefit(Properties.Resources.Benefit_Cash, 1000, Benefit.BenefitType.Cash);
        public static Benefit FiveThousandCredits = new Benefit(Properties.Resources.Benefit_Cash, 5000, Benefit.BenefitType.Cash);
    }
}
