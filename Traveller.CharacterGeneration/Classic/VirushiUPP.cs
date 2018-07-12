using org.DownesWard.Traveller.Shared.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.Shared.Classsic
{
    public class VirushiUPP : UPP
    {
        // Virushi have different physical parameters
        public override TravCode Str { get; } = new TravCode(25, Resources.UPP_Attr_Strength, "Str");
        public override TravCode Dex { get; } = new TravCode(17, Resources.UPP_Attr_Dexterity, "Dex");
        public override TravCode End { get; } = new TravCode(15, Resources.UPP_Attr_Endurance, "End");
    }
}
