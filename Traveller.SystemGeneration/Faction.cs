using org.DownesWard.Traveller.Shared;
using org.DownesWard.Traveller.SystemGeneration.Resources;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.SystemGeneration
{
    public class Faction
    {
        public TravCode Government { get; } = new TravCode(13);
        public string Name { get; set; }
        public TravCode Strength { get; } = new TravCode(12);
        public string Origin { get; set; }
        private Configuration _configuration;

        public Faction(Configuration configuration)
        {
            _configuration = configuration;
        }

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

        public string DisplayString()
        {
            var builder = new StringBuilder();
            if (_configuration.CurrentCampaign == Campaign.HAMMERSSLAMMERS)
            {
                builder.AppendFormat(Languages.Name, Name);
                builder.Append(" ");
            }

            builder.AppendFormat(Languages.Description, GovernmentString, StrengthString);

            if (_configuration.CurrentCampaign == Campaign.HAMMERSSLAMMERS)
            {
                builder.Append(" ");
                builder.AppendFormat(Languages.Origin, Origin);
            }
            return builder.ToString();
        }

        public static List<Faction> GenerateFactions(TravInfo world, Configuration configuration)
        {
            var list = new List<Faction>();
            var numFactions = Common.d3();

            if (world.Government.Value == 7)
            {
                numFactions++;
            }
            else if (world.Government.Value > 10)
            {
                numFactions--;
            }
            if (numFactions > 0 && world.Pop.Value > 0)
            {
                for (var i = 0; i < numFactions; i++)
                {
                    var faction = new Faction(configuration);
                    faction.Government.Value = Common.d6() + Common.d6() - 7 + world.Pop.Value;
                    faction.Strength.Value = Common.d6() + Common.d6();
                    if (configuration.CurrentCampaign == Campaign.HAMMERSSLAMMERS)
                    {
                        var roll1 = Common.d6();
                        var roll2 = Common.d6();
                        switch (roll1)
                        {
                            case 1:
                                switch (roll2)
                                {
                                    case 1: faction.Origin = Languages.FactionOrigin_EastUS; break;
                                    case 2: faction.Origin = Languages.FactionOrigin_MidWestUS; break;
                                    case 3: faction.Origin = Languages.FactionOrigin_SouthUS; break;
                                    case 4: faction.Origin = Languages.FactionOrigin_WestUS; break;
                                    case 5: faction.Origin = Languages.FactionOrigin_Canadian; break;
                                    case 6: faction.Origin = Languages.FactionOrigin_Evangelical; break;
                                }
                                break;
                            case 2:
                                switch (roll2)
                                {
                                    case 1: faction.Origin = Languages.FactionOrigin_Mexican; break;
                                    case 2: faction.Origin = Languages.FactionOrigin_CentralAmerican; break;
                                    case 3: faction.Origin = Languages.FactionOrigin_AmazonBasin; break;
                                    case 4: faction.Origin = Languages.FactionOrigin_Andean; break;
                                    case 5: faction.Origin = Languages.FactionOrigin_SouthAmerican; break;
                                    case 6: faction.Origin = Languages.FactionOrigin_Catholic; break;
                                }
                                break;
                            case 3:
                                switch (roll2)
                                {
                                    case 1: faction.Origin = Languages.FactionOrigin_Scandinavian; break;
                                    case 2: faction.Origin = Languages.FactionOrigin_BritishIsles; break;
                                    case 3: faction.Origin = Languages.FactionOrigin_EuropeanWestern; break;
                                    case 4: faction.Origin = Languages.FactionOrigin_EuropeanEastern; break;
                                    case 5: faction.Origin = Languages.FactionOrigin_EuropeanSouthern; break;
                                    case 6: faction.Origin = Languages.FactionOrigin_Protestant; break;
                                }
                                break;
                            case 4:
                                switch (roll2)
                                {
                                    case 1: case 2: faction.Origin = Languages.FactionOrigin_Russian; break;
                                    case 3: case 4: faction.Origin = Languages.FactionOrigin_Chinese; break;
                                    case 5: faction.Origin = Languages.FactionOrigin_Japanese; break;
                                    case 6: faction.Origin = Languages.FactionOrigin_Buddist; break;
                                }
                                break;
                            case 5:
                                switch (roll2)
                                {
                                    case 1: faction.Origin = Languages.FactionOrigin_Turkish; break;
                                    case 2: faction.Origin = Languages.FactionOrigin_NorthAfrican; break;
                                    case 3: faction.Origin = Languages.FactionOrigin_CentralAfrican; break;
                                    case 4: faction.Origin = Languages.FactionOrigin_SouthAfrican; break;
                                    case 5: faction.Origin = Languages.FactionOrigin_Indonesian; break;
                                    case 6: faction.Origin = Languages.FactionOrigin_Islamic; break;
                                }
                                break;
                            case 6:
                                switch (roll2)
                                {
                                    case 1: faction.Origin = Languages.FactionOrigin_Arabic; break;
                                    case 2: faction.Origin = Languages.FactionOrigin_Australian; break;
                                    case 3: faction.Origin = Languages.FactionOrigin_OldColony; break;
                                    case 4: faction.Origin = Languages.FactionOrigin_MiddleColony; break;
                                    case 5: faction.Origin = Languages.FactionOrigin_TheWay_Mainstream; break;
                                    case 6: faction.Origin = Languages.FactionOrigin_TheWay_Heretical; break;
                                }
                                break;
                        }
                        // Now do the name
                        roll1 = Common.d6();
                        roll2 = Common.d6();
                        switch (roll1)
                        {
                            case 1:
                                switch (roll2)
                                {
                                    case 1: faction.Name = Languages.FactionName_Republicans; break;
                                    case 2: faction.Name = Languages.FactionName_Federalists; break;
                                    case 3: faction.Name = Languages.FactionName_Royalists; break;
                                    case 4: faction.Name = Languages.FactionName_Sepratists; break;
                                    case 5: faction.Name = Languages.FactionName_Alliance; break;
                                    case 6: faction.Name = Languages.FactionName_LeagueofBarons; break;
                                }
                                break;
                            case 2:
                                switch (roll2)
                                {
                                    case 1: faction.Name = Languages.FactionName_TradeFederation; break;
                                    case 2: faction.Name = Languages.FactionName_FarmersUnion; break;
                                    case 3: faction.Name = Languages.FactionName_CitizensMovement; break;
                                    case 4: faction.Name = Languages.FactionName_Revolutionaries; break;
                                    case 5: faction.Name = Languages.FactionName_GloriousBrotherhood; break;
                                    case 6: faction.Name = Languages.FactionName_FreedomParty; break;
                                }
                                break;
                            case 3:
                                switch (roll2)
                                {
                                    case 1: faction.Name = Languages.FactionName_CorporateMilitia; break;
                                    case 2: faction.Name = Languages.FactionName_Rebels; break;
                                    case 3: faction.Name = Languages.FactionName_IndustryAlliance; break;
                                    case 4: faction.Name = Languages.FactionName_Coalition; break;
                                    case 5: faction.Name = Languages.FactionName_TripartateAlliance; break;
                                    case 6: faction.Name = Languages.FactionName_CoastalUnion; break;
                                }
                                break;
                            case 4:
                                switch (roll2)
                                {
                                    case 1: faction.Name = Languages.FactionName_Highlanders; break;
                                    case 2: faction.Name = Languages.FactionName_SouthernStates; break;
                                    case 3: faction.Name = Languages.FactionName_NorthStarLeague; break;
                                    case 4: faction.Name = Languages.FactionName_TerranLoyalists; break;
                                    case 5: faction.Name = Languages.FactionName_ProgressiveParty; break;
                                    case 6: faction.Name = Languages.FactionName_Crusaders; break;
                                }
                                break;
                            case 5:
                                switch (roll2)
                                {
                                    case 1: faction.Name = Languages.FactionName_MountainPartisans; break;
                                    case 2: faction.Name = Languages.FactionName_Centralists; break;
                                    case 3: faction.Name = Languages.FactionName_TreatyForces; break;
                                    case 4: faction.Name = Languages.FactionName_WesternAlliance; break;
                                    case 5: faction.Name = Languages.FactionName_EasternBloc; break;
                                    case 6: faction.Name = Languages.FactionName_StabilityParty; break;
                                }
                                break;
                            case 6:
                                switch (roll2)
                                {
                                    case 1: faction.Name = Languages.FactionName_ModerateAlliance; break;
                                    case 2: faction.Name = Languages.FactionName_PeoplesParty; break;
                                    case 3: faction.Name = Languages.FactionName_Imperialist; break;
                                    case 4: faction.Name = Languages.FactionName_Monarchists; break;
                                    case 5: faction.Name = Languages.FactionName_Liberators; break;
                                    case 6: faction.Name = Languages.FactionName_PeaceFaction; break;
                                }
                                break;
                        }
                    }
                    list.Add(faction);
                }
            }

            return list;
        }
    }
}
