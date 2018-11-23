﻿using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Aslan
{
    public class Culture : ICulture
    {
        public Constants.CultureType Id => Constants.CultureType.Aslan;

        public bool MultipleCareers => false;

        static Dictionary<string, Skill> offered = new Dictionary<string, Skill>();

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
                    careers.Add("Space", CharacterGeneration.Career.CareerType.Aslan_Space);
                    careers.Add("Space Officer", CharacterGeneration.Career.CareerType.Aslan_Space_Officer);
                    careers.Add("Military", CharacterGeneration.Career.CareerType.Aslan_Military);
                    careers.Add("Military Officer", CharacterGeneration.Career.CareerType.Aslan_Military_Officer);
                    careers.Add("Pirate", CharacterGeneration.Career.CareerType.Aslan_Pirate);
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

            return check;
        }

        public BasicCareer Drafted(Character character)
        {
            character.Profile.Soc.Value = 2;
            // Set to outcast
            throw new NotImplementedException();
        }

        public BasicCareer GetBasicCareer(CharacterGeneration.Career.CareerType career)
        {
            throw new NotImplementedException();
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
    }
}
