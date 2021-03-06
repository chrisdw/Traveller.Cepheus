﻿using System;

namespace org.DownesWard.Utilities
{
    public class Dice
    {
        public int Sides { get; private set; }
        private static Random random = new Random();
        private static object randLock = new object();

        public Dice()
        {
            Sides = 6;
        }

        public Dice(int sides)
        {
            Sides = sides;
        }

        public int roll()
        {
            return roll(1);
        }

        public int roll(int numRolls)
        {
            int result = 0;

            lock (randLock)
            {
                for (int count = 0; count < numRolls; count++)
                {
                    result += random.Next(1, Sides + 1);
                }
            }
            return result;
        }
    }
}