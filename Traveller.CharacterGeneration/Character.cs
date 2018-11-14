﻿using org.DownesWard.Traveller.CharacterGeneration.Classic.Zhodani;
using org.DownesWard.Traveller.Shared;
using org.DownesWard.Traveller.Shared.Classic;
using org.DownesWard.Traveller.Shared.Classsic;
using org.DownesWard.Utilities;
using System;
using System.Collections.Generic;
using System.Xml;

namespace org.DownesWard.Traveller.CharacterGeneration
{
    public class Character
    {
        public enum Species
        {
            Human_Imperial,
            Human_Zhodani,
            Human_Solomani,
            Human_Darrian,
            Human_Dynchia,
            Human_SwordWorlds,
            Human_Irklan,
            Human_Vilani,
            KKree,
            Aslan,
            Vargr,
            Hiver,
            Dolphin,
            Droyne,
            Hlanssai,
            GirugKagh,
            AelYael,
            Virushi,
            Bwap,
            Ithulkur,
            Vegan
        }

        public string Sex { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public bool Died { get; set; }
        public Species CharacterSpecies { get; set; }
        public List<string> Journal { get; } = new List<string>();
        public Dictionary<string, Skill> Skills { get; } = new Dictionary<string, Skill>();
        public Dictionary<string, Benefit> Benefits { get; } = new Dictionary<string, Benefit>();

        public UPP Profile { get; set; }
        public List<Career> Careers { get; set; } = new List<Career>();

        public Constants.CultureType Culture { get; set; }
        public Constants.GenerationStyle Style { get; set; } = Constants.GenerationStyle.Classic_Traveller;

        private Dice dice = new Dice(6);

        public void Generate()
        {
            switch (Style)
            {
                case Constants.GenerationStyle.Classic_Traveller:
                    GenerateClassic();
                    break;
            }
        }

        private void GenerateClassic()
        {
            var dice = new Dice(6);

            switch (Culture)
            {
                case Constants.CultureType.Imperial:
                    switch (CharacterSpecies)
                    {
                        case Species.Human_Imperial:
                            // use the standard UPP
                            Profile = new UPP();
                            Profile.Str.Value = dice.roll(2);
                            Profile.Dex.Value = dice.roll(2);
                            Profile.End.Value = dice.roll(2);
                            Profile.Int.Value = dice.roll(2);
                            Profile.Edu.Value = dice.roll(2);
                            Profile.Soc.Value = dice.roll(2);
                            break;
                        case Species.Bwap:
                            Profile = new UPP();
                            Profile.Str.Value = dice.roll(2) - 4;
                            Profile.Dex.Value = dice.roll(2);
                            Profile.End.Value = dice.roll(2) - 4;
                            Profile.Int.Value = dice.roll(2);
                            Profile.Edu.Value = dice.roll(2);
                            Profile.Soc.Value = dice.roll(2);
                            break;
                        case Species.Aslan:
                            Profile = new AslanUPP();
                            Profile.Str.Value = dice.roll(2) + 1;
                            Profile.Dex.Value = dice.roll(2) - 1;
                            Profile.End.Value = dice.roll(2) + 1;
                            Profile.Int.Value = dice.roll(2);
                            Profile.Edu.Value = dice.roll(2);
                            Profile.Soc.Value = dice.roll(2);
                            // Imperial Aslan get some automatic skills
                            AddSkill(new Skill() {  Class = Skill.SkillClass.None, Level = 1, Name= "Tolerance", SexApplicabilty = Skill.SkillSex.DontCare });
                            if (Sex.Equals("Male"))
                            {
                                AddSkill(new Skill() { Class = Skill.SkillClass.None, Level = 1, Name = "Independance", SexApplicabilty = Skill.SkillSex.Male });
                            }
                            break;
                        case Species.Vargr:
                            Profile = new VargrUPP();
                            Profile.Str.Value = dice.roll(2) - 1;
                            Profile.Dex.Value = dice.roll(2) + 1;
                            Profile.End.Value = dice.roll(2) - 1;
                            Profile.Int.Value = dice.roll(2);
                            Profile.Edu.Value = dice.roll(2);
                            Profile.Soc.Value = dice.roll(2);
                            Profile["CHR"].Value = dice.roll();
                            break;
                        case Species.AelYael:
                            Profile = new UPP();
                            Profile.Str.Value = dice.roll(2) - 1;
                            Profile.Dex.Value = dice.roll(2);
                            Profile.End.Value = dice.roll(2);
                            Profile.Int.Value = dice.roll(2);
                            Profile.Edu.Value = dice.roll(2);
                            Profile.Soc.Value = dice.roll(2);
                            break;
                        case Species.Virushi:
                            Profile = new VirushiUPP();
                            Profile.Str.Value = dice.roll(2) + 10;
                            Profile.Dex.Value = dice.roll(2) + 2;
                            Profile.End.Value = dice.roll(2) + 10;
                            Profile.Int.Value = dice.roll(2);
                            Profile.Edu.Value = dice.roll(2);
                            Profile.Soc.Value = dice.roll(2) - 2;
                            break;
                        case Species.Vegan:
                            Profile = new UPP();
                            Profile.Str.Value = dice.roll(2);
                            Profile.Dex.Value = dice.roll(2) + 1;
                            Profile.End.Value = dice.roll(2);
                            Profile.Int.Value = dice.roll(2);
                            Profile.Edu.Value = dice.roll(2);
                            Profile.Soc.Value = dice.roll(2);
                            break;
                    }
                    break;
                case Constants.CultureType.Darrian:
                    switch (CharacterSpecies)
                    {
                        case Species.Aslan:
                            Profile = new AslanUPP();
                            Profile.Str.Value = dice.roll(2) + 1;
                            Profile.Dex.Value = dice.roll(2) - 1;
                            Profile.End.Value = dice.roll(2) + 1;
                            break;
                        case Species.Human_Solomani:
                            Profile = new UPP();
                            Profile.Str.Value = dice.roll(2);
                            Profile.Dex.Value = dice.roll(2);
                            Profile.End.Value = dice.roll(2);
                            break;
                        case Species.Human_Darrian:
                            Profile = new UPP();
                            Profile.Str.Value = dice.roll() + 3;
                            Profile.Dex.Value = dice.roll(2);
                            Profile.End.Value = dice.roll() + 3;
                            break;
                    }
                    Profile.Int.Value = dice.roll(2);
                    Profile.Edu.Value = dice.roll(2);
                    Profile.Soc.Value = dice.roll(2);
                    break;
                case Constants.CultureType.Vargr:
                    // Assume all are vargr
                    Profile = new VargrUPP();
                    Profile.Str.Value = dice.roll(2) - 1;
                    Profile.Dex.Value = dice.roll(2) + 1;
                    Profile.End.Value = dice.roll(2) - 1;
                    Profile.Int.Value = dice.roll(2);
                    Profile.Edu.Value = dice.roll(2);
                    Profile["CHR"].Value = dice.roll();
                    break;
                case Constants.CultureType.SwordWorlds:
                    // Assume all are standard humans
                    Profile = new UPP();
                    Profile.Str.Value = dice.roll(2);
                    Profile.Dex.Value = dice.roll(2);
                    Profile.End.Value = dice.roll(2);
                    Profile.Int.Value = dice.roll(2);
                    Profile.Edu.Value = dice.roll(2);
                    Profile.Soc.Value = dice.roll(2);
                    break;
                case Constants.CultureType.Dynchia:
                    Profile = new UPP();
                    switch (CharacterSpecies)
                    {
                        case Species.Human_Dynchia:
                            Profile.Str.Value = dice.roll() + 3;
                            Profile.Dex.Value = dice.roll(2) + 1;
                            Profile.End.Value = dice.roll(2) + 1;
                            break;
                        case Species.Human_Solomani:
                            Profile.Str.Value = dice.roll(2);
                            Profile.Dex.Value = dice.roll(2);
                            Profile.End.Value = dice.roll(2);
                            break;
                    }
                    Profile.Int.Value = dice.roll(2);
                    Profile.Edu.Value = dice.roll(2);
                    Profile.Soc.Value = dice.roll(2);
                    break;
                case Constants.CultureType.Zhodani:
                    Profile = new ZhodaniUPP();
                    Profile.Str.Value = dice.roll(2);
                    Profile.Dex.Value = dice.roll(2);
                    Profile.End.Value = dice.roll(2);
                    Profile.Int.Value = dice.roll(2);
                    Profile.Edu.Value = dice.roll(2);
                    Profile.Soc.Value = dice.roll(2);
                    Profile["PSI"].Value = dice.roll(2);
                    if (Profile["PSI"].Value >= 10 && Profile.Soc.Value < 10)
                    {
                        Profile.Soc.Value = 10;
                    }
                    if (Profile.Edu.Value > Profile.Soc.Value)
                    {
                        Profile.Edu.Value = Profile.Soc.Value;
                    }
                    if (Profile.Soc.Value >= 10 && Profile.Edu.Value < 8)
                    {
                        Profile.Edu.Value = 8;
                    }
                    // TODO: Check this logic
                    if (Profile.Str.Value + Profile.Dex.Value + Profile.End.Value + Profile.Int.Value < Profile["PSI"].Value)
                    {
                        Profile["PSI"].Value = Profile.Str.Value + Profile.Dex.Value + Profile.End.Value + Profile.Int.Value;
                    }
                    break;
            }
        }
        public void AddSkill(Skill skill)
        {
            Journal.Add(string.Format("Received skill {0} at level {1}", skill.Name, skill.Level));
            if (!Skills.ContainsKey(skill.Name))
            {
                Skills.Add(skill.Name, skill);
            }
            else
            {
                Skills[skill.Name].Level += skill.Level;
            }
        }

        public void AddBenefit(Benefit benefit)
        {
            Journal.Add(string.Format("Received benefit {0} of value {1}", benefit.Name, benefit.Value));
            if (Benefits.ContainsKey(benefit.Name))
            {
                Benefits[benefit.Name].Value += benefit.Value;
            }
            else
            {
                Benefits.Add(benefit.Name, benefit);
            }
        }

        public void AddAttribute(string attribute, int value)
        {
            Journal.Add(string.Format("Received an uplift to {0} of value {1}", attribute, value));
            Profile[attribute].Value += value;
        }

        public bool AgingCheck()
        {
            var result = true;
            switch (CharacterSpecies)
            {
                case Species.Vargr:
                    if (Age >= 66)
                    {
                        CheckAttribute(Profile.Str, 9, -2);
                        CheckAttribute(Profile.Dex, 8, -2);
                        CheckAttribute(Profile.End, 9, -2);
                        CheckAttribute(Profile.Int, 9, -1);
                    }
                    else if (Age >= 50)
                    {
                        CheckAttribute(Profile.Str, 8, -1);
                        CheckAttribute(Profile.Dex, 7, -1);
                        CheckAttribute(Profile.End, 8, -1);
                    }
                    else if (Age >= 34)
                    {
                        CheckAttribute(Profile.Str, 7, -1);
                        CheckAttribute(Profile.Dex, 6, -1);
                        CheckAttribute(Profile.End, 7, -1);
                    }
                    break;
                case Species.Aslan:
                    if (Age >= 70)
                    {
                        CheckAttribute(Profile.Str, 10, -2);
                        CheckAttribute(Profile.Dex, 10, -2);
                        CheckAttribute(Profile.End, 10, -2);
                        CheckAttribute(Profile.Int, 9, -1);
                    }
                    else if (Age >= 66)
                    {
                        CheckAttribute(Profile.Str, 9, -2);
                        CheckAttribute(Profile.Dex, 9, -2);
                        CheckAttribute(Profile.End, 9, -2);
                    }
                    else if (Age >= 60)
                    {
                        CheckAttribute(Profile.Str, 9, -1);
                        CheckAttribute(Profile.Dex, 8, -1);
                        CheckAttribute(Profile.End, 7, -1);
                    }
                    else if (Age >= 56)
                    {
                        CheckAttribute(Profile.Str, 8, -1);
                        CheckAttribute(Profile.Dex, 9, -1);
                        CheckAttribute(Profile.End, 8, -1);
                    }
                    break;
                default:
                    if (Age >= 66)
                    {
                        CheckAttribute(Profile.Str, 9, -2);
                        CheckAttribute(Profile.Dex, 9, -2);
                        CheckAttribute(Profile.End, 9, -2);
                        CheckAttribute(Profile.Int, 9, -1);
                    }
                    else if (Age >= 50)
                    {
                        CheckAttribute(Profile.Str, 9, -1);
                        CheckAttribute(Profile.Dex, 8, -1);
                        CheckAttribute(Profile.End, 9, -1);
                    }
                    else if (Age >= 34)
                    {
                        CheckAttribute(Profile.Str, 8, -1);
                        CheckAttribute(Profile.Dex, 8, -1);
                        CheckAttribute(Profile.End, 8, -1);
                    }
                    break;
            }
            // Any ageable attributes reduced to 0
            if (Profile.Str.Value == 0 || Profile.Dex.Value == 0 || Profile.End.Value == 0 || Profile.Int.Value == 0)
            {
                // Possible aging crisis
                if (dice.roll(2) >= 8)
                {
                    if (Profile.Str.Value == 0)
                    {
                        Profile.Str.Value = 1;
                    }
                    if (Profile.Dex.Value == 0)
                    {
                        Profile.Dex.Value = 1;
                    }
                    if (Profile.End.Value == 0)
                    {
                        Profile.End.Value = 1;
                    }
                    if (Profile.Int.Value == 0)
                    {
                        Profile.Int.Value = 1;
                    }
                    Journal.Add(string.Format("You were ill for {0} months due to aging.", dice.roll()));
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }

        private void CheckAttribute(TravCode attribute, int check, int drop)
        {
            var save = dice.roll(2);
            if (save < check)
            {
                attribute.Value += drop;
                Journal.Add(string.Format("{0} reduced by {1} due to aging", attribute.Name, Math.Abs(drop)));
            }
        }

        public void SaveXML(XmlDocument doc)
        {
            var character = doc.CreateElement("Character");
            doc.AppendChild(character);
            var child = character.OwnerDocument.CreateElement("Age");
            child.AppendChild(child.OwnerDocument.CreateTextNode(Age.ToString()));
            character.AppendChild(child);
            child = character.OwnerDocument.CreateElement("Culture");
            child.AppendChild(child.OwnerDocument.CreateTextNode(Culture.ToString()));
            character.AppendChild(child);
            child = character.OwnerDocument.CreateElement("Name");
            child.AppendChild(child.OwnerDocument.CreateTextNode(Name));
            character.AppendChild(child);
            child = character.OwnerDocument.CreateElement("Sex");
            child.AppendChild(child.OwnerDocument.CreateTextNode(Sex));
            character.AppendChild(child);
            child = character.OwnerDocument.CreateElement("Species");
            child.AppendChild(child.OwnerDocument.CreateTextNode(CharacterSpecies.ToString()));
            character.AppendChild(child);
            child = character.OwnerDocument.CreateElement("System");
            child.AppendChild(child.OwnerDocument.CreateTextNode(Style.ToString()));
            character.AppendChild(child);
            var journal = character.OwnerDocument.CreateElement("Journal");
            foreach (var item in Journal)
            {
                child = character.OwnerDocument.CreateElement("JournalItem");
                child.AppendChild(child.OwnerDocument.CreateTextNode(item));
                journal.AppendChild(child);
            }
            character.AppendChild(journal);
            // Now save the child collections
            // Attributes
            var attribs = character.OwnerDocument.CreateElement("Attributes");
            Profile.SaveXML(attribs);
            character.AppendChild(attribs);
            // Skills
            var skills = character.OwnerDocument.CreateElement("Skills");
            foreach (var skill in Skills.Values)
            {
                skill.SaveXML(skills);
            }
            character.AppendChild(skills);
            // Careers
            var careers = character.OwnerDocument.CreateElement("Careers");
            foreach (var career in Careers)
            {
                career.SaveXML(careers);
            }
            character.AppendChild(careers);
            // Benefits
            var benefits = character.OwnerDocument.CreateElement("Benefits");
            foreach(var benefit in Benefits.Values)
            {
                benefit.SaveXML(benefits);
            }
            character.AppendChild(benefits);
        }
    }
}
