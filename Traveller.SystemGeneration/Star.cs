using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.SystemGeneration
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
        public StellarType StarType { get; set; }
        public char LumClass { get; set; }
        public char DecClass { get; set; }
        public double StellarMass { get; set; }
        public double Luminosity { get; set; }
        public List<Orbit> Orbits { get; } = new List<Orbit>();
        public short HZone { get; set; }
        public List<CompanionStar> Companions { get; } = new List<CompanionStar>();
        public string Name { get; set; }
    }
}
