using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus
{
    public class Culture : ICulture
    {
        public Constants.CultureType Id => Constants.CultureType.Cepheus_Generic;

        public bool MultipleCareers => true;

        public bool BenefitAllowed(Character character, Benefit benefit)
        {
            return true;
        }

        public Dictionary<string, CharacterGeneration.Career.CareerType> Careers(Character character)
        {
            var careers = new Dictionary<string, CharacterGeneration.Career.CareerType>();
            switch (character.Style)
            {
                case Constants.GenerationStyle.Cepheus_Engine:
                    careers.Add("Athlete", CharacterGeneration.Career.CareerType.Cepheus_Athlete);
                    break;
            }
            return careers;
        }

        public bool CheckSkill(Character character, Skill skill, int count)
        {
            return true; ;
        }

        public BasicCareer Drafted(Character character)
        {
            throw new NotImplementedException();
        }

        public BasicCareer GetBasicCareer(CharacterGeneration.Career.CareerType career)
        {
            switch (career)
            {
                case CharacterGeneration.Career.CareerType.Cepheus_Athlete:
                    return new Athlete { Culture = this };
                default:
                    return new Athlete { Culture = this };
            }
        }

        public Dictionary<string, Character.Species> Species(Constants.GenerationStyle generationStyle)
        {
            var list = new Dictionary<string, Character.Species>();
            switch (generationStyle)
            {
                case Constants.GenerationStyle.Cepheus_Engine:
                    list.Add(Properties.Resources.Species_Human, Character.Species.Human_Imperial);
                    break;
            }

            return list;
        }

        public int TableModifier(Character character, CharacterGeneration.Career career, SkillTable table)
        {
            return 0; ;
        }
    }
}
