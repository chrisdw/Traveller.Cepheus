using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.SystemGeneration
{
    public class Sattelite : Planet, IComparable
    {
        public int FleshOut(Configuration configuration, Planet planet, Orbit myOrbit, Star primary, short HZone, double ComLumAddFromPrim)
        {
            if (PlanetType == WorldType.RING)
            {
                OrbitRange = ((planet.Diameter / 2) * OrbitNumber);
                OrbitRange = OrbitRange / 100000000;
                OrbitRange = OrbitRange / Constants.MKM_PER_AU;
                return 0;
            }

            Diameter = GetDiameter();
            Dense = GetDensity(myOrbit);
            Normal.Atmosphere.Value = GetAtmosphere(myOrbit, configuration);
            Pressure = GetPressure();
            Normal.Hydro.Value = GetHydrographics(myOrbit, configuration);
            Maxpop = GetMaxPop(myOrbit, HZone, myOrbit.Number);

            // 149600000 km (149.6 Mkm) in one AU 
            OrbitRange = ((planet.Diameter / 2) * OrbitNumber);
            var D = OrbitRange;

            OrbitRange /= 100000000;
            OrbitRange /= Constants.MKM_PER_AU;

            D /= 400000;

            var X = (D * D * D * 793.64) / planet.Mass();

            OrbitPeriod = Math.Sqrt(X);

            // Convert Years to Days
            OrbitPeriod /= Constants.DAYS_PER_YEAR;

            Rotation = (4 * (Common.d6() + Common.d6() - 2)) + 5 + planet.Mass() / D;

            if (Rotation > 40.0)
            {
                if (Common.d10() >= 6)
                {
                    TidallyLocked = true;
                }
                else
                {
                    TidallyLocked = false;
                }
            }
            else
            {
                TidallyLocked = false;
            }

            if (TidallyLocked)
            {
                Rotation = OrbitPeriod;
            }

            GetTempChart(planet, myOrbit, ComLumAddFromPrim, primary, configuration);

            if (configuration.GenerateTravInfo)
            {
                GetTravInfo(configuration);
            }

            Collapse.Size.Value = Normal.Size.Value;
            Collapse.Atmosphere.Value = Normal.Atmosphere.Value;
            Collapse.Hydro.Value = Normal.Hydro.Value;

            return Maxpop;
        }

        public void Build(int worldSize, WorldType worldType)
        {
            var farAllowed = false;
            var dieroll = 0;

            if (worldType == WorldType.SGG)
            {
                dieroll = Common.d6() + Common.d6() - 6;
                farAllowed = true;
            }
            else if (worldType == WorldType.LGG)
            {
                dieroll = Common.d6() + Common.d6() - 4;
                farAllowed = true;
            }
            else
            {
                dieroll = worldSize - Common.d6();
            }
        }

        public void SetOrbit(int worldSize, WorldType worldType)
        {

        }

        private void GetTempChart(Planet planet, Orbit myOribit, double ComLumAddFromPrim, Star primary, Configuration configuration)
        {

        }
        public int CompareTo(object obj)
        {
            var other = (Sattelite)obj;
            return OrbitNumber.CompareTo(other.OrbitNumber);
        }
    }
}
