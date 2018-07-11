﻿using System;
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

        // Other benefits
        public static Benefit Blade = new Benefit("Blade", 1, Benefit.BenefitType.Weapon);
        public static Benefit Gun = new Benefit("Gun", 1, Benefit.BenefitType.Weapon);
        public static Benefit Travellers = new Benefit("Travellers", 1, Benefit.BenefitType.Material);

        // Tickets
        public static Benefit LowPsg = new Benefit("Low Psg", 1, Benefit.BenefitType.Material);
        public static Benefit MidPsg = new Benefit("Mid Psg", 1, Benefit.BenefitType.Material);
        public static Benefit HighPsg = new Benefit("High Psg", 1, Benefit.BenefitType.Material);
    }
}