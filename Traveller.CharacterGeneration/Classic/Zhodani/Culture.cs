﻿using org.DownesWard.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Zhodani
{
    public class Culture : ICulture
    {
        public event EventHandler<CharacterGeneration.Career.SkillOfferedEventArgs> SkillOffered;

        public Constants.CultureType Id => Constants.CultureType.Zhodani;

        public bool MultipleCareers => false;

        private Dice dice = new Dice(6);

        public bool BenefitAllowed(Character character, Benefit benefit)
        {
            if (benefit.TypeOfBenefit == Benefit.BenefitType.AttributeModification)
            { 
                if (benefit.Name.Equals("SOC") &&
                    character.Profile.Soc.Value <= 10 &&
                    character.Profile.Soc.Value + benefit.Value > 8)
                {
                    return false;
                }
                else if (benefit.Name.Equals("EDU") &&
                    character.Profile.Edu.Value + benefit.Value > character.Profile.Soc.Value)
                {
                    return false;
                }
            }
            return true;
        }

        public Dictionary<string, Career.CareerType> Careers(Character character)
        {
            var careers = new Dictionary<string, Career.CareerType>();
            switch (character.Style)
            {
                case Constants.GenerationStyle.Classic_Traveller:
                    careers.Add(Resources.Career_Navy, Career.CareerType.Zhodani_Navy);
                    if (character.Profile.Soc.Value >= 10 || character.Profile["PSI"].Value >= 9)
                    {
                        careers.Add(Resources.Career_ConsularGuard, Career.CareerType.Zhodani_ConsularGuard);
                    }
                    careers.Add(Resources.Career_Army, Career.CareerType.Zhodani_Army);
                    careers.Add(Resources.Career_Merchant, Career.CareerType.Zhodani_Merchant);
                    careers.Add(Resources.Career_Government, Career.CareerType.Zhodani_Government);
                    careers.Add(Resources.Career_Prole, Career.CareerType.Zhodani_Prole);
                    break;
            }
            return careers;
        }

        public bool CheckSkill(Character character, Skill skill, int count)
        {
            if (skill.Class == Skill.SkillClass.Psionic)
            {
                if (character.Skills.Values.Where(s => s.Class == Skill.SkillClass.Psionic).Count() >= 6)
                {
                    skill.Class = Skill.SkillClass.AttributeChange;
                    skill.Name = "PSI";
                    if (character.Profile["PSI"].Value < 15)
                    {

                        skill.Level = 1;
                    }
                    else
                    {
                        skill.Level = 0;
                    }
                }
                else if (skill.Name.Equals(SkillLibrary.Talent.Name))
                {
                    var list = character.Skills.Values.Where(s => s.Class == Skill.SkillClass.Psionic).Select(s => s.Name);
                    var items = new List<string>()
                    {
                        SkillLibrary.Telepathy.Name,
                        SkillLibrary.Clairvoyance.Name,
                        SkillLibrary.Telekinesis.Name,
                        SkillLibrary.Awareness.Name,
                        SkillLibrary.Teleportation.Name,
                        SkillLibrary.Special.Name
                    };
                    var selectList = items.Except(list);
                    var cascade = GetSkill(selectList.ToList(), Skill.SkillClass.Psionic);
                    OnSkillOffered(cascade, character);
                }
                else
                {
                    if (character.Skills.Keys.Contains(skill.Name))
                    {
                        skill.Class = Skill.SkillClass.AttributeChange;
                        skill.Name = "PSI";
                        if (character.Profile["PSI"].Value < 15)
                        {

                            skill.Level = 1;
                        }
                        else
                        {
                            skill.Level = 0;
                        }
                    }
                    else
                    {
                        skill.Level = character.Profile["PSI"].Value;
                    }
                }
            }
            else if (skill.Class == Skill.SkillClass.Prole)
            {
                if (character.Skills.Values.Where(s => s.Class == Skill.SkillClass.Prole).Count() >= 3)
                {
                    var items = character.Skills.Values.Where(s => s.Class == Skill.SkillClass.Prole).Select(s => s.Name);
                    var cascade = GetSkill(items.ToList(), Skill.SkillClass.Prole);
                    OnSkillOffered(cascade, character);
                }
                else
                {
                    var items = new List<string>()
                    {
                        SkillLibrary.Admin.Name,
                        SkillLibrary.Broker.Name,
                        SkillLibrary.Computer.Name,
                        SkillLibrary.Electronics.Name,
                        SkillLibrary.Mechanical.Name,
                        SkillLibrary.Steward.Name,
                        SkillLibrary.Trader.Name
                    };
                    var cascade = GetSkill(items, Skill.SkillClass.Prole);
                    OnSkillOffered(cascade, character);
                }
            }
            else if (skill.Class == Skill.SkillClass.AttributeChange)
            {
                if (skill.Name.Equals("EDU"))
                {
                    if (character.Profile.Edu.Value + skill.Level > character.Profile.Soc.Value)
                    {
                        skill.Level = 0;
                    }
                }
            }
            return true;
        }

        public BasicCareer Drafted(Character character)
        {
            switch (dice.roll(1))
            {
                case 1:
                    return new Navy() { Culture = this };
                case 2:
                    return new ConsularGuard() { Culture = this };
                case 3:
                    return new Army() { Culture = this };
                case 4:
                    return new Merchant() { Culture = this };
                case 5:
                    return new Government() { Culture = this };
                case 6:
                    return new Prole() { Culture = this };
            }
            // Should never reach here
            return new Army() { Culture = this };
        }

        public BasicCareer GetBasicCareer(Career.CareerType career)
        {
            switch (career)
            {
                case CharacterGeneration.Career.CareerType.Zhodani_Army:
                    return new Army() { Culture = this };
                case CharacterGeneration.Career.CareerType.Zhodani_ConsularGuard:
                    return new ConsularGuard() { Culture = this };
                case CharacterGeneration.Career.CareerType.Zhodani_Government:
                    return new Government() { Culture = this };
                case CharacterGeneration.Career.CareerType.Zhodani_Merchant:
                    return new Merchant() { Culture = this };
                case CharacterGeneration.Career.CareerType.Zhodani_Navy:
                    return new Navy() { Culture = this };
                case CharacterGeneration.Career.CareerType.Zhodani_Prole:
                    return new Prole() { Culture = this };
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
                    list.Add(Properties.Resources.Species_Human_Zhodani, Character.Species.Human_Zhodani);
                    break;
            }

            return list;
        }

        private Skill GetSkill(List<string> names, Skill.SkillClass skillClass)
        {
            var cascade = new Skill() { Class = skillClass, Level = 1 };
            if (names.Count > 1)
            {
                foreach (var name in names)
                {
                    cascade.Cascade.Add(new Skill() { Name = name, Class = skillClass, Level = 1 });
                }
            }
            else
            {
                cascade.Name = names[0];
            }
            return cascade;
        }

        protected virtual void OnSkillOffered(Skill skill, Character character)
        {
            var e = new CharacterGeneration.Career.SkillOfferedEventArgs() { OfferedSkill = skill, Owner = character };
            SkillOffered?.Invoke(this, e);
        }

        public int TableModifier(Character character, CharacterGeneration.Career career, SkillTable table)
        {
            return 0;
        }
    }
}
