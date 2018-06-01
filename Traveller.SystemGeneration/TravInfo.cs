using org.DownesWard.Traveller.Shared;
using org.DownesWard.Traveller.SystemGeneration.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace org.DownesWard.Traveller.SystemGeneration
{
    public class TravInfo : UPP
    {
        public int PopMult { get; set; }
        public string Remarks { get; set; }
        public string Bases { get; set; }
        public string ConflictReason { get; set; }

        public string UPPString
        {
            get
            {
                return UPP();
            }
        }
        public List<Faction> Factions { get; } = new List<Faction>();

        public TravInfo()
        {
            Starport = 'X';
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
                    builder.AppendFormat("{0}-S{1}{2}-{3}-{4}", Starport, Atmosphere.ToString(), Hydro.ToString(), SocialUPP(), TechLevel.ToString());
                    break;
                case Planet.WorldType.RING:
                    builder.AppendFormat("{0}-R00-{1}-{2}", Starport, SocialUPP(), TechLevel.ToString());
                    break;
                case Planet.WorldType.NORMAL:
                case Planet.WorldType.PLANETOID:
                    builder.AppendFormat("{0}-{1}{2}-{3}", Starport, PhysicalUPP(), SocialUPP(), TechLevel.ToString());
                    break;
                case Planet.WorldType.STAR:
                    builder.Append(Languages.CompanionStar);
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
                builder.AppendFormat("{0} ", Languages.TradeCode_Agricultural);
            }
            if (Size.Value == 0)
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_Asteroid);
            }
            if (Pop.Value == 0)
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_Barren);
            }
            if (Atmosphere.Value >= 2 && Hydro.Value == 0)
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_Desert);
            }
            if (Atmosphere.Value >= 10 && Hydro.Value > 0)
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_FluidOceans);
            }
            if (Pop.Value >= 9)
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_HighPopulation);
            }
            if (Atmosphere.Value <= 1 && Hydro.Value > 0)
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_IceCapped);
            }
            if (((Atmosphere.Value >= 2 && Atmosphere.Value <= 4) || Atmosphere.Value == 7 || Atmosphere.Value == 9) && Pop.Value >= 9)
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_Industrial);
            }
            if (Pop.Value > 0 && Hydro.Value <= 4)
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_LowPopulation);
            }
            if (Atmosphere.Value <= 3 && Hydro.Value <= 4 && Pop.Value >= 6)
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_NonAgricultural);
            }
            if (Pop.Value > 0 && Pop.Value <= 6)
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_NonIndustrial);
            }
            if ((Atmosphere.Value >= 2 && Atmosphere.Value <= 5) && Hydro.Value <= 3 && Pop.Value > 0)
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_Poor);
            }
            if ((Atmosphere.Value == 6 || Atmosphere.Value == 8) && (Pop.Value >= 6 && Pop.Value <= 8) && (Government.Value >= 4 && Government.Value <= 9))
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_Rich);
            }
            if (Size.Value > 0 && Atmosphere.Value == 0)
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_VaccumWorld);
            }
            if (Hydro.Value == 10)
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_WaterWorld);
            }
            Remarks += builder.ToString();
        }

        // Make the changes necessary to represent the social information 
        // of a subordiate world relative to the main world
        public void DoSubordinate(TravInfo main)
        {
            var builder = new StringBuilder();

            // Government
            var dieroll = Common.d6();
            if (main.Government.Value == 6)
            {
                dieroll += Pop.Value;
            }
            if (main.Government.Value == 7)
            {
                dieroll -= 1;
            }
            if (Pop.Value == 0)
            {
                Government.Value = 0;
            }
            else
            {
                if (dieroll > 4)
                {
                    Government.Value = 6;
                }
                else
                {
                    Government.Value = dieroll - 1;
                }
            }

            // Law
            if (Pop.Value == 0)
            {
                Law.Value = 0;
            }
            else
            {
                Law.Value = Common.d6() - 3 + main.Law.Value;
            }

            // Farming world
            if ((Atmosphere.Value >= 4 && Atmosphere.Value <= 9) && (Hydro.Value >= 4 && Hydro.Value <= 8) && Pop.Value >= 2)
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_FarmingColony);
            }
            
            // Mining world
            if (main.Remarks.Contains(Languages.TradeCode_Industrial))
            {
                if (Pop.Value >= 2)
                {
                    builder.AppendFormat("{0} ", Languages.TradeCode_MiningColony);
                }
            }

            // Colony world
            if (Government.Value == 6 && Pop.Value >= 5)
            {
                builder.AppendFormat("{0} ", Languages.TradeCode_Colony);
            }

            // Research world
            if (main.TechLevel.Value >= 9 && main.Pop.Value >= 1 && Pop.Value > 0)
            {
                dieroll = Common.d6() + Common.d6();
                if (main.TechLevel.Value >= 10)
                {
                    dieroll += 2;
                }
                if (dieroll >= 11)
                {
                    builder.AppendFormat("{0} ", Languages.TradeCode_ResearchColony);
                }
            }

            // Mi?
            if (main.Pop.Value >= 8 && !main.Remarks.Contains(Languages.TradeCode_Poor) && Pop.Value > 0)
            {
                dieroll = Common.d6() + Common.d6();
                if (main.Pop.Value >= 9)
                {
                    dieroll += 1;
                }
                if (main.Atmosphere.Value == Atmosphere.Value)
                {
                    dieroll += 1;
                }
                if (dieroll >= 12)
                {
                    builder.AppendFormat("{0} ", Languages.TradeCode_MiningColony);
                }
            }

            // Bases
            if (main.Bases.Contains(Languages.Base_Naval) && Pop.Value >= 3)
            {
                builder.AppendFormat("{0} ", Languages.Subbase_Naval);
            }
            if (main.Bases.Contains("S") && Pop.Value >= 2)
            {
                builder.AppendFormat("{0} ", Languages.Subbase_Scout);
            }

            // Finish up
            Remarks += builder.ToString();

            // Tech Level
            TechLevel.Value = main.TechLevel.Value - 1;
            if (Remarks.Contains(Languages.Subbase_Scout) || 
                Remarks.Contains(Languages.Subbase_Naval) || 
                Remarks.Contains(Languages.TradeCode_ResearchColony))
            {
                TechLevel.Value += 1;
            }

            // Spaceport
            dieroll = Common.d6();
            if (Pop.Value >= 6)
            {
                dieroll += 2;
            }
            if (Pop.Value == 1)
            {
                dieroll -= 2;
            }
            if (Pop.Value == 0)
            {
                dieroll -= 3;
            }

            if (dieroll < 3)
            {
                Starport = 'Y';
            }
            else if (dieroll == 3)
            {
                Starport = 'H';
            }
            else if (dieroll >= 6)
            {
                Starport = 'F';
            }

            if (TechLevel.Value != main.TechLevel.Value)
            {
                TechLevel.Value += 1;
            }
            else
            {
                Starport = 'G';
            }
            if (Pop.Value == 0)
            {
                TechLevel.Value = 0;
            }
        }

        public void ReduceStarport(int levels)
        {
            switch (Starport)
            {
                case 'A':
                    if (levels == 1)
                    {
                        Starport = 'B';
                    }
                    else
                    {
                        Starport = 'C';
                    }
                    break;
                case 'B':
                    if (levels == 1)
                    {
                        Starport = 'C';
                    }
                    else
                    {
                        Starport = 'D';
                    }
                    break;
                case 'C':
                    if (levels == 1)
                    {
                        Starport = 'D';
                    }
                    else
                    {
                        Starport = 'E';
                    }
                    break;
                case 'D':
                    if (levels == 1)
                    {
                        Starport = 'E';
                    }
                    else
                    {
                        Starport = 'X';
                    }
                    break;
                case 'E':
                    Starport = 'X';
                    break;
                case 'F':
                    if (levels == 1)
                    {
                        Starport = 'G';
                    }
                    else
                    {
                        Starport = 'H';
                    }
                    break;
                case 'G':
                    if (levels == 1)
                    {
                        Starport = 'H';
                    }
                    else
                    {
                        Starport = 'Y';
                    }
                    break;
                case 'H':
                    Starport = 'Y';
                    break;
            }
        }

        public void GetTravInfo(Configuration config)
        {
            Pop.Value = Common.d6() + Common.d6() - 2;

            if (Size.Value <= 2) Pop.Value -= 1;
            if (Atmosphere.Value <= 3) Pop.Value -= 3;
            if (Atmosphere.Value == 10) Pop.Value -= 2;
            if (Atmosphere.Value == 11) Pop.Value -= 3;
            if (Atmosphere.Value == 12) Pop.Value -= 4;
            if (Atmosphere.Value > 12) Pop.Value -= 2;
            if (Atmosphere.Value == 6) Pop.Value += 3;
            if (Atmosphere.Value == 5 || Atmosphere.Value == 8)  Pop.Value += 1;
            if (Hydro.Value == 0 && Atmosphere.Value > 3)  Pop.Value -= 2;

            if (config.HardScience)
            {
                if (Size.Value <= 2) Pop.Value -= 1;
                if (Size.Value >= 10) Pop.Value -= 1;
                if (Atmosphere.Value != 5 && Atmosphere.Value != 6 && Atmosphere.Value != 8)
                {
                    Pop.Value -= 1;
                }
                else
                {
                    Pop.Value += 1;
                }
            }

            if (Pop.Value == 0)
            {
                Government.Value = 0;
                Law.Value = 0;
                TechLevel.Value = 0;
                PopMult = 0;
            }
            else
            {
                Government.Value = Common.d6() + Common.d6() - 7 + Pop.Value;
                Law.Value = Common.d6() + Common.d6() - 7 + Government.Value;
                do
                {
                    PopMult = (short)Common.d10();
                } while (PopMult == 10) ;
            }
        }

        public void CompleteTravInfo(Configuration config)
        {
            Starport = GetStarport(config);
            if (Pop.Value == 0)
            {
                TechLevel.Value = 0;
            }
            else
            {
                TechLevel.Value = Common.d6();
                switch (Starport)
                {
                    case 'A':
                        TechLevel.Value += 6;
                        break;
                    case 'B':
                        TechLevel.Value += 4;
                        break;
                    case 'C':
                        TechLevel.Value += 2;
                        break;
                    case 'X':
                        TechLevel.Value -= 4;
                        break;
                }

                switch (Size.Value)
                {
                    case 0:
                    case 1:
                        TechLevel.Value += 2;
                        break;
                    case 2:
                    case 3:
                    case 4:
                    case 11:
                    case 12:
                        TechLevel.Value += 1;
                        break;
                }

                switch (Atmosphere.Value)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 10:
                        TechLevel.Value += 1;
                        break;
                    case 13:
                        TechLevel.Value -= 2;
                        break;
                }

                switch (Hydro.Value)
                {
                    case 9:
                        TechLevel.Value += 1;
                        break;
                    case 10:
                        TechLevel.Value += 2;
                        break;
                }

                switch (Pop.Value)
                {
                    case 9:
                        TechLevel.Value += 2;
                        break;
                    case 10:
                        TechLevel.Value += 4;
                        break;
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        TechLevel.Value += 1;
                        break;
                }

                switch (Government.Value)
                {
                    case 0:
                    case 5:
                        TechLevel.Value += 1;
                        break;
                }

                if ((Hydro.Value == 0 || Hydro.Value == 10) && Pop.Value > 5)
                {
                    if (TechLevel.Value < 4)
                    {
                        TechLevel.Value = 4;
                    }
                }

                switch (Atmosphere.Value)
                {
                    case 0:
                    case 2:
                    case 3:
                    case 10:
                    case 11:
                    case 12:
                        if (TechLevel.Value < 7)
                        {
                            TechLevel.Value = 7;
                        }
                        break;
                    case 4:
                    case 7:
                    case 9:
                        if (TechLevel.Value < 5)
                        {
                            TechLevel.Value = 5;
                        }
                        break;
                    case 13:
                    case 14:
                        if (Hydro.Value == 10)
                        {
                            if (TechLevel.Value < 7)
                            {
                                TechLevel.Value = 7;
                            }
                        }
                        break;
                }
            }

            DoFactions(config);

            // Bases
            switch (Starport)
            {
                case 'A':
                    var dieroll = Common.d6() + Common.d6();
                    if (dieroll >= 8)
                    {
                        Bases += Languages.Base_Naval;
                    }
                    dieroll = Common.d6() + Common.d6();
                    if (dieroll >= 10)
                    {
                        Bases += Languages.Base_Scout;
                    }
                    dieroll = Common.d6() + Common.d6();
                    if (dieroll >= 10)
                    {
                        Bases += Languages.Base_Military;
                    }
                    break;
                case 'B':
                    dieroll = Common.d6() + Common.d6();
                    if (dieroll >= 8)
                    {
                        Bases += Languages.Base_Naval;
                    }
                    dieroll = Common.d6() + Common.d6();
                    if (dieroll >= 9)
                    {
                        Bases += Languages.Base_Scout;
                    }
                    dieroll = Common.d6() + Common.d6();
                    if (dieroll >= 9)
                    {
                        Bases += Languages.Base_Military;
                    }
                    break;
                case 'C':
                    dieroll = Common.d6() + Common.d6();
                    if (dieroll >= 8)
                    {
                        Bases += Languages.Base_Scout;
                    }
                    dieroll = Common.d6() + Common.d6();
                    if (dieroll >= 8)
                    {
                        Bases += Languages.Base_Military;
                    }
                    break;
                case 'D':
                    dieroll = Common.d6() + Common.d6();
                    if (dieroll >= 7)
                    {
                        Bases += Languages.Base_Scout;
                    }
                    break;
            }

            DoTradeClassification();

            if (config.CurrentCampaign == Campaign.HAMMERSSLAMMERS && Pop.Value > 0)
            {
                var conflictDm = 0;
                if (Pop.Value >= 7 && TechLevel.Value >= 11)
                {
                    Remarks += string.Format(" {0}", Languages.ColonyAge_Old);
                    conflictDm += 2;
                }
                else if (Pop.Value >= 6 && Pop.Value <= 9 && TechLevel.Value >= 8)
                {
                    Remarks += string.Format(" {0}", Languages.ColonyAge_Middle);
                }
                else if (Pop.Value >= 4 && Pop.Value <= 8)
                {
                    Remarks += string.Format(" {0}" , Languages.ColonyAge_Young);
                    conflictDm -= 2;
                }
                if (Government.Value == 7)
                {
                    conflictDm += 2;
                }
                else if (Government.Value == 6)
                {
                    conflictDm -= 2;
                }
                ConflictReason = SeedOfConflict(conflictDm);
            }
        }

        private string SeedOfConflict(int conflictDm)
        {
            var conflictSeed = string.Empty;

            var conflictScore = Common.d6() + Common.d6() + conflictDm;

            if (conflictScore <= 2)
            {
                conflictSeed = Languages.Conflict_RebellionParent;
            }
            else if (conflictScore >= 11)
            {
                conflictSeed = Languages.Conflict_CivilWar;
            }
            else
            {
                switch (conflictScore)
                {
                    case 3:
                        conflictSeed = Languages.Conflict_RebellionOffWorld;
                        break;
                    case 4:
                        conflictSeed = Languages.Conflict_TradeWar;
                        break;
                    case 5:
                        conflictSeed = Languages.Conflict_Divide;
                        break;
                    case 6: conflictSeed = Languages.Conflict_Territory; break;
                    case 7: conflictSeed = string.Format(Languages.Conflict_Invasion, SeedOfConflict(conflictDm)); break;
                    case 8: conflictSeed = Languages.Conflict_ResourceWar; break;
                    case 9: conflictSeed = Languages.Conflict_Peacekeeping; break;
                    case 10: conflictSeed = Languages.Conflict_FailedCoup; break;
                }
            }
            return conflictSeed;
        }
        public void DoFactions(Configuration config)
        {
            var numFactions = Common.d3();

            if (Government.Value == 7)
            {
                numFactions++;
            }
            else if (Government.Value > 10)
            {
                numFactions--;
            }
            if (numFactions > 0 && Pop.Value > 0)
            {
                for (var i = 0; i < numFactions; i++)
                {
                    var faction = new Faction();
                    faction.Government.Value = Common.d6() + Common.d6() - 7 + Pop.Value;
                    faction.Strength.Value = Common.d6() + Common.d6();
                    if (config.CurrentCampaign == Campaign.HAMMERSSLAMMERS)
                    {
                        var roll1 = Common.d6();
                        var roll2 = Common.d6();
                        switch (roll1)
                        {
                            case 1:
                                switch (roll2)
                                {
                                    case 1: faction.Origin = Languages.FactionOrigin_EastUS;  break;
                                    case 2: faction.Origin = Languages.FactionOrigin_MidWestUS;  break;
                                    case 3: faction.Origin = Languages.FactionOrigin_SouthUS; break;
                                    case 4: faction.Origin = Languages.FactionOrigin_WestUS;  break;
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
                                    case 1 : faction.Origin = Languages.FactionOrigin_Scandinavian; break;
                                    case 2 : faction.Origin = Languages.FactionOrigin_BritishIsles; break;
                                    case 3 : faction.Origin = Languages.FactionOrigin_EuropeanWestern; break;
                                    case 4 : faction.Origin = Languages.FactionOrigin_EuropeanEastern; break;
                                    case 5 : faction.Origin = Languages.FactionOrigin_EuropeanSouthern; break;
                                    case 6 : faction.Origin = Languages.FactionOrigin_Protestant; break;
                                }
                                break;
                            case 4:
                                switch (roll2)
                                {
                                    case 1: case 2 : faction.Origin = Languages.FactionOrigin_Russian; break;
                                    case 3: case 4 : faction.Origin = Languages.FactionOrigin_Chinese; break;
                                    case 5 : faction.Origin = Languages.FactionOrigin_Japanese; break;
                                    case 6 : faction.Origin = Languages.FactionOrigin_Buddist; break;
                                }
                                break;
                            case 5:
                                switch (roll2)
                                {
                                    case 1 : faction.Origin = Languages.FactionOrigin_Turkish; break;
                                    case 2 : faction.Origin = Languages.FactionOrigin_NorthAfrican; break;
                                    case 3 : faction.Origin = Languages.FactionOrigin_CentralAfrican; break;
                                    case 4 : faction.Origin = Languages.FactionOrigin_SouthAfrican; break;
                                    case 5 : faction.Origin = Languages.FactionOrigin_Indonesian; break;
                                    case 6 : faction.Origin = Languages.FactionOrigin_Islamic; break;
                                }
                                break;
                            case 6:
                                switch (roll2)
                                {
                                    case 1 : faction.Origin = Languages.FactionOrigin_Arabic; break;
                                    case 2 : faction.Origin = Languages.FactionOrigin_Australian; break;
                                    case 3 : faction.Origin = Languages.FactionOrigin_OldColony; break;
                                    case 4 : faction.Origin = Languages.FactionOrigin_MiddleColony; break;
                                    case 5 : faction.Origin = Languages.FactionOrigin_TheWay_Mainstream; break;
                                    case 6 : faction.Origin = Languages.FactionOrigin_TheWay_Heretical; break;
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
                                    case 1 : faction.Name = Languages.FactionName_Republicans; break;
                                    case 2 : faction.Name = Languages.FactionName_Federalists; break;
                                    case 3 : faction.Name = Languages.FactionName_Royalists; break;
                                    case 4 : faction.Name = Languages.FactionName_Sepratists; break;
                                    case 5 : faction.Name = Languages.FactionName_Alliance; break;
                                    case 6 : faction.Name = Languages.FactionName_LeagueofBarons; break;
                                }
                                break;
                            case 2:
                                switch (roll2)
                                {
                                    case 1 : faction.Name = "Trade Federation"; break;
                                    case 2 : faction.Name = "Farmers Union"; break;
                                    case 3 : faction.Name = "Citizen's Movement"; break;
                                    case 4 : faction.Name = "Revolutionaries"; break;
                                    case 5 : faction.Name = "Glorious Brotherhood"; break;
                                    case 6 : faction.Name = "Freedom Party"; break;
                                }
                                break;
                            case 3:
                                switch (roll2)
                                {
                                    case 1 : faction.Name = "Corporate Militia"; break;
                                    case 2 : faction.Name = "Rebels"; break;
                                    case 3 : faction.Name = "Industry Alliance"; break;
                                    case 4 : faction.Name = "Coalition"; break;
                                    case 5 : faction.Name = "Tripartate Alliance"; break;
                                    case 6 : faction.Name = "Coastal Union"; break;
                                }
                                break;
                            case 4:
                                switch (roll2)
                                {
                                    case 1 : faction.Name = "Highlanders"; break;
                                    case 2 : faction.Name = "Southern States"; break;
                                    case 3 : faction.Name = "North Star League"; break;
                                    case 4 : faction.Name = "Terran Loyalists"; break;
                                    case 5 : faction.Name = "Progressive Party"; break;
                                    case 6 : faction.Name = "Crusaders"; break;
                                }
                                break;
                            case 5:
                                switch (roll2)
                                {
                                    case 1 : faction.Name = "Mountain Partisans"; break;
                                    case 2 : faction.Name = "Centralists"; break;
                                    case 3 : faction.Name = "Treaty Forces"; break;
                                    case 4 : faction.Name = "Western Alliance"; break;
                                    case 5 : faction.Name = "Eastern Bloc"; break;
                                    case 6 : faction.Name = "Stability Party"; break;
                                }
                                break;
                            case 6:
                                switch (roll2)
                                {
                                    case 1 : faction.Name = "Moderate Alliance"; break;
                                    case 2 : faction.Name = "People's Party"; break;
                                    case 3 : faction.Name = "Imperialist"; break;
                                    case 4 : faction.Name = "Monarchists"; break;
                                    case 5 : faction.Name = "Liberators"; break;
                                    case 6 : faction.Name = "Peace Faction"; break;
                                }
                                break;
                        }
                    }
                    Factions.Add(faction);
                }
            }
        }

        public double Population()
        {
            return PopMult * Math.Pow(10, Pop.Value);
        }

        private char GetStarport(Configuration config)
        {
            var dieroll = Common.d6() + Common.d6();
            if (config.HardScience)
            {
                dieroll = dieroll + 7 - Pop.Value;
            }
            else
            {
                if (Pop.Value == 1) dieroll += 2;
                if (Pop.Value == 2) dieroll += 1;
                if (Pop.Value >= 6 && Pop.Value <= 9) dieroll -= 1;
                if (Pop.Value == 10) dieroll -= 2;
            }

            if (Pop.Value == 0)
                return 'X';

            char starport = 'X';

            switch (config.StarportTable)
            {
                case StarportTableType.BACKWATER:
                    if (dieroll < 3) starport = 'A';
                    else if (dieroll < 6) starport = 'B';
                    else if (dieroll < 9) starport = 'C';
                    else if (dieroll < 10) starport = 'D';
                    else if (dieroll < 12) starport = 'E';
                    else starport = 'X';
                    break;
                case StarportTableType.STANDARD:
                    if (dieroll < 4) starport = 'A';
                    else if (dieroll < 7) starport = 'B';
                    else if (dieroll < 9) starport = 'C';
                    else if (dieroll < 10) starport = 'D';
                    else if (dieroll < 12) starport = 'E';
                    else starport = 'X';
                    break;
                case StarportTableType.MATURE:
                    if (dieroll < 4) starport = 'A';
                    else if (dieroll < 7) starport = 'B';
                    else if (dieroll < 9) starport = 'C';
                    else if (dieroll < 10) starport = 'D';
                    else starport = 'E';
                    break;
                case StarportTableType.CLUSTER:
                    if (dieroll < 6) starport = 'A';
                    else if (dieroll < 8) starport = 'B';
                    else if (dieroll < 10) starport = 'C';
                    else if (dieroll < 11) starport = 'D';
                    else if (dieroll < 12) starport = 'E';
                    else starport = 'X';
                    break;
            }

            return starport;
        }

        public void SaveToXML(XmlElement objWorld, Configuration configuration)
        {
            var xeInfo = objWorld.OwnerDocument.CreateElement("TravellerInfo");
            objWorld.AppendChild(xeInfo);
            var xeChild = objWorld.OwnerDocument.CreateElement("Starport");
            xeChild.AppendChild(objWorld.OwnerDocument.CreateTextNode(Starport.ToString()));
            xeInfo.AppendChild(xeChild);
            xeChild = objWorld.OwnerDocument.CreateElement("Size");
            xeChild.AppendChild(objWorld.OwnerDocument.CreateTextNode(Size.Value.ToString()));
            xeInfo.AppendChild(xeChild);
            xeChild = objWorld.OwnerDocument.CreateElement("Atmosphere");
            xeChild.AppendChild(objWorld.OwnerDocument.CreateTextNode(Atmosphere.Value.ToString()));
            xeInfo.AppendChild(xeChild);
            xeChild = objWorld.OwnerDocument.CreateElement("Hydro");
            xeChild.AppendChild(objWorld.OwnerDocument.CreateTextNode(Hydro.Value.ToString()));
            xeInfo.AppendChild(xeChild);
            xeChild = objWorld.OwnerDocument.CreateElement("Pop");
            xeChild.AppendChild(objWorld.OwnerDocument.CreateTextNode(Pop.Value.ToString()));
            xeInfo.AppendChild(xeChild);
            xeChild = objWorld.OwnerDocument.CreateElement("Government");
            xeChild.AppendChild(objWorld.OwnerDocument.CreateTextNode(Government.Value.ToString()));
            xeInfo.AppendChild(xeChild);
            xeChild = objWorld.OwnerDocument.CreateElement("Law");
            xeChild.AppendChild(objWorld.OwnerDocument.CreateTextNode(Law.Value.ToString()));
            xeInfo.AppendChild(xeChild);
            xeChild = objWorld.OwnerDocument.CreateElement("TechLevel");
            xeChild.AppendChild(objWorld.OwnerDocument.CreateTextNode(TechLevel.Value.ToString()));
            xeInfo.AppendChild(xeChild);
            xeChild = objWorld.OwnerDocument.CreateElement("PopMult");
            xeChild.AppendChild(objWorld.OwnerDocument.CreateTextNode(PopMult.ToString()));
            xeInfo.AppendChild(xeChild);
            xeChild = objWorld.OwnerDocument.CreateElement("Remarks");
            xeChild.AppendChild(objWorld.OwnerDocument.CreateTextNode(Remarks));
            xeInfo.AppendChild(xeChild);
            xeChild = objWorld.OwnerDocument.CreateElement("Bases");
            xeChild.AppendChild(objWorld.OwnerDocument.CreateTextNode(Bases));
            xeInfo.AppendChild(xeChild);

            if (configuration.CurrentCampaign == Campaign.HAMMERSSLAMMERS)
            {
                xeChild = objWorld.OwnerDocument.CreateElement("ConflictReason");
                xeChild.AppendChild(objWorld.OwnerDocument.CreateTextNode(ConflictReason));
            }

            xeInfo.AppendChild(xeChild);
            xeChild = objWorld.OwnerDocument.CreateElement("Factions");
            foreach (var faction in Factions)
            {
                var xeFactionChild = objWorld.OwnerDocument.CreateElement("Faction");
                var xeAttrib = objWorld.OwnerDocument.CreateAttribute("Government");
                xeAttrib.Value = faction.GovernmentString;
                xeFactionChild.Attributes.Append(xeAttrib);
                xeAttrib = objWorld.OwnerDocument.CreateAttribute("Strength");
                xeAttrib.Value = faction.StrengthString;
                xeFactionChild.Attributes.Append(xeAttrib);
                if (configuration.CurrentCampaign == Campaign.HAMMERSSLAMMERS)
                {
                    xeAttrib = objWorld.OwnerDocument.CreateAttribute("Origin");
                    xeAttrib.Value = faction.Origin;
                    xeFactionChild.Attributes.Append(xeAttrib);
                    xeAttrib = objWorld.OwnerDocument.CreateAttribute("Name");
                    xeAttrib.Value = faction.Name;
                    xeFactionChild.Attributes.Append(xeAttrib);
                }
                xeFactionChild.AppendChild(objWorld.OwnerDocument.CreateTextNode(faction.DisplayString(configuration)));
                xeChild.AppendChild(xeFactionChild);
            }
            xeInfo.AppendChild(xeChild);
        }
    }
}
