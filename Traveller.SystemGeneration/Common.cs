using System;
using System.Collections.Generic;
using System.Text;
using org.DownesWard.Utilities;

namespace org.DownesWard.Traveller.SystemGeneration
{
    public enum StarportTableType
    {
        BACKWATER,
        STANDARD,
        MATURE,
        CLUSTER
    }

    public enum Campaign
    {
        CLASSIC,
        HOSTILE,
        HAMMERSSLAMMERS,
        THENEWERA
    }

    public enum GenerationType
    {
        SIMPLE,
        FULL
    }

    public static class Common
    {
        public static Dice od6 = new Dice(6);
        public static Dice od10 = new Dice(10);
        public static Dice od3 = new Dice(3);

        public static int d3()
        {
            return od3.roll();
        }

        public static int d6()
        {
            return od6.roll();
        }

        public static int d10()
        {
            return od10.roll();
        }

        public static double CalcGaiaFactor(double l, double o, double g, double e)
        {
            var Ideal = 288.0 / l / o / g;
            var TheFactor = (Ideal - e) / 2;

            if (TheFactor > 0.0)
            {
                if (TheFactor > 0.1)
                {
                    TheFactor = 0.1;
                }
            }
            else
            {
                if (TheFactor < -0.1)
                {
                    TheFactor = -0.1;
                }
            }

            return (e + TheFactor);
        }

        public static double Change(double original)
        {
            var X = (d10() * 10.0) + d10();
            var Y = X / 100.0;

            if (d6() < 4)
            {
                return original * Y;
            }
            else
            {
                return (original * (1.0 + Y));
            }
        }

        public static double CtoF(double cdegrees)
        {
            return ((cdegrees * 1.8) + 32);
        }
    }
}
