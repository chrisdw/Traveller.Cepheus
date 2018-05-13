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

        public DensityType Dense { get; set; }
        public double Pressure { get; set; }
        public int maxpop { get; set; }
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

        // TODO: Add reference to encounter table generator

        // Temprature talbes
        public double[] Summer { get; } = new double[Constants.NUM_HEX_ROWS * 2];
        public double[] Fall { get; } = new double[Constants.NUM_HEX_ROWS * 2];
        public double[] Winter { get; } = new double[Constants.NUM_HEX_ROWS * 2];

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
    }
}
