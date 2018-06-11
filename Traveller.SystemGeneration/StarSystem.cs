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

        public TravInfo Information { get; private set; } = new TravInfo();

        public Planet Mainworld { get; private set; }

        public string BG { get; private set; }

        private int SystemHability;
        private Star Primary;
        private SystemType systemType;

        private void FleshOut(Configuration configuration)
        {
            SystemHability = Primary.FleshOut(configuration);
        }
        private void Generate(Configuration configuration)
        {
            // Just need the UPP, trade code and remarks
            Mainworld = new Planet();
            Mainworld.Generate(configuration);

            if (configuration.CurrentCampaign == Campaign.THENEWERA)
            {
                Mainworld.DoCollapse(configuration);
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
            SystemHability = 0;
            var ComLumAddFromPrimary = 0.0;

            if (configuration.Generation == GenerationType.FULL)
            {
                systemType = Nature(false);
                Primary = new Star();
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
                    var companion = new CompanionStar();
                    Primary.Companions.Add(companion);
                    var retry = false;
                    do
                    {
                        for (var j = 0; j < i - 1; j++)
                        {
                            if (Primary.Companions[i].OrbitNum == Primary.Companions[j].OrbitNum)
                            {
                                Primary.Companions.Remove(companion);
                                companion = new CompanionStar();
                                Primary.Companions.Add(companion);
                                retry = true;
                            }
                        }
                    } while (retry);
                    Primary.AvaialbleOribits(i);
                }
                FleshOut(configuration);

                // Get the BG string
                BG = string.Format("{0}{1}", Primary.Count(Planet.WorldType.PLANETOID),
                    Primary.Count(Planet.WorldType.LGG) + Primary.Count(Planet.WorldType.SGG));
            }
            else
            {
                Generate(configuration);
            }
        }

        public StarSystem(Configuration configuration, string primaryDescriptor)
        {
            var ComLumAddFromPrim = 0.0;
            SystemHability = 0;
            systemType = SystemType.SOLO;
            Primary = new Star(Star.CharToType(primaryDescriptor[0]), primaryDescriptor[1], primaryDescriptor[2]);
            Primary.BuildSystem(ComLumAddFromPrim);
            FleshOut(configuration);
        }

        public StarSystem(Configuration configuration, string primaryDescriptor, string secondaryDescriptor)
        {
            var ComLumAddFromPrim = 0.0;
            SystemHability = 0;
            systemType = SystemType.BINARY;
            Primary = new Star(Star.CharToType(primaryDescriptor[0]), primaryDescriptor[1], primaryDescriptor[2]);
            Primary.BuildSystem(ComLumAddFromPrim);
            Primary.NumCompanions = 1;
            var companion = new CompanionStar(Star.CharToType(secondaryDescriptor[0]), secondaryDescriptor[1], secondaryDescriptor[2]);
            Primary.Companions.Add(companion);
            Primary.AvaialbleOribits(0);
            FleshOut(configuration);
        }

        public StarSystem(Configuration configuration, string primaryDescriptor, string secondaryDescriptor, string trinaryDescriptor)
        {
            var ComLumAddFromPrim = 0.0;
            SystemHability = 0;
            systemType = SystemType.TRINARY;
            Primary = new Star(Star.CharToType(primaryDescriptor[0]), primaryDescriptor[1], primaryDescriptor[2]);
            Primary.BuildSystem(ComLumAddFromPrim);
            Primary.NumCompanions = 2;
            var companion = new CompanionStar(Star.CharToType(secondaryDescriptor[0]), secondaryDescriptor[1], secondaryDescriptor[2]);
            Primary.Companions.Add(companion);

            companion = new CompanionStar(Star.CharToType(trinaryDescriptor[0]), trinaryDescriptor[1], trinaryDescriptor[2]);
            Primary.Companions.Add(companion);

            var retry = false;
            {
                retry = false;
                // Stop two companions existing iu the same orbit
                if (Primary.Companions[0].OrbitNum == Primary.Companions[1].OrbitNum)
                {
                    Primary.Companions.RemoveAt(1);
                    companion = new CompanionStar(Star.CharToType(trinaryDescriptor[0]), trinaryDescriptor[1], trinaryDescriptor[2]);
                    Primary.Companions.Add(companion);
                    retry = true;
                }
            } while (retry) ;
            Primary.AvaialbleOribits(0);
        }

        public void Develop(Configuration configuration)
        {
            if (configuration.Generation == GenerationType.FULL)
            {
                var mainworld = Primary.GetMainWorld();
                if (mainworld != null)
                {
                    mainworld.MainWorld = true;
                    Mainworld = mainworld;

                    if (configuration.GenerateTravInfo)
                    {
                        mainworld.CompleteTravInfo(configuration);
                    }
                    if (configuration.CurrentCampaign == Campaign.THENEWERA)
                    {
                        mainworld.DoCollapse(configuration);
                        Information = Mainworld.Collapse;
                    }
                    else
                    {
                        Information = Mainworld.Normal;
                    }
                    Primary.Devlop(configuration, mainworld);
                }
            }
        }

    }
}
