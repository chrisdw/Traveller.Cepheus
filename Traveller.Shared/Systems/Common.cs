using System;
using System.Collections.Generic;
using System.Text;
using org.DownesWard.Utilities;

namespace org.DownesWard.Traveller.Shared.Systems
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
        HOSTILE
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
    }
}
