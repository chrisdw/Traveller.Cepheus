using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.Shared.Systems
{
    public class Star
    {
        public enum StellarType
        {
            O,
            B,
            A,
            F,
            G,
            K,
            M
        }

        public const int FAR_ORBIT = 99;

        protected int TypeRoll { get; set; }
        protected int ClassRoll { get; set; }
        public StellarType Type { get; set; }
        public char LumClass { get; set; }
        public char DecClass { get; set; }
        public double StellarMass { get; set; }
        public double Luminosity { get; set; }
        public short NumOrbits { get; set; }
        public short HZone { get; set; }
        public int NumCompanions { get; set; }
        public string Name { get; set; }
    }
}
