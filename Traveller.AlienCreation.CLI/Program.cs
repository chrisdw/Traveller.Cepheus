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
            Console.WriteLine("Niche {0} ({1})", alien.EcologicalType, alien.EcologicalSubtype);
            Console.WriteLine("STR: {0}", alien.STR);
            Console.WriteLine("DEX: {0}", alien.DEX);
            Console.WriteLine("END: {0}", alien.END);
            Console.WriteLine("INT: {0}", alien.INT);
            Console.WriteLine("EDU: {0}", alien.EDU);
            Console.WriteLine("SOC: {0}", alien.SOC);
            Console.WriteLine("Metabolism {0}", alien.Metabolism);
            Console.WriteLine("Genders {0}:{1}", alien.GenderModel, alien.NumGenders);
            Console.WriteLine("Reproduction: {0}", alien.ReproductionMethod);
            Console.WriteLine("Size: {0}, DM {1:+0;-#}", alien.Size, alien.AttackDM);
            Console.WriteLine("Symmetry: {0}", alien.Symmetry);
            Console.WriteLine("Limbs {0} ({1} Pairs)", alien.LimbCount, alien.LimbPairs);
            foreach (var s in alien.LimbGroupTypes.OrderBy(s => s))
            {
                Console.WriteLine(s);
            }
            if (alien.LandMovementRate != 0)
            {
                Console.WriteLine("Land Movement: {0}", alien.LandMovementRate);
            }
            if (alien.FlyMovementRate != 0)
            {
                Console.WriteLine("Flying Movement: {0}", alien.FlyMovementRate);
            }
            if (alien.SwimMovementRate != 0)
            {
                Console.WriteLine("Swiming Movement: {0}", alien.SwimMovementRate);
            }
            if (alien.ClimbMovementRate != 0)
            {
                Console.WriteLine("Climbing Movement: {0}", alien.ClimbMovementRate);
            }
            foreach (var s in alien.Traits.OrderBy(s => s))
            {
                Console.WriteLine(s);
            }

            foreach (var s in alien.Weapons.OrderBy(s => s))
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("Matures at {0}, aging begins at {1} DM {2:+0;-#}", alien.StartingAge, alien.AgingBegins, alien.AgingModifier);
            Console.WriteLine("Height {0} + {1}", alien.BaseHeight, alien.HeightModifier);
            Console.WriteLine("Weight {0} + {1}", alien.BaseWeight, alien.WeightModifier);
        }
    }
}
