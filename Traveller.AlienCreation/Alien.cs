using org.DownesWard.Traveller.Shared;
using org.DownesWard.Traveller.SystemGeneration;
using org.DownesWard.Utilities;
using System.Collections.Generic;

namespace org.DownesWard.Traveller.AlienCreation
{
    public class Alien
    {
        private Dice dice = new Dice(6);

        public enum EcologicalTypes
        {
            Scavenger,
            Herbivore,
            Omnivore,
            Carnivore
        }

        public enum EcologicalSubtypes
        {
            Filter,
            Intermittent,
            Grazer,
            Gatherer,
            Hunter,
            Eater,
            Pouncer,
            Trapper,
            Chaser,
            Siren,
            Killer,
            Reducer,
            Hijacker,
            Intimidator,
            CarrionEater
        }

        public enum Metabolisms
        {
            WarmBlooded,
            ColdBlooded
        }
        public enum Genders
        {
            Asexual,
            Hermaphroditic,
            BiGender,
            MultiGender
        }

        public enum ReproductionMethods
        {
            ExternalBudding,
            LiveBearing,
            EggLaying
        }

        public EcologicalTypes EcologicalType { get; private set; }
        public EcologicalSubtypes EcologicalSubtype { get; private set; }
        public Metabolisms Metabolism { get; private set; }
        public Genders GenderModel { get; private set; }
        public int NumGenders { get; private set; }
        public ReproductionMethods ReproductionMethod { get; private set; }

        public List<string> Traits { get; private set; } = new List<string>();

        public void Generate(Planet homeworld)
        {
            WorldSizeEffects(homeworld.Normal.Size);
            WorldAtmospherEffects(homeworld.Normal.Atmosphere);
            WorldHydrographicsEffects(homeworld.Normal.Hydro);
            WorldClimateEffects(homeworld);
            EcologicalNiche();
            GenerateMetabolism();
            GenerateGenders();
            GenerateReproduction();
            GenerateRespiration(homeworld);
        }

        private void GenerateRespiration(Planet homeworld)
        {
            var respiration = dice.roll(2);
            if (homeworld.Normal.Hydro.Value >= 8)
            {
                respiration += 2;
            }
            if (respiration == 12 && homeworld.Normal.Hydro.Value != 0)
            {
                AddTrait("Aquatic");
                AddTrait("Natural Swimmer");
                AddTrait("Amphibious");
                var res = dice.roll(2);
                if (res >= 6 && res < 10)
                {
                    AddTrait("Water Dependent");
                }
                else
                {
                    RemoveTrait("Amphibious");
                    RemoveTrait("Water Dependent");
                }
            }
        }

        private void GenerateReproduction()
        {
            var reproduction = dice.roll(2);
            if (GenderModel == Genders.Asexual)
            {
                reproduction -= 3;
            }
            if (Metabolism == Metabolisms.ColdBlooded)
            {
                reproduction += 3;
            }
            reproduction = reproduction.Clamp(2, 12);
            switch (reproduction)
            {
                case 2:
                    ReproductionMethod = ReproductionMethods.ExternalBudding;
                    break;
                case 9:
                case 10:
                case 11:
                case 12:
                    ReproductionMethod = ReproductionMethods.EggLaying;
                    break;
                default:
                    ReproductionMethod = ReproductionMethods.LiveBearing;
                    break;
            }
        }

        private void AddTrait(string trait)
        {
            if (!Traits.Contains(trait))
            {
                Traits.Add(trait);
            }
        }

        private void RemoveTrait(string trait)
        {
            if (Traits.Contains(trait))
            {
                Traits.Remove(trait);
            }
        }

        private void GenerateGenders()
        {
            switch (dice.roll(2))
            {
                case 2:
                    GenderModel = Genders.Asexual;
                    NumGenders = 1;
                    break;
                case 3:
                case 4:
                    GenderModel = Genders.Hermaphroditic;
                    NumGenders = 1;
                    break;
                case 12:
                    GenderModel = Genders.MultiGender;
                    NumGenders = dice.roll() + 1;
                    if (NumGenders == 2)
                    {
                        GenderModel = Genders.BiGender;
                    }
                    break;
                default:
                    GenderModel = Genders.BiGender;
                    NumGenders = 2;
                    break;
            }
            if (NumGenders >= 2)
            {
                if (dice.roll(2) >= 12)
                {
                    AddTrait("Gendermorphic");
                }
            }
        }

        private void GenerateMetabolism()
        {
            if (dice.roll(2) >= 11)
            {
                Metabolism = Metabolisms.ColdBlooded;
            }
            else
            {
                Metabolism = Metabolisms.WarmBlooded;
            }
        }

        private void WorldSizeEffects(TravCode size)
        {
            if (size.Value == 0)
            {
                AddTrait("Zero-Gravity Adaptation");
                AddTrait("Gravity Intolerance");
            }
            else if (size.Value >= 1 && size.Value <= 3)
            {
                AddTrait("Low Gravity Adaptation");
                AddTrait("Gravity Intolerance");
            }
            else if (size.Value >= 4 && size.Value <= 6)
            {
                AddTrait("Low Gravity Adaptation");
            }
            else if (size.Value >= 10)
            {
                AddTrait("High Gravity Adaptation");
            }
        }

        private void WorldAtmospherEffects(TravCode atmosphere)
        {
            if (atmosphere.Value == 0)
            {
                AddTrait("Vaccum Survival");
            }
            else if (atmosphere.Value == 1)
            {
                Traits.Add("Trace Breather");
                if (dice.roll(2) >= 11)
                {
                    AddTrait("Vaccum Survival (Limited)");
                }
            }
            else if (atmosphere.Value == 2 || atmosphere.Value == 3)
            {             
                if (dice.roll(2) >= 11)
                {
                    AddTrait("Trace Breather");
                    AddTrait("Vaccum Survival (Limited)");
                }
                else
                {
                    Traits.Add("Trace Breather (Limited)");
                }
            }
            else if (atmosphere.Value >= 10 && atmosphere.Value == 12)
            {
                AddTrait("Atmospheric Requirements");
            }
            if (atmosphere.Value == 2 || 
                atmosphere.Value == 4 ||
                atmosphere.Value == 7 ||
                atmosphere.Value == 9)
            {
                if (dice.roll(2) >= 9)
                {
                    AddTrait("Taint Immunity");
                }
                else
                {
                    AddTrait("Tainted Breather");
                }
            }
        }

        private void WorldHydrographicsEffects(TravCode hydro)
        {
            if (hydro.Value == 0)
            {
                AddTrait("Desert Adaptation");
            }
            else if (hydro.Value == 10)
            {
                AddTrait("Aquatic");
                AddTrait("Natural Swimmer");
                if (dice.roll(2) >= 10)
                {
                    AddTrait("Amphibious");
                    if (dice.roll(2) >= 6)
                    {
                        AddTrait("Water Dependent");
                    }
                }

            }
        }

        private void WorldClimateEffects(Planet homeworld)
        {
            if (homeworld.Temp <= -25)
            {
                AddTrait("Cold Endurance");
                AddTrait("Cold Resistance");
                AddTrait("Fire Vulnerability");
            }
            else if (homeworld.Temp <= 0)
            {
                AddTrait("Cold Endurance");
            }
            else if (homeworld.Temp >= 25 && homeworld.Temp < 45)
            {
                AddTrait("Heat Endurance");
            }
            else if (homeworld.Temp >= 45)
            {
                AddTrait("Heat Endurance");
                AddTrait("Heat Resistance");
                AddTrait("Cold Vulnerability");
            }
        }

        private void EcologicalNiche()
        {
            var roll = dice.roll(2);
            switch (roll)
            {
                case 2:
                case 3:
                    EcologicalType = EcologicalTypes.Scavenger;
                    ScavengerSubtype();
                    break;
                case 4:
                case 5:
                case 10:
                    EcologicalType = EcologicalTypes.Herbivore;
                    HerbivoreSubtype();
                    break;
                case 6:
                case 7:
                case 8:
                case 9:
                    EcologicalType = EcologicalTypes.Omnivore;
                    OmnivoreSubtype();
                    break;
                case 11:
                case 12:
                    EcologicalType = EcologicalTypes.Carnivore;
                    CarnivoreSubType();
                    break;
            }
        }

        private void ScavengerSubtype()
        {
            var subRoll = dice.roll(2);
            switch (subRoll)
            {
                case 2:
                    EcologicalSubtype = EcologicalSubtypes.Reducer;
                    break;
                case 3:
                case 4:
                    EcologicalSubtype = EcologicalSubtypes.Hijacker;
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    EcologicalSubtype = EcologicalSubtypes.Intimidator;
                    break;
                default:
                    EcologicalSubtype = EcologicalSubtypes.CarrionEater;
                    break;
            }
        }

        private void HerbivoreSubtype()
        {
            int subRoll = dice.roll(2);
            switch (subRoll)
            {
                case 2:
                case 3:
                    EcologicalSubtype = EcologicalSubtypes.Filter;
                    break;
                case 4:
                case 5:
                case 6:
                case 7:
                    EcologicalSubtype = EcologicalSubtypes.Intermittent;
                    break;
                default:
                    EcologicalSubtype = EcologicalSubtypes.Grazer;
                    break;
            }
        }

        private void OmnivoreSubtype()
        {
            int subRoll = dice.roll(2);
            switch (subRoll)
            {
                case 2:
                case 3:
                case 4:
                    EcologicalSubtype = EcologicalSubtypes.Gatherer;
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    EcologicalSubtype = EcologicalSubtypes.Hunter;
                    break;
                default:
                    EcologicalSubtype = EcologicalSubtypes.Eater;
                    break;
            }
        }

        private void CarnivoreSubType()
        {
            int subRoll = dice.roll(2);
            switch (subRoll)
            {
                case 2:
                case 3:
                case 4:
                    EcologicalSubtype = EcologicalSubtypes.Pouncer;
                    break;
                case 5:
                    EcologicalSubtype = EcologicalSubtypes.Trapper;
                    break;
                case 6:
                case 7:
                case 8:
                case 9:
                    EcologicalSubtype = EcologicalSubtypes.Chaser;
                    break;
                case 10:
                    EcologicalSubtype = EcologicalSubtypes.Siren;
                    break;
                default:
                    EcologicalSubtype = EcologicalSubtypes.Killer;
                    break;
            }
        }
    }
}
