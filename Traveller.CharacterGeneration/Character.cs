using org.DownesWard.Traveller.Shared;
using org.DownesWard.Utilities;
using System;
using System.Collections.Generic;

namespace org.DownesWard.Traveller.CharacterGeneration
{
    public class Character
    {
        public enum Species
        {
            Human_Imperial,
            Human_Zhodani,
            Human_Solomani,
            Human_Darrian,
            Human_Dynchia,
            Human_SwordWorlds,
            Human_Irklan,
            Human_Vilani,
            KKree,
            Aslan,
            Vargr,
            Hiver,
            Dolphin,
            Droyne,
            Hlanssai,
            GirugKagh,
            AelYael,
            Virushi,
            Bwap,
            Ithulkur,
            Vegan
        }

        public string Sex { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public bool Died { get; set; }
        public Species CharacterSpecies { get; set; }
        public List<string> Journal { get; } = new List<string>();
        public Dictionary<string, Skill> Skills { get; } = new Dictionary<string, Skill>();
        public Dictionary<string, Benefit> Benefits { get; } = new Dictionary<string, Benefit>();

        public UPP Profile { get; set; }
        public List<Career> Careers { get; set; } = new List<Career>();

        public Constants.CultureType Culture { get; set; }
        public Constants.GenerationStyle Style { get; set; } = Constants.GenerationStyle.Classic_Traveller;

        public void Generate()
        {
            switch (Style)
            {
                case Constants.GenerationStyle.Classic_Traveller:
                    GenerateClassic();
                    break;
            }
        }

        private void GenerateClassic()
        {
            var dice = new Dice(6);

            switch (Culture)
            {
                case Constants.CultureType.Imperial:
                    switch (CharacterSpecies)
                    {
                        case Species.Human_Imperial:
                            // use the standard UPP
                            Profile = new UPP();
                            Profile.Str.Value = dice.roll(2);
                            Profile.Dex.Value = dice.roll(2);
                            Profile.End.Value = dice.roll(2);
                            Profile.Int.Value = dice.roll(2);
                            Profile.Edu.Value = dice.roll(2);
                            Profile.Soc.Value = dice.roll(2);
                            break;
                    }
                    break;
            }
        }
    }
}
