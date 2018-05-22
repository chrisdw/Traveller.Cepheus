using org.DownesWard.Traveller.Shared;
using org.DownesWard.Traveller.SystemGeneration.Resources;
using System.Text;

namespace org.DownesWard.Traveller.SystemGeneration
{
    public class Faction
    {
        public TravCode Government { get; } = new TravCode(13);
        public string Name { get; set; }
        public TravCode Strength { get; } = new TravCode(12);
        public string Origin { get; set; }

        public string StrengthString
        {
            get
            {
                var longString = string.Empty;
                if (Strength.Value <= 3)
                {
                    longString = Resources.Resources.Str_1;
                }
                else if (Strength.Value <= 5)
                {
                    longString = Resources.Resources.Str_2;
                }
                else if (Strength.Value <= 7)
                {
                    longString = Resources.Resources.Str_3;
                }
                else if (Strength.Value <= 9)
                {
                    longString = Resources.Resources.Str_4;
                }
                else if (Strength.Value <= 11)
                {
                    longString = Resources.Resources.Str_5;
                }
                else
                {
                    longString = Resources.Resources.Str_6;
                }
                return longString;
            }
        }
        public string GovernmentString
        {
            get
            {
                var longString = string.Empty;
                // Note: not a perfect overlap with government types
                switch (Government.Value)
                {
                    case 0: longString = Resources.Resources.Gov_0; break;
                    case 1: longString = Resources.Resources.Gov_1; break;
                    case 2: longString = Resources.Resources.Gov_2; break;
                    case 3: longString = Resources.Resources.Gov_3; break;
                    case 4: longString = Resources.Resources.Gov_4; break;
                    case 5: longString = Resources.Resources.Gov_5; break;
                    case 6: longString = Resources.Resources.Gov_6; break;
                    case 7: longString = Resources.Resources.Gov_7; break;
                    case 8: longString = Resources.Resources.Gov_8; break;
                    case 9: longString = Resources.Resources.Gov_9; break;
                    case 10: longString = Resources.Resources.Gov_10; break;
                    case 11: longString = Resources.Resources.Gov_11; break;
                    case 12: longString = Resources.Resources.Gov_12; break;
                    case 13: longString = Resources.Resources.Gov_13; break;
                    default: longString = string.Format(Resources.Resources.Gov_Other, Government.Value.ToString()); break;
                }
                return longString;
            }
        }

        public string DisplayString(Configuration config)
        {
            var builder = new StringBuilder();
            if (config.CurrentCampaign == Campaign.HAMMERSSLAMMERS)
            {
                builder.AppendFormat(Resources.Resources.Name, Name);
                builder.Append(" ");
            }

            builder.AppendFormat(Resources.Resources.Description, GovernmentString, StrengthString);

            if (config.CurrentCampaign == Campaign.HAMMERSSLAMMERS)
            {
                builder.Append(" ");
                builder.AppendFormat(Resources.Resources.Origin, Origin);
            }
            return builder.ToString();
        }
    }
}
