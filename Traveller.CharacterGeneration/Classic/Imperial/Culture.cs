using org.DownesWard.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Imperial
{
    public class Culture : ICulture
    {
        public Constants.CultureType Id => Constants.CultureType.Imperial;

        public bool MultipleCareers => false;

        static Dictionary<string, Skill> offered = new Dictionary<string, Skill>();

        // Do we use the extra careers from Citizens of the the Imperium
        public bool UseCitizenRules { get; set; } = false;

        public bool BenefitAllowed(Character character, Benefit benefit)
        {
            // Imperial culture allows all benefits to everyone
            return true;
        }

        public Dictionary<string, Career.CareerType> Careers(Character character)
        {
            var careers = new Dictionary<string, Career.CareerType>();
            switch (character.Style)
            {
                case Constants.GenerationStyle.Classic_Traveller:
                    switch (character.CharacterSpecies)
                    {
                        case Character.Species.AelYael:
                            // Won't join merchants
                            careers.Add("Army", Career.CareerType.Imperial_Army);
                            careers.Add("Marines", Career.CareerType.Imperial_Marines);
                            careers.Add("Navy", Career.CareerType.Imperial_Navy);
                            if (UseCitizenRules)
                            {
                                // TODO: Add citizen careers
                            }
                            else
                            {
                                // TODO: Add other careeer
                            }
                            break;
                        case Character.Species.Virushi:
                            // No military careers
                            careers.Add("Merchants", Career.CareerType.Imperial_Merchants);
                            if (UseCitizenRules)
                            {
                                // TODO: Add citizen careers
                            }
                            else
                            {
                                // TODO: Add other careeer
                            }
                            break;
                        case Character.Species.Dolphin:
                            // Unique careers
                            break;
                        default:
                            careers.Add("Army", Career.CareerType.Imperial_Army);
                            careers.Add("Marines", Career.CareerType.Imperial_Marines);
                            careers.Add("Navy", Career.CareerType.Imperial_Navy);
                            careers.Add("Merchants", Career.CareerType.Imperial_Merchants);
                            if (UseCitizenRules)
                            {
                                // TODO: Add citizen careers
                                if (character.CharacterSpecies == Character.Species.Vargr)
                                {
                                    // TODO: Add corsair
                                }
                                if (character.Profile.Soc.Value > 10)
                                {
                                    // TODO: Add Noble
                                }
                            }
                            else
                            {
                                // TODO: Add other careeer
                            }
                            break;
                    }
                    break;
            }
            return careers;
        }

        public bool CheckSkill(Character character, Skill skill, int count)
        {
            var check = true;
            if (count == 1 && offered.Count > 0)
            {
                offered.Clear();
            }
            switch (character.CharacterSpecies)
            {
                case Character.Species.Bwap:
                    if (skill.Name.Equals("Admin"))
                    {
                        if (!character.Skills.ContainsKey("Admin"))
                        {
                            // Bwap always get 2 levels of admin on the first go
                            skill.Level = 2;
                        }
                    }
                    break;
                case Character.Species.Vargr:
                    if (skill.Name.Equals("Brawling"))
                    {
                        skill.Name = "Infighting";
                    }
                    break;
                case Character.Species.Aslan:
                    if (skill.Name.Equals("Brawling"))
                    {
                        skill.Name = "Dewclaw";
                    }
                    if (!offered.ContainsKey(skill.Name))
                    {
                        if (skill.SexApplicabilty == Skill.SkillSex.Female && character.Sex.Equals("Male"))
                        {
                            check = false;
                        }
                        else if (skill.SexApplicabilty == Skill.SkillSex.Male && character.Sex.Equals("Female"))
                        {
                            check = false;
                        }
                        if (!check)
                        {
                            offered.Add(skill.Name, skill);
                        }
                    }
                    break;
                case Character.Species.Virushi:
                    if (skill.Name.Equals("SOC"))
                    {
                        skill.Name = "EDU";
                    }
                    else if (skill.Class == Skill.SkillClass.Weapon)
                    {
                        // Virushi always start at level 0 for weapon skills
                        if (!character.Skills.ContainsKey(skill.Name))
                        {
                            skill.Level = 0;
                        }
                    }
                    break;
            }
            return check;
        }

        public BasicCareer GetBasicCareer(Career.CareerType career)
        {
            switch (career)
            {
                case Career.CareerType.Imperial_Marines:
                    return new BasicMarines() { Culture = this };
                case Career.CareerType.Imperial_Army:
                    return new BasicArmy() { Culture = this };
                case Career.CareerType.Imperial_Navy:
                    return new BasicNavy() { Culture = this };
                case Career.CareerType.Imperial_Merchants:
                    return new BasicMerchants() { Culture = this };
                default:
                    return new BasicArmy() { Culture = this };
            }
        }

        public Dictionary<string, Character.Species> Species(Constants.GenerationStyle generationStyle)
        {
            var list = new Dictionary<string, Character.Species>();
            switch (generationStyle)
            {
                case Constants.GenerationStyle.Classic_Traveller:
                    list.Add("Human", Character.Species.Human_Imperial);
                    list.Add("Dolphin", Character.Species.Dolphin);
                    list.Add("Aslan", Character.Species.Aslan);
                    list.Add("Vargr", Character.Species.Vargr);
                    list.Add("Ael Yael", Character.Species.AelYael);
                    list.Add("Virushi", Character.Species.Virushi);
                    list.Add("Bwap", Character.Species.Bwap);
                    list.Add("Vegan", Character.Species.Vegan);
                    break;
            }
            
            return list;
        }

        public BasicCareer Drafted(Character character)
        {
            var dice = new Dice(6);
            switch (dice.roll(1))
            {
                case 1:
                    return new BasicNavy() { Culture = this, Drafted = true };
                case 2:
                    return new BasicMarines() { Culture = this, Drafted = true };
                case 3:
                    return new BasicArmy() { Culture = this, Drafted = true };
                case 4:
                    return new BasicMerchants() { Culture = this, Drafted = true };
            }
            return new BasicArmy();
        }
    }
}
