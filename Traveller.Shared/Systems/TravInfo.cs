using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.Shared.Systems
{
    public class TravInfo : UPP
    {
        public short PopMult { get; set; }
        public string Remarks { get; set; }
        public string Bases { get; set; }

        public TravInfo()
        {
            Starport = 'X';
            PopMult = 0;
            Remarks = string.Empty;
            Bases = string.Empty;
        }

        public string UPP(Planet.WorldType type = Planet.WorldType.NORMAL, double diameter = 0)
        {
            var builder = new StringBuilder();
            switch (type)
            {
                case Planet.WorldType.LGG:
                    builder.AppendFormat("LGG - diameter {0} km", diameter.ToString("F"));
                    break;
                case Planet.WorldType.SGG:
                    builder.AppendFormat("SGG - diameter {0} km", diameter.ToString("F"));
                    break;
                case Planet.WorldType.SMALL:
                    builder.AppendFormat("{0}-S{1}{2}-{3}-{4}", Starport, Atmosphere.ToString(), Hydro.ToString(), SocialUPP(), TechLevel.ToString());
                    break;
                case Planet.WorldType.RING:
                    builder.AppendFormat("{0}-R00-{1}-{2}", Starport, SocialUPP(), TechLevel.ToString());
                    break;
                case Planet.WorldType.NORMAL:
                case Planet.WorldType.PLANETOID:
                    builder.AppendFormat("{0}-{1}{2}-{3}", Starport, PhysicalUPP(), SocialUPP(), TechLevel.ToString());
                    break;
                case Planet.WorldType.STAR:
                    builder.Append("Companion Star");
                    break;
            }
            return builder.ToString();
        }

        public string DisplayString(Planet.WorldType type = Planet.WorldType.NORMAL, double diameter = 0)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("{0}\t{1}\t{2}", UPP(), Bases, Remarks);
            if (type != Planet.WorldType.LGG && type != Planet.WorldType.SGG && type != Planet.WorldType.STAR)
            {
                if (PopMult > 0)
                {
                    builder.AppendFormat("\t{0}", PopMult);
                }
            }
            return builder.ToString();
        }

        public void DoTradeClassification()
        {
            var builder = new StringBuilder();

            if ((Atmosphere.Value >= 4 && Atmosphere.Value <= 9) && (Hydro.Value >= 4 && Hydro.Value <= 8) && (Pop.Value >= 5 && Pop.Value <= 7))
            {
                builder.Append("Ag ");
            }
            if (Size.Value == 0)
            {
                builder.Append("As ");
            }
            if (Pop.Value == 0)
            {
                builder.Append("Ba ");
            }
            if (Atmosphere.Value >= 2 && Hydro.Value == 0)
            {
                builder.Append("De ");
            }
            if (Atmosphere.Value >= 10 && Hydro.Value > 0)
            {
                builder.Append("Fl ");
            }
            if (Pop.Value >= 9)
            {
                builder.Append("Hi ");
            }
            if (Atmosphere.Value <= 1 && Hydro.Value > 0)
            {
                builder.Append("Ic ");
            }
            if (((Atmosphere.Value >= 2 && Atmosphere.Value <= 4) || Atmosphere.Value == 7 || Atmosphere.Value == 9) && Pop.Value >= 9)
            {
                builder.Append("In ");
            }
            if (Pop.Value > 0 && Hydro.Value <= 4)
            {
                builder.Append("Lo ");
            }
            if (Atmosphere.Value <= 3 && Hydro.Value <= 4 && Pop.Value >= 6)
            {
                builder.Append("Na ");
            }
            if (Pop.Value > 0 && Pop.Value <= 6)
            {
                builder.Append("Ni ");
            }
            if ((Atmosphere.Value >= 2 && Atmosphere.Value <= 5) && Hydro.Value <= 3 && Pop.Value > 0)
            {
                builder.Append("Po ");
            }
            if ((Atmosphere.Value == 6 || Atmosphere.Value == 8) && (Pop.Value >= 6 && Pop.Value <= 8) && (Government.Value >= 4 && Government.Value <= 9))
            {
                builder.Append("Ri ");
            }
            if (Size.Value > 0 && Atmosphere.Value == 0)
            {
                builder.Append("Va ");
            }
            if (Hydro.Value == 10)
            {
                builder.Append("Wa ");
            }
            Remarks += builder.ToString();
        }

        // Make the changes necessary to represent the social information 
        // of a subordiate world relative to the main world
        public void DoSubordinate(TravInfo main)
        {
            var builder = new StringBuilder();

            // Government
            var dieroll = Common.d6();
            if (main.Government.Value == 6)
            {
                dieroll += Pop.Value;
            }
            if (main.Government.Value == 7)
            {
                dieroll -= 1;
            }
            if (Pop.Value == 0)
            {
                Government.Value = 0;
            }
            else
            {
                if (dieroll > 4)
                {
                    Government.Value = 6;
                }
                else
                {
                    Government.Value = dieroll - 1;
                }
            }

            // Law
            if (Pop.Value == 0)
            {
                Law.Value = 0;
            }
            else
            {
                Law.Value = Common.d6() - 3 + main.Law.Value;
            }

            // Farming world
            if ((Atmosphere.Value >= 4 && Atmosphere.Value <= 9) && (Hydro.Value >= 4 && Hydro.Value <= 8) && Pop.Value >= 2)
            {
                builder.AppendFormat("{0} ", "Fa");
            }
            
            // Mining world
            if (main.Remarks.Contains("In"))
            {
                if (Pop.Value >= 2)
                {
                    builder.AppendFormat("{0} ", "Mn");
                }
            }

            // Colony world
            if (Government.Value == 6 && Pop.Value >= 5)
            {
                builder.AppendFormat("{0} ", "Co");
            }

            // Research world
            if (main.TechLevel.Value >= 9 && main.Pop.Value >= 1 && Pop.Value > 0)
            {
                dieroll = Common.d6() + Common.d6();
                if (main.TechLevel.Value >= 10)
                {
                    dieroll += 2;
                }
                if (dieroll >= 11)
                {
                    builder.AppendFormat("{0} ", "Re");
                }
            }

            // Mi?
            if (main.Pop.Value >= 8 && !main.Remarks.Contains("Po") && Pop.Value > 0)
            {
                dieroll = Common.d6() + Common.d6();
                if (main.Pop.Value >= 9)
                {
                    dieroll += 1;
                }
                if (main.Atmosphere.Value == Atmosphere.Value)
                {
                    dieroll += 1;
                }
                if (dieroll >= 12)
                {
                    builder.AppendFormat("{0} ", "Mi");
                }
            }

            // Bases
            if (main.Bases.Contains("N") && Pop.Value >= 3)
            {
                builder.AppendFormat("{0} ", "Nv");
            }
            if (main.Bases.Contains("S") && Pop.Value >= 2)
            {
                builder.AppendFormat("{0} ", "Sc");
            }

            // Finish up
            Remarks += builder.ToString();

            // Tech Level
            TechLevel.Value = main.TechLevel.Value - 1;
            if (Remarks.Contains("Sc") || Remarks.Contains("Nv") || Remarks.Contains("Re"))
            {
                TechLevel.Value += 1;
            }

            // Spaceport
            dieroll = Common.d6();
            if (Pop.Value >= 6)
            {
                dieroll += 2;
            }
            if (Pop.Value == 1)
            {
                dieroll -= 2;
            }
            if (Pop.Value == 0)
            {
                dieroll -= 3;
            }

            if (dieroll < 3)
            {
                Starport = 'Y';
            }
            else if (dieroll == 3)
            {
                Starport = 'H';
            }
            else if (dieroll >= 6)
            {
                Starport = 'F';
            }

            if (TechLevel.Value != main.TechLevel.Value)
            {
                TechLevel.Value += 1;
            }
            else
            {
                Starport = 'G';
            }
            if (Pop.Value == 0)
            {
                TechLevel.Value = 0;
            }
        }

        public void ReduceStarport(int levels)
        {
            switch (Starport)
            {
                case 'A':
                    if (levels == 1)
                    {
                        Starport = 'B';
                    }
                    else
                    {
                        Starport = 'C';
                    }
                    break;
                case 'B':
                    if (levels == 1)
                    {
                        Starport = 'C';
                    }
                    else
                    {
                        Starport = 'D';
                    }
                    break;
                case 'C':
                    if (levels == 1)
                    {
                        Starport = 'D';
                    }
                    else
                    {
                        Starport = 'E';
                    }
                    break;
                case 'D':
                    if (levels == 1)
                    {
                        Starport = 'E';
                    }
                    else
                    {
                        Starport = 'X';
                    }
                    break;
                case 'E':
                    Starport = 'X';
                    break;
                case 'F':
                    if (levels == 1)
                    {
                        Starport = 'G';
                    }
                    else
                    {
                        Starport = 'H';
                    }
                    break;
                case 'G':
                    if (levels == 1)
                    {
                        Starport = 'H';
                    }
                    else
                    {
                        Starport = 'Y';
                    }
                    break;
                case 'H':
                    Starport = 'Y';
                    break;
            }
        }

        public void GetTravInfo(Configuration config)
        {
            Pop.Value = Common.d6() + Common.d6() - 2;

            if (Size.Value <= 2) Pop.Value -= 1;
            if (Atmosphere.Value <= 3) Pop.Value -= 3;
            if (Atmosphere.Value == 10) Pop.Value -= 2;
            if (Atmosphere.Value == 11) Pop.Value -= 3;
            if (Atmosphere.Value == 12) Pop.Value -= 4;
            if (Atmosphere.Value > 12) Pop.Value -= 2;
            if (Atmosphere.Value == 6) Pop.Value += 3;
            if (Atmosphere.Value == 5 || Atmosphere.Value == 8)  Pop.Value += 1;
            if (Hydro.Value == 0 && Atmosphere.Value > 3)  Pop.Value -= 2;

            if (config.HardScience)
            {
                if (Size.Value <= 2) Pop.Value -= 1;
                if (Size.Value >= 10) Pop.Value -= 1;
                if (Atmosphere.Value != 5 && Atmosphere.Value != 6 && Atmosphere.Value != 8)
                {
                    Pop.Value -= 1;
                }
                else
                {
                    Pop.Value += 1;
                }
            }

            if (Pop.Value == 0)
            {
                Government.Value = 0;
                Law.Value = 0;
                TechLevel.Value = 0;
                PopMult = 0;
            }
            else
            {
                Government.Value = Common.d6() + Common.d6() - 7 + Pop.Value;
                Law.Value = Common.d6() + Common.d6() - 7 + Government.Value;
                {
                    PopMult = (short)Common.d10();
                } while (PopMult == 10) ;
            }
        }

        public void CompleteTravInfo(Configuration config)
        {
            Starport = GetStarport(config);
            if (Pop.Value == 0)
            {
                TechLevel.Value = 0;
            }
            else
            {
                TechLevel.Value = Common.d6();
                switch (Starport)
                {
                    case 'A':
                        TechLevel.Value += 6;
                        break;
                    case 'B':
                        TechLevel.Value += 4;
                        break;
                    case 'C':
                        TechLevel.Value += 2;
                        break;
                    case 'X':
                        TechLevel.Value -= 4;
                        break;
                }

                switch (Size.Value)
                {
                    case 0:
                    case 1:
                        TechLevel.Value += 2;
                        break;
                    case 2:
                    case 3:
                    case 4:
                    case 11:
                    case 12:
                        TechLevel.Value += 1;
                        break;
                }

                switch (Atmosphere.Value)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 10:
                        TechLevel.Value += 1;
                        break;
                    case 13:
                        TechLevel.Value -= 2;
                        break;
                }

                switch (Hydro.Value)
                {
                    case 9:
                        TechLevel.Value += 1;
                        break;
                    case 10:
                        TechLevel.Value += 2;
                        break;
                }

                switch (Pop.Value)
                {
                    case 9:
                        TechLevel.Value += 2;
                        break;
                    case 10:
                        TechLevel.Value += 4;
                        break;
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        TechLevel.Value += 1;
                        break;
                }

                switch (Government.Value)
                {
                    case 0:
                    case 5:
                        TechLevel.Value += 1;
                        break;
                }
            }
        }

        public double Population()
        {
            return PopMult * Math.Pow(10, Pop.Value);
        }

        private char GetStarport(Configuration config)
        {
            var dieroll = Common.d6() + Common.d6();
            if (config.HardScience)
            {
                dieroll = dieroll + 7 - Pop.Value;
            }
            else
            {
                if (Pop.Value == 1) dieroll += 2;
                if (Pop.Value == 2) dieroll += 1;
                if (Pop.Value >= 6 && Pop.Value <= 9) dieroll -= 1;
                if (Pop.Value == 10) dieroll -= 2;
            }

            if (Pop.Value == 0)
                return 'X';

            char starport = 'X';

            switch (config.StarportTable)
            {
                case StarportTableType.BACKWATER:
                    if (dieroll < 3) starport = 'A';
                    else if (dieroll < 6) starport = 'B';
                    else if (dieroll < 9) starport = 'C';
                    else if (dieroll < 10) starport = 'D';
                    else if (dieroll < 12) starport = 'E';
                    else starport = 'X';
                    break;
                case StarportTableType.STANDARD:
                    if (dieroll < 4) starport = 'A';
                    else if (dieroll < 7) starport = 'B';
                    else if (dieroll < 9) starport = 'C';
                    else if (dieroll < 10) starport = 'D';
                    else if (dieroll < 12) starport = 'E';
                    else starport = 'X';
                    break;
                case StarportTableType.MATURE:
                    if (dieroll < 4) starport = 'A';
                    else if (dieroll < 7) starport = 'B';
                    else if (dieroll < 9) starport = 'C';
                    else if (dieroll < 10) starport = 'D';
                    else starport = 'E';
                    break;
                case StarportTableType.CLUSTER:
                    if (dieroll < 6) starport = 'A';
                    else if (dieroll < 8) starport = 'B';
                    else if (dieroll < 10) starport = 'C';
                    else if (dieroll < 11) starport = 'D';
                    else if (dieroll < 12) starport = 'E';
                    else starport = 'X';
                    break;
            }

            return starport;
        }
    }
}
