using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            sw.WriteLine(Terrain.TerrainName(Region));
            var start = 2;
            var col1 = "2d6";
            if (Critters.Count == 6)
            {
                start = 1;
                col1 = "1d6";
            }
            sw.WriteLine("{0} #App Size   Subtype     Move     UPP Weapons              Armour", col1);

            foreach (var c in Critters)
            {
                if (c.EcologicalType != EcologicalTypes.Event)
                {
                    var sb = new List<string>();
                    foreach (var w in c.Weapons.OrderBy(w => w))
                    {
                        sb.Add(string.Format("{0} ({1}d6)", w, c.DamageDice));
                    }
                    var wpns = string.Join(", ", sb);
                    sw.WriteLine("{0,3} {1,4} {2:0,0}kg {3} ({4}) {5} {6}m {7} {8,20} {9} ({10})", start++, c.NumberAppearing, c.Weight, c.EcologicalSubTypeLong, c.EcologicalTypeShort, c.Motion, c.Move, c.Profile.Display, wpns, c.ArmourType, c.Armour);
                }
                else
                {
                    sw.WriteLine("{0,3} Event", start++);
                }
            }
        }
    }
}
