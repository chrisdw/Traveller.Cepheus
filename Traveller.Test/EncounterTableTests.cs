using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.DownesWard.Traveller.AnimalEncounters.Cepheus;
using System;

namespace Traveller.Test
{
    [TestClass]
    public class EncounterTableTests
    {
        [TestMethod]
        public void Cepheus2d6TableTest()
        {
            var tg = new TableGenerator();
            var tables = tg.Generate(2);
            foreach (var t in tables)
            {
                t.WriteStreamAsText(Console.Out);
            }
        }

        [TestMethod]
        public void Cepheus1D6TableTest()
        {
            var tg = new TableGenerator();
            var tables = tg.Generate(1);
            foreach (var t in tables)
            {
                t.WriteStreamAsText(Console.Out);
            }
        }

        [TestMethod]
        public void CreateCritter()
        {
            var c = new Critter(EcologicalTypes.Carnivore, 0, 0, 0,  Motions.Walking);
            c.Write(Console.Out);
        }

        [TestMethod]
        public void CreateFlyngCritter()
        {
            var c = new Critter(EcologicalTypes.Carnivore, 0, 0, -4, Motions.Flying);
            c.Write(Console.Out);
        }

        [TestMethod]
        public void CreateSwimmingCritter()
        {
            var c = new Critter(EcologicalTypes.Herbivore, 4, 2, 4, Motions.Swimming);
            c.Write(Console.Out);
        }
    }
}
