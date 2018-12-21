using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.DownesWard.Traveller.AlienCreation;
using org.DownesWard.Traveller.SystemGeneration;
using System;

namespace Traveller.Test
{
    [TestClass]
    public class AlienCreationTests
    {
        [TestMethod]
        public void TestBasic()
        {
            var config = new Configuration
            {
                CurrentCampaign = Campaign.CLASSIC
            };

            var homeworld = new Planet(config);
            homeworld.Normal.Starport = 'C';
            homeworld.Normal.Size.Value = 8;
            homeworld.Normal.Atmosphere.Value = 6;
            homeworld.Normal.Hydro.Value = 7;
            homeworld.Normal.TechLevel.Value = 15;
            homeworld.Temp = 20;
            var alien = new Alien() { PsionicsAllowed = true };
            alien.Generate(homeworld);
            alien.Write(Console.Out);
        }

        [TestMethod]
        public void TestLowGrav()
        {
            var config = new Configuration
            {
                CurrentCampaign = Campaign.CLASSIC
            };

            var homeworld = new Planet(config);
            homeworld.Normal.Starport = 'X';
            homeworld.Normal.Size.Value = 3;
            homeworld.Normal.Atmosphere.Value = 4;
            homeworld.Normal.Hydro.Value = 4;
            homeworld.Normal.TechLevel.Value = 7;
            homeworld.Temp = 20;
            var alien = new Alien() { PsionicsAllowed = true };
            alien.Generate(homeworld);
            alien.Write(Console.Out);
        }


        [TestMethod]
        public void TestDesertLowTech()
        {
            var config = new Configuration
            {
                CurrentCampaign = Campaign.CLASSIC
            };

            var homeworld = new Planet(config);
            homeworld.Normal.Starport = 'X';
            homeworld.Normal.Size.Value = 8;
            homeworld.Normal.Atmosphere.Value = 4;
            homeworld.Normal.Hydro.Value = 0;
            homeworld.Normal.TechLevel.Value = 0;
            homeworld.Temp = 20;
            var alien = new Alien() { PsionicsAllowed = true };
            alien.Generate(homeworld);
            alien.Write(Console.Out);
        }
    }
}
