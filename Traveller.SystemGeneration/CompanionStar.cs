using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.SystemGeneration
{
    public class CompanionStar : Star
    {
        public short OrbitNum { get; set; }
        public double Ranage { get; set; }
        public StarSystem.SystemType SysNat { get; set; }
    }
}
