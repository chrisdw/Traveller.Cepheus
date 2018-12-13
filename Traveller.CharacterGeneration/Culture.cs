using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration
{
    public class Culture
    {
        public static List<string> GetCulturesForCampaign(string campaign)
        {
            var list = new List<string>();
            switch (campaign)
            {
                case "3rd Imperium":
                    list.Add("Imperial");
                    list.Add("Zhodani");
                    //Cultures.Add("Solomani");
                    list.Add("Aslan");
                    list.Add("Vargr");
                    list.Add("Darrian");
                    list.Add("Sword Worlds");
                    //Cultures.Add("Droyne");
                    list.Add("Dynchia");
                    break;
                case "Generic":
                    list.Add("Generic");
                    break;
                case "Hostile":
                    list.Add("Hostile");
                    break;
                case "Bughunters":
                    list.Add("United Terra");
                    list.Add("Ancients");
                    list.Add("Others");
                    break;
                case "Commonwealth":
                    list.Add("K'Hiff");
                    list.Add("Terran Commonwealth");
                    list.Add("Covenant of the Lord");
                    list.Add("Lucerne Compact");
                    list.Add("Shanthaa");
                    list.Add("Froog");
                    break;
            }
            return list;
        }

        public static ICulture CreateCulture(string culture, bool useCitizens, bool useMishaps)
        {
            switch (culture)
            {
                case "Imperial":
                    return new Classic.Imperial.Culture()
                    {
                        UseCitizenRules = useCitizens
                    };
                case "Darrian":
                    return new Classic.Darrian.Culture();
                case "Dynchia":
                    return new Classic.Dynchia.Culture();
                case "Sword Worlds":
                    return new Classic.SwordWorlds.Culture();
                case "Zhodani":
                    return new Classic.Zhodani.Culture();
                case "Aslan":
                    return new Classic.Aslan.Culture();
                case "Generic":
                    return new Cepheus.Culture()
                    {
                        UseMishaps = useMishaps
                    };
                case "Hostile":
                    return new Cepheus.Hostile.Culture()
                    {
                        UseMishaps = useMishaps
                    };
                case "United Terra":
                    return new Cepheus.Bughunters.Culture()
                    {
                        UseMishaps = useMishaps,
                        Mode = 0
                    };
                case "Ancients":
                    return new Cepheus.Bughunters.Culture()
                    {
                        UseMishaps = useMishaps,
                        Mode = 1
                    };
                case "Others":
                    return new Cepheus.Bughunters.Culture()
                    {
                        UseMishaps = useMishaps,
                        Mode = 2
                    };
                case "K'Hiff":
                    return new Cepheus.Commonwealth.Khiff.Culture()
                    {
                        UseMishaps = useMishaps
                    };
                case "Terran Commonwealth":
                    return new Cepheus.Commonwealth.Terran.Culture()
                    {
                        UseMishaps = useMishaps
                    };
                case "Covenant of the Lord":
                    return new Cepheus.Commonwealth.Covenant.Culture()
                    {
                        UseMishaps = useMishaps
                    };
                case "Lucerne Compact":
                    return new Cepheus.Commonwealth.Lucerne.Culture()
                    {
                        UseMishaps = useMishaps
                    };
                case "Shanthaa":
                    return new Cepheus.Commonwealth.Shanthaa.Culture()
                    {
                        UseMishaps = useMishaps
                    };
                case "Froog":
                    return new Cepheus.Commonwealth.Froog.Culture()
                    {
                        UseMishaps = useMishaps
                    };
                default:
                    return new Classic.Imperial.Culture()
                    {
                        UseCitizenRules = useCitizens
                    };
            }
        }
    }
}
