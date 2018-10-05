using org.DownesWard.Utilities;
using System.Collections.Generic;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Vargr
{
    public class Culture : ICulture
    {
        public Constants.CultureType Id => Constants.CultureType.Vargr;

        public bool MultipleCareers => true;

        private Dice dice = new Dice(6);

        public bool BenefitAllowed(Character character, Benefit benefit)
        {
            return true;
        }

        public Dictionary<string, CharacterGeneration.Career.CareerType> Careers(Character character)
        {
            var careers = new Dictionary<string, Career.CareerType>();
            switch (character.Style)
            {
                case Constants.GenerationStyle.Classic_Traveller:
                    careers.Add("Navy", CharacterGeneration.Career.CareerType.Vargr_Navy);
                    careers.Add("Corsairs", CharacterGeneration.Career.CareerType.Vargr_Corsair);
                    careers.Add("Army", CharacterGeneration.Career.CareerType.Vargr_Army);
                    careers.Add("Emissaries", CharacterGeneration.Career.CareerType.Vargr_Emissary);
                    careers.Add("Merchants", CharacterGeneration.Career.CareerType.Vargr_Merchant);
                    careers.Add("Merchants", CharacterGeneration.Career.CareerType.Vargr_Loner);
                    break;
            }
            return careers;
        }

        public bool CheckSkill(Character character, Skill skill, int count)
        {
            if (skill.Name.Equals("Brawling"))
            {
                skill.Name = "Infighting";
            }
            return true;
        }

        public BasicCareer Drafted(Character character)
        {
            switch (dice.roll(1))
            {
                case 2:
                    return new Corsair(Corsair.Mode.Vargr) { Culture = this };
                case 3:
                    return new Army() { Culture = this };
                case 4:
                    return new Emissary() { Culture = this };
                default:
                    return new Army() { Culture = this };
            }

        }

        public BasicCareer GetBasicCareer(CharacterGeneration.Career.CareerType career)
        {
            switch (career)
            {
                case CharacterGeneration.Career.CareerType.Vargr_Army:
                    return new Army() { Culture = this };
                case CharacterGeneration.Career.CareerType.Vargr_Corsair:
                    return new Corsair(Corsair.Mode.Vargr) { Culture = this };
                case CharacterGeneration.Career.CareerType.Vargr_Emissary:
                    return new Emissary() { Culture = this };
                default:
                    return new Army() { Culture = this };
            }
        }

        public Dictionary<string, Character.Species> Species(Constants.GenerationStyle generationStyle)
        {
            var list = new Dictionary<string, Character.Species>();
            switch (generationStyle)
            {
                case Constants.GenerationStyle.Classic_Traveller:
                    list.Add("Vargr", Character.Species.Vargr);
                    break;
            }

            return list;
        }
    }
}
