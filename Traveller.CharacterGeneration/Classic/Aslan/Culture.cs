using org.DownesWard.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Aslan
{
    public class Culture : ICulture
    {
        public Constants.CultureType Id => Constants.CultureType.Aslan;

        public bool MultipleCareers => true;

        public int ROPScore { get; set; }
        private Dice dice = new Dice(6);

        static Dictionary<string, Skill> offered = new Dictionary<string, Skill>();

        public bool BenefitAllowed(Character character, Benefit benefit)
        {
            if (character.Sex.Equals(Properties.Resources.Sex_Female) && 
                benefit.Name.Equals(BenefitLibrary.Independance.Name))
            {
                benefit.Name = BenefitLibrary.Weapon.Name;
            }
            return true;
        }

        public Dictionary<string, CharacterGeneration.Career.CareerType> Careers(Character character)
        {
            var careers = new Dictionary<string, CharacterGeneration.Career.CareerType>();
            switch (character.Style)
            {
                case Constants.GenerationStyle.Classic_Traveller:
                    careers.Add("Space", CharacterGeneration.Career.CareerType.Aslan_Space);
                    if (character.Profile.Soc.Value > 9)
                    {
                        careers.Add("Space Officer", CharacterGeneration.Career.CareerType.Aslan_Space_Officer);
                    }
                    careers.Add("Military", CharacterGeneration.Career.CareerType.Aslan_Military);
                    if (character.Profile.Soc.Value > 9)
                    {
                        careers.Add("Military Officer", CharacterGeneration.Career.CareerType.Aslan_Military_Officer);
                    }
                    if (character.Careers.Any(c => c.Name.Equals("Outcast")))
                    {
                        careers.Add("Pirate", CharacterGeneration.Career.CareerType.Aslan_Pirate);
                    }
                    if (character.Sex.Equals(Properties.Resources.Sex_Male))
                    {
                        careers.Add("Wanderer", CharacterGeneration.Career.CareerType.Aslan_Wanderer);
                        careers.Add("Flyer", CharacterGeneration.Career.CareerType.Aslan_Flyer);
                        if (character.Profile.Soc.Value > 10)
                        {
                            careers.Add("Envoy", CharacterGeneration.Career.CareerType.Aslan_Envoy);
                        }
                    }
                    else
                    {
                        careers.Add("Management", CharacterGeneration.Career.CareerType.Aslan_Management);
                        careers.Add("Scientist", CharacterGeneration.Career.CareerType.Aslan_Scientist);
                        careers.Add("Medic", CharacterGeneration.Career.CareerType.Aslan_Medic);
                        careers.Add("Belter", CharacterGeneration.Career.CareerType.Aslan_Belter);
                    }
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



            if (skill.Name.Equals(CharacterGeneration.SkillLibrary.Brawling.Name))
            {
                skill.Name = CharacterGeneration.SkillLibrary.DewClaw.Name;
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

            if (skill.Name.Equals(CharacterGeneration.SkillLibrary.Independance.Name)
                && character.Sex.Equals(Properties.Resources.Sex_Male))
            {
                check = (dice.roll(2) < character.Profile.Soc.Value);
            }
            return check;
        }

        public BasicCareer Drafted(Character character)
        {
            character.Profile.Soc.Value = 2;
            // Set to outcast
            return new Outcast { Culture = this };
        }

        public BasicCareer GetBasicCareer(CharacterGeneration.Career.CareerType career)
        {
            switch (career)
            {
                case CharacterGeneration.Career.CareerType.Aslan_Space:
                    return new Space { Culture = this, Officer = false };
                case CharacterGeneration.Career.CareerType.Aslan_Space_Officer:
                    return new Space { Culture = this, Officer = true };
                case CharacterGeneration.Career.CareerType.Aslan_Military:
                    return new Military { Culture = this, Officer = false };
                case CharacterGeneration.Career.CareerType.Aslan_Military_Officer:
                    return new Military { Culture = this, Officer = true };
                case CharacterGeneration.Career.CareerType.Aslan_Pirate:
                    return new Pirate { Culture = this };
                case CharacterGeneration.Career.CareerType.Aslan_Outcast:
                    return new Outcast { Culture = this };
                default:
                    return new Space { Culture = this, Officer = false };
            }
        }

        public Dictionary<string, Character.Species> Species(Constants.GenerationStyle generationStyle)
        {
            var list = new Dictionary<string, Character.Species>();
            switch (generationStyle)
            {
                case Constants.GenerationStyle.Classic_Traveller:
                    list.Add(Properties.Resources.Species_Human_Solomani, Character.Species.Human_Solomani);
                    list.Add(Properties.Resources.Species_Aslan, Character.Species.Aslan);
                    break;
            }

            return list;
        }

        public void CalculateROP(Character character)
        {
            var roll = dice.roll(2);
            var score = 0;

            if (character.Sex.Equals(Properties.Resources.Sex_Male))
            {
                score += character.Profile.Str.Value - roll;
                score += character.Profile.Dex.Value - roll;
                score += character.Profile.End.Value - roll;
                score += character.Profile.Int.Value - roll;
                score += character.Profile.Edu.Value - roll;
                score += character.Profile.Soc.Value - roll;
            }
            else if (character.Sex.Equals(Properties.Resources.Sex_Female))
            {
                score += (character.Profile.Int.Value - roll) * 2;
                score += (character.Profile.Edu.Value - roll) * 2;
                score += (character.Profile.Soc.Value - roll) * 2;
            }
            ROPScore = score;
        }

        public int TableModifier(Character character, CharacterGeneration.Career career, SkillTable table)
        {
            if (career.Name.Equals("Space") || career.Name.Equals("Military") || career.Name.Equals("Outcast")
                || career.Name.Equals("Management"))
            {
                if (table.Name.Equals("Personal Development") || table.Name.Equals("Service Skills"))
                {
                    if (character.Sex.Equals(Properties.Resources.Sex_Male))
                    {
                        return -1;
                    }
                    else
                    {
                        return +1;
                    }
                }
            }
            else if (career.Name.Equals("Pirate"))
            {
                if (character.Sex.Equals(Properties.Resources.Sex_Male))
                {
                    return -1;
                }
                else
                {
                    return +1;
                }
            }
            return 0;
        }
    }
}
