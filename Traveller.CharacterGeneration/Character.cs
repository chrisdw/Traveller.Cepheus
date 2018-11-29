using org.DownesWard.Traveller.CharacterGeneration.Classic.Zhodani;
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
            Vegan,
            Human
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
                case Constants.GenerationStyle.Cepheus_Engine:
                    GenerateCepheus();
                    break;
            }
        }

        private void GenerateCepheus()
        {
            var dice = new Dice(6);
            switch (Culture)
            {
                case Constants.CultureType.Cepheus_Generic:
                    switch (CharacterSpecies)
                    {
                        case Species.Human:
                            // use the standard UPP
                            Profile = new UPP();
                            Profile.Str.Value = dice.roll(2);
                            Profile.Dex.Value = dice.roll(2);
                            Profile.End.Value = dice.roll(2);
                            Profile.Int.Value = dice.roll(2);
                            Profile.Edu.Value = dice.roll(2);
                            Profile.Soc.Value = dice.roll(2);
                            break;
                    }
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
                            if (Sex.Equals(Properties.Resources.Sex_Random))
                            {
                                // Sex is by die roll
                                if (dice.roll(2) <= 5)
                                {
                                    Sex = Properties.Resources.Sex_Male;
                                }
                                else
                                {
                                    Sex = Properties.Resources.Sex_Female;
                                }
                            }
                            // Imperial Aslan get some automatic skills
                            AddSkill(SkillLibrary.Tolerance);
                            if (Sex.Equals(Properties.Resources.Sex_Male))
                            {
                                AddSkill(SkillLibrary.Independance);
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
                        case Species.Dolphin:
                            Profile = new DolphinUPP();
                            Profile.Int.Value = dice.roll(2) - 2;
                            Profile["HitsU"].Value = dice.roll(6);
                            Profile["HitsD"].Value = dice.roll(3);
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
                            if (Sex.Equals(Properties.Resources.Sex_Random))
                            {
                                // Sex is by die roll
                                if (dice.roll(2) <= 5)
                                {
                                    Sex = Properties.Resources.Sex_Male;
                                }
                                else
                                {
                                    Sex = Properties.Resources.Sex_Female;
                                }
                            }
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
                case Constants.CultureType.Aslan:
                    switch (CharacterSpecies)
                    {
                        case Species.Aslan:
                            Profile = new AslanUPP();
                            Profile.Str.Value = dice.roll(2) + 1;
                            Profile.Dex.Value = dice.roll(2) - 1;
                            Profile.End.Value = dice.roll(2) + 1;
                            if (Sex.Equals(Properties.Resources.Sex_Random))
                            {
                                // Sex is by die roll
                                if (dice.roll(2) <= 5)
                                {
                                    Sex = Properties.Resources.Sex_Male;
                                }
                                else
                                {
                                    Sex = Properties.Resources.Sex_Female;
                                }
                            }
                            break;
                        case Species.Human_Solomani:
                            Profile = new UPP();
                            Profile.Str.Value = dice.roll(2);
                            Profile.Dex.Value = dice.roll(2);
                            Profile.End.Value = dice.roll(2);
                            break;
                    }
                    Profile.Int.Value = dice.roll(2);
                    Profile.Edu.Value = dice.roll(2);
                    Profile.Soc.Value = dice.roll(2);
                    break;
            }
        }
        public void AddSkill(Skill skill)
        {
            Journal.Add(string.Format(Properties.Resources.Msg_ReceivedSkill, skill.Name, skill.Level));
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
            Journal.Add(string.Format(Properties.Resources.Msg_RececivedBenefit, benefit.Name, benefit.Value));
            if (Benefits.ContainsKey(benefit.Name))
            {
                Benefits[benefit.Name].Value += benefit.Value;
            }
            else
            {
                Benefits.Add(benefit.Name, benefit.Clone());
            }
        }

        public void AddAttribute(string attribute, int value)
        {
            Journal.Add(string.Format(Properties.Resources.Msg_ReceivedAttribute, attribute, value));
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
                case Species.Dolphin:
                    if (Age >= 30)
                    {
                        CheckAttribute(Profile["HITSU"], 8, -1);
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
            if (CharacterSpecies != Species.Dolphin) // Dolphins are treated as animals
            {
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
                        Journal.Add(string.Format(Properties.Resources.Msg_Ill, dice.roll()));
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            else
            {
                return Profile["HITSU"].Value != 0;
            }
            return result;
        }

        private void CheckAttribute(TravCode attribute, int check, int drop)
        {
            var save = dice.roll(2);
            if (save < check)
            {
                attribute.Value += drop;
                Journal.Add(string.Format(Properties.Resources.Msg_Aging, attribute.Name, Math.Abs(drop)));
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
        public static Character Load(XmlDocument doc)
        {
            var styleStr = doc.GetElementsByTagName("System")[0].InnerText;
            var character = new Character();
            Enum.TryParse(styleStr, out Constants.GenerationStyle style);
            character.Style = style;

            var cultureStr = doc.GetElementsByTagName("Culture")[0].InnerText;
            Enum.TryParse(cultureStr, out Constants.CultureType culture);
            character.Culture = culture;

            var speciesStr = doc.GetElementsByTagName("Species")[0].InnerText;
            Enum.TryParse(speciesStr, out Species species);
            character.CharacterSpecies = species;

            character.Sex = doc.GetElementsByTagName("Sex")[0].InnerText;
            character.Name = doc.GetElementsByTagName("Name")[0].InnerText;
            character.Age = int.Parse(doc.GetElementsByTagName("Age")[0].InnerText);

            // Now have enough to generate the correct profile
            switch (character.CharacterSpecies)
            {
                case Species.Human_Imperial:
                    // use the standard UPP
                    character.Profile = new UPP();
                    break;
                case Species.Bwap:
                    character.Profile = new UPP();
                    break;
                case Species.Aslan:
                    character.Profile = new AslanUPP();
                    break;
                case Species.Vargr:
                    character.Profile = new VargrUPP();
                    break;
                case Species.AelYael:
                    character.Profile = new UPP();
                    break;
                case Species.Virushi:
                    character.Profile = new VirushiUPP();
                    break;
                case Species.Vegan:
                    character.Profile = new UPP();
                    break;
                case Species.Dolphin:
                    character.Profile = new DolphinUPP();
                    break;
                case Species.Human_Solomani:
                    character.Profile = new UPP();
                    break;
                case Species.Human_SwordWorlds:
                    character.Profile = new UPP();
                    break;
                case Species.Human_Darrian:
                    character.Profile = new UPP();
                    break;
                case Species.Human_Dynchia:
                    character.Profile = new UPP();
                    break;
                case Species.Human_Zhodani:
                    character.Profile = new ZhodaniUPP();
                    break;
                default:
                    character.Profile = new UPP();
                    break;
            }

            var attribs = doc.GetElementsByTagName("Attributes")[0] as XmlElement;
            character.Profile.LoadXML(attribs);

            var journalItems = doc.GetElementsByTagName("JournalItem");
            foreach (var item in journalItems)
            {
                var journal = item as XmlElement;
                character.Journal.Add(journal.InnerText);
            }
            var careers = doc.GetElementsByTagName("Career");
            foreach (var item in careers)
            {
                var career = item as XmlElement;
                character.Careers.Add(Career.Load(career));
            }

            var skills = doc.GetElementsByTagName("Skill");
            foreach (var item in skills)
            {
                var skill = item as XmlElement;
                character.AddSkill(Skill.Load(skill));
            }

            var benefits = doc.GetElementsByTagName("Benefit");
            foreach (var item in benefits)
            {
                var benefit = item as XmlElement;
                character.AddBenefit(Benefit.Load(benefit));
            }
            return character;
        }
    }
}
