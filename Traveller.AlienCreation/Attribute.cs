using org.DownesWard.Utilities;

namespace org.DownesWard.Traveller.AlienCreation
{
    public class Attribute
    {
        private Dice dice = new Dice(6);

        public int Dice { get; internal set; }
        public int Modifier { get; internal set; }

        public int Generate()
        {
            return dice.roll(Dice) + Modifier;
        }

        public static Attribute[] TraitSteps = new Attribute[15]
        {
            new Attribute() { Dice = 1, Modifier = -1},
            new Attribute() { Dice = 1, Modifier = 0},
            new Attribute() { Dice = 1, Modifier = 1},
            new Attribute() { Dice = 2, Modifier = -2},
            new Attribute() { Dice = 2, Modifier = -1},
            new Attribute() { Dice = 2, Modifier = 0},
            new Attribute() { Dice = 2, Modifier = 1},
            new Attribute() { Dice = 2, Modifier = 2},
            new Attribute() { Dice = 3, Modifier = -1},
            new Attribute() { Dice = 3, Modifier = 0},
            new Attribute() { Dice = 3, Modifier = 1},
            new Attribute() { Dice = 4, Modifier = -1},
            new Attribute() { Dice = 4, Modifier = 0},
            new Attribute() { Dice = 4, Modifier = 1},
            new Attribute() { Dice = 4, Modifier = 2}
        };

        public static int StepChange(int result)
        {
            var change = 0;
            if (result <= 9)
            {
                change = -5;
            }
            else if (result >= -8 && result <= -6)
            {
                change = -4;
            }
            else if (result >= -5 && result <= -3)
            {
                change = -3;
            }
            else if (result >= -2 && result <= 0)
            {
                change = -2;
            }
            else if (result >= 1 && result <= 3)
            {
                change = -1;
            }
            else if (result >= 4 && result <= 10)
            {
                change = 0;
            }
            else if (result >= 11 && result <= 13)
            {
                change = 1;
            }
            else if (result >= 14 && result <= 16)
            {
                change = 2;
            }
            else if (result >= 17 && result <= 19)
            {
                change = 3;
            }
            else if (result >= 20 && result <= 22)
            {
                change = 4;
            }
            else if (result >= 23)
            {
                change = 5;
            }
            return change;
        }

        public static Attribute Score(int baseValue, int result, int maxStep, out int change)
        {
            var baseStep = 5;
            if (baseValue == 1)
            {
                baseStep = 1;
            }
            else if (baseValue == 3)
            {
                baseStep = 9;
            }
            var step = baseStep + StepChange(result);
            if (maxStep > TraitSteps.Length - 1)
            {
                maxStep = TraitSteps.Length - 1;
            }
            step = step.Clamp(0, maxStep);
            change = baseStep - step;
            return TraitSteps[step];
        }

        public override string ToString()
        {
            return string.Format("{0}D6{1:+0;-#}", Dice, Modifier);
        }
    }
}
