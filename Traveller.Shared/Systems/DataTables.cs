using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.Shared.Systems
{
    public class DataTables
    {
        // Stellar Mass tables
        public double[] MassTableIa = { 16, 16, 14, 11, 10, 8.1, 8.1, 10, 11, 14, 16, 16 };
        public double[] MassTableIb = { 16, 16, 14, 11, 10, 8.1, 8.1, 10, 11, 14, 16, 16 };

        public double[,] EnergyAbsorbHZ = {
            {0.9, 0.9, 0.74, 0.9},
            {0.829, 0.9, 0.697, 0.9},
            {0.803, 0.86, 0.672, 0.882},
            {0.811, 0.86, 0.676, 0.883},
            {0.782, 0.82, 0.648, 0.866},
            {0.789, 0.78, 0.613, 0.85},
            {0.795, 0.74, 0.577, 0.836},
            {0.802, 0.7, 0.539, 0.821},
            {0.808, 0.66, 0.5, 0.807},
            {0.814, 0.62, 0.5, 0.793},
            {0.818, 0.619, 0.5, 0.773}
        };
    }
}
