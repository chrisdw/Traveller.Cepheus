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
                    longString = Languages.Str_1;
                }
                else if (Strength.Value <= 5)
                {
                    longString = Languages.Str_2;
                }
                else if (Strength.Value <= 7)
                {
                    longString = Languages.Str_3;
                }
                else if (Strength.Value <= 9)
                {
                    longString = Languages.Str_4;
                }
                else if (Strength.Value <= 11)
                {
                    longString = Languages.Str_5;
                }
                else
                {
                    longString = Languages.Str_6;
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
                    case 0: longString = Languages.Gov_0; break;
                    case 1: longString = Languages.Gov_1; break;
                    case 2: longString = Languages.Gov_2; break;
                    case 3: longString = Languages.Gov_3; break;
                    case 4: longString = Languages.Gov_4; break;
                    case 5: longString = Languages.Gov_5; break;
                    case 6: longString = Languages.Gov_6; break;
                    case 7: longString = Languages.Gov_7a; break;
                    case 8: longString = Languages.Gov_8; break;
                    case 9: longString = Languages.Gov_9; break;
                    case 10: longString = Languages.Gov_10; break;
                    case 11: longString = Languages.Gov_11; break;
                    case 12: longString = Languages.Gov_12; break;
                    case 13: longString = Languages.Gov_13; break;
                    default: longString = string.Format(Languages.Gov_Other, Government.Value.ToString()); break;
                }
                return longString;
            }
        }

        public string DisplayString(Configuration config)
        {
            var builder = new StringBuilder();
            if (config.CurrentCampaign == Campaign.HAMMERSSLAMMERS)
            {
                builder.AppendFormat(Languages.Name, Name);
                builder.Append(" ");
            }

            builder.AppendFormat(Languages.Description, GovernmentString, StrengthString);

            if (config.CurrentCampaign == Campaign.HAMMERSSLAMMERS)
            {
                builder.Append(" ");
                builder.AppendFormat(Languages.Origin, Origin);
            }
            return builder.ToString();
        }
    }
}
