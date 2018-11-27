using org.DownesWard.Traveller.CharacterGeneration.Classic.Vargr;
using org.DownesWard.Utilities;
using System.Collections.Generic;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Imperial
{
    public class Culture : ICulture
    {
        public Constants.CultureType Id => Constants.CultureType.Imperial;

        public bool MultipleCareers => false;

        static Dictionary<string, Skill> offered = new Dictionary<string, Skill>();

        // Do we use the extra careers from Citizens of the the Imperium
        public bool UseCitizenRules { get; set; } = false;

        private Dice dice = new Dice(6);

        public bool BenefitAllowed(Character character, Benefit benefit)
        {
            // Imperial culture allows all benefits to everyone
            return true;
        }

        public Dictionary<string, CharacterGeneration.Career.CareerType> Careers(Character character)
        {
            var careers = new Dictionary<string, CharacterGeneration.Career.CareerType>();
            switch (character.Style)
            {
                case Constants.GenerationStyle.Classic_Traveller:
                    switch (character.CharacterSpecies)
                    {
                        case Character.Species.AelYael:
                            // Won't join merchants or become noble
                            careers.Add(Properties.Resources.Career_Army, CharacterGeneration.Career.CareerType.Imperial_Army);
                            careers.Add(Properties.Resources.Career_Marines, CharacterGeneration.Career.CareerType.Imperial_Marines);
                            careers.Add(Properties.Resources.Career_Navy, CharacterGeneration.Career.CareerType.Imperial_Navy);
                            careers.Add(Properties.Resources.Career_Scouts, CharacterGeneration.Career.CareerType.Imperial_Scouts);
                            if (UseCitizenRules)
                            {
                                AddCitizenCareersExceptNoble(careers);
                            }
                            else
                            {
                                careers.Add(Properties.Resources.Career_Other, CharacterGeneration.Career.CareerType.Imperial_Other);
                            }
                            break;
                        case Character.Species.Virushi:
                            // No military careers or noble
                            careers.Add(Properties.Resources.Career_Merchants, CharacterGeneration.Career.CareerType.Imperial_Merchants);
                            careers.Add(Properties.Resources.Career_Scouts, CharacterGeneration.Career.CareerType.Imperial_Scouts);
                            if (UseCitizenRules)
                            {
                                AddCitizenCareersExceptNoble(careers);
                            }
                            else
                            {
                                careers.Add(Properties.Resources.Career_Other, CharacterGeneration.Career.CareerType.Imperial_Other);
                            }
                            break;
                        case Character.Species.Dolphin:
                            careers.Add(Dolphin.Resources.Career_Civilian, CharacterGeneration.Career.CareerType.Dolphin_Civilian);
                            careers.Add(Dolphin.Resources.Career_Military, CharacterGeneration.Career.CareerType.Dolphin_Military);
                            break;
                        default:
                            careers.Add(Properties.Resources.Career_Army, CharacterGeneration.Career.CareerType.Imperial_Army);
                            careers.Add(Properties.Resources.Career_Marines, CharacterGeneration.Career.CareerType.Imperial_Marines);
                            careers.Add(Properties.Resources.Career_Navy, CharacterGeneration.Career.CareerType.Imperial_Navy);
                            careers.Add(Properties.Resources.Career_Merchants, CharacterGeneration.Career.CareerType.Imperial_Merchants);
                            careers.Add(Properties.Resources.Career_Scouts, CharacterGeneration.Career.CareerType.Imperial_Scouts);
                            if (UseCitizenRules)
                            {
                                AddCitizenCareersExceptNoble(careers);
                                if (character.CharacterSpecies == Character.Species.Vargr)
                                {
                                    careers.Add(Vargr.Resources.Career_Corsair, CharacterGeneration.Career.CareerType.Vargr_Corsair);
                                }
                                if (character.Profile.Soc.Value > 10)
                                {
                                    careers.Add(Citizen.Resources.Career_Noble, CharacterGeneration.Career.CareerType.Citizen_Noble);
                                }
                            }
                            else
                            {
                                careers.Add(Properties.Resources.Career_Other, CharacterGeneration.Career.CareerType.Imperial_Other);
                            }
                            break;
                    }
                    break;
            }
            return careers;
        }

        private static void AddCitizenCareersExceptNoble(Dictionary<string, CharacterGeneration.Career.CareerType> careers)
        {
            careers.Add(Citizen.Resources.Career_Barbarian, CharacterGeneration.Career.CareerType.Citizen_Barbarian);
            careers.Add(Citizen.Resources.Career_Belter, CharacterGeneration.Career.CareerType.Citizen_Belter);
            careers.Add(Citizen.Resources.Career_Bureaucrat, CharacterGeneration.Career.CareerType.Citizen_Bureaucrat);
            careers.Add(Citizen.Resources.Career_Diplomat, CharacterGeneration.Career.CareerType.Citizen_Diplomat);
            careers.Add(Citizen.Resources.Career_Doctor, CharacterGeneration.Career.CareerType.Citizen_Doctor);
            careers.Add(Citizen.Resources.Career_Flyer, CharacterGeneration.Career.CareerType.Citizen_Flyer);
            careers.Add(Citizen.Resources.Career_Hunter, CharacterGeneration.Career.CareerType.Citizen_Hunter);
            careers.Add(Citizen.Resources.Career_Pirate, CharacterGeneration.Career.CareerType.Citizen_Pirate);
            careers.Add(Citizen.Resources.Career_Rogue, CharacterGeneration.Career.CareerType.Citizen_Rogue);
            careers.Add(Citizen.Resources.Career_Sailor, CharacterGeneration.Career.CareerType.Citizen_Sailor);
            careers.Add(Citizen.Resources.Career_Scientist, CharacterGeneration.Career.CareerType.Citizen_Scientist);
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
                    if (skill.Name.Equals(SkillLibrary.Admin.Name))
                    {
                        if (!character.Skills.ContainsKey(SkillLibrary.Admin.Name))
                        {
                            // Bwap always get 2 levels of admin on the first go
                            skill.Level = 2;
                        }
                    }
                    break;
                case Character.Species.Vargr:
                    if (skill.Name.Equals(SkillLibrary.Brawling.Name))
                    {
                        skill.Name = SkillLibrary.Infighting.Name;
                    }
                    break;
                case Character.Species.Aslan:
                    if (skill.Name.Equals(SkillLibrary.Brawling.Name))
                    {
                        skill.Name = SkillLibrary.DewClaw.Name;
                    }
                    if (!offered.ContainsKey(skill.Name))
                    {
                        if (skill.SexApplicabilty == Skill.SkillSex.Female && character.Sex.Equals(Properties.Resources.Sex_Male))
                        {
                            check = false;
                        }
                        else if (skill.SexApplicabilty == Skill.SkillSex.Male && character.Sex.Equals(Properties.Resources.Sex_Female))
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

        public BasicCareer GetBasicCareer(CharacterGeneration.Career.CareerType career)
        {
            switch (career)
            {
                case CharacterGeneration.Career.CareerType.Imperial_Marines:
                    return new BasicMarines() { Culture = this };
                case CharacterGeneration.Career.CareerType.Imperial_Army:
                    return new BasicArmy() { Culture = this };
                case CharacterGeneration.Career.CareerType.Imperial_Navy:
                    return new BasicNavy() { Culture = this };
                case CharacterGeneration.Career.CareerType.Imperial_Merchants:
                    return new BasicMerchants() { Culture = this };
                case CharacterGeneration.Career.CareerType.Imperial_Scouts:
                    return new BasicScouts() { Culture = this };
                case CharacterGeneration.Career.CareerType.Imperial_Other:
                    return new BasicOther() { Culture = this };
                case CharacterGeneration.Career.CareerType.Citizen_Barbarian:
                    return new Citizen.Barbarian() { Culture = this };
                case CharacterGeneration.Career.CareerType.Citizen_Belter:
                    return new Citizen.Belter() { Culture = this };
                case CharacterGeneration.Career.CareerType.Citizen_Bureaucrat:
                    return new Citizen.Bureaucrat() { Culture = this };
                case CharacterGeneration.Career.CareerType.Citizen_Diplomat:
                    return new Citizen.Diplomat() { Culture = this };
                case CharacterGeneration.Career.CareerType.Citizen_Doctor:
                    return new Citizen.Doctor() { Culture = this };
                case CharacterGeneration.Career.CareerType.Citizen_Flyer:
                    return new Citizen.Flyer() { Culture = this };
                case CharacterGeneration.Career.CareerType.Citizen_Hunter:
                    return new Citizen.Hunter() { Culture = this };
                case CharacterGeneration.Career.CareerType.Citizen_Noble:
                    return new Citizen.Noble() { Culture = this };
                case CharacterGeneration.Career.CareerType.Citizen_Pirate:
                    return new Citizen.Pirate() { Culture = this };
                case CharacterGeneration.Career.CareerType.Citizen_Rogue:
                    return new Citizen.Rogue() { Culture = this };
                case CharacterGeneration.Career.CareerType.Citizen_Sailor:
                    return new Citizen.Sailor() { Culture = this };
                case CharacterGeneration.Career.CareerType.Citizen_Scientist:
                    return new Citizen.Scientist() { Culture = this };
                case CharacterGeneration.Career.CareerType.Vargr_Corsair:
                    return new Corsair(Corsair.Mode.Imperial) { Culture = this };
                case CharacterGeneration.Career.CareerType.Dolphin_Civilian:
                    return new Dolphin.Civilian() { Culture = this };
                case CharacterGeneration.Career.CareerType.Dolphin_Military:
                    return new Dolphin.Military() { Culture = this };
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
                    list.Add(Properties.Resources.Species_Human, Character.Species.Human_Imperial);
                    list.Add(Properties.Resources.Species_Dolphin, Character.Species.Dolphin);
                    list.Add(Properties.Resources.Species_Aslan, Character.Species.Aslan);
                    list.Add(Properties.Resources.Species_Vargr, Character.Species.Vargr);
                    list.Add(Properties.Resources.Species_AelYael, Character.Species.AelYael);
                    list.Add(Properties.Resources.Species_Virushi, Character.Species.Virushi);
                    list.Add(Properties.Resources.Species_Bwap, Character.Species.Bwap);
                    list.Add(Properties.Resources.Species_Vegan, Character.Species.Vegan);
                    break;
            }
            
            return list;
        }

        public BasicCareer Drafted(Character character)
        {
            switch (dice.roll(1))
            {
                case 1:
                    return new BasicNavy() { Culture = this };
                case 2:
                    return new BasicMarines() { Culture = this };
                case 3:
                    return new BasicArmy() { Culture = this };
                case 4:
                    return new BasicMerchants() { Culture = this };
                case 5:
                    return new BasicScouts() { Culture = this };
                case 6:
                    return new BasicOther() { Culture = this };
            }
            // Should never reach here
            return new BasicArmy() { Culture = this };
        }

        public int TableModifier(Character character, CharacterGeneration.Career career, SkillTable table)
        {
            return 0;
        }
    }
}
