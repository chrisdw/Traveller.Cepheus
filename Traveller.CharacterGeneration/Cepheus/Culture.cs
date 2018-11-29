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

        public bool UseMishaps { get; set; }

        public bool BenefitAllowed(CharacterGeneration.Character character, Benefit benefit)
        {
            return true;
        }

        public Dictionary<string, CharacterGeneration.Career.CareerType> Careers(CharacterGeneration.Character character)
        {
            var careers = new Dictionary<string, CharacterGeneration.Career.CareerType>();
            switch (character.Style)
            {
                case Constants.GenerationStyle.Cepheus_Engine:
                    careers.Add("Athlete", CharacterGeneration.Career.CareerType.Cepheus_Athlete);
                    careers.Add("Aerospace Defence", CharacterGeneration.Career.CareerType.Cepheus_Aerospace_Defence);
                    careers.Add("Drifter", CharacterGeneration.Career.CareerType.Cepheus_Drifter);
                    careers.Add("Marine", CharacterGeneration.Career.CareerType.Cepheus_Marine);
                    careers.Add("Maritime Defence", CharacterGeneration.Career.CareerType.Cepheus_Maritime_Defence);
                    break;
            }
            return careers;
        }

        public bool CheckSkill(CharacterGeneration.Character character, Skill skill, int count)
        {
            return true; ;
        }

        public BasicCareer Drafted(CharacterGeneration.Character character)
        {
            switch (dice.roll(1))
            {
                case 1:
                    return new AerospaceDefence() { Culture = this };
                case 2:
                    return new Marine() { Culture = this };
                case 3:
                    return new MaritimeDefence() { Culture = this };

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
                    return new Athlete { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Cepheus_Aerospace_Defence:
                    return new AerospaceDefence { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Cepheus_Drifter:
                    return new Drifter { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Cepheus_Marine:
                    return new Marine { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Cepheus_Maritime_Defence:
                    return new MaritimeDefence { Culture = this, Mishaps = UseMishaps };
                default:
                    return new Athlete { Culture = this, Mishaps = UseMishaps };
            }
        }

        public Dictionary<string, CharacterGeneration.Character.Species> Species(Constants.GenerationStyle generationStyle)
        {
            var list = new Dictionary<string, CharacterGeneration.Character.Species>();
            switch (generationStyle)
            {
                case Constants.GenerationStyle.Cepheus_Engine:
                    list.Add(Properties.Resources.Species_Human, CharacterGeneration.Character.Species.Human);
                    list.Add("Avians", CharacterGeneration.Character.Species.Avian);
                    list.Add("Insectans", CharacterGeneration.Character.Species.Insectans);
                    list.Add("Merfolk", CharacterGeneration.Character.Species.Merfolk);
                    list.Add("Repltilians", CharacterGeneration.Character.Species.Reptilians);
                    break;
            }

            return list;
        }

        public int TableModifier(CharacterGeneration.Character character, CharacterGeneration.Career career, SkillTable table)
        {
            return 0; ;
        }
    }
}
