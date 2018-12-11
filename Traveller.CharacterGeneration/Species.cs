using org.DownesWard.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration
{
    public class Species
    {
        private static Dice dice = new Dice(6);

        public static List<string> SexList(Character.Species species)
        {
            var sexes = new List<string>();
            switch (species)
            {
                case Character.Species.Froog:
                    sexes.Add("Leader");
                    sexes.Add("Technician");
                    sexes.Add("Warrior");
                    break;
                case Character.Species.Insectans:
                    sexes.Add(Properties.Resources.Sex_Worker);
                    sexes.Add(Properties.Resources.Sex_Soldier);
                    sexes.Add(Properties.Resources.Sex_Drone);
                    sexes.Add(Properties.Resources.Sex_Queen);
                    break;
                case Character.Species.Aslan:
                    sexes.Add(Properties.Resources.Sex_Male);
                    sexes.Add(Properties.Resources.Sex_Female);
                    sexes.Add(Properties.Resources.Sex_Random);
                    break;
                default:
                    sexes.Add(Properties.Resources.Sex_Male);
                    sexes.Add(Properties.Resources.Sex_Female);
                    break;
            }
            return sexes;
        }

        public static string ResolveRandom(Character.Species species)
        {
            switch (species)
            {
                case Character.Species.Aslan:
                    if (dice.roll(2) <= 5)
                    {
                        return Properties.Resources.Sex_Male;
                    }
                    else
                    {
                        return Properties.Resources.Sex_Female;
                    }
                default:
                    if (dice.roll() <= 3)
                    {
                        return Properties.Resources.Sex_Male;
                    }
                    else
                    {
                        return Properties.Resources.Sex_Female;
                    }
            }

        }
    }
}
