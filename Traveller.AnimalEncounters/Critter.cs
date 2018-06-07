using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.AnimalEncounters
{
    public class Critter
    {
        public int CritterType { get; internal set; }
        public int Dienum { get; internal set; }
        public string Attribute { get; internal set; }
        public string Weight { get; internal set; }
        public string Armour { get; internal set; }
        public string Weapons { get; internal set; }
        public string Wounds { get; internal set; }
        public int Attack { get; internal set; }
        public int Flee { get; internal set; }
        public int Speed { get; internal set; }
        public int Family { get; internal set; }

        public string CritterTypeString
        {
            get
            {
                return TableData.ctypes[CritterType].Trim();
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (CritterType < 14)
            {
                sb.AppendFormat("{0,2} {1} {2} {3} {4} {5}{6} F{7}A{8}S{9}", 
                    Dienum, Attribute, TableData.ctypes[CritterType], Weight, Armour, Weapons, Wounds, Flee, Attack, Speed);
            }
            else if (CritterType > 55)
            {
                sb.AppendFormat("{0,2} {1} {2}", Dienum, Attribute, TableData.ctypes[CritterType]);
            }
            else
            {
                sb.AppendFormat("{0,2} {1} {2} {3} {4} {5}{6} A{7}F{8}S{9}",
                     Dienum, Attribute, TableData.ctypes[CritterType], Weight, Armour, Weapons, Wounds, Attack, Flee, Speed);
            }
            if (Family > 0)
            {
                sb.AppendFormat(" *{0,2}", Family);
            }
            return sb.ToString();
        }
    }
}
