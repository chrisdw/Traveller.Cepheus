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
            StartPort = 'X';
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
                    builder.AppendFormat("{0}-S{1}{2}-{3}-{4}", StartPort, Atmosphere.ToString(), Hydro.ToString(), SocialUPP(), TechLevel.ToString());
                    break;
                case Planet.WorldType.RING:
                    builder.AppendFormat("{0}-R00-{1}-{2}", StartPort, SocialUPP(), TechLevel.ToString());
                    break;
                case Planet.WorldType.NORMAL:
                case Planet.WorldType.PLANETOID:
                    builder.AppendFormat("{0}-{1}-{3}-{4}", StartPort, PhysicalUPP(), SocialUPP(), TechLevel.ToString());
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
            Remarks = builder.ToString();
        }
    }
}
