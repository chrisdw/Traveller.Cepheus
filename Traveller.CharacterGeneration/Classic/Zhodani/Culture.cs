using System;
using System.Collections.Generic;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Zhodani
{
    public class Culture : ICulture
    {
        public Constants.CultureType Id => Constants.CultureType.Zhodani;

        public bool MultipleCareers => false;

        public bool BenefitAllowed(Character character, Benefit benefit)
        {
            if (benefit.TypeOfBenefit == Benefit.BenefitType.AttributeModification)
            { 
                if (benefit.Name.Equals("SOC") &&
                    character.Profile.Soc.Value <= 10 &&
                    character.Profile.Soc.Value + benefit.Value > 8)
                {
                    return false;
                }
                else if (benefit.Name.Equals("EDU") &&
                    character.Profile.Edu.Value + benefit.Value > character.Profile.Soc.Value)
                {
                    return false;
                }
            }
            return true;
        }

        public Dictionary<string, Career.CareerType> Careers(Character character)
        {
            var careers = new Dictionary<string, Career.CareerType>();
            switch (character.Style)
            {
                case Constants.GenerationStyle.Classic_Traveller:
                    careers.Add("Navy", Career.CareerType.Zhodani_Navy);
                    if (character.Profile.Soc.Value >= 10 || character.Profile["PSI"].Value >= 9)
                    {
                        careers.Add("Consular Guard", Career.CareerType.Zhodani_ConsularGuard);
                    }
                    careers.Add("Army", Career.CareerType.Zhodani_Army);
                    careers.Add("Merchants", Career.CareerType.Zhodani_Merchant);
                    careers.Add("Government", Career.CareerType.Zhodani_Government);
                    careers.Add("Prole", Career.CareerType.Zhodani_Prole);
                    break;
            }
            return careers;
        }

        public bool CheckSkill(Character character, Skill skill, int count)
        {
            throw new NotImplementedException();
        }

        public BasicCareer Drafted(Character character)
        {
            throw new NotImplementedException();
        }

        public BasicCareer GetBasicCareer(Career.CareerType career)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, Character.Species> Species(Constants.GenerationStyle generationStyle)
        {
            var list = new Dictionary<string, Character.Species>();
            switch (generationStyle)
            {
                case Constants.GenerationStyle.Classic_Traveller:
                    list.Add("Human (Zhodani)", Character.Species.Human_Zhodani);
                    break;
            }

            return list;
        }
    }
}
