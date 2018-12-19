using org.DownesWard.Traveller.Shared;
using org.DownesWard.Traveller.Shared.Properties;
using System.Xml;

namespace org.DownesWard.Traveller.AnimalEncounters.Cepheus
{
    public class CritterProfile : UPP
    {
        // Critters can get really big
        public override TravCode Str { get; } = new TravCode(100, Resources.UPP_Attr_Strength, "STR");
        public override TravCode Dex { get; } = new TravCode(25, Resources.UPP_Attr_Dexterity, "DEX");
        public override TravCode End { get; } = new TravCode(100, Resources.UPP_Attr_Endurance, "END");

        public TravCode Instinct { get; } = new TravCode(15, "Instinct", "INS");
        public TravCode Pack { get; } = new TravCode(15, "Pack", "PAC");

        public override TravCode this[string index]
        {
            get
            {
                if (index.ToUpperInvariant().Equals("INS"))
                {
                    return Instinct;
                }
                else if (index.ToUpperInvariant().Equals("PAC"))
                {
                    return Pack;
                }
                else
                {
                    return base[index];
                }
            }
        }

        public override string SocialUPP()
        {
            return string.Format("{0}{1}{2}", Int.ToString(), Instinct.ToString(), Pack.ToString());
        }

        public override void SaveXML(XmlElement ele)
        {
            base.SaveXML(ele);
            Instinct.SaveXML(ele);
            Pack.SaveXML(ele);
        }

        public override void LoadXML(XmlElement ele)
        {
            base.LoadXML(ele);
            Instinct.LoadXML(ele);
            Pack.LoadXML(ele);
        }
    }
}
