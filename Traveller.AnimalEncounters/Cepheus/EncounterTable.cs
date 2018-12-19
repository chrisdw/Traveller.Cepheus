using System.Collections.Generic;
using System.IO;
using System.Text;

namespace org.DownesWard.Traveller.AnimalEncounters.Cepheus
{
    public class EncounterTable
    {
        public Regions Region { get; set; }
        public string Name { get; set; }
        public List<Critter> Critters { get; private set; } = new List<Critter>();

        public void WriteStreamAsText(TextWriter sw)
        {
            sw.WriteLine("{0} {1}", Region, Name);
            var start = 2;
            var col1 = "2d6";
            if (Critters.Count == 6)
            {
                start = 1;
                col1 = "1d6";
            }
            sw.WriteLine("{0} #App Size   Subtype     Move     UPP Weapons Armour", col1);

            foreach (var c in Critters)
            {
                var wpns = string.Join(", ", c.Weapons);
                sw.WriteLine("{0,3} {1,4} {2:0,0}kg {3} ({4}) {5} {6}m {7} {8} ({9}d6) {10}", start++, c.NumberAppearing, c.Weight, c.EcologicalSubtype, c.EcologicalType,  c.Motion, c.Move, c.Profile.Display, wpns, c.DamageDice, c.Armour);
            }
        }
    }
}
