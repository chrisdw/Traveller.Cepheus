using org.DownesWard.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Commonwealth.Covenant
{
    public class Culture : ICulture
    {
        private Dice dice = new Dice(6);

        public Constants.CultureType Id =>  Constants.CultureType.Cepheus_Covenant;

        public bool MultipleCareers => false;

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
                    if (character.Sex.Equals(Properties.Resources.Sex_Female))
                    {
                        careers.Add(Cepheus.Resources.Career_Agent, CharacterGeneration.Career.CareerType.Cepheus_Agent);
                        careers.Add(Cepheus.Resources.Career_Athlete, CharacterGeneration.Career.CareerType.Cepheus_Athlete);
                        careers.Add(Cepheus.Resources.Career_Bureaucrat, CharacterGeneration.Career.CareerType.Cepheus_Bureaucrat);
                        careers.Add(Cepheus.Resources.Career_Colonist, CharacterGeneration.Career.CareerType.Cepheus_Colonist);
                        careers.Add(Cepheus.Resources.Career_Diplomat, CharacterGeneration.Career.CareerType.Cepheus_Diplomat);
                        careers.Add(Cepheus.Resources.Career_Drifter, CharacterGeneration.Career.CareerType.Cepheus_Drifter);
                        careers.Add(Cepheus.Resources.Career_Entertainer, CharacterGeneration.Career.CareerType.Cepheus_Entertainer);
                        careers.Add(Cepheus.Resources.Career_Merchant, CharacterGeneration.Career.CareerType.Cepheus_Merchant);
                        careers.Add(Cepheus.Resources.Career_Physician, CharacterGeneration.Career.CareerType.Cepheus_Physician);
                        careers.Add(Cepheus.Resources.Career_Pirate, CharacterGeneration.Career.CareerType.Cepheus_Pirate);
                        careers.Add(Cepheus.Resources.Career_Rogue, CharacterGeneration.Career.CareerType.Cepheus_Rogue);
                        careers.Add(Resources.Career_RogueWitch, CharacterGeneration.Career.CareerType.Commonwealth_RogueWitch);
                        careers.Add(Cepheus.Resources.Career_Scientist, CharacterGeneration.Career.CareerType.Cepheus_Scientist);
                        careers.Add(Cepheus.Resources.Career_Technician, CharacterGeneration.Career.CareerType.Cepheus_Technician);
                    }
                    else
                    {
                        careers.Add(Cepheus.Resources.Career_AerospaceDefence, CharacterGeneration.Career.CareerType.Cepheus_Aerospace_Defence);
                        careers.Add(Cepheus.Resources.Career_Agent, CharacterGeneration.Career.CareerType.Cepheus_Agent);
                        careers.Add(Cepheus.Resources.Career_Athlete, CharacterGeneration.Career.CareerType.Cepheus_Athlete);
                         careers.Add(Cepheus.Resources.Career_Bureaucrat, CharacterGeneration.Career.CareerType.Cepheus_Bureaucrat);
                        careers.Add(Cepheus.Resources.Career_Colonist, CharacterGeneration.Career.CareerType.Cepheus_Colonist);
                        careers.Add(Cepheus.Resources.Career_Diplomat, CharacterGeneration.Career.CareerType.Cepheus_Diplomat);
                        careers.Add(Cepheus.Resources.Career_Drifter, CharacterGeneration.Career.CareerType.Cepheus_Drifter);
                        careers.Add(Cepheus.Resources.Career_Entertainer, CharacterGeneration.Career.CareerType.Cepheus_Entertainer);
                        careers.Add(Cepheus.Resources.Career_Hunter, CharacterGeneration.Career.CareerType.Cepheus_Hunter);
                        careers.Add(Cepheus.Resources.Career_Marine, CharacterGeneration.Career.CareerType.Cepheus_Marine);
                        careers.Add(Cepheus.Resources.Career_MaritimeDefence, CharacterGeneration.Career.CareerType.Cepheus_Maritime_Defence);
                        careers.Add(Cepheus.Resources.Career_Mercenary, CharacterGeneration.Career.CareerType.Cepheus_Mercenary);
                        careers.Add(Cepheus.Resources.Career_Merchant, CharacterGeneration.Career.CareerType.Cepheus_Merchant);
                        careers.Add(Cepheus.Resources.Career_Navy, CharacterGeneration.Career.CareerType.Cepheus_Navy);
                        careers.Add(Cepheus.Resources.Career_Noble, CharacterGeneration.Career.CareerType.Cepheus_Noble);
                        careers.Add(Cepheus.Resources.Career_Physician, CharacterGeneration.Career.CareerType.Cepheus_Physician);
                        careers.Add(Cepheus.Resources.Career_Pirate, CharacterGeneration.Career.CareerType.Cepheus_Pirate);
                        careers.Add(Cepheus.Resources.Career_Rogue, CharacterGeneration.Career.CareerType.Cepheus_Rogue);
                        careers.Add(Resources.Career_RogueWitch, CharacterGeneration.Career.CareerType.Commonwealth_RogueWitch);
                        careers.Add(Cepheus.Resources.Career_Scientist, CharacterGeneration.Career.CareerType.Cepheus_Scientist);
                        careers.Add(Cepheus.Resources.Career_Scout, CharacterGeneration.Career.CareerType.Cepheus_Scout);
                        careers.Add(Cepheus.Resources.Career_SurfaceDefence, CharacterGeneration.Career.CareerType.Cepheus_Surface_Defence);
                        careers.Add(Cepheus.Resources.Career_Technician, CharacterGeneration.Career.CareerType.Cepheus_Technician);
                    }
                    break;
            }
            return careers;
        }

        public bool CheckSkill(CharacterGeneration.Character character, Skill skill, int count)
        {
            if (character.Sex.Equals(Properties.Resources.Sex_Male) && skill.SexApplicabilty == Skill.SkillSex.Female)
            {
                return false;
            }
            if (character.Sex.Equals(Properties.Resources.Sex_Female) && skill.SexApplicabilty == Skill.SkillSex.Male)
            {
                return false;
            }
            return true;
        }

        public BasicCareer Drafted(CharacterGeneration.Character character)
        {
            if (character.Sex.Equals(Properties.Resources.Sex_Female))
            {
                switch (dice.roll(1))
                {
                    case 1:
                        return new Agent() { Culture = this, Mishaps = UseMishaps };
                    case 2:
                        return new Colonist() { Culture = this, Mishaps = UseMishaps };
                    case 3:
                        return new Entertainer() { Culture = this, Mishaps = UseMishaps };
                    case 4:
                        return new Merchant() { Culture = this, Mishaps = UseMishaps };
                    case 5:
                        return new Scientist() { Culture = this, Mishaps = UseMishaps };
                    case 6:
                        return new Technician() { Culture = this, Mishaps = UseMishaps };
                }
            }
            else
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
                    return new Athlete { Culture = this, Mishaps = UseMishaps };
            }
        }

        public Dictionary<string, CharacterGeneration.Character.Species> Species(Constants.GenerationStyle generationStyle)
        {
            var list = new Dictionary<string, CharacterGeneration.Character.Species>();
            switch (generationStyle)
            {
                case Constants.GenerationStyle.Cepheus_Engine:
                    list.Add(Properties.Resources.Species_Human, CharacterGeneration.Character.Species.Commonwealth_Human);
                    list.Add(Resources.Species_Khiff, CharacterGeneration.Character.Species.Khiff);
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
