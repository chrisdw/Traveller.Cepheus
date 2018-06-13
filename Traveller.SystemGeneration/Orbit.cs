using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace org.DownesWard.Traveller.SystemGeneration
{
    public class Orbit
    {
        private const int HabitLow = 330;
        private const int HabitHigh = 400;

        public enum OrbitType
        {
            INNER,
            HABITABLE,
            OUTER,
            UNAVAILABLE
        }

        public OrbitType OrbitalType { get; set; }

        public enum OccupiedBy
        {
            WORLD,
            STAR,
            CAPTURED,
            UNOCCUPIED,
            GASGIANT,
            EMPTY,
            PLANETOID
        }

        public OccupiedBy Occupied { get; set; }

        public int Number { get; set; }

        public double Range { get; set; }

        public Planet World { get; set; }

        public void OrbitRange(int orbitnum)
        {
            Number = orbitnum;

            switch (orbitnum)
            {
                case 0: Range = 0.1 + Common.Change(0.1); break;
                case 1: Range = 0.3 + Common.Change(0.1); break;
                case 2: Range = 0.6 + Common.Change(0.1); break;
                case 3: Range = 0.8 + Common.Change(0.2); break;
                case 4: Range = 1.2 + Common.Change(0.4); break;
                case 5: Range = 2.0 + Common.Change(0.8); break;
                case 6: Range = 3.6 + Common.Change(1.6); break;
                case 7: Range = 6.8 + Common.Change(3.2); break;
                case 8: Range = 13.2 + Common.Change(6.4); break;
                case 9: Range = 26.0 + Common.Change(12.8); break;
                case 10: Range = 51.6 + Common.Change(25.6); break;
                case 11: Range = 102.8 + Common.Change(51.2); break;
                case 12: Range = 205.2 + Common.Change(102.4); break;
                case 13: Range = 410.0 + Common.Change(204.8); break;
                case 14: Range = 819.6 + Common.Change(409.6); break;
                case 15: Range = 1638.8 + Common.Change(819.2); break;
                case 16: Range = 3277.2 + Common.Change(1638.4); break;
                case 17: Range = 6554.0 + Common.Change(3276.8); break;
                case 18: Range = 13107.6 + Common.Change(6553.6); break;
                case 19: Range = 26214.8 + Common.Change(131.07); break;
            }
        }

        public void SetOrbitType(double luminosity, double ComLumAddFromPrim)
        {
            var L = luminosity;
            var O = Constants.HABITNUM / Math.Sqrt(Range);
            var LO = L * O;
            LO += ComLumAddFromPrim;

            if (LO > 8 * HabitHigh)
            {
                // Vaporised!
                OrbitalType = OrbitType.UNAVAILABLE;
            }
            else if (LO > HabitHigh)
            {
                OrbitalType = OrbitType.INNER;
            }
            else if (LO < HabitLow)
            {
                OrbitalType = OrbitType.OUTER;
            }
            else
            {
                OrbitalType = OrbitType.HABITABLE;
            }
        }

        public int Count(Planet.WorldType worldType)
        {
            if (World != null)
            {
                if (World.PlanetType == worldType)
                {
                    return 1;
                }
            }
            return 0;
        }

        public void SaveToXML(XmlElement objStar, Configuration configuration)
        {
            var xeOrbit = objStar.OwnerDocument.CreateElement("Orbit");
            objStar.AppendChild(xeOrbit);
            Common.CreateTextNode(objStar, "Type", OrbitalType.ToString());
            Common.CreateTextNode(objStar, "Occupied", Occupied.ToString());

            if (Occupied == OccupiedBy.CAPTURED)
            {
                Common.CreateTextNode(objStar, "Number", World.OrbitNumber.ToString());
            }
            else
            {
                Common.CreateTextNode(objStar, "Number", Number.ToString());
            }

            Common.CreateTextNode(objStar, "Range", Range.ToString());
            Common.CreateTextNode(objStar, "HasWorld", (World != null).ToString());

            if (World != null)
            {
                World.SaveToXML(xeOrbit, configuration);
            }
        }

        public double Population(bool forCollapse)
        {
            if (World != null)
            {
                return World.Population(forCollapse);
            }
            return 0;
        }

        public Orbit()
        {
            OrbitalType = OrbitType.UNAVAILABLE;
            Occupied = OccupiedBy.UNOCCUPIED;
        }
    }
}
