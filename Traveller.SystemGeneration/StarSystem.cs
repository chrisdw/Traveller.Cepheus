using System;
using System.Collections.Generic;
using System.Text;

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

        public TravInfo Information { get; private set;  } = new TravInfo();
        
        public Planet Mainworld { get; private set; }

        public void Generate(Configuration config)
        {
            // TODO: Add generation code
            if (config.Generation == GenerationType.SIMPLE)
            {
                // Just need the UPP, trade code and remarks
                Mainworld = new Planet();
                Mainworld.Generate(config);
                Information = Mainworld.Normal;
                if (config.CurrentCampaign == Campaign.THENEWERA)
                {
                    Mainworld.DoCollapse();
                }
            }
        }
    }
}
