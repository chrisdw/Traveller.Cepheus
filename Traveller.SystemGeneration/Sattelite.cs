using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.SystemGeneration
{
    public class Sattelite : Planet, IComparable
    {
        public int CompareTo(object obj)
        {
            var other = (Sattelite)obj;
            return OrbitNumber.CompareTo(other.OrbitNumber);
        }
    }
}
