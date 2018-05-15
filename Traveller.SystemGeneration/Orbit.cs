using System;
using System.Collections.Generic;
using System.Text;

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

        public short Number { get; set; }

        public double Range { get; set; }

        public Planet World { get; set; }

    }
}
