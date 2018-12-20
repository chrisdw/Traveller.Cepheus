using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.DownesWard.Traveller.AnimalEncounters.Cepheus;
using org.DownesWard.Traveller.Shared;
using System;

namespace Traveller.Test
{
    [TestClass]
    public class EncounterTableTests
    {
        [TestMethod]
        public void Cepheus2d6TableTest()
        {
            UWP uwp = new UWP();
            uwp.Atmosphere.Value = 7;
            uwp.Hydro.Value = 7;
            uwp.Size.Value = 7;

            var tg = new TableGenerator();
            var tables = tg.Generate(2, uwp);
            foreach (var t in tables)
            {
                t.WriteStreamAsText(Console.Out);
            }
        }

        [TestMethod]
        public void Cepheus1D6TableTest()
        {
            UWP uwp = new UWP();
            uwp.Atmosphere.Value = 7;
            uwp.Hydro.Value = 7;
            uwp.Size.Value = 7;

            var tg = new TableGenerator();
            var tables = tg.Generate(1, uwp);
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

        [TestMethod]
        public void CreateOmnivoreCritter()
        {
            var c = new Critter(EcologicalTypes.Omnivore, 0, -2, -2, Motions.Walking)
            {
                Region = Regions.Woods
            };
            c.Write(Console.Out);
        }

        [TestMethod]
        public void Create5OmnivoreCritter()
        {
            for (int i = 0; i < 5; i++)
            {
                var c = new Critter(EcologicalTypes.Omnivore, 0, -2, -2, Motions.Walking)
                {
                    Region = Regions.Woods
                };
                c.Write(Console.Out);
            }
        }
    }
}
