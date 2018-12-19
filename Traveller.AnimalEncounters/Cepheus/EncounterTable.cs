using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.AnimalEncounters.Cepheus
{
    public class EncounterTable
    {
        public Regions Region { get; set; }
        public string Name { get; set; }
        public List<Critter> Critters { get; private set; } = new List<Critter>();
    }
}
