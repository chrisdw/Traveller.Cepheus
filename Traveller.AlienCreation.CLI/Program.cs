using org.DownesWard.Traveller.SystemGeneration;
using System;

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

            var homeworld = new Planet(config);
            homeworld.Normal.Starport = 'C';
            homeworld.Normal.Size.Value = 7;
            homeworld.Normal.Atmosphere.Value = 7;
            homeworld.Normal.Hydro.Value = 7;
            homeworld.Normal.TechLevel.Value = 7;
            homeworld.Temp = 20;
            var alien = new Alien() { PsionicsAllowed = true };
            alien.Generate(homeworld);
            Console.Write("Niche {0} ({1})", alien.EcologicalType, alien.EcologicalSubtype);
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
            foreach (var s in alien.LimbGroupTypes)
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
            foreach (var s in alien.Traits)
            {
                Console.WriteLine(s);
            }

            foreach (var s in alien.Weapons)
            {
                Console.WriteLine(s);
            }
            Console.Write("Matures at {0}, aging begins at {1} DM {2:+0;-#}", alien.StartingAge, alien.AgingBegins, alien.AgingModifier);
        }
    }
}
