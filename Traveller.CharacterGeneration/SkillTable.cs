using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration
{
    public class SkillTable
    {
        public bool Available { get; set; } = true;
        public string Name { get; set; }
        public Skill[] Skills { get; } = new Skill[6];
    }
}
