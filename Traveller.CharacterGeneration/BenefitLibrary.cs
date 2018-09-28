using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration
{
    public static class BenefitLibrary
    {
        // Attribute Modifications
        public static Benefit Int = new Benefit("INT", 1, Benefit.BenefitType.AttributeModification);
        public static Benefit Edu = new Benefit("EDU", 1, Benefit.BenefitType.AttributeModification);
        public static Benefit Soc = new Benefit("SOC", 1, Benefit.BenefitType.AttributeModification);

        // Weapon benefits
        public static Benefit Blade = new Benefit("Blade", 1, Benefit.BenefitType.Weapon);
        public static Benefit Gun = new Benefit("Gun", 1, Benefit.BenefitType.Weapon);
        public static Benefit Weapon = new Benefit("Weapon", 1, Benefit.BenefitType.Weapon);

        // Other benefits
        public static Benefit Instruments = new Benefit("Instruments", 1, Benefit.BenefitType.Material);
        public static Benefit Travellers = new Benefit("Travellers", 1, Benefit.BenefitType.Material);
        public static Benefit Nothing = new Benefit("Nothing", 1, Benefit.BenefitType.Material);
        public static Benefit Watch = new Benefit("Watch", 1, Benefit.BenefitType.Material);

        // Tickets
        public static Benefit LowPsg = new Benefit("Low Psg", 1, Benefit.BenefitType.Material);
        public static Benefit MidPsg = new Benefit("Mid Psg", 1, Benefit.BenefitType.Material);
        public static Benefit HighPsg = new Benefit("High Psg", 1, Benefit.BenefitType.Material);

        // Ship shares
        public static Benefit Corsair = new Benefit("Corsair", 1, Benefit.BenefitType.Material);
        public static Benefit LabShip = new Benefit("Lab Ship", 1, Benefit.BenefitType.Material);
        public static Benefit Merchant = new Benefit("Merchant", 1, Benefit.BenefitType.Material);
        public static Benefit Scout = new Benefit("Scout", 1, Benefit.BenefitType.Material);
        public static Benefit SafariShip = new Benefit("Safari Ship", 1, Benefit.BenefitType.Material);
        public static Benefit Seeker = new Benefit("Seeker", 1, Benefit.BenefitType.Material);
        public static Benefit Yacht = new Benefit("Yacht", 1, Benefit.BenefitType.Material);
    }
}
