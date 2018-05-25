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
        public short HZone { get; set; }
        public List<CompanionStar> Companions { get; } = new List<CompanionStar>();
        public string Name { get; set; }
        public int NumOrbits { get; set; }
        public int NumCompanions { get; set; }

        public Star()
        {
            StarType = GetStellarType();
            LumClass = GetLumClass();
            StellarMass = GetStellarMass();
            Luminosity = GetLuminosity();
            if (StarType != StellarType.O)
            {
                NumOrbits = GetNumOrbits();
            }
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
            dieroll.Clamp(0, 19);

            return dieroll;
        }
    }
}
