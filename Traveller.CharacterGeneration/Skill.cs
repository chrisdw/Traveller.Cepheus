﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;

namespace org.DownesWard.Traveller.CharacterGeneration
{
    public class Skill : INotifyPropertyChanged, IEquatable<Skill>
    {
        public enum SkillClass
        {
            Unset = -1,
            Psionic,
            Military,
            Civilian,
            NoLevel,
            AttributeChange,
            None,
            Weapon,
            Prole
        }

        public enum SkillSex
        {
            Male,
            Female,
            DontCare
        }

        // This is the only property whose changes we actaully care about
        private int level = 0;
        public int Level {
            get {
                return level;
            }
            set {
                if (value != level)
                {
                    level = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Name { get; set; }
        public SkillClass Class { get; set; }
        public SkillSex SexApplicabilty { get; set; }

        public List<Skill> Cascade { get; set; } = new List<Skill>();

        public Skill()
        {
            Level = 1;
            Class = SkillClass.Unset;
            SexApplicabilty = SkillSex.DontCare;
            Name = string.Empty;
        }

        public Skill(string name)
        {
            Level = 1;
            Class = SkillClass.Unset;
            SexApplicabilty = SkillSex.DontCare;
            Name = name;
        }

        public Skill(string name, SkillClass skillClass, SkillSex skillSex = SkillSex.DontCare)
        {
            if (skillClass == SkillClass.NoLevel)
            {
                Level = 0;
            }
            else
            {
                Level = 1;
            }
            Class = skillClass;
            SexApplicabilty = skillSex;
            Name = name;
        }

        public Skill(string name, SkillClass skillClass, int level = 1, SkillSex skillSex = SkillSex.DontCare)
        {
            if (skillClass == SkillClass.NoLevel)
            {
                Level = 0;
            }
            else
            {
                Level = level;
            }
            Class = skillClass;
            SexApplicabilty = skillSex;
            Name = name;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<Skill> ResolveSkill()
        {
            List<Skill> possible = new List<Skill>();

            if (Cascade.Count == 0)
            {
                possible.Add(this);
                return possible;
            }
 
            var toVisit = new Stack<Skill>();
            toVisit.Push(this);

            while (toVisit.Count > 0)
            {
                var current = toVisit.Pop();

                foreach (var child in current.Cascade)
                {
                    if (child.Cascade.Count == 0)
                    {
                        possible.Add(child);
                    }
                    else
                    {
                        toVisit.Push(child);
                    }
                }
            }
             
            return possible;
        }

        public Skill Clone()
        {
            var clone = new Skill() { Name = Name, Class = Class, Level = Level, SexApplicabilty = SexApplicabilty };

            foreach (var skill in Cascade)
            {
                clone.Cascade.Add(skill.Clone());
            }

            return clone;
        }

        public void SaveXML(XmlElement ele)
        {
            var skill = ele.OwnerDocument.CreateElement("Skill");
            skill.SetAttribute("Name", Name);
            skill.SetAttribute("Level", Level.ToString());
            ele.AppendChild(skill);
        }

        public static Skill Load(XmlElement element)
        {
            var skill = new Skill
            {
                Name = element.GetAttribute("Name"),
                Level = int.Parse(element.GetAttribute("Level"))
            };
            return skill;
        }

        public bool Equals(Skill other)
        {
            return other.Name.Equals(Name) && other.Level == Level;
        }

        public override int GetHashCode()
        {
            //Get hash code for the Name field if it is not null. 
            int hashSkillName = Name == null ? 0 : Name.GetHashCode();

            //Get hash code for the Level field. 
            int hashSkillLevel = Level.GetHashCode();

            //Calculate the hash code for the product. 
            return hashSkillName ^ hashSkillLevel;
        }
    }
}
