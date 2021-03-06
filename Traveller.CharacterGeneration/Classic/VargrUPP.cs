﻿using System.Xml;
using org.DownesWard.Traveller.Shared.Properties;

namespace org.DownesWard.Traveller.Shared.Classic
{
    public class VargrUPP : UPP
    {
        public TravCode Chr { get; } = new TravCode(15, Resources.UPP_Attr_Charisma, "Chr");

        public override TravCode this[string index]
        {
            get
            {
                if (index.ToUpperInvariant().Equals("CHR"))
                {
                    return Chr;
                }
                else
                {
                    return base[index];
                }
            }
        }

        public override string SocialUPP()
        {
            return Int.ToString() + Edu.ToString() + Soc.ToString() + Chr.ToString();
        }

        public override void SaveXML(XmlElement ele)
        {
            base.SaveXML(ele);
            Chr.SaveXML(ele);
        }

        public override void LoadXML(XmlElement ele)
        {
            base.LoadXML(ele);
            Chr.LoadXML(ele);
        }
    }
 }
