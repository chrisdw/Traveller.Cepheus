using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration
{
    public class Campaign
    {
        public static List<string> GetCampaignForRuleset(string ruleset)
        {
            var list = new List<string>();
            switch (ruleset)
            {
                case "Classic":
                    list.Add("3rd Imperium");
                    break;
                case "Mega Traveller":
                    list.Add("The Rebellion");
                    break;
                case "Cepheus Engine":
                    list.Add("Generic");
                    list.Add("Hostile");
                    list.Add("Orbital");
                    list.Add("Bughunters");
                    list.Add("Commonwealth");
                    break;
            }
            return list;
        }
    }
}
