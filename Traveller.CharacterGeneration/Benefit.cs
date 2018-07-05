using System;
using System.Collections.Generic;
using System.Linq;
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


        public static List<string> GetWeaponList(Benefit benefit)
        {
            switch (benefit.Name)
            {
                case "Bow":
                    return Cascades.BowCombat.ResolveSkill().Select(sk => sk.Name).ToList();
                case "Gun":
                    return Cascades.GunCombat.ResolveSkill().Select(sk => sk.Name).ToList();
                case "Blade":
                    return Cascades.BladeCombat.ResolveSkill().Select(sk => sk.Name).ToList();
                case "Weapon":
                    var list = new List<string>();
                    list.AddRange(Cascades.BowCombat.ResolveSkill().Select(sk => sk.Name).ToList());
                    list.AddRange(Cascades.GunCombat.ResolveSkill().Select(sk => sk.Name).ToList());
                    list.AddRange(Cascades.BladeCombat.ResolveSkill().Select(sk => sk.Name).ToList());
                    return list;
                default:
                    return new List<string>();
            }
        }
    }
}
