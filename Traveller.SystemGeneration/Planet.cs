using org.DownesWard.Traveller.AnimalEncounters;
using org.DownesWard.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.SystemGeneration
{
    public class Planet
    {
        public enum WorldType
        {
            RING,
            SMALL,
            NORMAL,
            LGG,
            SGG,
            PLANETOID,
            STAR
        }

        public enum DensityType
        {
            LIGHT,
            AVERAGE,
            HEAVY
        }

        public DensityType Dense { get; set; }
        public double Pressure { get; set; }
        public int Maxpop { get; set; }
        public double OrbitPeriod { get; set; }
        public double OrbitRange { get; set; }

        public double Tilt { get; set; }
        public double Ecc { get; set; }
        public double Rotation { get; set; }
        public bool TidallyLocked { get; set; }
        public double Temp { get; set; }
        public double Diameter { get; set; }
        public List<Sattelite> Sattelites { get; set; } = new List<Sattelite>();
        public bool MainWorld { get; set; }

        public double OrbitNumber { get; set; }

        public TravInfo Normal { get; } = new TravInfo();
        public TravInfo Collapse { get; } = new TravInfo();

        public bool Life { get; set; }
        public int LifeFactor { get; set; }
        public string Name { get; set; }

        public TableGenerator TableGenerator { get; } = new TableGenerator();

        // Temprature talbes
        public double[] Summer { get; } = new double[Constants.NUM_HEX_ROWS * 2];
        public double[] Fall { get; } = new double[Constants.NUM_HEX_ROWS * 2];
        public double[] Winter { get; } = new double[Constants.NUM_HEX_ROWS * 2];

        public WorldType PlanetType { get; set; }

        public void Generate(Configuration config)
        {
            if (config.Generation == GenerationType.SIMPLE)
            {
                Normal.Size.Value = Common.d6() + Common.d6() - 2;
                Normal.Atmosphere.Value = Common.d6() + Common.d6() - 7 + Normal.Size.Value;
                Normal.Hydro.Value = Common.d6() + Common.d6() - 7 + Normal.Atmosphere.Value;
                Normal.GetTravInfo(config);
                Normal.DoTradeClassification();
                Normal.CompleteTravInfo(config);
            }
        }


        public double Mass()
        {
            var R = Diameter / 2;
            var V = (4 * Math.PI) / 3 * (R * R * R);
            V = V / Constants.EARTHMASS;
            switch (Dense)
            {
                case DensityType.LIGHT:
                    V = V * 0.55;
                    break;
                case DensityType.HEAVY:
                    V = V * 1.55;
                    break;
            }
            return V;
        }

        public double Grav()
        {
            var R = Diameter / 2;
            var V = (4 * Math.PI) / 3 * R;
            V = V / Constants.EARTHGRAV;
            switch (Dense)
            {
                case DensityType.LIGHT:
                    V = V * 0.55;
                    break;
                case DensityType.HEAVY:
                    V = V * 1.55;
                    break;
            }
            return V;
        }

        public int FleshOut(Configuration configuration, double OrbitNum, Orbit myOrbit, Star primary, short HZone, double ComLumAddFromPrim)
        {
            var M = primary.StellarMass;
            var D = myOrbit.Range;
            var numsats = 0;
            var dieroll = 0;

            OrbitNumber = OrbitNum;

            switch (myOrbit.Occupied)
            {
                case Orbit.OccupiedBy.GASGIANT:
                    if (Common.d6() < 3)
                    {
                        numsats = Common.d6() + Common.d6() - 4;
                        PlanetType = WorldType.SGG;
                        Diameter = 20000 + Common.Change(20000);
                    }
                    else
                    {
                        numsats = Common.d6() + Common.d6();
                        PlanetType = WorldType.LGG;
                        Diameter = 60000 + Common.Change(100000);
                    }
                    Dense = DensityType.LIGHT;
                    if (numsats < 0)
                    {
                        numsats = 0;
                    }

                    Maxpop = BuildSattelites(configuration, OrbitNum, myOrbit, primary, HZone, ComLumAddFromPrim, numsats);
                    break;
                case Orbit.OccupiedBy.CAPTURED:
                case Orbit.OccupiedBy.WORLD:
                    dieroll = Common.d6() + Common.d6() - 2;
                    if (OrbitNum == 0)
                    {
                        dieroll -= 5;
                    }
                    else if (OrbitNum == 1)
                    {
                        dieroll -= 4;
                    }
                    else if (OrbitNum == 2)
                    {
                        dieroll -= 2;
                    }
                    if (primary.StarType == Star.StellarType.M)
                    {
                        dieroll -= 2;
                    }
                    if (dieroll < 1)
                    {
                        dieroll = 0;
                    }
                    Normal.Size.Value = dieroll;
                    Diameter = GetDiameter();
                    Dense = GetDensity(myOrbit);
                    Normal.Atmosphere.Value = GetAtmosphere(myOrbit, configuration);
                    Pressure = GetPressure();
                    Normal.Hydro.Value = GetHydrographics(myOrbit, configuration);
                    Maxpop = GetMaxPop(myOrbit, HZone, OrbitNum);
                    Rotation = (4 * (Common.d6() + Common.d6() - 2)) + 5 + (M / D);
                    if (Rotation > 40.0)
                    {
                        // Chance of tidal locking
                        if (primary.StarType == Star.StellarType.M)
                        {
                            if (myOrbit.OrbitalType == Orbit.OrbitType.HABITABLE)
                            {
                                dieroll = Common.d6();
                                if (dieroll >= 3)
                                {
                                    TidallyLocked = true;
                                }
                                else
                                {
                                    TidallyLocked = false;
                                }
                            }
                            else if (myOrbit.OrbitalType == Orbit.OrbitType.INNER)
                            {
                                TidallyLocked = true;
                            }
                            else
                            {
                                TidallyLocked = false;
                            }
                        }
                        else if (primary.StarType == Star.StellarType.K)
                        {
                            dieroll = Common.d6();
                            if (dieroll >= 4)
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
                            dieroll = Common.d10();
                            if (dieroll >= 6)
                            {
                                TidallyLocked = true;
                            }
                            else
                            {
                                TidallyLocked = false;
                            }
                        }
                    }
                    else
                    {
                        TidallyLocked = false;
                    }

                    if (myOrbit.Occupied == Orbit.OccupiedBy.CAPTURED)
                    {
                        double roll = Common.d6() + Common.d6() - 7;
                        roll = roll / 10.0;
                        OrbitNumber += roll;
                    }

                    // TODO: Temprature charts
                    // TODO: Native life

                    if (Normal.Size.Value == 0 || myOrbit.Occupied == Orbit.OccupiedBy.CAPTURED)
                    {
                        numsats = 0;
                    }
                    else 
                    {
                        numsats = Common.d6() - 3;
                    }
                    if (numsats < 0)
                    {
                        numsats = 0;
                    }
                    var satMaxPop = BuildSattelites(configuration, OrbitNum, myOrbit, primary, HZone, ComLumAddFromPrim, numsats);

                    Maxpop = Math.Max(satMaxPop, Maxpop);

                    break;

                case Orbit.OccupiedBy.PLANETOID:
                    PlanetType = WorldType.PLANETOID;
                    break;
                case Orbit.OccupiedBy.STAR:
                    PlanetType = WorldType.STAR;
                    break;
            }
            return Maxpop;
        }

        private int BuildSattelites(Configuration configuration, double OrbitNum, Orbit myOrbit, Star primary, short HZone, double ComLumAddFromPrim, int numsats)
        {
            return 0;
        }

        protected double GetDiameter()
        {
            var diam = 0.0;

            switch (Normal.Size.Value)
            {
                case 0:
                    diam = 200 + Common.Change(300);
                    PlanetType = WorldType.SMALL;
                    break;
                case 1:
                    diam = 800 + Common.Change(800);
                    PlanetType = WorldType.NORMAL;
                    break;
                case 2:
                    diam = 2400 + Common.Change(1600);
                    PlanetType = WorldType.NORMAL;
                    break;
                case 3:
                    diam = 4000 + Common.Change(800);
                    PlanetType = WorldType.NORMAL;
                    break;
                case 4:
                    diam = 5600 + Common.Change(800);
                    PlanetType = WorldType.NORMAL;
                    break;
                case 5:
                    diam = 7200 + Common.Change(800);
                    PlanetType = WorldType.NORMAL;
                    break;
                case 6:
                    diam = 8800 + Common.Change(800);
                    PlanetType = WorldType.NORMAL;
                    break;
                case 7:
                    diam = 10400 + Common.Change(800);
                    PlanetType = WorldType.NORMAL;
                    break;
                case 8:
                    diam = 12000 + Common.Change(800);
                    PlanetType = WorldType.NORMAL;
                    break;
                case 9:
                    diam = 13600 + Common.Change(800);
                    PlanetType = WorldType.NORMAL;
                    break;
                case 10:
                    diam = 15200 + Common.Change(800);
                    PlanetType = WorldType.NORMAL;
                    break;
            }
            return diam;
        }

        protected DensityType GetDensity(Orbit myOrbit)
        {
            var dense = DensityType.AVERAGE;

            var dieroll = Common.d6();
            if (myOrbit.OrbitalType == Orbit.OrbitType.OUTER)
            {
                dieroll -= 2;
            }
            if (dieroll < 2)
            {
                dense = DensityType.LIGHT;
            }
            else if (dieroll > 4)
            {
                dense = DensityType.HEAVY;
            }

            return dense;
        }

        protected int GetAtmosphere(Orbit orbit, Configuration configuration)
        {
            var dieroll = Common.d6() + Common.d6() - 7 + Normal.Size.Value;
            if (orbit.OrbitalType == Orbit.OrbitType.INNER)
            {
                dieroll -= 2;
            }
            else if (orbit.OrbitalType == Orbit.OrbitType.OUTER)
            {
                dieroll -= 4;
            }

            if (Normal.Size.Value == 0)
            {
                dieroll = 0;
            }
            if (dieroll < 1)
            {
                dieroll = 0;
            }

            if (configuration.SpaceOpera)
            {
                if (Normal.Size.Value <= 2)
                {
                    dieroll = 0;
                }
                else if (Normal.Size.Value <= 4)
                {
                    if (dieroll <= 2)
                    {
                        dieroll = 0;
                    }
                    else if (dieroll <= 5)
                    {
                        dieroll = 1;
                    }
                    else if (dieroll >= 6)
                    {
                        dieroll = 10;
                    }
                }
            }

            return dieroll;
        }

        protected double GetPressure()
        {
            var press = 0.0;
            switch (Normal.Atmosphere.Value)
            {
                case 0:
                    press = 0.0;
                    break;
                case 1:
                    press = 0.0 + Common.Change(0.01);
                    break;
                case 2:
                case 3:
                    press = 0.1 + Common.Change(0.016);
                    break;
                case 4:
                case 5:
                    press = 0.42 + Common.Change(0.014);
                    break;
                case 6:
                case 7:
                    press = 0.7 + Common.Change(0.4);
                    break;
                case 8:
                case 9:
                    press = 1.5 + Common.Change(0.5);
                    break;
                default:
                    press = 0.0 + Common.Change(3.0);
                    break;
            }
            return press; 
        }

        protected int GetHydrographics(Orbit orbit, Configuration configuration)
        {
            var dieroll = Common.d6() + Common.d6() - 7 + Normal.Size.Value;

            if (orbit.OrbitalType == Orbit.OrbitType.INNER)
            {
                dieroll = 0;
            }
            else if (orbit.OrbitalType == Orbit.OrbitType.OUTER)
            {
                dieroll -= 2;
            }

            if (Normal.Atmosphere.Value < 2)
            {
                dieroll = 0;
            }

            if (Normal.Size.Value < 2)
            {
                dieroll -= 4;
            }

            if (Normal.Atmosphere.Value >= 10 && Normal.Atmosphere.Value <= 2)
            {
                dieroll -= 4;
            }
            else if (Normal.Atmosphere.Value == 14)
            {
                dieroll -= 2;
            }

            if (configuration.SpaceOpera)
            {
                if (Normal.Size.Value >= 3 && Normal.Size.Value <= 4 && Normal.Atmosphere.Value == 10)
                {
                    dieroll -= 6;
                }
                if (Normal.Atmosphere.Value <= 1)
                {
                    dieroll -= 6;
                }
                if (Normal.Atmosphere.Value == 2 || Normal.Atmosphere.Value == 3 || Normal.Atmosphere.Value == 11 || Normal.Atmosphere.Value == 12)
                {
                    dieroll -= 4;
                }
            }
            dieroll = dieroll.Clamp(0, 10);

            return dieroll;
        }

        protected int GetMaxPop(Orbit orbit, short HZone, double OrbitNum)
        {
            var maxpop = 10;

            if (Normal.Size.Value < 4)
            {
                maxpop -= 1;
            }
            if (Normal.Size.Value < 7)
            {
                maxpop -= 1;
            }
            if (Normal.Atmosphere.Value == 5 || Normal.Atmosphere.Value == 7 || Normal.Atmosphere.Value == 9)
            {
                maxpop -= 1;
            }
            if (Normal.Atmosphere.Value == 4)
            {
                maxpop -= 2;
            }
            if (Normal.Atmosphere.Value > 12)
            {
                maxpop -= 3;
            }
            if (Normal.Hydro.Value < 3)
            {
                maxpop -= 1;
            }
            if (Normal.Hydro.Value < 3)
            {
                maxpop -= 1;
            }
            if (Normal.Hydro.Value == 0)
            {
                maxpop -= 1;
            }
            if (Normal.Atmosphere.Value < 4)
            {
                maxpop = 0;
            }
            if (Normal.Atmosphere.Value > 9)
            {
                maxpop = 0;
            }
            if (orbit.OrbitalType != Orbit.OrbitType.HABITABLE)
            {
                if (OrbitNum > HZone)
                {
                    maxpop = maxpop - (((int)OrbitNum - HZone) * 2);
                }
                else
                {
                    maxpop = maxpop - ((HZone - (int)OrbitNum) * 2);
                }
            }
            maxpop = Math.Max(0, maxpop);
            return maxpop;
        }
    }
}
