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

        public TravInfo Normal { get; } = new TravInfo();
        public TravInfo Collapse { get; } = new TravInfo();

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
    }
}
