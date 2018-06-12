using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.SystemGeneration.Campaigns
{
    public class Hostile : Classic, ICampaign
    {
        public override int GenerateTechLevel(TravInfo travInfo)
        {
            // Hostile has a single uniform culture
            return 12;
        }

        public override int GenerateSubordinateTechLevel(TravInfo travInfo, TravInfo mainworld)
        {
            // Hostile has a single uniform culture
            return 12;
        }
    }
}
