using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.SystemGeneration.Campaigns
{
    public interface ICampaign
    {
        int GenerateTechLevel(TravInfo travInfo);
        int GenerateSubordinateTechLevel(TravInfo travInfo, TravInfo mainworld);
        string GenerateTradeCodes(TravInfo travInfo);
    }
}
