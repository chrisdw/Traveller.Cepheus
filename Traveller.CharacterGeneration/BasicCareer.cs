using System;
using System.Collections.Generic;
using System.Text;

namespace Traveller.CharacterGeneration
{
    public abstract class BasicCareer : Career
    {
        public Skill[,] SkillTables { get; }
        public string[] Ranks { get; }
    }
}
