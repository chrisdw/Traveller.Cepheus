using org.DownesWard.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Commonwealth.Froog
{
    public class Culture : ICulture
    {
        private Dice dice = new Dice(6);

        public bool UseMishaps { get; set; }
        public Constants.CultureType Id => Constants.CultureType.Cepheus_Froog;

        public bool MultipleCareers => true;

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
                    careers.Add(Cepheus.Resources.Career_AerospaceDefence, CharacterGeneration.Career.CareerType.Cepheus_Aerospace_Defence);
                    careers.Add(Cepheus.Resources.Career_Agent, CharacterGeneration.Career.CareerType.Cepheus_Agent);
                    careers.Add(Cepheus.Resources.Career_Athlete, CharacterGeneration.Career.CareerType.Cepheus_Athlete);
                    careers.Add(Cepheus.Resources.Career_Bureaucrat, CharacterGeneration.Career.CareerType.Cepheus_Bureaucrat);
                    careers.Add(Cepheus.Resources.Career_Colonist, CharacterGeneration.Career.CareerType.Cepheus_Colonist);
                    if (character.Sex.Equals(Resources.Sex_Leader))
                    {
                        careers.Add(Cepheus.Resources.Career_Diplomat, CharacterGeneration.Career.CareerType.Cepheus_Diplomat);
                    }
                    careers.Add(Cepheus.Resources.Career_Entertainer, CharacterGeneration.Career.CareerType.Cepheus_Entertainer);
                    careers.Add(Cepheus.Resources.Career_Hunter, CharacterGeneration.Career.CareerType.Cepheus_Hunter);
                    careers.Add(Resources.Career_LicencedWitch, CharacterGeneration.Career.CareerType.Commonwealth_LicencedWitch);
                    careers.Add(Cepheus.Resources.Career_Marine, CharacterGeneration.Career.CareerType.Cepheus_Marine);
                    careers.Add(Cepheus.Resources.Career_MaritimeDefence, CharacterGeneration.Career.CareerType.Cepheus_Maritime_Defence);
                    careers.Add(Cepheus.Resources.Career_Mercenary, CharacterGeneration.Career.CareerType.Cepheus_Mercenary);
                    careers.Add(Cepheus.Resources.Career_Merchant, CharacterGeneration.Career.CareerType.Cepheus_Merchant);
                    careers.Add(Resources.Career_MilitaryWitch, CharacterGeneration.Career.CareerType.Commonwealth_MilitaryWitch);
                    careers.Add(Cepheus.Resources.Career_Navy, CharacterGeneration.Career.CareerType.Cepheus_Navy);
                    if (character.Sex.Equals(Resources.Sex_Leader))
                    {
                        careers.Add(Cepheus.Resources.Career_Noble, CharacterGeneration.Career.CareerType.Cepheus_Noble);
                    }
                    if (character.Sex.Equals(Resources.Sex_Technician))
                    {
                        careers.Add(Cepheus.Resources.Career_Physician, CharacterGeneration.Career.CareerType.Cepheus_Physician);
                    }
                    careers.Add(Cepheus.Resources.Career_Pirate, CharacterGeneration.Career.CareerType.Cepheus_Pirate);
                    if (character.Sex.Equals(Resources.Sex_Technician))
                    {
                        careers.Add(Cepheus.Resources.Career_Scientist, CharacterGeneration.Career.CareerType.Cepheus_Scientist);
                    }
                    careers.Add(Cepheus.Resources.Career_Scout, CharacterGeneration.Career.CareerType.Cepheus_Scout);
                    careers.Add(Cepheus.Resources.Career_SurfaceDefence, CharacterGeneration.Career.CareerType.Cepheus_Surface_Defence);
                    if (character.Sex.Equals(Resources.Sex_Technician))
                    {
                        careers.Add(Cepheus.Resources.Career_Technician, CharacterGeneration.Career.CareerType.Cepheus_Technician);
                    }
                    break;
            }
            return careers;
        }

        public bool CheckSkill(CharacterGeneration.Character character, Skill skill, int count)
        {
            // Only leaders can lead
            if (skill.Name.Equals(Properties.Resources.Skill_Leader) && !character.Sex.Equals(Resources.Sex_Leader))
            {
                return false;
            }
            // Only technicians have the flexibilty to be J-o-T
            if (skill.Name.Equals(Properties.Resources.Skill_JackOfTrades) && !character.Sex.Equals(Resources.Sex_Technician))
            {
                return false;
            }
            return true;
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
                case CharacterGeneration.Career.CareerType.Cepheus_Bureaucrat:
                    return new Bureaucrat { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Cepheus_Colonist:
                    return new Colonist { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Cepheus_Diplomat:
                    return new Diplomat { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Cepheus_Entertainer:
                    return new Entertainer { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Cepheus_Hunter:
                    return new Hunter { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Cepheus_Mercenary:
                    return new Mercenary { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Cepheus_Merchant:
                    return new Merchant { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Cepheus_Noble:
                    return new Noble { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Cepheus_Physician:
                    return new Physician { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Cepheus_Pirate:
                    return new Pirate { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Cepheus_Rogue:
                    return new Rogue { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Cepheus_Scientist:
                    return new Scientist { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Cepheus_Technician:
                    return new Technician { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Commonwealth_MilitaryWitch:
                    return new MilitaryWitch { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Commonwealth_RogueWitch:
                    return new RogueWitch { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Commonwealth_LicencedWitch:
                    return new LicencedWitch { Culture = this, Mishaps = UseMishaps };
                default:
                    return new SurfaceDefence { Culture = this, Mishaps = UseMishaps };
            }
        }

        public Dictionary<string, CharacterGeneration.Character.Species> Species(Constants.GenerationStyle generationStyle)
        {
            var list = new Dictionary<string, CharacterGeneration.Character.Species>();
            switch (generationStyle)
            {
                case Constants.GenerationStyle.Cepheus_Engine:
                    list.Add(Properties.Resources.Species_Human, CharacterGeneration.Character.Species.Commonwealth_Human);
                    list.Add(Resources.Species_Froog, CharacterGeneration.Character.Species.Froog);
                    break;
            }

            return list;
        }

        public int TableModifier(CharacterGeneration.Character character, CharacterGeneration.Career career, SkillTable table)
        {
            return 0;
        }
    }
}
