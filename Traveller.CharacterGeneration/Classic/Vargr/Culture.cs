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
                    careers.Add(Resources.Career_Navy, CharacterGeneration.Career.CareerType.Vargr_Navy);
                    careers.Add(Resources.Career_Corsair, CharacterGeneration.Career.CareerType.Vargr_Corsair);
                    careers.Add(Resources.Career_Army, CharacterGeneration.Career.CareerType.Vargr_Army);
                    careers.Add(Resources.Career_Emissary, CharacterGeneration.Career.CareerType.Vargr_Emissary);
                    careers.Add(Resources.Career_Merchant, CharacterGeneration.Career.CareerType.Vargr_Merchant);
                    careers.Add(Resources.Career_Loner, CharacterGeneration.Career.CareerType.Vargr_Loner);
                    break;
            }
            return careers;
        }

        public bool CheckSkill(Character character, Skill skill, int count)
        {
            if (skill.Name.Equals(SkillLibrary.Brawling.Name))
            {
                skill.Name = SkillLibrary.Infighting.Name;
            }
            return true;
        }

        public BasicCareer Drafted(Character character)
        {
            switch (dice.roll(1))
            {
                case 1:
                    return new Navy() { Culture = this };
                case 2:
                    return new Corsair(Corsair.Mode.Vargr) { Culture = this };
                case 3:
                    return new Army() { Culture = this };
                case 4:
                    return new Emissary() { Culture = this };
                case 5:
                    return new Merchant() { Culture = this };
                case 6:
                    return new Loner() { Culture = this };
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
                case CharacterGeneration.Career.CareerType.Vargr_Loner:
                    return new Loner() { Culture = this };
                case CharacterGeneration.Career.CareerType.Vargr_Merchant:
                    return new Merchant() { Culture = this };
                case CharacterGeneration.Career.CareerType.Vargr_Navy:
                    return new Navy() { Culture = this };
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
                    list.Add(Properties.Resources.Species_Vargr, Character.Species.Vargr);
                    break;
            }

            return list;
        }

        public int TableModifier(Character character, CharacterGeneration.Career career, SkillTable table)
        {
            return 0;
        }
    }
}
