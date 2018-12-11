using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Commonwealth.Khiff
{
    public class Culture : ICulture
    {
        public bool UseMishaps { get; set; }

        public Constants.CultureType Id => Constants.CultureType.Cepheus_Khiff;

        public bool MultipleCareers => false;

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
                    careers.Add(Resources.Career_Colonist, CharacterGeneration.Career.CareerType.Cepheus_Colonist);
                    careers.Add(Resources.Career_Hunter, CharacterGeneration.Career.CareerType.Cepheus_Hunter);
                    careers.Add(Resources.Career_Mercenary, CharacterGeneration.Career.CareerType.Cepheus_Mercenary);
                    careers.Add(Resources.Career_SurfaceDefence, CharacterGeneration.Career.CareerType.Cepheus_Surface_Defence);
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
            return new SurfaceDefence() { Culture = this };
        }

        public BasicCareer GetBasicCareer(CharacterGeneration.Career.CareerType career)
        {
            switch (career)
            {
                case CharacterGeneration.Career.CareerType.Cepheus_Surface_Defence:
                    return new SurfaceDefence { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Cepheus_Colonist:
                    return new Colonist { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Cepheus_Hunter:
                    return new Hunter { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Cepheus_Mercenary:
                    return new Mercenary { Culture = this, Mishaps = UseMishaps };
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
                    list.Add("K'Hiff", CharacterGeneration.Character.Species.Khiff);
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
