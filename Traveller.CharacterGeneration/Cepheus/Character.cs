using org.DownesWard.Traveller.Shared;
using org.DownesWard.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus
{
    public class Character : CharacterGeneration.Character
    {
        public List<string> Traits { get; } = new List<string>();

        public override void Generate()
        {

            switch (Style)
            {
                case Constants.GenerationStyle.Classic_Traveller:
                    base.Generate();
                    break;
                case Constants.GenerationStyle.Cepheus_Engine:
                    GenerateCepheus();
                    break;
            }
        }

        private void GenerateCepheus()
        {
            var dice = new Dice(6);
            switch (Culture)
            {
                case Constants.CultureType.Cepheus_Generic:
                    switch (CharacterSpecies)
                    {
                        case Species.Human:
                            // use the standard UPP
                            Profile = new UPP();
                            Profile.Str.Value = dice.roll(2);
                            Profile.Dex.Value = dice.roll(2);
                            Profile.End.Value = dice.roll(2);
                            Profile.Int.Value = dice.roll(2);
                            Profile.Edu.Value = dice.roll(2);
                            Profile.Soc.Value = dice.roll(2);
                            break;
                        case Species.Avian:
                            Profile = new UPP();
                            Profile.Str.Value = dice.roll(1);
                            Profile.Dex.Value = dice.roll(3);
                            Profile.End.Value = dice.roll(1);
                            Profile.Int.Value = dice.roll(2);
                            Profile.Edu.Value = dice.roll(2);
                            Profile.Soc.Value = dice.roll(2);
                            Traits.Add("Flyer");
                            Traits.Add("Low Gravity Adaptation");
                            Traits.Add("Natural Pilot");
                            Traits.Add("Slow Speed");
                            Traits.Add("Small");
                            break;
                        case Species.Insectans:
                            Profile = new UPP();
                            Profile.Str.Value = dice.roll(2) + 2;
                            Profile.Dex.Value = dice.roll(2);
                            Profile.End.Value = dice.roll(2);
                            Profile.Int.Value = dice.roll(2);
                            Profile.Edu.Value = dice.roll(2);
                            Profile.Soc.Value = dice.roll(2);
                            Traits.Add("Armoured");
                            Traits.Add("Bad First Impression");
                            Traits.Add("Caste");
                            Traits.Add("Cold Blooded");
                            Traits.Add("Fast Speed");
                            Traits.Add("Great Leaper");
                            Traits.Add("Hive Mentaility");
                            break;
                        case Species.Merfolk:
                            Profile = new UPP();
                            Profile.Str.Value = dice.roll(2);
                            Profile.Dex.Value = dice.roll(2);
                            Profile.End.Value = dice.roll(2);
                            Profile.Int.Value = dice.roll(2);
                            Profile.Edu.Value = dice.roll(2);
                            Profile.Soc.Value = dice.roll(2);
                            Traits.Add("Amphibious");
                            Traits.Add("Aquatic");
                            Traits.Add("Natural Swimmer");
                            Traits.Add("Water Dependant");
                            break;
                        case Species.Reptilians:
                            Profile = new UPP();
                            Profile.Str.Value = dice.roll(2) + 1;
                            Profile.Dex.Value = dice.roll(2) + 1;
                            Profile.End.Value = dice.roll(2) - 2;
                            Profile.Int.Value = dice.roll(2);
                            Profile.Edu.Value = dice.roll(2);
                            Profile.Soc.Value = dice.roll(2);
                            Traits.Add("Anti-Psionic");
                            Traits.Add("Fast Speed");
                            Traits.Add("Heat Endurance");
                            Traits.Add("Low-Light Vision");
                            Traits.Add("Natural Weapons");
                            Traits.Add("Low Gravity Adaptation");
                            break;
                    }
                    break;
            }
        }

        public override void SaveXML(XmlDocument doc)
        {
            base.SaveXML(doc);
            var character = doc.GetElementsByTagName("Character")[0];
            var traits = doc.OwnerDocument.CreateElement("Traits");
            foreach (var item in Journal)
            {
                var child = character.OwnerDocument.CreateElement("TraitItem");
                child.AppendChild(child.OwnerDocument.CreateTextNode(item));
                traits.AppendChild(child);
            }
            character.AppendChild(traits);
        }
    }
}
