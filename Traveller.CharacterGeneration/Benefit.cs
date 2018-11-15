using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

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

        public void SaveXML(XmlElement ele)
        {
            var benefit = ele.OwnerDocument.CreateElement("Benefit");
            benefit.SetAttribute("Name", Name);
            benefit.SetAttribute("Value", Value.ToString());
            benefit.SetAttribute("Type", TypeOfBenefit.ToString());
            ele.AppendChild(benefit);
        }

        public static Benefit Load(XmlElement element)
        {
            var benefit = new Benefit
            {
                Name = element.GetAttribute("Name"),
                Value = int.Parse(element.GetAttribute("Value"))
            };
            Enum.TryParse(element.GetAttribute("Type"), out BenefitType benefitType);
            benefit.TypeOfBenefit = benefitType;
            return benefit;
        }
    }
}
