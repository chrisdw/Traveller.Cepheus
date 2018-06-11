using org.DownesWard.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.SystemGeneration
{
    public class Star
    {
        public enum StellarType
        {
            O,
            B,
            A,
            F,
            G,
            K,
            M
        }

        public const int FAR_ORBIT = 99;

        protected int TypeRoll { get; set; }
        protected int ClassRoll { get; set; }
        public StellarType StarType { get; set; }
        public char LumClass { get; set; }
        public char DecClass { get; set; }
        public double StellarMass { get; set; }
        public double Luminosity { get; set; }
        public List<Orbit> Orbits { get; } = new List<Orbit>();
        public int HZone { get; set; }
        public List<CompanionStar> Companions { get; } = new List<CompanionStar>();
        public string Name { get; set; }
        public int NumOrbits { get; set; }
        public int NumCompanions { get; set; }

        public Star()
        {
            StarType = GetStellarType();
            LumClass = GetLumClass();
            DecClass = GetDecClass();
            StellarMass = GetStellarMass();
            Luminosity = GetLuminosity();
            if (StarType != StellarType.O)
            {
                NumOrbits = GetNumOrbits();
            }
        }

        public Star(StellarType stellarType, char stellarClass, char decimalClass)
        {
            StarType = stellarType;
            LumClass = stellarClass;
            DecClass = decimalClass;
            StellarMass = GetStellarMass();
            Luminosity = GetLuminosity();
            if (StarType != StellarType.O)
            {
                NumOrbits = GetNumOrbits();
            }
        }

        public static bool Validate(string descriptor)
        {
            var valid = true;

            if (string.IsNullOrEmpty(descriptor) || descriptor.Length  < 3)
            {
                throw new ArgumentException("The descriptor is too short", "descriptor");
            }

            return valid;
        }

        public StellarType GetStellarType()
        {
            var dieroll = Common.d6() + Common.d6() + Common.d6();
            TypeRoll = 2;
            if (dieroll == 3)
            {
                return StellarType.O;
            }
            else if (dieroll == 4)
            {
                return StellarType.B;
            }
            dieroll = Common.d6() + Common.d6();
            TypeRoll = dieroll;
            switch (dieroll)
            {
                case 2:
                    return StellarType.A;
                case 3:
                    return StellarType.M;
                case 4:
                    return StellarType.M;
                case 5:
                    return StellarType.M;
                case 6:
                    return StellarType.M;
                case 7:
                    return StellarType.M;
                case 8:
                    return StellarType.K;
                case 9:
                    return StellarType.G;
                case 10:
                    return StellarType.G;
                case 11:
                    return StellarType.F;
                case 12:
                default:
                    return StellarType.F;
            }
        }

        public char GetLumClass()
        {
            var dieroll = Common.d6() + Common.d6() + Common.d6();
            ClassRoll = 2;
            if (dieroll == 3)
            {
                return 'a';
            }
            else if (dieroll == 4)
            {
                return 'b';
            }
            dieroll = Common.d6() + Common.d6();
            ClassRoll = dieroll;
            switch (dieroll)
            {
                case 2:
                    return '2';
                case 3:
                    return '3';
                case 4:
                    if (StarType == StellarType.M)
                    {
                        return '5';
                    }
                    else
                    {
                        return '4';
                    }
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                    return '5';
                default:
                    return ' ';
            }
        }

        private char GetDecClass()
        {
            var dieroll = Common.d10() - 1;
            if (StarType == StellarType.O && dieroll < 5)
            {
                dieroll += 5;
            }

            if (StarType == StellarType.K && LumClass == '4' && dieroll > 4)
            {
                dieroll -= 5;
            }
            dieroll = dieroll.Clamp(0, 9);
            return dieroll.ToString()[0];
        }

        public double GetStellarMass()
        {
            var starSize = 0;
            var A = 0.0;
            var B = 0.0;
            var X = int.Parse(DecClass.ToString());

            switch (StarType)
            {
                case StellarType.O: starSize = 0; break;
                case StellarType.B: starSize = 0; break;
                case StellarType.F: starSize = 4; break;
                case StellarType.G: starSize = 6; break;
                case StellarType.K: starSize = 8; break;
                case StellarType.M: starSize = 10; break;
            }

            switch (LumClass)
            {
                case 'a':
                    A = DataTables.MassTableIa[starSize];
                    B = DataTables.MassTableIa[starSize + 1];
                    break;
                case 'b':
                    A = DataTables.MassTableIb[starSize];
                    B = DataTables.MassTableIb[starSize + 1];
                    break;
                case '2':
                    A = DataTables.MassTableII[starSize];
                    B = DataTables.MassTableII[starSize + 1];
                    break;
                case '3':
                    A = DataTables.MassTableIII[starSize];
                    B = DataTables.MassTableIII[starSize + 1];
                    break;
                case '4':
                    A = DataTables.MassTableIV[starSize];
                    B = DataTables.MassTableIV[starSize + 1];
                    break;
                case '5':
                    A = DataTables.MassTableV[starSize];
                    B = DataTables.MassTableV[starSize + 1];
                    break;
                case 'D':
                    A = DataTables.MassTableD[starSize];
                    B = DataTables.MassTableD[starSize + 1];
                    break;
            }
            return ((((B - A) / 5) * X) + A);
        }

        public double GetLuminosity()
        {
            var starSize = 0;
            var A = 0.0;
            var B = 0.0;
            var X = int.Parse(DecClass.ToString());

            switch (StarType)
            {
                case StellarType.O: starSize = 0; break;
                case StellarType.B: starSize = 0; break;
                case StellarType.F: starSize = 4; break;
                case StellarType.G: starSize = 6; break;
                case StellarType.K: starSize = 8; break;
                case StellarType.M: starSize = 10; break;
            }

            switch (LumClass)
            {
                case 'a':
                    A = DataTables.LumTableIa[starSize];
                    B = DataTables.LumTableIa[starSize + 1];
                    if (StarType == StellarType.M && DecClass == '9')
                    {
                        return DataTables.LumTableIa[12];
                    }
                    break;
                case 'b':
                    A = DataTables.LumTableIb[starSize];
                    B = DataTables.LumTableIb[starSize + 1];
                    if (StarType == StellarType.M && DecClass == '9')
                    {
                        return DataTables.LumTableIb[12];
                    }
                    break;
                case '2':
                    A = DataTables.LumTableII[starSize];
                    B = DataTables.LumTableII[starSize + 1];
                    if (StarType == StellarType.M && DecClass == '9')
                    {
                        return DataTables.LumTableII[12];
                    }
                    break;
                case '3':
                    A = DataTables.LumTableIII[starSize];
                    B = DataTables.LumTableIII[starSize + 1];
                    if (StarType == StellarType.M && DecClass == '9')
                    {
                        return DataTables.LumTableIII[12];
                    }
                    break;
                case '4':
                    A = DataTables.LumTableIV[starSize];
                    B = DataTables.LumTableIV[starSize + 1];
                    if (StarType == StellarType.M && DecClass == '9')
                    {
                        return DataTables.LumTableIV[12];
                    }
                    break;
                case '5':
                    A = DataTables.LumTableV[starSize];
                    B = DataTables.LumTableV[starSize + 1];
                    if (StarType == StellarType.M && DecClass == '9')
                    {
                        return DataTables.LumTableV[12];
                    }
                    break;
                case 'D':
                    A = DataTables.LumTableD[starSize];
                    B = DataTables.LumTableD[starSize + 1];
                    if (StarType == StellarType.M && DecClass == '9')
                    {
                        return DataTables.LumTableD[12];
                    }
                    break;
            }

            return ((((B - A) / 5) * X) + A);
        }

        public int GetNumOrbits()
        {
            var dieroll = Common.d6() + Common.d6();

            if (LumClass == '1' || LumClass == 'a' || LumClass == 'b' || LumClass == '2')
            {
                dieroll += 8;
            }
            else if (LumClass == '3')
            {
                dieroll += 4;
            }
            else if (LumClass == '4')
            {
                dieroll += 2;
            }

            if (StarType == StellarType.K)
            {
                dieroll -= 2;
            }
            if (StarType == StellarType.M)
            {
                dieroll -= 4;
            }
            dieroll = dieroll.Clamp(0, 19);

            return dieroll;
        }

        public int FleshOut(Configuration configuration)
        {
            var SystemHabitability = 0;
            var ComLumAddFromPrim = 0.0;
            var k = 0;
            var nextChar = 'A';

            if (Companions.Count > 0)
            {
                Name = configuration.BaseName + "-" + nextChar++;
            }
            else
            {
                Name = configuration.BaseName;
            }

            SystemHabitability = FleshOutWorlds(configuration, ComLumAddFromPrim);

            foreach (var companion in Companions)
            {
                if (companion.OrbitNum != FAR_ORBIT)
                {
                    ComLumAddFromPrim = Constants.HABITNUM / Math.Sqrt(Orbits[companion.OrbitNum].Range);
                    Orbits[companion.OrbitNum].World.Normal.Remarks = companion.DisplayString();
                }
                else
                {
                    ComLumAddFromPrim = 0.0;
                }

                k = 0;

                companion.Name = configuration.BaseName + "-" + nextChar++;
                companion.BuildSystem(ComLumAddFromPrim);

                k = companion.FleshOut(configuration, ComLumAddFromPrim);
                SystemHabitability = Math.Max(k, SystemHabitability);
            }

            return SystemHabitability;
        }

        public void BuildSystem(double ComLumAddFromPrim)
        {
            HZone = -2;

            for (int i = 0; i < Constants.MAX_ORBITS; i++)
            {
                var orbit = new Orbit();
                orbit.OrbitRange(i);
                orbit.SetOrbitType(Luminosity, ComLumAddFromPrim);
                if (orbit.OrbitalType == Orbit.OrbitType.HABITABLE)
                {
                    HZone = i;
                }
                else if (orbit.OrbitalType == Orbit.OrbitType.OUTER && HZone == -2)
                {
                    HZone = i - 1;
                }
                Orbits.Add(orbit);
            }
            if (HZone == -2)
            {
                HZone = 10;
            }

            PlaceEmptyOrbits();
            PlaceCapturedPlanets();
            PlaceGasGiants();
            PlacePlanetoidBelts();

            foreach (var orbit in Orbits)
            {
                if (orbit.Number > NumOrbits)
                {
                    if (orbit.Occupied != Orbit.OccupiedBy.CAPTURED)
                    {
                        orbit.OrbitalType = Orbit.OrbitType.UNAVAILABLE;
                    }
                }
            }
        }

        public int FleshOut(Configuration configuration, double ComLumAddFromPrim)
        {
            var SystemHabitability = 0;
            var k = 0;

            SystemHabitability = FleshOutWorlds(configuration, ComLumAddFromPrim);
            foreach (var companion in Companions)
            {
                if (companion.OrbitNum != FAR_ORBIT)
                {
                    ComLumAddFromPrim = Constants.HABITNUM / Math.Sqrt(Orbits[companion.OrbitNum].Range);
                    Orbits[companion.OrbitNum].World.Normal.Remarks = companion.DisplayString();
                }
                else
                {
                    ComLumAddFromPrim = 0.0;
                }

                k = 0;

                companion.BuildSystem(ComLumAddFromPrim);

                k = companion.FleshOut(configuration, ComLumAddFromPrim);
                SystemHabitability = Math.Max(k, SystemHabitability);
            }
            return SystemHabitability;
        }

        public int FleshOutWorlds(Configuration configuration, double ComLumAddFromPrim)
        {
            var SystemHabitability = 0;
            var k = 0;
            var nextChar = 'A';

            foreach (var orbit in Orbits)
            {
                switch (orbit.OrbitalType)
                {
                    case Orbit.OrbitType.UNAVAILABLE:
                        if (orbit.Occupied != Orbit.OccupiedBy.STAR)
                        {
                            orbit.Occupied = Orbit.OccupiedBy.EMPTY;
                        }
                        break;
                    default:
                        if (orbit.Occupied != Orbit.OccupiedBy.UNOCCUPIED)
                        {
                            orbit.Occupied = Orbit.OccupiedBy.WORLD;
                        }
                        break;
                }
                if (orbit.Occupied != Orbit.OccupiedBy.EMPTY)
                {
                    var planet = new Planet
                    {
                        Name = Name + "/" + nextChar++
                    };
                    orbit.World = planet;
                    k = planet.FleshOut(configuration, orbit.Number, orbit, this, HZone, ComLumAddFromPrim);
                    SystemHabitability = Math.Max(k, SystemHabitability);
                }
            }
            return SystemHabitability;
        }

        public static int NumGasGiants()
        {
            var dieroll = Common.d6() + Common.d6();

            if (dieroll < 5)
            {
                return 0;
            }

            dieroll = Common.d6() + Common.d6();

            switch (dieroll)
            {
                case 2:
                case 3:
                    return 1;
                case 4:
                case 5:
                    return 2;
                case 6:
                case 7:
                    return 3;
                case 8:
                case 9:
                case 10:
                    return 4;
                case 11:
                case 12:
                    return 5;
            }
            return 0;
        }

        public static int NumPlanetoids()
        {
            var dieroll = Common.d6() + Common.d6();

            if (dieroll < 8)
            {
                return 0;
            }

            dieroll = Common.d6() + Common.d6();

            switch (dieroll)
            {
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    return 1;
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                    return 2;
                case 12:
                    return 3;
            }
            return 0;
        }

        private int NumEmptyOrbits()
        {
            var dieroll = Common.d6();

            if (StarType == StellarType.A || StarType == StellarType.B)
            {
                dieroll += 1;
            }
            if (dieroll < 5)
            {
                return 0;
            }
            dieroll = Common.d6();
            if (StarType == StellarType.A || StarType == StellarType.B)
            {
                dieroll += 1;
            }
            switch (dieroll)
            {
                case 1:
                case 2:
                    return 1;
                case 3:
                    return 2;
                default:
                    return 3;
            }
        }

        private int NumCapturedPlanets()
        {
            var dieroll = Common.d6();

            if (StarType == StellarType.A || StarType == StellarType.B)
            {
                dieroll += 1;
            }
            if (dieroll < 5)
            {
                return 0;
            }
            dieroll = Common.d6();
            switch (dieroll)
            {
                case 1:
                case 2:
                case 3:
                    return 1;
                case 4:
                case 5:
                    return 2;
                default:
                    return 3;
            }
        }
        private void PlaceEmptyOrbits()
        {
            var emptyOrbits = NumEmptyOrbits();
            var i = emptyOrbits;
            var hitcount = 0;
            while (i > 0)
            {
                var dieroll = Common.d6() + Common.d6() - 2;
                if (Orbits[dieroll].Occupied == Orbit.OccupiedBy.UNOCCUPIED)
                {
                    Orbits[dieroll].Occupied = Orbit.OccupiedBy.EMPTY;
                    i--;
                }
                else
                {
                    hitcount++;
                    if (hitcount > 100)
                    {
                        break;
                    }
                }
            }
        }

        private void PlaceCapturedPlanets()
        {
            var capturedPlanets = NumCapturedPlanets();
            var i = capturedPlanets;
            var hitcount = 0;
            while (i > 0)
            {
                var dieroll = Common.d6() + Common.d6();
                if (Orbits[dieroll].Occupied == Orbit.OccupiedBy.UNOCCUPIED)
                {
                    Orbits[dieroll].Occupied = Orbit.OccupiedBy.CAPTURED;
                    i--;
                }
                else
                {
                    hitcount++;
                    if (hitcount > 100)
                    {
                        break;
                    }
                }
            }
        }

        private void PlaceGasGiants()
        {
            var gasGiants = NumGasGiants();
            var i = gasGiants;
            var hitcount = 0;
            while (i > 0)
            {
                var dieroll = Common.d6() + Common.d6() - 3 + HZone;
                dieroll = dieroll.Clamp(0, Constants.MAX_ORBITS - 1);

                if (Orbits[dieroll].Occupied == Orbit.OccupiedBy.UNOCCUPIED)
                {
                    Orbits[dieroll].Occupied = Orbit.OccupiedBy.GASGIANT;
                    i--;
                }
                else
                {
                    hitcount++;
                    if (hitcount > 100)
                    {
                        break;
                    }
                }
            }
        }

        private void PlacePlanetoidBelts()
        {
            var planetoidBelts = NumPlanetoids();
            var i = planetoidBelts;
            var hitcount = 0;
            while (i > 0)
            {
                var dieroll = Common.d6() + Common.d6() - 1;
                if (Orbits[dieroll].Occupied == Orbit.OccupiedBy.UNOCCUPIED)
                {
                    Orbits[dieroll].Occupied = Orbit.OccupiedBy.PLANETOID;
                    i--;
                }
                else if (Orbits[dieroll].Occupied == Orbit.OccupiedBy.GASGIANT &&
                    Orbits[dieroll - 1].Occupied == Orbit.OccupiedBy.UNOCCUPIED)
                {
                    Orbits[dieroll-1].Occupied = Orbit.OccupiedBy.PLANETOID;
                    i--;
                }
                else
                {
                    hitcount++;
                    if (hitcount > 100)
                    {
                        break;
                    }
                }
            }
        }

        public void AvaialbleOribits(int num)
        {
            if (Companions[num].OrbitNum < FAR_ORBIT)
            {
                if (Companions[num].OrbitNum > 2)
                {
                    for (var j = (Companions[num].OrbitNum /2) + 1;  j < Companions[num].OrbitNum; j++)
                    {
                        if (Orbits[j].Occupied != Orbit.OccupiedBy.STAR)
                        {
                            Orbits[j].OrbitalType = Orbit.OrbitType.UNAVAILABLE;
                            Orbits[j].Occupied = Orbit.OccupiedBy.UNOCCUPIED;
                        }
                    }
                }
                Orbits[Companions[num].OrbitNum].OrbitalType = Orbit.OrbitType.UNAVAILABLE;
                Orbits[Companions[num].OrbitNum].Occupied = Orbit.OccupiedBy.STAR;
                if (Orbits[Companions[num].OrbitNum + 1].Occupied != Orbit.OccupiedBy.STAR)
                {
                    Orbits[Companions[num].OrbitNum + 1].OrbitalType = Orbit.OrbitType.UNAVAILABLE;
                    Orbits[Companions[num].OrbitNum + 1].Occupied = Orbit.OccupiedBy.UNOCCUPIED;
                }
            }
        }

        public static string TypeToChar(StellarType stellarType)
        {
            switch (stellarType)
            {
                case StellarType.O:
                    return "O";
                case StellarType.B:
                    return "B";
                case StellarType.A:
                    return "A";
                case StellarType.F:
                    return "F";
                case StellarType.G:
                    return "G";
                case StellarType.K:
                    return "K";
                case StellarType.M:
                    return "M";
            }
            return "X";
        }

        public static StellarType CharToType(char stellarClass)
        {
            switch (stellarClass)
            {
                case 'O':
                    return StellarType.O;
                case 'B':
                    return StellarType.B;
                case 'A':
                    return StellarType.A;
                case 'F':
                    return StellarType.F;
                case 'G':
                    return StellarType.G;
                case 'K':
                    return StellarType.K;
                case 'M':
                    return StellarType.M;
            }
            throw new ArgumentException("Stellar type out of range OBAFGKM", "stellarClass");
        }

        public string PrintLumClass()
        {
            if (LumClass == 'a')
            {
                return "Ia";
            }
            else if (LumClass == 'b')
            {
                return "Ib";
            }
            else if (LumClass == '1')
            {
                return "I";
            }
            else if (LumClass == '2')
            {
                return "II";
            }
            else if (LumClass == '3')
            {
                return "III";
            }
            else if (LumClass == '4')
            {
                return "IV";
            }
            else if (LumClass == '5')
            {
                return "V";
            }
            else if (LumClass == 'D')
            {
                return "D";
            }
            return "X";
        }

        public Planet GetMainWorld()
        {
            Planet cmw = null;
 
            foreach (var orbit in Orbits)
            {
                if (orbit.World != null)
                {
                    if (cmw == null)
                    {
                        cmw = orbit.World;
                    }

                    if (orbit.World.Normal.Population() > cmw.Normal.Population())
                    {
                        cmw = orbit.World;
                    }

                    // Check satellites
                    foreach (var satellite in orbit.World.Sattelites)
                    {
                        if (satellite.Normal.Population() > cmw.Normal.Population())
                        {
                            cmw = satellite;
                        }
                    }
                }
            }

            foreach (var star in Companions)
            {
                var mainworld = star.GetMainWorld();
                if (mainworld != null)
                {
                    if (mainworld.Normal.Population() > cmw.Normal.Population())
                    {
                        cmw = mainworld;
                    }
                }
            }

            return cmw;
        }

        public void Devlop(Configuration configuration, Planet mainworld)
        {
            foreach (var orbit in Orbits)
            {
                if (orbit.World != null)
                {
                    orbit.World.CompleteTravInfo(configuration, mainworld);
                    if (configuration.CurrentCampaign == Campaign.THENEWERA)
                    {
                        orbit.World.DoCollapse(configuration);
                    }
                }
            }

            foreach (var star in Companions)
            {
                star.Devlop(configuration, mainworld);
            }
        }

        public int Count(Planet.WorldType worldType)
        {
            var c = 0;

            foreach (var orbit in Orbits)
            {
                c += orbit.Count(worldType);
            }
            foreach (var companion in Companions)
            {
                c += companion.Count(worldType);
            }
            return c;
        }
        public string DisplayString()
        {
            return string.Format("{0}{1} ({2})", TypeToChar(StarType), DecClass, PrintLumClass());
        }
    }
}
