using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.Shared.Systems
{
    public class Faction
    {
        public TravCode Government { get; } = new TravCode(13);
        public string Name { get; set; }
        public TravCode Strength { get; } = new TravCode(12);
        public string Origin { get; set; }

        public string StrenghtString()
        {
            var longString = string.Empty;
            if (Strength.Value <= 3)
            {
                longString = "Obscure Group";
            }
            else if (Strength.Value <= 5)
            {
                longString = "Fringe Group";
            }
            else if (Strength.Value <= 7)
            {
                longString = "Minor Group";
            }
            else if (Strength.Value <= 9)
            {
                longString = "Notable Group";
            }
            else if (Strength.Value <= 11)
            {
                longString = "Significant Group";
            }
            else
            {
                longString = "Overwelming Popular Support";
            }
            return longString;
        }
        public string GovernmentString()
        {
            var longString = string.Empty;
            // Note: not a perfect overlap with government types
            switch (Government.Value)
            {
                case 0: longString = "None"; break;
                case 1: longString = "Company"; break;
                case 2: longString = "Participating Democracy"; break;
                case 3: longString = "Self perpetuating oligarchy"; break;
                case 4: longString = "Representative Democracy"; break;
                case 5: longString = "Feudal Technocracy"; break;
                case 6: longString = "Off world interests"; break;
                case 7: longString = "Anarchists"; break;
                case 8: longString = "Civil Service Bureaucracy"; break;
                case 9: longString = "Impersonal Bureaucracy"; break;
                case 10: longString = "Charismatic Dictator"; break;
                case 11: longString = "Non-charismatic Dictator"; break;
                case 12: longString = "Charismatic oligarchy"; break;
                case 13: longString = "Religious Dictatorship"; break;
                default: longString = string.Format("Other ({0})", Government.Value.ToString()); break;
            }
            return longString;
        }

        public string DisplayString(Configuration config)
        {
            var builder = new StringBuilder();
            if (config.CurrentCampaign == Campaign.HAMMERSSLAMMERS)
            {
                builder.AppendFormat("Name: {0}, ", Name);
            }

            builder.AppendFormat("Government: {0}, Strength: {1}", GovernmentString(), StrenghtString());

            if (config.CurrentCampaign == Campaign.HAMMERSSLAMMERS)
            {
                builder.AppendFormat(" Origin: {0}, ", Origin);
            }
            return builder.ToString();
        }
    }
}
