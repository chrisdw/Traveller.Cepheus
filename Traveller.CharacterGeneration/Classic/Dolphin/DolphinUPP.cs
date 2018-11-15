using org.DownesWard.Traveller.Shared;
using org.DownesWard.Traveller.Shared.Properties;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace org.DownesWard.Traveller.Shared.Classsic
{
    public class DolphinUPP : UPP
    {
        public TravCode HitsU { get; } = new TravCode(40, Resources.UPP_Attr_HitsU, "HitsU");
        public TravCode HitsD { get; } = new TravCode(20, Resources.UPP_Attr_HitsD, "HitsD");
        public override TravCode Int { get; } = new TravCode(13, Resources.UPP_Attr_Intelligence, "Int");

        public override TravCode this[string index]
        {
            get
            {
                if (index.ToUpperInvariant().Equals("HITSU"))
                {
                    return HitsU;
                }
                else if (index.ToUpperInvariant().Equals("HITSD"))
                {
                    return HitsD;
                }
                else
                {
                    return base[index];
                }
            }
        }

        public override string PhysicalUPP()
        {
            return string.Format("{0}/{1}", HitsU.ToString(), HitsD.ToString());
        }

        public override string SocialUPP()
        {
            return Int.ToString();
        }

        public override void SaveXML(XmlElement ele)
        {
            base.SaveXML(ele);
            HitsU.SaveXML(ele);
            HitsD.SaveXML(ele);
        }

        public override void LoadXML(XmlElement ele)
        {
            base.LoadXML(ele);
            HitsU.LoadXML(ele);
            HitsD.LoadXML(ele);
        }
    }
}
