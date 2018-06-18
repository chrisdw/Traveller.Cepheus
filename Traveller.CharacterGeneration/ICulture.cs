using System;
using System.Collections.Generic;
using System.Text;

namespace Traveller.CharacterGeneration
{
    public interface ICulture
    {
        Constants.CultureType Id { get; }
        List<Career> Careers(Character character);
        List<Character.Species> Species(Constants.GenerationStyle generationStyle);
        // BasicCareer Career(Career.CareerType career)
        // BasicCareer Drafted(Character character);
        bool CheckSkill(Character character, Skill skill, int count);
        bool BenefitAllowed(Character character, Benefit benefit);
    }
}
