using org.DownesWard.Traveller.SystemGeneration;
using System;
using System.Linq;

namespace org.DownesWard.Traveller.AlienCreation.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new Configuration
            {
                CurrentCampaign = Campaign.CLASSIC
            };

            // Expect 3 arguments in the form of a full UPP e.g. C-777777-7
            // a temprature e.g. 20
            // a flag to say is psionics exist e.g. True or False
            var homeworld = new Planet(config);
            homeworld.Normal.Starport = args[0][0];
            homeworld.Normal.Size.Value = int.Parse(args[0][2].ToString(), System.Globalization.NumberStyles.HexNumber);
            homeworld.Normal.Atmosphere.Value = int.Parse(args[0][3].ToString(), System.Globalization.NumberStyles.HexNumber);
            homeworld.Normal.Hydro.Value = int.Parse(args[0][4].ToString(), System.Globalization.NumberStyles.HexNumber);
            homeworld.Normal.TechLevel.Value = int.Parse(args[0][9].ToString(), System.Globalization.NumberStyles.HexNumber);
            homeworld.Temp = int.Parse(args[1]);
            var alien = new Alien() { PsionicsAllowed = bool.Parse(args[2]) };
            alien.Generate(homeworld);
            alien.Write(Console.Out);
        }
    }
}
