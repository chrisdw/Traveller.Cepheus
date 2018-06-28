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

        private Dice dice = new Dice(6);

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
                        case Species.Bwap:
                            Profile = new UPP();
                            Profile.Str.Value = dice.roll(2) - 4;
                            Profile.Dex.Value = dice.roll(2);
                            Profile.End.Value = dice.roll(2) - 4;
                            Profile.Int.Value = dice.roll(2);
                            Profile.Edu.Value = dice.roll(2);
                            Profile.Soc.Value = dice.roll(2);
                            break;
                    }
                    break;
            }
        }
        public void AddSkill(Skill skill)
        {
            if (!Skills.ContainsKey(skill.Name))
            {
                Skills.Add(skill.Name, skill);
            }
            else
            {
                Skills[skill.Name].Level += skill.Level;
            }
        }

        public void AddBenefit(Benefit benefit)
        {
            if (Benefits.ContainsKey(benefit.Name))
            {
                Benefits[benefit.Name].Value += benefit.Value;
            }
            else
            {
                Benefits.Add(benefit.Name, benefit);
            }
        }

        public bool AgingCheck()
        {
            var result = true;
            switch (CharacterSpecies)
            {
                default:
                    if (Age >= 66)
                    {
                        CheckAttribute(Profile.Str, 9, -2);
                        CheckAttribute(Profile.Dex, 9, -2);
                        CheckAttribute(Profile.End, 9, -2);
                        CheckAttribute(Profile.Int, 9, -1);
                    }
                    else if (Age >= 50)
                    {
                        CheckAttribute(Profile.Str, 9, -1);
                        CheckAttribute(Profile.Dex, 8, -1);
                        CheckAttribute(Profile.End, 9, -1);
                    }
                    else if (Age >= 34)
                    {
                        CheckAttribute(Profile.Str, 8, -1);
                        CheckAttribute(Profile.Dex, 8, -1);
                        CheckAttribute(Profile.End, 8, -1);
                    }
                    break;
            }
            // Any ageable attributes reduced to 0
            if (Profile.Str.Value == 0 || Profile.Dex.Value == 0 || Profile.End.Value == 0 || Profile.Int.Value == 0)
            {
                // Possible aging crisis
                if (dice.roll(2) >= 8)
                {
                    if (Profile.Str.Value == 0)
                    {
                        Profile.Str.Value = 1;
                    }
                    if (Profile.Dex.Value == 0)
                    {
                        Profile.Dex.Value = 1;
                    }
                    if (Profile.End.Value == 0)
                    {
                        Profile.End.Value = 1;
                    }
                    if (Profile.Int.Value == 0)
                    {
                        Profile.Int.Value = 1;
                    }
                    Journal.Add(string.Format("You were ill for {0} months due to aging.", dice.roll()));
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }

        private void CheckAttribute(TravCode attribute, int check, int drop)
        {
            var save = dice.roll(2);
            if (save < check)
            {
                attribute.Value += drop;
                Journal.Add(string.Format("{0} reduced by {1} due to aging", attribute.Name, Math.Abs(drop)));
            }
        }
    }
}
