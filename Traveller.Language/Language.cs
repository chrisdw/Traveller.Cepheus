using org.DownesWard.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace org.DownesWard.Traveller.Language
{
    public class Language
    {
        public string Description { get; set; }
        public string Root { get; set; }
        public int Tables { get; set; }

        private Dice dice = new Dice(6);
        private string endSylable;
        private string after;
        private string[,,] sylables = new string[3, 6, 6];
        private string[,,] initial = new string[6, 6, 6];
        private string[,,] vowel = new string[6, 6, 6];
        private string[,,] final = new string[6, 6, 6];

        public string Name { get; set; }

        public Language()
        {

        }

        public Language(string filename)
        {
            LoadFromFile(filename);
        }


        private void LoadData(string name)
        {
            var filename = System.IO.Path.Combine(Root, name + ".ini");
            LoadFromFile(filename);
        }

        private void LoadFromFile(string filename)
        {
            Name = Path.GetFileNameWithoutExtension(filename).Trim();
            Root = Path.GetDirectoryName(filename);
            if (System.IO.File.Exists(filename))
            {
                var file = System.IO.File.OpenText(filename);
                Load(file);
            }
        }

        private void Load(System.IO.TextReader reader)
        {
            // Get the descriptions
            var line = GetLine(reader);
            var elements = line.Split('=');
            Description = elements[1].Trim();

            // Get the number of sylable tables
            line = GetLine(reader);
            elements = line.Split('=');
            Tables = int.Parse(elements[1]);

            // Get the structure
            line = GetLine(reader);
            elements = line.Split('=');
            endSylable = elements[1].Trim();

            // Load the syllable tables
            for (var i = 0; i < Tables; i++)
            {
                if (Tables == 2 && i == 1)
                {
                    line = GetLine(reader);
                    elements = line.Split('=');
                    after = elements[1].Trim();
                }
                for (var j = 0; j <= 5; j++)
                {
                    line = GetLine(reader);
                    elements = line.Split(',');
                    for (var k = 0; k < elements.Length; k++)
                    {
                        sylables[i, j, k] = elements[k].Trim();
                    }
                }
            }
            // load the initial table
            for (var i = 0; i <= 5; i++)
            {
                for (var j = 0; j <= 5; j++)
                {
                    line = GetLine(reader);
                    elements = line.Split(',');
                    for (var k = 0; k < elements.Length; k++)
                    {
                        initial[i, j, k] = elements[k].Trim();
                    }
                }
            }
            // load the vowel table
            for (var i = 0; i <= 5; i++)
            {
                for (var j = 0; j <= 5; j++)
                {
                    line = GetLine(reader);
                    elements = line.Split(',');
                    for (var k = 0; k < elements.Length; k++)
                    {
                        vowel[i, j, k] = elements[k].Trim();
                    }
                }
            }
            // load the final table
            for (var i = 0; i <= 5; i++)
            {
                for (var j = 0; j <= 5; j++)
                {
                    line = GetLine(reader);
                    elements = line.Split(',');
                    for (var k = 0; k < elements.Length; k++)
                    {
                        final[i, j, k] = elements[k].Trim();
                    }
                }
            }
        }

        private string GetLine(System.IO.TextReader reader)
        {
            var result = string.Empty;
            do
            {
                result = reader.ReadLine();
            } while (result.Trim().StartsWith("//"));
            return result;
        }

        private string GenerateSyllable(string structure)
        {
            var initialConstNeeded = structure.StartsWith("C");
            var finalConstNeeded = structure.EndsWith("C");
            var vowelNeeded = structure.Contains("V");
            var syllable = new StringBuilder();

            if (initialConstNeeded)
            {
                syllable.Append(initial[dice.roll() - 1, dice.roll() - 1, dice.roll() - 1]);
            }
            if (vowelNeeded)
            {
                syllable.Append(vowel[dice.roll() - 1, dice.roll() - 1, dice.roll() - 1]);
            }
            if (finalConstNeeded)
            {
                syllable.Append(final[dice.roll() - 1, dice.roll() - 1, dice.roll() - 1]);
            }
            return syllable.ToString();
        }

        public string GenerateWord(int syllablecount = -1)
        {
            var numberOfSyllables = syllablecount;
            var word = new StringBuilder();

            if (numberOfSyllables == -1)
            {
                numberOfSyllables = dice.roll();
            }

            // Always start on the basic table
            var structure = sylables[0, dice.roll() - 1, dice.roll() - 1];
            word.Append(GenerateSyllable(structure));
            for (var i = 2; i <= numberOfSyllables; i++)
            {
                switch (Tables)
                {
                    case 1:
                        structure = sylables[0, dice.roll() - 1, dice.roll() - 1];
                        word.Append(GenerateSyllable(structure));
                        break;
                    case 2:
                        if (structure.EndsWith(after))
                        {
                            structure = sylables[1, dice.roll() - 1, dice.roll() - 1];
                            word.Append(GenerateSyllable(structure));
                        }
                        else
                        {
                            structure = sylables[0, dice.roll() - 1, dice.roll() - 1];
                            word.Append(GenerateSyllable(structure));
                        }
                        break;
                    case 3:
                        if (structure.EndsWith("C"))
                        {
                            structure = sylables[2, dice.roll() - 1, dice.roll() - 1];
                            word.Append(GenerateSyllable(structure));
                        }
                        else
                        {
                            structure = sylables[1, dice.roll() - 1, dice.roll() - 1];
                            word.Append(GenerateSyllable(structure));
                        }
                        break;
                }
                if (structure.Equals(endSylable))
                {
                    break;
                }
            }
            var res = word.ToString().ToLowerInvariant();
            var textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;

            return textInfo.ToTitleCase(res);
        }
    }
}
