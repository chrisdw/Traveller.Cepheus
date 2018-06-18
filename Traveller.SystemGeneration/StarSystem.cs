using org.DownesWard.Traveller.AnimalEncounters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace org.DownesWard.Traveller.SystemGeneration
{
    public class StarSystem
    {
        public enum SystemType
        {
            SOLO,
            BINARY,
            TRINARY
        }

        public TravInfo Information { get; private set; } 

        public Planet Mainworld { get; private set; }

        public string BG { get; private set; }

        private int SystemHabitability;
        public Star Primary { get; internal set; }
        private SystemType systemType;
        private Configuration _configuration;

        private void FleshOut()
        {
            SystemHabitability = Primary.FleshOut();
        }
        private void Generate()
        {
            // Just need the UPP, trade code and remarks
            Mainworld = new Planet(_configuration);
            Mainworld.Generate();

            if (_configuration.CurrentCampaign == Campaign.THENEWERA)
            {
                Mainworld.DoCollapse();
                Information = Mainworld.Collapse;
            }
            else
            {
                Information = Mainworld.Normal;
            }
            var generator = new TableGenerator();
            Mainworld.Life = true;
            Mainworld.Encounters = generator.Generate(2, Mainworld.Normal);
            // Get the BG string
            BG = string.Format("{0}{1}", Star.NumPlanetoids(), Star.NumGasGiants());

        }

        public static SystemType Nature(bool companion)
        {
            var dieroll = Common.d6() + Common.d6();

            if (companion)
            {
                dieroll--;
            }
            if (dieroll < 8)
            {
                return SystemType.SOLO;
            }
            if (dieroll == 12)
            {
                return SystemType.TRINARY;
            }
            return SystemType.BINARY;
        }

        public StarSystem(Configuration configuration)
        {
            _configuration = configuration;
            Information = new TravInfo(_configuration);
            SystemHabitability = 0;
            var ComLumAddFromPrimary = 0.0;

            if (_configuration.Generation == GenerationType.FULL)
            {
                systemType = Nature(false);
                Primary = new Star(configuration);
                if (systemType == SystemType.TRINARY)
                {
                    Primary.NumCompanions = 2;
                }
                else if (systemType == SystemType.BINARY)
                {
                    Primary.NumCompanions = 1;
                }
                Primary.BuildSystem(ComLumAddFromPrimary);
                for (var i = 0; i < Primary.NumCompanions; i++)
                {
                    var companion = new CompanionStar(configuration);
                    Primary.Companions.Add(companion);
                    var retry = false;
                    do
                    {
                        for (var j = 0; j < i - 1; j++)
                        {
                            if (Primary.Companions[i].OrbitNum == Primary.Companions[j].OrbitNum)
                            {
                                Primary.Companions.Remove(companion);
                                companion = new CompanionStar(configuration);
                                Primary.Companions.Add(companion);
                                retry = true;
                            }
                        }
                    } while (retry);
                    Primary.AvaialbleOribits(i);
                }
                FleshOut();

                // Get the BG string
                BG = string.Format("{0}{1}", Primary.Count(Planet.WorldType.PLANETOID),
                    Primary.Count(Planet.WorldType.LGG) + Primary.Count(Planet.WorldType.SGG));
            }
            else
            {
                Generate();
            }
        }

        public StarSystem(Configuration configuration, string primaryDescriptor)
        {
            _configuration = configuration;
            Information = new TravInfo(_configuration);
            var ComLumAddFromPrim = 0.0;
            SystemHabitability = 0;
            systemType = SystemType.SOLO;
            Primary = new Star(configuration, Star.CharToType(primaryDescriptor[0]), primaryDescriptor[1], primaryDescriptor[2]);
            Primary.BuildSystem(ComLumAddFromPrim);
            FleshOut();
        }

        public StarSystem(Configuration configuration, string primaryDescriptor, string secondaryDescriptor)
        {
            _configuration = configuration;
            Information = new TravInfo(_configuration);
            var ComLumAddFromPrim = 0.0;
            SystemHabitability = 0;
            systemType = SystemType.BINARY;
            Primary = new Star(configuration, Star.CharToType(primaryDescriptor[0]), primaryDescriptor[1], primaryDescriptor[2]);
            Primary.BuildSystem(ComLumAddFromPrim);
            Primary.NumCompanions = 1;
            var companion = new CompanionStar(configuration, Star.CharToType(secondaryDescriptor[0]), secondaryDescriptor[1], secondaryDescriptor[2]);
            Primary.Companions.Add(companion);
            Primary.AvaialbleOribits(0);
            FleshOut();
        }

        public StarSystem(Configuration configuration, string primaryDescriptor, string secondaryDescriptor, string trinaryDescriptor)
        {
            _configuration = configuration;
            Information = new TravInfo(_configuration);
            var ComLumAddFromPrim = 0.0;
            SystemHabitability = 0;
            systemType = SystemType.TRINARY;
            Primary = new Star(configuration, Star.CharToType(primaryDescriptor[0]), primaryDescriptor[1], primaryDescriptor[2]);
            Primary.BuildSystem(ComLumAddFromPrim);
            Primary.NumCompanions = 2;
            var companion = new CompanionStar(configuration, Star.CharToType(secondaryDescriptor[0]), secondaryDescriptor[1], secondaryDescriptor[2]);
            Primary.Companions.Add(companion);

            companion = new CompanionStar(configuration, Star.CharToType(trinaryDescriptor[0]), trinaryDescriptor[1], trinaryDescriptor[2]);
            Primary.Companions.Add(companion);

            var retry = false;
            {
                retry = false;
                // Stop two companions existing iu the same orbit
                if (Primary.Companions[0].OrbitNum == Primary.Companions[1].OrbitNum)
                {
                    Primary.Companions.RemoveAt(1);
                    companion = new CompanionStar(configuration, Star.CharToType(trinaryDescriptor[0]), trinaryDescriptor[1], trinaryDescriptor[2]);
                    Primary.Companions.Add(companion);
                    retry = true;
                }
            } while (retry) ;
            Primary.AvaialbleOribits(0);
        }

        public void Develop()
        {
            if (_configuration.Generation == GenerationType.FULL)
            {
                var mainworld = Primary.GetMainWorld();
                if (mainworld != null)
                {
                    mainworld.MainWorld = true;
                    Mainworld = mainworld;

                    if (_configuration.GenerateTravInfo)
                    {
                        mainworld.CompleteTravInfo();
                    }
                    if (_configuration.CurrentCampaign == Campaign.THENEWERA)
                    {
                        mainworld.DoCollapse();
                        Information = Mainworld.Collapse;
                    }
                    else
                    {
                        Information = Mainworld.Normal;
                    }
                    Primary.Devlop(mainworld);
                }
            }
        }

        public void SaveToXML(XmlDocument objDoc)
        {
            var xeSystem = objDoc.CreateElement("System");
            objDoc.AppendChild(xeSystem);
            Common.CreateTextNode(xeSystem, "SysNat", systemType.ToString());
            Common.CreateTextNode(xeSystem, "SystemHabitability", SystemHabitability.ToString());
            Common.CreateTextNode(xeSystem, "CurrentCampaign", _configuration.CurrentCampaign.ToString());
            Common.CreateTextNode(xeSystem, "GenerateTravInfo", _configuration.GenerateTravInfo.ToString());
            Common.CreateTextNode(xeSystem, "UseFarenheight", _configuration.UseFarenheight.ToString());
            // Summary information for quick access
            Common.CreateTextNode(xeSystem, "Summary", systemType.ToString());
            Common.CreateTextNode(xeSystem, "Primary", Primary.DisplayString);
            foreach (var companion in Primary.Companions)
            {
                Common.CreateTextNode(xeSystem, "Companion", companion.DisplayString);
                foreach (var otherComp in companion.Companions)
                {
                    Common.CreateTextNode(xeSystem, "Companion", companion.DisplayString);
                }
            }
            Common.CreateTextNode(xeSystem, "PlanetoidBelts", "There are " + Primary.Count(Planet.WorldType.PLANETOID) + " planetoid belts");
            Common.CreateTextNode(xeSystem, "GasGiants", "There are " + (Primary.Count(Planet.WorldType.LGG) + Primary.Count(Planet.WorldType.SGG)) + " Gas Giants");
            var mainworld = Primary.GetMainWorld();
            if (mainworld != null)
            {
                Common.CreateTextNode(xeSystem, "Mainworld", mainworld.DisplayString);
                Common.CreateTextNode(xeSystem, "PBG", mainworld.Normal.PopMult.ToString() + BG);
                if (_configuration.CurrentCampaign == Campaign.THENEWERA)
                {
                    Common.CreateTextNode(xeSystem, "PostCollapseMainworld", mainworld.Collapse.DisplayString(mainworld.PlanetType, mainworld.Diameter));
                }
            }
            if (_configuration.GenerateTravInfo)
            {
                Common.CreateTextNode(xeSystem, "SystemPopulation", Primary.Population(false).ToString());
                if (_configuration.CurrentCampaign == Campaign.THENEWERA)
                {
                    Common.CreateTextNode(xeSystem, "PostCollapseSystemPopulation", Primary.Population(true).ToString());
                }
            }
            Primary.SaveToXML(xeSystem);
        }
    }
}
