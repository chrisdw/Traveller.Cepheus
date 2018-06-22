using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration
{
    public interface ICulture
    {
        Constants.CultureType Id { get; }
        Dictionary<string, Career.CareerType> Careers(Character character);
        Dictionary<string, Character.Species> Species(Constants.GenerationStyle generationStyle);
        BasicCareer Drafted(Character character);
        bool CheckSkill(Character character, Skill skill, int count);
        bool BenefitAllowed(Character character, Benefit benefit);

        BasicCareer GetBasicCareer(Career.CareerType career);
    }
}
