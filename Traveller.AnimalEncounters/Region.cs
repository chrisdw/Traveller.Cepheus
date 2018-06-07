using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.AnimalEncounters
{
    public class Region
    {
        public string Name { get; internal set; }
        public List<Critter> Critters { get; } = new List<Critter>();
    }
}
