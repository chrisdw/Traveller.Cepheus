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

        public string UPP(Planet.WorldType type, double diameter)
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
                    builder.Append("QCompanion Star");
                    break;
            }
            return builder.ToString();
        }
    }
}
