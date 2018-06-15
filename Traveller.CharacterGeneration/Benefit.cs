using System;
using System.Collections.Generic;
using System.Text;

namespace Traveller.CharacterGeneration
{
    public class Benefit
    {
        public enum BenefitType
        {
            Cash,
            AttributeModification,
            Material,
            Weapon,
            None,
            Skill
        }

        public string Name { get; set; }
        public int Value { get; set; }
        public BenefitType TypeOfBenefit { get; set; }
    }
}
