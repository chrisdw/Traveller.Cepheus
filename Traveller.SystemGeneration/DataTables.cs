﻿using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.SystemGeneration
{
    public class DataTables
    {
        // Stellar Mass tables
        public double[] MassTableIa = { 16, 16, 14, 11, 10, 8.1, 8.1, 10, 11, 14, 16, 16 };
        public double[] MassTableIb = { 16, 16, 14, 11, 10, 8.1, 8.1, 10, 11, 14, 16, 16 };
        public double[] MassTableII = { 16, 16, 14, 11, 10, 8.1, 8.1, 10, 11, 14, 16, 16 };
        public double[] MassTableIII = { 14, 14, 12, 9, 8, 5, 2.5, 3.2, 4, 5, 6.3, 7.4 };
        public double[] MassTableIV = { 10, 8, 6, 4, 2.5, 2, 1.75, 2, 2.3, 0, 0, 0 };
        public double[] MassTableV = { 6, 4, 3.2, 2.1, 1.7, 1.3, 1.04, 0.94, 0.825, 0.57, 0.489, 0.331 };
        public double[] MassTableD = { 0.3, 0.3, 0.36, 0.36, 0.42, 0.42, 0.63, 0.63, 0.83, 0.83, 1.11, 1.11 };

        // Luminisoity Tables
        public double[] LumTableIa = { 11.0, 10.0, 6.85, 5.4, 4.95, 4.75, 4.86, 5.22, 5.46, 7.04, 8.24, 11.05, 11.28 };
        public double[] LumTableIb = { 11.0, 10.0, 6.85, 5.4, 4.95, 4.75, 4.86, 5.22, 5.46, 7.04, 8.24, 11.05, 11.28 };
        public double[] LumTableII = { 11.0, 10.0, 6.85, 5.4, 4.95, 4.75, 4.86, 5.22, 5.46, 7.04, 8.24, 11.05, 11.28 };
        public double[] LumTableIII = { 8.0, 6.0, 4.09, 3.08, 2.7, 2.56, 2.66, 2.94, 3.12, 4.23, 4.65, 6.91, 7.2 };
        public double[] LumTableIV = { 6.0, 4.0, 3.53, 2.47, 2.09, 1.86, 1.6, 1.49, 1.47, 1.27, 1.25, 1.1, 1.02 };
        public double[] LumTableV = { 4.2, 3.5, 3.08, 2.0, 1.69, 1.37, 1.05, 0.9, 0.81, 0.53, 0.45, 0.29, 0.18 };
        public double[] LumTableD = { 0.37, 0.31, 0.27, 0.27, 0.13, 0.13, 0.09, 0.09, 0.08, 0.08, 0.07, 0.07, 0.07 };

        // Energy absorption factors
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
        public double[,] EnergyAbsorbNHZ = {
            {0.8, 0.8, 0.68, 0.8},
            {0.744, 0.811, 0.646, 0.811},
            {0.736, 0.789, 0.635, 0.807},
            {0.752, 0.799, 0.644, 0.817},
            {0.738, 0.774, 0.625, 0.813},
            {0.753, 0.747, 0.599, 0.809},
            {0.767, 0.718, 0.57, 0.805},
            {0.782, 0.687, 0.537, 0.8},
            {0.796, 0.654, 0.5, 0.794},
            {0.81, 0.619, 0.5, 0.787},
            {0.818, 0.619, 0.5, 0.773}
        };

        // Greenhouse factors
        public double[] Greenhouse = { 1.0, 1.0, 1.0, 1.0, 1.05, 1.05, 1.1, 1.1, 1.15, 1.15, 1.15, 1.0, 1.1, 1.15, 1.0, 1.1 };

        // Latitude Temperature Modifiers
        public double[,] LatitudeMods = {
            {6, 9, 12, 13, 15, 16, 18, 17, 21, 22, 24},
            {4, 6, 8, 9, 10, 11, 12, 13, 14, 15, 16},
            {2, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {-2, -3, -4, -4, -5, -5, -6, -6, -7, -7, -8},
            {-4, -6, -8, -9, -10, -11, -12, -13, -14, -15, -16},
            {-6, -9, -12, -13, -15, -16, -18, -17, -21, -22, -24},
            {-8, -12, -16, -18, -20, -22, -24, -26, -28, -30, -32},
            {-10, -15, -20, -22, -25, -27, -30, -32, -35, -37, -40},
            {-12, -18, -24, -27, -30, -33, -36, -39, -42, -45, -48},
            {-14, -21, -28, -31, -35, -38, -42, -45, -49, -52, -56}
        };

        public double[,] AxialTiltEffects = {
            {0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.25, 0.5, 0.75, 1.0},
            {0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.25, 0.5, 0.75, 1.0, 1.0},
            {0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.25, 0.5, 0.75, 1.0, 1.0, 1.0},
            {0.0, 0.0, 0.0, 0.0, 0.0, 0.25, 0.5, 0.75, 1.0, 1.0, 1.0, 1.0},
            {0.0, 0.0, 0.0, 0.0, 0.25, 0.5, 0.75, 1.0, 1.0, 1.0, 1.0, 1.0},
            {0.0, 0.0, 0.0, 0.25, 0.5, 0.75, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0},
            {0.0, 0.0, 0.25, 0.5, 0.75, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0},
            {0.0, 0.0, 0.5, 0.75, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0},
            {0.0, 0.25, 0.75, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0},
            {0.0, 0.5, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0},
            {0.25, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0}
        };
    }
}