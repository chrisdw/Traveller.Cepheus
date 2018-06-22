using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration
{
    public class Skill
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

        public int Level { get; set; }
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

        public List<Skill> ResolveSkill()
        {
            List<Skill> possible = new List<Skill>();

            if (Cascade.Count == 0)
            {
                possible.Add(this);
                return possible;
            }
 
            var toVisit = new Stack<Skill>(Cascade);

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
    }
}
