﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }
}
