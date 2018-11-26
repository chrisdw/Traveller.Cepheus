namespace org.DownesWard.Traveller.CharacterGeneration
{
    public static class BenefitLibrary
    {
        // Attribute Modifications
        public static Benefit Int = new Benefit("INT", 1, Benefit.BenefitType.AttributeModification);
        public static Benefit Int2 = new Benefit("INT", 2, Benefit.BenefitType.AttributeModification);
        public static Benefit Edu = new Benefit("EDU", 1, Benefit.BenefitType.AttributeModification);
        public static Benefit Edu2 = new Benefit("EDU", 2, Benefit.BenefitType.AttributeModification);
        public static Benefit Soc = new Benefit("SOC", 1, Benefit.BenefitType.AttributeModification);
        public static Benefit Soc2 = new Benefit("SOC", 2, Benefit.BenefitType.AttributeModification);
        public static Benefit Chr = new Benefit("CHR", 1, Benefit.BenefitType.AttributeModification);
        public static Benefit Psi = new Benefit("PSI", 1, Benefit.BenefitType.AttributeModification);

        // Weapon benefits
        public static Benefit Blade = new Benefit(Properties.Resources.Benefit_Blade, 1, Benefit.BenefitType.Weapon);
        public static Benefit Bow = new Benefit(Properties.Resources.Benefit_Bow, 1, Benefit.BenefitType.Weapon);
        public static Benefit Gun = new Benefit(Properties.Resources.Benefit_Gun, 1, Benefit.BenefitType.Weapon);
        public static Benefit Weapon = new Benefit(Properties.Resources.Benefit_Weapon, 1, Benefit.BenefitType.Weapon);

        // Other benefits
        public static Benefit Land = new Benefit("Land", 1, Benefit.BenefitType.Material);
        public static Benefit Legion = new Benefit(Properties.Resources.Benefit_Legion, 1, Benefit.BenefitType.Material);
        public static Benefit Independance = new Benefit(Properties.Resources.Skill_Independance, 1, Benefit.BenefitType.Skill);
        public static Benefit Instruments = new Benefit(Properties.Resources.Benefit_Instruments, 1, Benefit.BenefitType.Material);
        public static Benefit Travellers = new Benefit(Properties.Resources.Benefit_Travellers, 1, Benefit.BenefitType.Material);
        public static Benefit Nothing = new Benefit(Properties.Resources.Benefit_Nothing, 1, Benefit.BenefitType.Material);
        public static Benefit Verbalization = new Benefit(Properties.Resources.Benefit_Verbalization, 1, Benefit.BenefitType.Skill);
        public static Benefit Voucher = new Benefit(Properties.Resources.Benefit_Voucher, 1, Benefit.BenefitType.Material);
        public static Benefit WaldoSet = new Benefit(Properties.Resources.Benefit_WaldoSet, 1, Benefit.BenefitType.Material);
        public static Benefit Watch = new Benefit(Properties.Resources.Benefit_Watch, 1, Benefit.BenefitType.Material);

        // Tickets
        public static Benefit LowPsg = new Benefit(Properties.Resources.Benefit_LowPsg, 1, Benefit.BenefitType.Material);
        public static Benefit MidPsg = new Benefit(Properties.Resources.Benefit_MidPsg, 1, Benefit.BenefitType.Material);
        public static Benefit HighPsg = new Benefit(Properties.Resources.Benefit_HighPsg, 1, Benefit.BenefitType.Material);

        // Ship shares
        public static Benefit Corsair = new Benefit(Properties.Resources.Ship_Corsair, 1, Benefit.BenefitType.Material);
        public static Benefit LabShip = new Benefit(Properties.Resources.Ship_LabShip, 1, Benefit.BenefitType.Material);
        public static Benefit Merchant = new Benefit(Properties.Resources.Ship_Merchant, 1, Benefit.BenefitType.Material);
        public static Benefit Scout = new Benefit(Properties.Resources.Ship_Scout, 1, Benefit.BenefitType.Material);
        public static Benefit SafariShip = new Benefit(Properties.Resources.Ship_SafariShip, 1, Benefit.BenefitType.Material);
        public static Benefit Seeker = new Benefit(Properties.Resources.Ship_Seeker, 1, Benefit.BenefitType.Material);
        public static Benefit Trader = new Benefit(Properties.Resources.Ship_Trader, 1, Benefit.BenefitType.Material);
        public static Benefit Yacht = new Benefit(Properties.Resources.Ship_Yacht, 1, Benefit.BenefitType.Material);
    }
}
