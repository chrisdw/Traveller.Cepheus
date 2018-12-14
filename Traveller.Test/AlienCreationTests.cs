using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.DownesWard.Traveller.AlienCreation;
using org.DownesWard.Traveller.SystemGeneration;

namespace Traveller.Test
{
    [TestClass]
    public class AlienCreationTests
    {
        [TestMethod]
        public void TestBasic()
        {
            var config = new Configuration();
            config.CurrentCampaign = Campaign.CLASSIC;

            var homeworld = new Planet(config);
            homeworld.Normal.Size.Value = 7;
            homeworld.Normal.Atmosphere.Value = 7;
            homeworld.Normal.Hydro.Value = 7;
            homeworld.Temp = 20;
            var alien = new Alien();
            alien.Generate(homeworld);

        }
    }
}
