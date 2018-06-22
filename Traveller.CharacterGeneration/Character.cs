using org.DownesWard.Traveller.Shared;
using System;
using System.Collections.Generic;

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
        public List<Career> Careers { get; set; }

        public Constants.CultureType Culture { get; set; }
        public Constants.GenerationStyle Style { get; set; } = Constants.GenerationStyle.Classic_Traveller;
    }
}
