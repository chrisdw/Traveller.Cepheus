using org.DownesWard.Traveller.Shared;
using org.DownesWard.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using static org.DownesWard.Traveller.CharacterGeneration.Career;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus
{
    public class Character : CharacterGeneration.Character
    {
        public List<string> Traits { get; } = new List<string>();

        public event EventHandler<SkillOfferedEventArgs> SkillOffered;

        private Dice dice = new Dice(6);
        private Dice d3 = new Dice(3);

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
                            Traits.Add(Resources.Trait_Flyer);
                            Traits.Add(Resources.Trait_LowGravityAdaptation);
                            Traits.Add(Resources.Trait_NaturalPilot);
                            Traits.Add(Resources.Trait_SlowSpeed);
                            Traits.Add(Resources.Trait_Small);
                            break;
                        case Species.Insectans:
                            Profile = new UPP();
                            Profile.Str.Value = dice.roll(2) + 2;
                            Profile.Dex.Value = dice.roll(2);
                            Profile.End.Value = dice.roll(2);
                            Profile.Int.Value = dice.roll(2);
                            Profile.Edu.Value = dice.roll(2);
                            Profile.Soc.Value = dice.roll(2);
                            Traits.Add(Resources.Trait_Armoured);
                            Traits.Add(Resources.Trait_BadFirstImpression);
                            Traits.Add(Resources.Trait_Caste);
                            Traits.Add(Resources.Trait_ColdBlooded);
                            Traits.Add(Resources.Trait_FastSpeed);
                            Traits.Add(Resources.Trait_GreatLeaper);
                            Traits.Add(Resources.Trait_HiveMentaility);
                            break;
                        case Species.Merfolk:
                            Profile = new UPP();
                            Profile.Str.Value = dice.roll(2);
                            Profile.Dex.Value = dice.roll(2);
                            Profile.End.Value = dice.roll(2);
                            Profile.Int.Value = dice.roll(2);
                            Profile.Edu.Value = dice.roll(2);
                            Profile.Soc.Value = dice.roll(2);
                            Traits.Add(Resources.Trait_Amphibious);
                            Traits.Add(Resources.Trait_Aquatic);
                            Traits.Add(Resources.Trait_NaturalSwimmer);
                            Traits.Add(Resources.Trait_WaterDependant);
                            break;
                        case Species.Reptilians:
                            Profile = new UPP();
                            Profile.Str.Value = dice.roll(2) + 1;
                            Profile.Dex.Value = dice.roll(2) + 1;
                            Profile.End.Value = dice.roll(2) - 2;
                            Profile.Int.Value = dice.roll(2);
                            Profile.Edu.Value = dice.roll(2);
                            Profile.Soc.Value = dice.roll(2);
                            Traits.Add(Resources.Trait_AntiPsionic);
                            Traits.Add(Resources.Trait_FastSpeed);
                            Traits.Add(Resources.Trait_HeatEndurance);
                            Traits.Add(Resources.Trait_LowLightVision);
                            Traits.Add(Resources.Trait_NaturalWeapons);
                            Traits.Add(Resources.Trait_LowGravityAdaptation);
                            break;
                        case Species.Dolphin:
                            Profile = new UPP();
                            Profile.Str.Value = dice.roll(2) + 4;
                            Profile.Dex.Value = dice.roll(2);
                            Profile.End.Value = dice.roll(2) + 2;
                            Profile.Int.Value = dice.roll(2) - 2;
                            Profile.Edu.Value = dice.roll(2) - 2;
                            Profile.Soc.Value = dice.roll(2) - 2;
                            Traits.Add(Resources.Trait_Aquatic);
                            Traits.Add(Resources.Trait_NoFineManipulators);
                            Traits.Add(Resources.Trait_Uplifited);
                            break;
                        case Species.Uplifted_Ape:
                            Profile = new UPP();
                            Profile.Str.Value = dice.roll(2) + 2;
                            Profile.Dex.Value = dice.roll(2) - 2;
                            Profile.End.Value = dice.roll(2) + 2;
                            Profile.Int.Value = dice.roll(2) - 2;
                            Profile.Edu.Value = dice.roll(2) - 2;
                            Profile.Soc.Value = dice.roll(2) - 2;
                            Traits.Add(Resources.Trait_Uplifited);
                            break;
                    }
                    var backgroundSkills = 3 + Profile.Edu.Modifier;
                    var skillList = new Skill()
                    {
                        Name = "Background Skills",
                        Level = 0,
                        Cascade =
                        {
                            CharacterGeneration.SkillLibrary.Admin,
                            SkillLibrary.Advocate,
                            SkillLibrary.Animals,
                            CharacterGeneration.SkillLibrary.Carousing,
                            CharacterGeneration.SkillLibrary.Communications,
                            CharacterGeneration.SkillLibrary.Computer,
                            CharacterGeneration.SkillLibrary.Electronics,
                            CharacterGeneration.SkillLibrary.Engineering,
                            SkillLibrary.Linguistics,
                            CharacterGeneration.SkillLibrary.Mechanical,
                            CharacterGeneration.SkillLibrary.Medic,
                            SkillLibrary.Sciences,
                        }
                    };
                    for (var i = 0; i < backgroundSkills; i++)
                    {
                        OnSkillOffered(skillList);
                    }
                    break;
                case Constants.CultureType.Cepheus_Hostile:
                    switch (CharacterSpecies)
                    {
                        case Species.Human:
                            Profile = new UPP();
                            Profile.Str.Value = dice.roll(2);
                            Profile.Dex.Value = dice.roll(2);
                            Profile.End.Value = dice.roll(2);
                            Profile.Int.Value = dice.roll(2);
                            Profile.Edu.Value = dice.roll(2);
                            Profile.Soc.Value = dice.roll(2);
                            break;
                        case Species.Android:
                            Profile = new UPP();
                            Profile.Str.Value = dice.roll(1) + 5;
                            Profile.Dex.Value = dice.roll(1) + 3;
                            Profile.End.Value = dice.roll(1) + 8;
                            Profile.Int.Value = d3.roll(1) + 5;
                            Profile.Edu.Value = d3.roll(1) + 9;
                            Profile.Soc.Value = 7;
                            break;
                    }
                    break;
            }
        }

        public override bool AgingCheck()
        {
            var result = true;
            switch (CharacterSpecies)
            {
                case Species.Avian:
                    if (Age >= 46)
                    {
                        PerformAging();
                    }
                    break;
                case Species.Reptilians:
                    if (Age >= 42)
                    {
                        PerformAging();
                    }
                    break;
                default:
                    if (Age >= 34)
                    {
                        PerformAging();
                    }
                    break;
            }
            return result;
        }

        private void PerformAging()
        {
            var roll = dice.roll(2) - Careers.Select(c => c.TermsServed).Sum();
            if (roll <= 0)
            {
                switch (roll)
                {
                    case 0:
                        ReduceOnePhysicalCharacteristic(1);
                        break;
                    case -1:
                        ReduceOnePhysicalCharacteristic(1);
                        ReduceOnePhysicalCharacteristic(1);
                        break;
                    case -2:
                        ReduceOnePhysicalCharacteristic(1);
                        ReduceOnePhysicalCharacteristic(1);
                        ReduceOnePhysicalCharacteristic(1);
                        break;
                    case -3:
                        ReduceOnePhysicalCharacteristic(2);
                        ReduceOnePhysicalCharacteristic(1);
                        ReduceOnePhysicalCharacteristic(1);
                        break;
                    case -4:
                        ReduceOnePhysicalCharacteristic(2);
                        ReduceOnePhysicalCharacteristic(2);
                        ReduceOnePhysicalCharacteristic(1);
                        break;
                    case -5:
                        ReduceOnePhysicalCharacteristic(2);
                        ReduceOnePhysicalCharacteristic(2);
                        ReduceOnePhysicalCharacteristic(2);
                        break;
                    case -6:
                        ReduceOnePhysicalCharacteristic(2);
                        ReduceOnePhysicalCharacteristic(2);
                        ReduceOnePhysicalCharacteristic(2);
                        ReduceOneMentalCharacteristic(1);
                        break;
                }
            }
        }

        public override void SaveXML(XmlDocument doc)
        {
            base.SaveXML(doc);
            var character = doc.GetElementsByTagName("Character")[0];
            var traits = character.OwnerDocument.CreateElement("Traits");
            foreach (var item in Traits)
            {
                var child = character.OwnerDocument.CreateElement("TraitItem");
                child.AppendChild(child.OwnerDocument.CreateTextNode(item));
                traits.AppendChild(child);
            }
            character.AppendChild(traits);
        }

        private void ReduceOnePhysicalCharacteristic(int by)
        {
            switch (dice.roll())
            {
                case 1:
                case 2:
                    Journal.Add(string.Format(Resources.Msg_AgingReduction, "STR", by));
                    Profile.Str.Value -= by;
                    break;
                case 3:
                case 4:
                    Journal.Add(string.Format(Resources.Msg_AgingReduction, "DEX", by));
                    Profile.Dex.Value -= by;
                    break;
                case 5:
                case 6:
                    Journal.Add(string.Format(Resources.Msg_AgingReduction, "END", by));
                    Profile.End.Value -= by;
                    break;
            }
        }

        private void ReduceOneMentalCharacteristic(int by)
        {
            switch (dice.roll())
            {
                case 1:
                case 2:
                case 3:
                    Journal.Add(string.Format(Resources.Msg_AgingReduction, "INT", by));
                    Profile.Int.Value -= by;
                    break;
                case 4:
                case 5:
                case 6:
                    Journal.Add(string.Format(Resources.Msg_AgingReduction, "EDU", by));
                    Profile.Edu.Value -= by;
                    break;
            }
        }

        protected virtual void OnSkillOffered(Skill skill)
        {
            var e = new SkillOfferedEventArgs() { OfferedSkill = skill, Owner = this };
            SkillOffered?.Invoke(this, e);
        }

        public new static Character Load(XmlDocument doc)
        {
            var styleStr = doc.GetElementsByTagName("System")[0].InnerText;
            var character = new Character();
            Enum.TryParse(styleStr, out Constants.GenerationStyle style);
            character.Style = style;

            var cultureStr = doc.GetElementsByTagName("Culture")[0].InnerText;
            Enum.TryParse(cultureStr, out Constants.CultureType culture);
            character.Culture = culture;

            var speciesStr = doc.GetElementsByTagName("Species")[0].InnerText;
            Enum.TryParse(speciesStr, out Species species);
            character.CharacterSpecies = species;

            character.Sex = doc.GetElementsByTagName("Sex")[0].InnerText;
            character.Name = doc.GetElementsByTagName("Name")[0].InnerText;
            character.Age = int.Parse(doc.GetElementsByTagName("Age")[0].InnerText);

            // Now have enough to generate the correct profile
            switch (character.CharacterSpecies)
            {
                case Species.Human:
                    // use the standard UPP
                    character.Profile = new UPP();
                    break;
                default:
                    character.Profile = new UPP();
                    break;
            }

            var attribs = doc.GetElementsByTagName("Attributes")[0] as XmlElement;
            character.Profile.LoadXML(attribs);

            var journalItems = doc.GetElementsByTagName("JournalItem");
            foreach (var item in journalItems)
            {
                var journal = item as XmlElement;
                character.Journal.Add(journal.InnerText);
            }
            var careers = doc.GetElementsByTagName("Career");
            foreach (var item in careers)
            {
                var career = item as XmlElement;
                character.Careers.Add(Career.Load(career));
            }

            var skills = doc.GetElementsByTagName("Skill");
            foreach (var item in skills)
            {
                var skill = item as XmlElement;
                character.AddSkill(Skill.Load(skill));
            }

            var benefits = doc.GetElementsByTagName("Benefit");
            foreach (var item in benefits)
            {
                var benefit = item as XmlElement;
                character.AddBenefit(Benefit.Load(benefit));
            }

            var traitItems = doc.GetElementsByTagName("TraitItem");
            foreach (var item in traitItems)
            {
                var trait = item as XmlElement;
                character.Traits.Add(trait.InnerText);
            }
            return character;
        }
    }
}
