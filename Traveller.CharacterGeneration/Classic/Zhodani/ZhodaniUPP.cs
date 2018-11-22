using org.DownesWard.Traveller.Shared;
using System.Xml;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Zhodani
{
    public class ZhodaniUPP : UPP
    {
        public TravCode Psi { get; } = new TravCode(15, Shared.Properties.Resources.UPP_Attr_Psionics, "Psi");

        public override TravCode this[string index]
        {
            get
            {
                if (index.ToUpperInvariant().Equals("PSI"))
                {
                    return Psi;
                }
                else
                {
                    return base[index];
                }
            }
        }

        public override string SocialUPP()
        {
            return string.Format("{0}{1}{2}-{3}", Int.ToString(), Edu.ToString(), Soc.ToString(), Psi.ToString());
        }

        public override void SaveXML(XmlElement ele)
        {
            base.SaveXML(ele);
            Psi.SaveXML(ele);
        }

        public override void LoadXML(XmlElement ele)
        {
            base.LoadXML(ele);
            Psi.LoadXML(ele);
        }
    }
}
