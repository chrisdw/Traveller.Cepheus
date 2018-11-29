using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace org.DownesWard.Traveller.CharacterGeneration
{
    public class Benefit : IEquatable<Benefit>
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
            if (benefit.Name.Equals(Properties.Resources.Benefit_Bow))
            {
                return SkillLibrary.BowCombat.ResolveSkill().Select(sk => sk.Name).ToList();
            }
            else if (benefit.Name.Equals(Properties.Resources.Benefit_Gun))
            {
                return SkillLibrary.GunCombat.ResolveSkill().Select(sk => sk.Name).ToList();
            }
            else if (benefit.Name.Equals(Properties.Resources.Benefit_Blade))
            {
                return SkillLibrary.BladeCombat.ResolveSkill().Select(sk => sk.Name).ToList();
            }
            else if (benefit.Name.Equals(Properties.Resources.Benefit_Weapon))
            {
                var list = new List<string>();
                list.AddRange(SkillLibrary.BowCombat.ResolveSkill().Select(sk => sk.Name).ToList());
                list.AddRange(SkillLibrary.GunCombat.ResolveSkill().Select(sk => sk.Name).ToList());
                list.AddRange(SkillLibrary.BladeCombat.ResolveSkill().Select(sk => sk.Name).ToList());
                return list;
            }
            else
            {
                return new List<string>();
            }
        }

        public Benefit Clone()
        {
            var benefit = new Benefit()
            {
                Name = Name,
                Value = Value,
                TypeOfBenefit = TypeOfBenefit
            };
            return benefit;
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

        public bool Equals(Benefit other)
        {
            return other.Name.Equals(Name) && other.Value == Value && other.TypeOfBenefit == TypeOfBenefit;
        }

        public override int GetHashCode()
        {
            //Get hash code for the Name field if it is not null. 
            int hashBenefitName = Name == null ? 0 : Name.GetHashCode();

            //Get hash code for the Value field. 
            int hashBenefitValue = Value.GetHashCode();

            //Get hash code for the Value field. 
            int hashBenefitType = TypeOfBenefit.GetHashCode();

            //Calculate the hash code for the product. 
            return hashBenefitName ^ hashBenefitValue ^ hashBenefitType;
        }
    }
}
