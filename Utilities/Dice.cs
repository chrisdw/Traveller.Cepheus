using System;

namespace org.DownesWard.Utilities
{
    public class Dice
    {
        public int Sides { get; private set; }

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

            var rnd = new Random();

            for (int count = 0; count < numRolls; count++)
            {
                result += rnd.Next(1, Sides + 1);
            }

            return result;
        }
    }
}