using System;
using System.Collections.Generic;
using System.Text;

namespace Traveller.CharacterGeneration
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
    }
}
