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
                    return SkillLibrary.BowCombat.ResolveSkill().Select(sk => sk.Name).ToList();
                case "Gun":
                    return SkillLibrary.GunCombat.ResolveSkill().Select(sk => sk.Name).ToList();
                case "Blade":
                    return SkillLibrary.BladeCombat.ResolveSkill().Select(sk => sk.Name).ToList();
                case "Weapon":
                    var list = new List<string>();
                    list.AddRange(SkillLibrary.BowCombat.ResolveSkill().Select(sk => sk.Name).ToList());
                    list.AddRange(SkillLibrary.GunCombat.ResolveSkill().Select(sk => sk.Name).ToList());
                    list.AddRange(SkillLibrary.BladeCombat.ResolveSkill().Select(sk => sk.Name).ToList());
                    return list;
                default:
                    return new List<string>();
            }
        }
    }
}
