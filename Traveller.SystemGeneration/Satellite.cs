using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace org.DownesWard.Traveller.SystemGeneration
{
    public class Satellite : Planet, IComparable
    {
        public Satellite(Configuration configuration) : base(configuration)
        {

        }

        public int FleshOut(Configuration configuration, Planet planet, Orbit myOrbit, Star primary, int HZone, double ComLumAddFromPrim)
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

            var X = (D * D * D * 793.64) / planet.Mass;

            OrbitPeriod = Math.Sqrt(X);

            // Convert Years to Days
            OrbitPeriod /= Constants.DAYS_PER_YEAR;

            Rotation = (4 * (Common.d6() + Common.d6() - 2)) + 5 + planet.Mass / D;

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

            GetTempChart(planet, myOrbit, ComLumAddFromPrim, primary, configuration, true);

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

            if (dieroll == 0)
            {
                PlanetType = WorldType.RING;
                // Rings are in orbits 0-2
                dieroll = Common.d6();
                if (dieroll == 6)
                {
                    OrbitNumber = 3;
                }
                else if (dieroll <= 3)
                {
                    OrbitNumber = 1;
                }
                else
                {
                    OrbitNumber = 2;
                }
            }
            else
            {
                if ((dieroll >= worldSize) && (worldSize >= 0))
                {
                    dieroll = worldSize - 1;
                }
                if (dieroll < 1)
                {
                    dieroll = 0;
                }
                Normal.Size.Value = 0;

                if (Normal.Size.Value == 0)
                {
                    PlanetType = WorldType.SMALL;
                }
                else
                {
                    PlanetType = WorldType.NORMAL;
                }

                OrbitNumber = SatOrbit(farAllowed);
            }
        }

        public void SetOrbit(int worldSize, WorldType worldType)
        {
            var farAllowed = false;

            if (worldType == WorldType.SGG || worldType == WorldType.LGG)
            {
                farAllowed = true;
            }

            if (PlanetType == WorldType.RING)
            {
                var dieroll = Common.d6();
                if (dieroll == 6)
                {
                    OrbitNumber = 3;
                }
                else if (dieroll <= 3)
                {
                    OrbitNumber = 1;
                }
                else
                {
                    OrbitNumber = 2;
                }
            }
            else
            {
                OrbitNumber = SatOrbit(farAllowed);
            }
        }

        public int SatOrbit(bool farAllowed)
        {
            var dieroll = Common.d6() + Common.d6();

            if (dieroll < 8)
            {
                switch (Common.d6() + Common.d6())
                {
                    case 2:
                        return (3);
                    case 3:
                        return (4);
                    case 4:
                        return (5);
                    case 5:
                        return (6);
                    case 6:
                        return (7);
                    case 7:
                        return (8);
                    case 8:
                        return (9);
                    case 9:
                        return (10);
                    case 10:
                        return (11);
                    case 11:
                    case 12:
                    default:
                        return (12);
                }
            }
            else if (dieroll < 12 || (!farAllowed && dieroll == 12))
            {
                switch (Common.d6() + Common.d6())
                {
                    case 2:
                        return (15);
                    case 3:
                        return (20);
                    case 4:
                        return (25);
                    case 5:
                        return (30);
                    case 6:
                        return (35);
                    case 7:
                        return (40);
                    case 8:
                        return (45);
                    case 9:
                        return (50);
                    case 10:
                        return (55);
                    case 11:
                        return (60);
                    case 12:
                        return (65);
                    default:
                        return (70);
                }
            }
            else
            {
                switch (Common.d6() + Common.d6())
                {
                    case 2:
                        return (75);
                    case 3:
                        return (100);
                    case 4:
                        return (125);
                    case 5:
                        return (150);
                    case 6:
                        return (175);
                    case 7:
                        return (200);
                    case 8:
                        return (225);
                    case 9:
                        return (250);
                    case 10:
                        return (275);
                    case 11:
                        return (300);
                    case 12:
                        return (325);
                    default:
                        return (350);
                }
            }
        }

        public int CompareTo(object obj)
        {
            var other = obj as Satellite;
            return OrbitNumber.CompareTo(other.OrbitNumber);
        }

        public override void SaveToXML(XmlElement objOrbit, Configuration configuration)
        {
            var xePlanet = objOrbit.OwnerDocument.CreateElement("Satellite");
            objOrbit.AppendChild(xePlanet);
            Common.CreateTextNode(xePlanet, "OrbitNumber", OrbitNumber.ToString());
            base.SaveToXML(xePlanet, configuration);
        }
    }
}
