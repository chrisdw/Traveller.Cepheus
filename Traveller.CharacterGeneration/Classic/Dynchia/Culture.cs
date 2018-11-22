using org.DownesWard.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Dynchia
{
    public class Culture : ICulture
    {
        public Constants.CultureType Id => Constants.CultureType.Dynchia;

        public bool MultipleCareers => false;

        private Dice dice = new Dice(6);

        public bool BenefitAllowed(Character character, Benefit benefit)
        {
            return true;
        }

        public Dictionary<string, CharacterGeneration.Career.CareerType> Careers(Character character)
        {
            var careers = new Dictionary<string, CharacterGeneration.Career.CareerType>();
            switch (character.Style)
            {
                case Constants.GenerationStyle.Classic_Traveller:
                    careers.Add(Properties.Resources.Career_Army, CharacterGeneration.Career.CareerType.Imperial_Army);
                    careers.Add(Properties.Resources.Career_Marines, CharacterGeneration.Career.CareerType.Imperial_Marines);
                    careers.Add(Properties.Resources.Career_Navy, CharacterGeneration.Career.CareerType.Imperial_Navy);
                    careers.Add(Properties.Resources.Career_Merchants, CharacterGeneration.Career.CareerType.Imperial_Merchants);
                    careers.Add(Properties.Resources.Career_Scouts, CharacterGeneration.Career.CareerType.Imperial_Scouts);
                    careers.Add(Properties.Resources.Career_Other, CharacterGeneration.Career.CareerType.Imperial_Other);
                    break;
            }
            return careers;
        }

        public bool CheckSkill(Character character, Skill skill, int count)
        {
            return true;
        }

        public BasicCareer Drafted(Character character)
        {
            var roll = dice.roll();
            while (roll == 3)
            {
                roll = dice.roll();
            }
            switch (roll)
            {
                case 1:
                    return new Imperial.BasicNavy() { Culture = this };
                case 2:
                    return new Imperial.BasicMarines() { Culture = this };
                case 4:
                    return new Imperial.BasicMerchants() { Culture = this };
                case 5:
                    return new Imperial.BasicScouts() { Culture = this };
                case 6:
                    return new Imperial.BasicOther() { Culture = this };
            }
            // Should never reach here
            return new Imperial.BasicNavy() { Culture = this };
        }

        public BasicCareer GetBasicCareer(CharacterGeneration.Career.CareerType career)
        {
            switch (career)
            {
                case CharacterGeneration.Career.CareerType.Imperial_Marines:
                    return new Imperial.BasicMarines() { Culture = this };
                case CharacterGeneration.Career.CareerType.Imperial_Army:
                    return new Imperial.BasicArmy() { Culture = this };
                case CharacterGeneration.Career.CareerType.Imperial_Navy:
                    return new Imperial.BasicNavy() { Culture = this };
                case CharacterGeneration.Career.CareerType.Imperial_Merchants:
                    return new Imperial.BasicMerchants() { Culture = this };
                case CharacterGeneration.Career.CareerType.Imperial_Scouts:
                    return new Imperial.BasicScouts() { Culture = this };
                case CharacterGeneration.Career.CareerType.Imperial_Other:
                    return new Imperial.BasicOther() { Culture = this };
                default:
                    return new Imperial.BasicArmy() { Culture = this };
            }
        }

        public Dictionary<string, Character.Species> Species(Constants.GenerationStyle generationStyle)
        {
            var list = new Dictionary<string, Character.Species>();
            switch (generationStyle)
            {
                case Constants.GenerationStyle.Classic_Traveller:
                    list.Add(Properties.Resources.Species_Human_Dynchia, Character.Species.Human_Dynchia);
                    list.Add(Properties.Resources.Species_Human_Solomani, Character.Species.Human_Solomani);
                    break;
            }

            return list;
        }
    }
}
