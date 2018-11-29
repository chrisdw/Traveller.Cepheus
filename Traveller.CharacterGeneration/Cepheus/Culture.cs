using org.DownesWard.Utilities;
using System;
using System.Collections.Generic;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus
{
    public class Culture : ICulture
    {
        private Dice dice = new Dice(6);

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
                    careers.Add("Aerospace Defence", CharacterGeneration.Career.CareerType.Cepheus_Aerospace_Defence);
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
            switch (dice.roll(1))
            {
                case 1:
                    return new AerospaceDefence() { Culture = this };
                    //case 2: Marine
                    //case 3: Maritime defence
                    //case 4: Navy
                    //case 5: Scout
                    //case 6: Surface Defence
            }
            throw new NotImplementedException();
        }

        public BasicCareer GetBasicCareer(CharacterGeneration.Career.CareerType career)
        {
            switch (career)
            {
                case CharacterGeneration.Career.CareerType.Cepheus_Athlete:
                    return new Athlete { Culture = this };
                case CharacterGeneration.Career.CareerType.Cepheus_Aerospace_Defence:
                    return new AerospaceDefence { Culture = this };
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
                    list.Add(Properties.Resources.Species_Human, Character.Species.Human);
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
