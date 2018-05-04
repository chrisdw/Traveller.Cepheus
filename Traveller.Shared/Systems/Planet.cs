using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.Shared.Systems
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
    }
}
