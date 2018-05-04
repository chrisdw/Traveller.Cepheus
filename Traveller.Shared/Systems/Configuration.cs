using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.Shared.Systems
{
    public class Configuration
    {
        public enum Campaign
        {
            CLASSIC,
            HOSTILE
        }

        public Campaign CurrentCampaign { get; set; }
        public bool SpaceOpera { get; set; }
        public bool HardScience { get; set; }
    }
}
