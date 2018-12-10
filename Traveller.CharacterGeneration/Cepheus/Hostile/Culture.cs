using org.DownesWard.Utilities;
using System.Collections.Generic;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Hostile
{
    public class Culture : ICulture
    {
        private Dice dice = new Dice(6);

        public Constants.CultureType Id => Constants.CultureType.Cepheus_Hostile;

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
                    careers.Add(Resources.Career_Colonist, CharacterGeneration.Career.CareerType.Hostile_Colonist);
                    careers.Add(Resources.Career_CommercialSpacer, CharacterGeneration.Career.CareerType.Hostile_CommercialSpacer);
                    careers.Add(Resources.Career_CorporateAgent, CharacterGeneration.Career.CareerType.Hostile_CorporateAgent);
                    careers.Add(Resources.Career_CorporateExecutive, CharacterGeneration.Career.CareerType.Hostile_CorporateExec);
                    careers.Add(Resources.Career_Marine, CharacterGeneration.Career.CareerType.Hostile_Marine);
                    careers.Add(Resources.Career_Marshal, CharacterGeneration.Career.CareerType.Hostile_Marshall);
                    careers.Add(Resources.Career_MilitarySpacer, CharacterGeneration.Career.CareerType.Hostile_MilitarySpacer);
                    careers.Add(Resources.Career_Ranger, CharacterGeneration.Career.CareerType.Hostile_Ranger);
                    careers.Add(Resources.Career_Roughneck, CharacterGeneration.Career.CareerType.Hostile_Roughneck);
                    break;
            }
            return careers;
        }

        public bool CheckSkill(CharacterGeneration.Character character, Skill skill, int count)
        {
            return true;
        }

        public BasicCareer Drafted(CharacterGeneration.Character character)
        {
            switch (dice.roll(1))
            {
                case 1:
                    return new Ranger() { Culture = this, Mishaps = UseMishaps };
                case 2:
                case 3:
                case 4:
                    return new Colonist() { Culture = this, Mishaps = UseMishaps };
                case 5:
                case 6:
                    return new Roughneck() { Culture = this, Mishaps = UseMishaps };
            }
            return new Colonist() { Culture = this, Mishaps = UseMishaps };
        }

        public BasicCareer GetBasicCareer(CharacterGeneration.Career.CareerType career)
        {
            switch (career)
            {
                case CharacterGeneration.Career.CareerType.Hostile_Colonist:
                    return new Colonist { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Hostile_Ranger:
                    return new Ranger { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Hostile_Roughneck:
                    return new Roughneck { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Hostile_CorporateAgent:
                    return new CorporateAgent { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Hostile_CorporateExec:
                    return new CorporateExec { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Hostile_CommercialSpacer:
                    return new CommericalSpacer { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Hostile_Marine:
                    return new Marine { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Hostile_Marshall:
                    return new Marshal { Culture = this, Mishaps = UseMishaps };
                case CharacterGeneration.Career.CareerType.Hostile_MilitarySpacer:
                    return new MilitarySpacer { Culture = this, Mishaps = UseMishaps };
                default:
                    return new Colonist { Culture = this, Mishaps = UseMishaps };
            }
        }

        public Dictionary<string, CharacterGeneration.Character.Species> Species(Constants.GenerationStyle generationStyle)
        {
            var list = new Dictionary<string, CharacterGeneration.Character.Species>();
            switch (generationStyle)
            {
                case Constants.GenerationStyle.Cepheus_Engine:
                    list.Add(Properties.Resources.Species_Human, CharacterGeneration.Character.Species.Human);
                    list.Add(Resources.Species_Android, CharacterGeneration.Character.Species.Android);
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
