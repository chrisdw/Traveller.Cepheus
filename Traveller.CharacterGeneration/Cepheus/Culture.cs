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
                    careers.Add(Resources.Career_AerospaceDefence, CharacterGeneration.Career.CareerType.Cepheus_Aerospace_Defence);
                    careers.Add(Resources.Career_Agent, CharacterGeneration.Career.CareerType.Cepheus_Agent);
                    careers.Add(Resources.Career_Athlete, CharacterGeneration.Career.CareerType.Cepheus_Athlete);
                    careers.Add(Resources.Career_Barbarian, CharacterGeneration.Career.CareerType.Cepheus_Barbarian);
                    careers.Add(Resources.Career_Drifter, CharacterGeneration.Career.CareerType.Cepheus_Drifter);
                    careers.Add(Resources.Career_Marine, CharacterGeneration.Career.CareerType.Cepheus_Marine);
                    careers.Add(Resources.Career_MaritimeDefence, CharacterGeneration.Career.CareerType.Cepheus_Maritime_Defence);
                    careers.Add(Resources.Career_Navy, CharacterGeneration.Career.CareerType.Cepheus_Navy);
                    careers.Add(Resources.Career_Scout, CharacterGeneration.Career.CareerType.Cepheus_Scout);
                    careers.Add(Resources.Career_SurfaceDefence, CharacterGeneration.Career.CareerType.Cepheus_Surface_Defence);
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
                case CharacterGeneration.Career.CareerType.Cepheus_Agent:
                    return new Agent { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Cepheus_Barbarian:
                    return new Barbarian { Culture = this, Mishaps = UseMishaps };
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
                    list.Add(Resources.Species_Avians, CharacterGeneration.Character.Species.Avian);
                    list.Add(Resources.Species_Insectans, CharacterGeneration.Character.Species.Insectans);
                    list.Add(Resources.Species_Merfolk, CharacterGeneration.Character.Species.Merfolk);
                    list.Add(Resources.Species_Repltilians, CharacterGeneration.Character.Species.Reptilians);
                    list.Add(Resources.Species_UplifitedDolphin, CharacterGeneration.Character.Species.Dolphin);
                    list.Add(Resources.Species_UplifitedApe, CharacterGeneration.Character.Species.Uplifited_Ape);
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
