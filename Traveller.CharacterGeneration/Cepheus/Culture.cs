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
                    careers.Add("Navy", CharacterGeneration.Career.CareerType.Cepheus_Navy);
                    careers.Add("Scout", CharacterGeneration.Career.CareerType.Cepheus_Scout);
                    careers.Add("Surface Defence", CharacterGeneration.Career.CareerType.Cepheus_Surface_Defence);
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
                    return new AerospaceDefence() { Culture = this, Mishaps = UseMishaps };
                case 2:
                    return new Marine() { Culture = this, Mishaps = UseMishaps };
                case 3:
                    return new MaritimeDefence() { Culture = this, Mishaps = UseMishaps };
                case 4:
                    return new Navy() { Culture = this, Mishaps = UseMishaps };
                case 5:
                    return new Scout() { Culture = this, Mishaps = UseMishaps };
                case 6:
                    return new SurfaceDefence() { Culture = this, Mishaps = UseMishaps };
            }
            return new SurfaceDefence() { Culture = this, Mishaps = UseMishaps };
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
                case CharacterGeneration.Career.CareerType.Cepheus_Navy:
                    return new Navy { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Cepheus_Scout:
                    return new Scout { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Cepheus_Surface_Defence:
                    return new SurfaceDefence { Culture = this, Mishaps = UseMishaps };
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
