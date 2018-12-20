using org.DownesWard.Utilities;
using System.Collections.Generic;

namespace org.DownesWard.Traveller.AnimalEncounters.Cepheus
{
    public class TableGenerator
    {
        private Dice dice = new Dice(6);

        private readonly EcologicalTypes[] d6Table = new EcologicalTypes[6]
        {
            EcologicalTypes.Scavenger,
            EcologicalTypes.Herbivore,
            EcologicalTypes.Herbivore,
            EcologicalTypes.Herbivore,
            EcologicalTypes.Omnivore,
            EcologicalTypes.Carnivore
        };

        private readonly EcologicalTypes[] twod6Table = new EcologicalTypes[11]
        {
            EcologicalTypes.Scavenger,
            EcologicalTypes.Omnivore,
            EcologicalTypes.Scavenger,
            EcologicalTypes.Omnivore,
            EcologicalTypes.Herbivore,
            EcologicalTypes.Herbivore,
            EcologicalTypes.Herbivore,
            EcologicalTypes.Carnivore,
            EcologicalTypes.Event,
            EcologicalTypes.Carnivore,
            EcologicalTypes.Carnivore
        };

        public List<EncounterTable> Generate(int size)
        {
            List<EncounterTable> tables = new List<EncounterTable>();
            EcologicalTypes[] table = twod6Table;
            if (size == 1)
            {
                table = d6Table;
            }
            foreach (var t in Terrain.Terrains)
            {
                var etable = new EncounterTable() { Region = t.Region };
                tables.Add(etable);

                for (var i = 0; i < table.Length; i++)
                {
                    if (table[i] != EcologicalTypes.Event)
                    {
                        var st = dice.roll() - 1;
                        Terrain.SubTerrain subTerrain = t.SubTerrains[st];
                        var c = new Critter(table[i], t.SubtypeDM, t.SizeDM, subTerrain.SizeDM, subTerrain.Motion)
                        {
                            Region = t.Region
                        };
                        etable.Critters.Add(c);
                    }
                    else
                    {
                        var c = new Critter();
                        etable.Critters.Add(c);
                    }
                }
            }
            return tables;
        }
    }
}
