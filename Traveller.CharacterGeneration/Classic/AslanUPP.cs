using org.DownesWard.Traveller.Shared.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.Shared.Classsic
{
    public class AslanUPP : UPP
    {
        // Aslan have different physical parameters
        public override TravCode Str { get; } = new TravCode(16, Resources.UPP_Attr_Strength, "Str");
        public override TravCode Dex { get; } = new TravCode(14, Resources.UPP_Attr_Dexterity, "Dex");
        public override TravCode End { get; } = new TravCode(16, Resources.UPP_Attr_Endurance, "End");
    }
}
