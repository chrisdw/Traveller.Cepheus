using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration
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

        public Benefit()
        {

        }

        public Benefit(string name, int value, BenefitType benefitType)
        {
            Name = name;
            Value = value;
            TypeOfBenefit = benefitType;
        }
    }
}
