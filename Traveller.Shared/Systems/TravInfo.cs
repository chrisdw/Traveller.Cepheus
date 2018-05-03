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
            }
            return builder.ToString();
        }
    }
}
