using System.Xml;

namespace org.DownesWard.Traveller.SystemGeneration
{
    public class CompanionStar : Star
    {
        public int OrbitNum { get; set; }
        public double Range { get; set; }
        public StarSystem.SystemType SysNat { get; set; }

        public CompanionStar() : base()
        {
            Build();
        }

        public CompanionStar(StellarType stellarType, char stellarClass, char decimalClass) : base(stellarType, stellarClass, decimalClass)
        {
            Build();
        }

        private int ComOrbit()
        {
            var dieroll = Common.d6() + Common.d6();

            switch (dieroll)
            {
                case 2:
                case 3:
                    return 0;
                case 4:
                    return 1;
                case 5:
                    return 2;
                case 6:
                    return 3;
                case 7:
                    return 4 + Common.d6();
                case 8:
                    return 5 + Common.d6();
                case 9:
                    return 6 + Common.d6();
                case 10:
                    return 7 + Common.d6();
                case 11:
                    return 8 + Common.d6();
                case 12:
                    return FAR_ORBIT;
                default:
                    return -1;
            }
        }

        private void Build()
        {
            OrbitNum = ComOrbit();

            if (NumOrbits > (OrbitNum / 2))
            {
                NumOrbits = OrbitNum / 2;
            }

            if (OrbitNum == 0)
            {
                Orbits[0].OrbitalType = Orbit.OrbitType.UNAVAILABLE;
            }

            if (OrbitNum == FAR_ORBIT)
            {
                Range = Common.d6() * 1000;
                SysNat = StarSystem.Nature(true);
                if (SysNat == StarSystem.SystemType.BINARY)
                {
                    NumCompanions = 1;
                    Companions[0] = new CompanionStar();
                    AvaialbleOribits(0);
                }
            }
        }

        public override void SaveToXML(XmlElement objStar, Configuration configuration)
        {
            var xeStar = objStar.OwnerDocument.CreateElement("Star");
            objStar.AppendChild(xeStar);

            // Companion specific attributes
            var xeChild = objStar.OwnerDocument.CreateElement("OrbitNum");
            xeChild.AppendChild(objStar.OwnerDocument.CreateTextNode(OrbitNum.ToString()));
            xeStar.AppendChild(xeChild);
            xeChild = objStar.OwnerDocument.CreateElement("Range");
            xeChild.AppendChild(objStar.OwnerDocument.CreateTextNode(Range.ToString()));
            xeStar.AppendChild(xeChild);
            xeChild = objStar.OwnerDocument.CreateElement("SysNat");
            xeChild.AppendChild(objStar.OwnerDocument.CreateTextNode(SysNat.ToString()));
            xeStar.AppendChild(xeChild);

            xeChild = objStar.OwnerDocument.CreateElement("Type");
            xeChild.AppendChild(objStar.OwnerDocument.CreateTextNode(StarType.ToString()));
            xeStar.AppendChild(xeChild);
            xeChild = objStar.OwnerDocument.CreateElement("LumClass");
            xeChild.AppendChild(objStar.OwnerDocument.CreateTextNode(LumClass.ToString()));
            xeStar.AppendChild(xeChild);
            xeChild = objStar.OwnerDocument.CreateElement("DecClass");
            xeChild.AppendChild(objStar.OwnerDocument.CreateTextNode(DecClass.ToString()));
            xeStar.AppendChild(xeChild);
            xeChild = objStar.OwnerDocument.CreateElement("StellarMass");
            xeChild.AppendChild(objStar.OwnerDocument.CreateTextNode(StellarMass.ToString()));
            xeStar.AppendChild(xeChild);
            xeChild = objStar.OwnerDocument.CreateElement("Luminosity");
            xeChild.AppendChild(objStar.OwnerDocument.CreateTextNode(Luminosity.ToString()));
            xeStar.AppendChild(xeChild);
            xeChild = objStar.OwnerDocument.CreateElement("NumOrbits");
            xeChild.AppendChild(objStar.OwnerDocument.CreateTextNode(NumOrbits.ToString()));
            xeStar.AppendChild(xeChild);
            xeChild = objStar.OwnerDocument.CreateElement("HZone");
            xeChild.AppendChild(objStar.OwnerDocument.CreateTextNode(HZone.ToString()));
            xeStar.AppendChild(xeChild);
            xeChild = objStar.OwnerDocument.CreateElement("TypeRoll");
            xeChild.AppendChild(objStar.OwnerDocument.CreateTextNode(TypeRoll.ToString()));
            xeStar.AppendChild(xeChild);
            xeChild = objStar.OwnerDocument.CreateElement("ClassRoll");
            xeChild.AppendChild(objStar.OwnerDocument.CreateTextNode(ClassRoll.ToString()));
            xeStar.AppendChild(xeChild);
            xeChild = objStar.OwnerDocument.CreateElement("NumCompanions");
            xeChild.AppendChild(objStar.OwnerDocument.CreateTextNode(NumCompanions.ToString());
            xeStar.AppendChild(xeChild);
            xeChild = objStar.OwnerDocument.CreateElement("Name");
            xeChild.AppendChild(objStar.OwnerDocument.CreateTextNode(Name.ToString()));
            xeStar.AppendChild(xeChild);

            foreach (var orbit in Orbits)
            {
                orbit.SaveToXML(xeStar, configuration);
            }

            var xeStars = objStar.OwnerDocument.CreateElement("Companions");
            xeStar.AppendChild(xeStars);

            foreach (var companion in Companions)
            {
                companion.SaveToXML(xeStars, configuration);
            }
        }
    }

}
