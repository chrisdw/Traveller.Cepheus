using org.DownesWard.Traveller.AnimalEncounters;
using org.DownesWard.Traveller.SystemGeneration.Campaigns;
using org.DownesWard.Traveller.SystemGeneration.Resources;
using org.DownesWard.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

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
        public List<Satellite> Satellites { get; set; } = new List<Satellite>();
        public bool MainWorld { get; set; }

        public double OrbitNumber { get; set; }

        public TravInfo Normal { get; }
        public TravInfo Collapse { get; } 

        public bool Life { get; set; }
        public int LifeFactor { get; set; }
        public string Name { get; set; }

        public TableGenerator TableGenerator { get; } = new TableGenerator();
        public Encounters Encounters { get; set; }

        // Temprature tables
        public double[] Summer { get; } = new double[Constants.NUM_HEX_ROWS * 2];
        public double[] Fall { get; } = new double[Constants.NUM_HEX_ROWS * 2];
        public double[] Winter { get; } = new double[Constants.NUM_HEX_ROWS * 2];

        public WorldType PlanetType { get; set; }

        protected Configuration _configuration;

        public string DisplayString
        {
            get
            {
                return Normal.UWP(PlanetType, Diameter);
            }
        }

        public string OrbitRangeString
        {
            get
            {
                var range = OrbitRange;
                if (range > 1)
                {
                    // Use AU
                    return string.Format("{0:N2} AU", range);
                }
                else
                {
                    // KM
                    range *= Constants.MKM_PER_AU;
                    if (range < 0.01)
                    {
                        range *= 100000000;
                        return string.Format("{0:N2} Km", range);
                    }
                    else
                    {
                        return string.Format("{0:N2} MKm", range);
                    }
                }
            }
        }

        public string OrbitPeriodString
        {
            get
            {
                var period = OrbitPeriod;
                if (period < 3.0)
                {
                    return string.Format("{0:N2} Days", period * Constants.DAYS_PER_YEAR);
                }
                else
                {
                    return string.Format("{0:N2} Years", period);
                }
            }
         }

        public List<TemperatureData> Temperature
        {
            get
            {
                var list = new List<TemperatureData>();
                for (var i = 0; i < (Constants.NUM_HEX_ROWS * 2) - 1; i += 2)
                {
                    var temp = new TemperatureData()
                    {
                        Row = (i / 2 + 1)
                    };

                    temp.Summer = string.Format("{0:N2}/{1:N2}", Summer[i], Summer[i + 1]);
                    temp.Fall = string.Format("{0:N2}/{1:N2}", Fall[i], Fall[i + 1]);
                    temp.Winter = string.Format("{0:N2}/{1:N2}", Winter[i], Winter[i + 1]);

                    list.Add(temp);
                }
                return list;
            }
        }

        public Planet(Configuration configuration)
        {
            _configuration = configuration;
            Normal = new TravInfo(configuration);
            Collapse = new TravInfo(configuration);

            if (_configuration.CurrentCampaign == Campaign.HOSTILE)
            {
                Normal.CurrentCampaign = new Hostile();
                Collapse.CurrentCampaign = new Hostile();
            }
            else
            {
                Normal.CurrentCampaign = new Classic();
                Collapse.CurrentCampaign = new Classic();
            }
        }

        public void Generate()
        {
            if (_configuration.Generation == GenerationType.SIMPLE)
            {
                Normal.Size.Value = Common.d6() + Common.d6() - 2;
                Normal.Atmosphere.Value = Common.d6() + Common.d6() - 7 + Normal.Size.Value;
                Normal.Hydro.Value = Common.d6() + Common.d6() - 7 + Normal.Atmosphere.Value;
                Normal.GetTravInfo();
                Normal.CompleteTravInfo();

                Maxpop = GetBasicMaxPop();

                // Intialise physical parameters of collapse data
                Collapse.Size.Value = Normal.Size.Value;
                Collapse.Atmosphere.Value = Normal.Atmosphere.Value;
                Collapse.Hydro.Value = Normal.Hydro.Value;
            }
        }


        public double Mass
        {
            get
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
        }

        public double Grav
        {
            get
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
        }

        protected void GetTravInfo()
        {
            Normal.GetTravInfo();
        }

        public int FleshOut(double OrbitNum, Orbit myOrbit, Star primary, int HZone, double ComLumAddFromPrim)
        {
            var M = primary.StellarMass;
            var D = myOrbit.Range;
            var numsats = 0;
            var dieroll = 0;

            var X = (D * D * D) / M;
            OrbitPeriod = Math.Sqrt(X);
            OrbitRange = myOrbit.Range;
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

                    Maxpop = BuildSatellites(OrbitNum, myOrbit, primary, HZone, ComLumAddFromPrim, numsats);
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
                    Normal.Atmosphere.Value = GetAtmosphere(myOrbit);
                    Pressure = GetPressure();
                    Normal.Hydro.Value = GetHydrographics(myOrbit);
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

                    GetTempChart(this, myOrbit, ComLumAddFromPrim, primary, false);

                    GetNativeLife(false, primary);

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
                    var satMaxPop = BuildSatellites(OrbitNum, myOrbit, primary, HZone, ComLumAddFromPrim, numsats);

                    Maxpop = Math.Max(satMaxPop, Maxpop);

                    if (_configuration.GenerateTravInfo)
                    {
                        GetTravInfo();
                    }
                    break;

                case Orbit.OccupiedBy.PLANETOID:
                    PlanetType = WorldType.PLANETOID;
                    break;
                case Orbit.OccupiedBy.STAR:
                    PlanetType = WorldType.STAR;
                    break;
            }

            if (Life)
            {
                if (LifeFactor > 5)
                {
                    Encounters = TableGenerator.Generate(2, Normal);
                }
                else
                {
                    Encounters = TableGenerator.Generate(1, Normal);
                }
            }
            Collapse.Size.Value = Normal.Size.Value;
            Collapse.Atmosphere.Value = Normal.Atmosphere.Value;
            Collapse.Hydro.Value = Normal.Hydro.Value;
            return Maxpop;
        }

        private int BuildSatellites(double OrbitNum, Orbit myOrbit, Star primary, int HZone, double ComLumAddFromPrim, int numsats)
        {
            var ringcount = 0;
            var retry = false;
            var ret = 0;

            for (var i = 0; i < numsats; i++)
            {
                var satellite = new Satellite(_configuration)
                {
                    Name = string.Format("{0}/A{1}", Name, i)
                };
                Satellites.Add(satellite);
                satellite.Build(Normal.Size.Value, PlanetType);
                if (satellite.PlanetType == WorldType.RING)
                {
                    ringcount += 1;
                }
                if (ringcount > 3)
                {
                    // Can only have 3 rings
                    while (satellite.PlanetType == WorldType.RING)
                    {
                        satellite.Build(Normal.Size.Value, PlanetType);
                    }
                }
                // Check for repeat orbits
                do
                {
                    retry = false;
                    for (var j = 0; j < i - 1; j++)
                    {
                        if (Satellites[j].OrbitNumber == Satellites[i].OrbitNumber)
                        {
                            satellite.SetOrbit(Normal.Size.Value, PlanetType);
                        }
                    }
                } while (retry);
                var k = satellite.FleshOut(this, myOrbit, primary, HZone, ComLumAddFromPrim);
                ret = Math.Max(k, ret);

            }
            Satellites.Sort();
            return ret;
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

        protected int GetAtmosphere(Orbit orbit)
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

            if (_configuration.SpaceOpera)
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

        protected int GetHydrographics(Orbit orbit)
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

            if (_configuration.SpaceOpera)
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

        protected int GetMaxPop(Orbit orbit, int HZone, double OrbitNum)
        {
            int maxpop = GetBasicMaxPop();
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

        private int GetBasicMaxPop()
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
            maxpop = Math.Max(0, maxpop);
            return maxpop;
        }

        protected void GetTempChart(Planet mainWorld, Orbit orbit, double ComLumAddFromPrim, Star primary, bool forSatellite)
        {
            var L = primary.Luminosity;
            var O = Constants.HABITNUM / Math.Sqrt(orbit.Range);
            var atmosCode = 0;
            var E = 0.0;

            if (Normal.Atmosphere.Value < 4)
            {
                atmosCode = 0;
            }
            else if (Normal.Atmosphere.Value < 10)
            {
                atmosCode = 1;
            }
            else if (Normal.Atmosphere.Value < 16)
            {
                atmosCode = 2;
            }
            else
            {
                atmosCode = 3;
            }

            if (orbit.OrbitalType == Orbit.OrbitType.HABITABLE)
            {
                E = DataTables.EnergyAbsorbHZ[Normal.Hydro.Value, atmosCode];
            }
            else
            {
                E = DataTables.EnergyAbsorbNHZ[Normal.Hydro.Value, atmosCode];
            }

            var G = DataTables.Greenhouse[Normal.Atmosphere.Value];

            if (_configuration.UseGaiaFactor && Maxpop > 5)
            {
                E = Common.CalcGaiaFactor(L, O, G, E);
            }

            var X = ((L * O) + ComLumAddFromPrim) * E * G;
            Temp = X - 273;

            var DayPlus = Daytime(Rotation / 2, L, orbit.Range, forSatellite);
            var NightMinus = Nighttime(Rotation / 2, forSatellite);
            Tilt = AxialTilt();
            var k = 0;
            if (Tilt == 0.0)
            {
                k = 0;
            }
            else if (Tilt < 6.0)
            {
                k = 1;
            }
            else if (Tilt < 110)
            {
                k = 2;
            }
            else if (Tilt < 16.0)
            {
                k = 3;
            }
            else if (Tilt < 21.0)
            {
                k = 4;
            }
            else if (Tilt < 26.0)
            {
                k = 5;
            }
            else if (Tilt < 31.0)
            {
                k = 6;
            }
            else if (Tilt < 36.0)
            {
                k = 7;
            }
            else if (Tilt < 46.0)
            {
                k = 8;
            }
            else if (Tilt < 61.0)
            {
                k = 9;
            }
            else if (Tilt < 85.0)
            {
                k = 10;
            }
            else
            {
                k = 11;
            }

            Ecc = OrbitEcc();

            for (var i = 0; i < (Constants.NUM_HEX_ROWS * 2) - 1; i++)
            {
                if (i % 2 == 0)
                {
                    X = DayPlus;
                }
                else
                {
                    X = NightMinus;
                }

                if (!TidallyLocked || forSatellite)
                {
                    var eccentricty = 0.0;
                    if (forSatellite)
                    {
                        eccentricty = mainWorld.Ecc;
                    }
                    else
                    {
                        eccentricty = Ecc;
                    }
                    Summer[i] = Temp + DataTables.LatitudeMods[i / 2, Normal.Size.Value] + (eccentricty * 30) + ((0.6 * Tilt) * DataTables.AxialTiltEffects[i / 2, k]) + X;
                    Fall[i] = Temp + DataTables.LatitudeMods[i / 2, Normal.Size.Value] + X;
                    Winter[i] = Temp + DataTables.LatitudeMods[i / 2, Normal.Size.Value] - (eccentricty * 30) - (Tilt * DataTables.AxialTiltEffects[i / 2, k]) + X;
                }
                else
                {
                    var latEffect = 0.0;
                    if (i % 2 == 0)
                    {
                        latEffect = Math.Min((X / Normal.Size.Value) * i / 2, X);
                    }
                    else
                    {
                        latEffect = Math.Max((X / Normal.Size.Value) * i / 2, X);
                    }
                    // Axial tilt has no effect on a tiddaly locked world
                    Summer[i] = Temp + DataTables.LatitudeMods[i / 2, Normal.Size.Value] + (Ecc * 30) + latEffect;
                    Fall[i] = Temp + DataTables.LatitudeMods[i / 2, Normal.Size.Value] + latEffect;
                    Winter[i] = Temp + DataTables.LatitudeMods[i / 2, Normal.Size.Value] - (Ecc * 30) + latEffect;
                }
                if (_configuration.UseFarenheight)
                {
                    Summer[i] = Common.CtoF(Summer[i]);
                    Fall[i] = Common.CtoF(Fall[i]);
                    Winter[i] = Common.CtoF(Winter[i]);
                }
            }
        }

        protected double Daytime(double daylength, double lum, double dist, bool forSatellite)
        {
            var X = 0.0;
            var Max = 0.0;

            var R = lum / Math.Sqrt(dist);
            switch (Normal.Atmosphere.Value)
            {
                case 0:
                    X = 1.0 * R * daylength;

                    Max = 0.1 * (Temp + 273) * R;
                    break;
                case 1:
                    X = 0.9 * R * daylength;
                    Max = 0.3 * (Temp + 273) * R;
                    break;
                case 2:
                case 3:
                case 14:
                    X = 0.8 * R * daylength;
                    Max = 0.8 * (Temp + 273) * R;
                    break;
                case 4:
                case 5:
                    X = 0.6 * R * daylength;
                    Max = 1.5 * (Temp + 273) * R;
                    break;
                case 6:
                case 7:
                    X = 0.5 * R * daylength;
                    Max = 2.5 * (Temp + 273) * R;
                    break;
                case 8:
                case 9:
                    X = 0.4 * R * daylength;
                    Max = 4.0 * (Temp + 273) * R;
                    break;
                default:
                    X = 0.2 * R * daylength;
                    Max = 5.0 * (Temp + 273) * R;
                    break;
            }
            if (X > Max)
            {
                X = Max;
            }
            if (TidallyLocked && !forSatellite)
            {
                return Max;
            }
            else
            {
                return X;
            }
        }

        protected double Nighttime(double daylength, bool forSatellite)
        {
            var X = 0.0;
            var Max = 0.0;

            switch (Normal.Atmosphere.Value)
            {
                case 0:
                    X = 20.0 * daylength;
                    Max = 0.8 * (Temp + 273);
                    break;
                case 1:
                    X = 15.0 * daylength;
                    Max = 0.7 * (Temp + 273);
                    break;
                case 2:
                case 3:
                case 14:
                    X = 8.0 * daylength;
                    Max = 0.5 * (Temp + 273);
                    break;
                case 4:
                case 5:
                    X = 3.0 * daylength;
                    Max = 0.3 * (Temp + 273);
                    break;
                case 6:
                case 7:
                    X = 1.0 * daylength;
                    Max = 0.15 * (Temp + 273);
                    break;
                case 8:
                case 9:
                    X = 0.5 * daylength;
                    Max = 0.1 * (Temp + 273);
                    break;
                default:
                    X = 0.2 * daylength;
                    Max = 0.05 * (Temp + 273);
                    break;
            }
            if (X > Max)
            {
                X = Max;
            }
            if (TidallyLocked && !forSatellite)
            {
                return -Max;
            }
            else
            {
                return -X;
            }
        }

        protected double AxialTilt()
        {
            var dieroll = Common.d6() + Common.d6();
            switch (dieroll)
            {
                case 2:
                case 3:
                    return (-1.0 + Common.d10());

                case 4:
                case 5:
                    return (9.0 + Common.d10());

                case 6:
                case 7:
                    return (19.0 + Common.d10());

                case 8:
                case 9:
                    return (20.0 + Common.d10());

                case 10:
                case 11:
                    return (29.0 + Common.d10());
                default:
                    dieroll = Common.d6();
                    switch (dieroll)
                    {
                        case 1:
                        case 2:
                            return (49 + Common.d10());

                        case 3:
                            return (59 + Common.d10());

                        case 4:
                            return (69 + Common.d10());

                        case 5:
                            return (79 + Common.d10());

                        default:
                            return (90.0);
                    }
            }
        }

        protected double OrbitEcc()
        {
            var dieroll = Common.d6() + Common.d6();
            switch (dieroll)
            {
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                    return 0.0;
                case 8:
                    return 0.005;
                case 9:
                    return 0.01;
                case 10:
                    return 0.015;
                case 11:
                    return 0.02;
                default:
                    dieroll = Common.d6() + Common.d6();
                    switch (dieroll)
                    {
                        case 1:
                            return 0.025;
                        case 2:
                            return 0.05;
                        case 3:
                            return 0.1;
                        case 4:
                            return 0.2;
                        case 5:
                            return 0.25;
                        default:
                            return 0.3;
                    }
            }
        }

        protected void GetNativeLife(bool basic, Star primary)
        {
            var dieroll = Common.d6() + Common.d6();

            // Atmosphere effects
            if (Normal.Atmosphere.Value == 0)
            {
                dieroll -= 3;
            }
            if (Normal.Atmosphere.Value >= 4 && Normal.Atmosphere.Value <= 9)
            {
                dieroll += 4;
            }

            // Hydrographic effects
            if (Normal.Hydro.Value == 0)
            {
                dieroll -= 2;
            }
            if (Normal.Hydro.Value >= 2 && Normal.Hydro.Value <= 8)
            {
                dieroll += 1;
            }

            if (!basic)
            {
                // Temprature effects
                if (Temp < -20.0)
                {
                    dieroll -= 1;
                }
                else if (Temp > 30)
                {
                    dieroll -= 1;
                }

                // Stellar effects
                if (primary.StarType == Star.StellarType.G || primary.StarType == Star.StellarType.K)
                {
                    dieroll += 1;
                }
                else if (primary.StarType == Star.StellarType.F ||
                    primary.StarType == Star.StellarType.A ||
                    primary.StarType == Star.StellarType.B)
                {
                    dieroll -= 1;
                }
            }

            Life = (dieroll >= 10);
            if (Life)
            {
                dieroll = Common.d6() + Common.d6() - 2;
                switch (Normal.Atmosphere.Value)
                {
                    case 0:
                    case 1:
                        dieroll -= 8;
                        break;
                    case 2:
                    case 3:
                        dieroll -= 6;
                        break;
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                        dieroll += 2;
                        break;
                    case 9:
                    case 10:
                        dieroll -= 4;
                        break;
                    case 11:
                        dieroll -= 6;
                        break;
                    case 12:
                        dieroll -= 8;
                        break;
                    default:
                        dieroll -= 2;
                        break;
                }
                if (Normal.Hydro.Value == 0)
                {
                    dieroll -= 4;
                }
                else if (Normal.Hydro.Value == 1)
                {
                    dieroll -= 2;
                }
                else if (Normal.Hydro.Value >= 6 && Normal.Hydro.Value <= 9)
                {
                    dieroll += 1;
                }
                else if (Normal.Hydro.Value == 10)
                {
                    dieroll -= 1;
                }
                if (dieroll < 1)
                {
                    dieroll = 1;
                }
                LifeFactor = dieroll;
            }
        }

        /// <summary>
        /// This version of Collapse is used for main worlds and simple 
        /// generation. There's another version used when walking an
        /// entire system.
        /// </summary>
        public void DoCollapse()
        {
            var dieroll = 0;

            // Tech level decline (or for low tech worlds possibly increase!)
            if (Normal.TechLevel.Value <= 8)
            {
                dieroll = Common.d6() - 3;
            }
            else if (Normal.TechLevel.Value >= 9 && Normal.TechLevel.Value <= 10)
            {
                dieroll = Common.d6();
            }
            else if (Normal.TechLevel.Value >= 11 && Normal.TechLevel.Value <= 13)
            {
                dieroll = Common.d6() + Common.d6();
            }
            else
            {
                dieroll = Common.d6() + Common.d6() + Common.d6();
            }
            Collapse.TechLevel.Value = Normal.TechLevel.Value - dieroll;
            var techLevelFall = Normal.TechLevel.Value - Collapse.TechLevel.Value;

            if (Normal.Pop.Value > Maxpop)
            {
                Collapse.Pop.Value = Maxpop;
            }
            else
            {
                Collapse.Pop.Value = Normal.Pop.Value;
            }

            if (Collapse.Pop.Value == 0)
            {
                // Everybody died!
                Collapse.Starport = 'X';
                Collapse.Government.Value = 0;
                Collapse.Law.Value = 0;
                Collapse.TechLevel.Value = 0;
                Collapse.PopMult = 0;
            }
            else
            {
                Collapse.PopMult = Normal.PopMult - (techLevelFall / 4);
                if (Collapse.PopMult < 0)
                {
                    Collapse.Pop.Value -= 1;
                    Collapse.PopMult += 9;
                }
                if (Collapse.Pop.Value <= 5)
                {
                    Collapse.TechLevel.Value -= 1;
                    Collapse.PopMult /= 2;
                    if (Collapse.PopMult < 1)
                    {
                        Collapse.Pop.Value -= 1;
                        Collapse.PopMult = 5;
                    }
                }

                // What happended to the Starport
                dieroll = Common.d6();
                if (dieroll > techLevelFall)
                {
                    Collapse.ReduceStarport(1);
                    // Did any bases survive
                    if (Normal.Bases.Contains(Languages.Base_Naval))
                    {
                        dieroll = Common.d10();
                        if (dieroll > 8)
                        {
                            Collapse.Bases += Languages.Base_Naval;
                        }
                    }
                    if (Normal.Bases.Contains(Languages.Base_Scout))
                    {
                        dieroll = Common.d10();
                        if (dieroll > 7)
                        {
                            Collapse.Bases += Languages.Base_Scout;
                        }
                    }
                    if (Normal.Bases.Contains(Languages.Base_Military))
                    {
                        dieroll = Common.d10();
                        if (dieroll > 8)
                        {
                            Collapse.Bases += Languages.Base_Military;
                        }
                    }
                }
                else if (dieroll == techLevelFall)
                {
                    Collapse.ReduceStarport(2);
                    Collapse.Bases = string.Empty;
                }
                else
                {
                    Collapse.Starport = 'X';
                    Collapse.Bases = string.Empty;
                }

                var balk = Collapse.Size.Value + Collapse.Pop.Value - Collapse.TechLevel.Value;

                dieroll = Common.d6() + Common.d6();
                if (dieroll <= balk)
                {
                    Collapse.Remarks += "Balkanised";
                }

                if (Collapse.Pop.Value > 4)
                {
                    dieroll = Common.d10();
                    if (dieroll < techLevelFall)
                    {
                        Collapse.Government.Value = 6;
                    }
                    else
                    {
                        Collapse.Government.Value = Common.d6() + Common.d6() - 7 + Collapse.Pop.Value;
                    }
                    Collapse.Law.Value = Common.d6() + Common.d6() - 7 + Collapse.Government.Value;
                }
                else
                {
                    Collapse.Law.Value = Common.d6() + Common.d6() - 7 + Collapse.Government.Value;
                }

                if (Collapse.Government.Value == 6)
                {
                    Collapse.Law.Value += 4;
                }

                // Population recovery
                Collapse.PopMult *= 2;
                if (Collapse.PopMult > 9)
                {
                    Collapse.Pop.Value += 1;
                    Collapse.PopMult -= 10;
                }

                Collapse.Factions = Faction.GenerateFactions(Collapse, _configuration);

                Collapse.DoTradeClassification();
            }
        }

        /// <summary>
        /// This version of DoCollapse is used when generating a complete system
        /// </summary>
        /// <param name="main"></param>
        public void DoCollapse(Planet main)
        {
            if (!MainWorld && PlanetType != WorldType.SGG && PlanetType != WorldType.LGG && PlanetType != WorldType.STAR)
            {
                var dieroll = 0;

                // Tech level decline (or for low tech worlds possibly increase!)
                if (Normal.TechLevel.Value <= 8)
                {
                    dieroll = Common.d6() - 3;
                }
                else if (Normal.TechLevel.Value >= 9 && Normal.TechLevel.Value <= 10)
                {
                    dieroll = Common.d6();
                }
                else if (Normal.TechLevel.Value >= 11 && Normal.TechLevel.Value <= 13)
                {
                    dieroll = Common.d6() + Common.d6();
                }
                else
                {
                    dieroll = Common.d6() + Common.d6() + Common.d6();
                }
                Collapse.TechLevel.Value = Normal.TechLevel.Value - dieroll;
                var techLevelFall = Normal.TechLevel.Value - Collapse.TechLevel.Value;

                if (Normal.Pop.Value > Maxpop)
                {
                    Collapse.Pop.Value = Maxpop;
                }
                else
                {
                    Collapse.Pop.Value = Normal.Pop.Value;
                }

                if (Collapse.Pop.Value == 0)
                {
                    // Everybody died!
                    Collapse.Starport = 'Y';
                    Collapse.Government.Value = 0;
                    Collapse.Law.Value = 0;
                    Collapse.TechLevel.Value = 0;
                    Collapse.PopMult = 0;
                }
                else
                {
                    Collapse.PopMult = Normal.PopMult - (techLevelFall / 4);
                    if (Collapse.PopMult < 0)
                    {
                        Collapse.Pop.Value -= 1;
                        Collapse.PopMult += 9;
                    }
                    if (Collapse.Pop.Value <= 5)
                    {
                        Collapse.TechLevel.Value -= 1;
                        Collapse.PopMult /= 2;
                        if (Collapse.PopMult < 1)
                        {
                            Collapse.Pop.Value -= 1;
                            Collapse.PopMult = 5;
                        }
                    }

                    dieroll = Common.d6();
                    if (dieroll > techLevelFall)
                    {
                        Collapse.ReduceStarport(1);
                    }
                    else if (dieroll == techLevelFall)
                    {
                        Collapse.ReduceStarport(2);
                        Collapse.Bases = string.Empty;
                    }
                    else
                    {
                        Collapse.Starport = 'Y';
                        Collapse.Bases = string.Empty;
                    }

                    var balk = Collapse.Size.Value + Collapse.Pop.Value - Collapse.TechLevel.Value;

                    dieroll = Common.d6() + Common.d6();
                    if (dieroll <= balk)
                    {
                        Collapse.Remarks += "Balk";
                    }

                    if (Collapse.Pop.Value > 4)
                    {
                        dieroll = Common.d10();
                        if (dieroll < techLevelFall)
                        {
                            Collapse.Government.Value = 6;
                        }
                        else
                        {
                            Collapse.Government.Value = Common.d6() + Common.d6() - 7 + Collapse.Pop.Value;
                        }
                    }
                    else
                    {
                        Collapse.Government.Value = Common.d6() + Common.d6() - 7 + Collapse.Pop.Value;
                    }
                    Collapse.Law.Value = Common.d6() + Common.d6() - 7 + Collapse.Government.Value;
                    if (Collapse.Government.Value == 6)
                    {
                        Collapse.Law.Value += 4;
                    }
                    // Population recovery
                    Collapse.PopMult *= 2;
                    if (Collapse.PopMult > 9)
                    {
                        Collapse.Pop.Value += 1;
                        Collapse.PopMult -= 10;
                    }
                    Collapse.DoSubordinate(main.Collapse);
                }
            }
            foreach (var satellite in Satellites)
            {
                satellite.DoCollapse(main);
            }
        }

        public void CompleteTravInfo()
        {
            Normal.CompleteTravInfo();
        }

        public double Population(bool forCollapse)
        {
            var pop = 0.0;

            foreach (var satellite in Satellites)
            {
                pop += satellite.Population(forCollapse);
            }
            if (forCollapse)
            {
                pop += Collapse.Population();
            }
            else
            {
                pop += Normal.Population();
            }
            return pop;
        }

        public void CompleteTravInfo(Planet mainworld)
        {
            if (!MainWorld && PlanetType != WorldType.SMALL && PlanetType != WorldType.LGG && PlanetType != WorldType.SGG)
            {
                Normal.DoSubordinate(mainworld.Normal);
            }
            foreach (var satellite in Satellites)
            {
                satellite.CompleteTravInfo(mainworld);
            }
        }
        public virtual void SaveToXML(XmlElement objOrbit)
        {
            var nfi = System.Globalization.NumberFormatInfo.InvariantInfo;

            var xePlanet = objOrbit.OwnerDocument.CreateElement("Planet");
            objOrbit.AppendChild(xePlanet);
            Common.CreateTextNode(xePlanet, "Type", PlanetType.ToString());
            Common.CreateTextNode(xePlanet, "Dense", Dense.ToString());
            Common.CreateTextNode(xePlanet, "Mass", Mass.ToString());
            Common.CreateTextNode(xePlanet, "Gravity", Grav.ToString());
            Common.CreateTextNode(xePlanet, "Pressure", Pressure.ToString());
            Common.CreateTextNode(xePlanet, "MaxPop", Maxpop.ToString());
            Common.CreateTextNode(xePlanet, "OrbitRange", OrbitRange.ToString());
            Common.CreateTextNode(xePlanet, "OrbitNumber", OrbitNumber.ToString());
            Common.CreateTextNode(xePlanet, "Tilt", Tilt.ToString());
            Common.CreateTextNode(xePlanet, "Ecc", Ecc.ToString());
            Common.CreateTextNode(xePlanet, "Rotation", Rotation.ToString());
            Common.CreateTextNode(xePlanet, "TidallyLocked", TidallyLocked.ToString());
            Common.CreateTextNode(xePlanet, "Temp", Temp.ToString());
            Common.CreateTextNode(xePlanet, "Diameter", Diameter.ToString());
            Common.CreateTextNode(xePlanet, "NumSats", Satellites.Count.ToString());
            Common.CreateTextNode(xePlanet, "Mainworld", MainWorld.ToString());
            Common.CreateTextNode(xePlanet, "NormalUWP", Normal.DisplayString(PlanetType, Diameter));
            Common.CreateTextNode(xePlanet, "CollapseUWP", Collapse.DisplayString(PlanetType, Diameter));
            Common.CreateTextNode(xePlanet, "Life", Life.ToString());
            Common.CreateTextNode(xePlanet, "LifeFactor", LifeFactor.ToString());
            Common.CreateTextNode(xePlanet, "Name", Name);

            var xeChild = objOrbit.OwnerDocument.CreateElement("Temperature");
            for (var i = 0; i < (Constants.NUM_HEX_ROWS * 2) - 1; i += 2)
            {
                var xeTemp = objOrbit.OwnerDocument.CreateElement("Row" + (i / 2 + 1).ToString());

                Common.CreateTextNode(xeTemp, "Summer", Summer[i].ToString("N", nfi) + "/" + Summer[i + 1].ToString("N", nfi));
                Common.CreateTextNode(xeTemp, "Fall", Fall[i].ToString("N", nfi) + "/" + Fall[i + 1].ToString("N", nfi));
                Common.CreateTextNode(xeTemp, "Winter", Winter[i].ToString("N", nfi) + "/" + Winter[i + 1].ToString("N", nfi));
 
                xeChild.AppendChild(xeTemp);
            }

            xePlanet.AppendChild(xeChild);

            if (Life)
            {
                xeChild = objOrbit.OwnerDocument.CreateElement("AnimalEncounters");
                TableGenerator.WriteToXML(xeChild);
                xePlanet.AppendChild(xeChild);
            }

            Normal.SaveToXML(xePlanet);
            Collapse.SaveToXML(xePlanet);

            foreach (var satellite in Satellites)
            {
                satellite.SaveToXML(xePlanet);
            }
        }

    }
}
