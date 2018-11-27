using org.DownesWard.Utilities;
using System.Collections.Generic;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Darrian
{
    public class Culture : ICulture
    {
        public Constants.CultureType Id => Constants.CultureType.Darrian;

        static Dictionary<string, Skill> offered = new Dictionary<string, Skill>();

        public bool MultipleCareers => false;


        private Dice dice = new Dice(6);

        public bool BenefitAllowed(Character character, Benefit benefit)
        {
            return true;
        }

        public Dictionary<string, CharacterGeneration.Career.CareerType> Careers(Character character)
        {
            var careers = new Dictionary<string, CharacterGeneration.Career.CareerType>();
            switch (character.Style)
            {
                case Constants.GenerationStyle.Classic_Traveller:
                    careers.Add(Resources.Career_Navy, CharacterGeneration.Career.CareerType.Darrian_Navy);
                    careers.Add(Resources.Career_SpecialArm, CharacterGeneration.Career.CareerType.Darrian_SpecialArm);
                    careers.Add(Resources.Career_Army, CharacterGeneration.Career.CareerType.Darrian_Army);
                    careers.Add(Resources.Career_Merchants, CharacterGeneration.Career.CareerType.Darrian_Merchant);
                    careers.Add(Resources.Career_Noble, CharacterGeneration.Career.CareerType.Darrian_Noble);
                    careers.Add(Resources.Career_Academic, CharacterGeneration.Career.CareerType.Darrian_Academic);
                    break;
            }
            return careers;
        }

        public bool CheckSkill(Character character, Skill skill, int count)
        {
            var check = true;
            if (count == 1 && offered.Count > 0)
            {
                offered.Clear();
            }
            switch (character.CharacterSpecies)
            {
                case Character.Species.Aslan:
                    if (skill.Name.Equals(SkillLibrary.Brawling.Name))
                    {
                        skill.Name = SkillLibrary.DewClaw.Name;
                    }
                    if (!offered.ContainsKey(skill.Name))
                    {
                        if (skill.SexApplicabilty == Skill.SkillSex.Female && character.Sex.Equals(Properties.Resources.Sex_Male))
                        {
                            check = false;
                        }
                        else if (skill.SexApplicabilty == Skill.SkillSex.Male && character.Sex.Equals(Properties.Resources.Sex_Female))
                        {
                            check = false;
                        }
                        if (!check)
                        {
                            offered.Add(skill.Name, skill);
                        }
                    }
                    break;
                default:
                    check = true;
                    break;
            }
            return check;
        }

        public BasicCareer Drafted(Character character)
        {
            switch (dice.roll(1))
            {
                case 1:
                    return new Navy() { Culture = this };
                case 2:
                    return new SpecialArm() { Culture = this };
                case 3:
                    return new Army() { Culture = this };
                case 4:
                    return new Merchants() { Culture = this };
                case 5:
                    return new Noble() { Culture = this };
                case 6:
                    return new Academic() { Culture = this };
            }
            // Should never reach here
            return new Army() { Culture = this };
        }

        public BasicCareer GetBasicCareer(CharacterGeneration.Career.CareerType career)
        {
            switch (career)
            {
                case CharacterGeneration.Career.CareerType.Darrian_SpecialArm:
                    return new SpecialArm() { Culture = this };
                case CharacterGeneration.Career.CareerType.Darrian_Army:
                    return new Army() { Culture = this };
                case CharacterGeneration.Career.CareerType.Darrian_Navy:
                    return new Navy() { Culture = this };
                case CharacterGeneration.Career.CareerType.Darrian_Merchant:
                    return new Merchants() { Culture = this };
                case CharacterGeneration.Career.CareerType.Darrian_Noble:
                    return new Noble() { Culture = this };
                case CharacterGeneration.Career.CareerType.Darrian_Academic:
                    return new Academic() { Culture = this };
                default:
                    return new Army() { Culture = this };
            }
        }

        public Dictionary<string, Character.Species> Species(Constants.GenerationStyle generationStyle)
        {
            var list = new Dictionary<string, Character.Species>();
            switch (generationStyle)
            {
                case Constants.GenerationStyle.Classic_Traveller:
                    list.Add(Properties.Resources.Species_Human_Darrian, Character.Species.Human_Darrian);
                    list.Add(Properties.Resources.Species_Human_Solomani, Character.Species.Human_Solomani);
                    list.Add(Properties.Resources.Species_Aslan, Character.Species.Aslan);
                    break;
            }

            return list;
        }

        public int TableModifier(Character character, CharacterGeneration.Career career, SkillTable table)
        {
            return 0;
        }
    }
}
