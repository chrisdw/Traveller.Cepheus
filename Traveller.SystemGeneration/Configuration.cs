using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.SystemGeneration
{ 
    public class Configuration
    {
        public Campaign CurrentCampaign { get; set; } = Campaign.CLASSIC;
        public StarportTableType StarportTable { get; set; } = StarportTableType.STANDARD;
        public GenerationType Generation { get; set; } = GenerationType.SIMPLE;
        public bool SpaceOpera { get; set; } = false;
        public bool HardScience { get; set; } = false;
        public bool GenerateTravInfo { get; set; } = true;
        public bool UseGaiaFactor { get; set; } = true;
        public bool UseFarenheight { get; set; } = false;
        public string BaseName { get; set; }
    }
}
