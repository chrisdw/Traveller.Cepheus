using org.DownesWard.Traveller.Shared;
using org.DownesWard.Traveller.SystemGeneration;
using org.DownesWard.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace org.DownesWard.Traveller.AlienCreation
{
    public class Alien
    {
        private Dice dice = new Dice(6);
        private Dice d3 = new Dice(3);

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

        public enum Sizes
        {
            Tiny,
            Small,
            Medium,
            Large,
            Huge
        }

        public enum Symmetries
        {
            Trilateral,
            Bilateral,
            Radial
        }

        public enum MovementRates
        {
            None,
            Slow,
            Average,
            Fast
        }

        public class Attribute
        {
            public int dice;
            public int modifier;
        }
        public EcologicalTypes EcologicalType { get; private set; }
        public EcologicalSubtypes EcologicalSubtype { get; private set; }
        public Metabolisms Metabolism { get; private set; }
        public Genders GenderModel { get; private set; }
        public int NumGenders { get; private set; }
        public ReproductionMethods ReproductionMethod { get; private set; }
        public Sizes Size { get; private set; }
        public Attribute STR { get; private set; } = new Attribute();
        public Attribute DEX { get; private set; } = new Attribute();
        public Attribute END { get; private set; } = new Attribute();
        public Attribute INT { get; private set; } = new Attribute();
        public Attribute EDU { get; private set; } = new Attribute();
        public Attribute SOC { get; private set; } = new Attribute();
        public int AttackDM { get; private set; }
        public Symmetries Symmetry { get; private set; }
        public int LimbCount { get; private set; }
        public int LimbPairs { get; private set; }
        public List<string> LimbGroupTypes { get; private set; } = new List<string>();
        public MovementRates LandMovement { get; private set; }
        public MovementRates FlyMovement { get; private set; }
        public MovementRates SwimMovement { get; private set; }
        public MovementRates ClimbMovement { get; private set; }
        public double LandMovementRate { get; private set; }
        public double FlyMovementRate { get; private set; }
        public double SwimMovementRate { get; private set; }
        public double ClimbMovementRate { get; private set; }
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
            GenerateLocomotion(homeworld);
            GenerateSize(homeworld);
            GenerateLimbs();
            GenerateMovement();
        }

        public UPP Generate()
        {
            var upp = new UPP();
            upp.Str.Value = dice.roll(STR.dice) + STR.modifier;
            upp.Dex.Value = dice.roll(DEX.dice) + DEX.modifier;
            upp.End.Value = dice.roll(END.dice) + END.modifier;
            upp.Int.Value = dice.roll(INT.dice) + INT.modifier;
            upp.Edu.Value = dice.roll(EDU.dice) + EDU.modifier;
            upp.Soc.Value = dice.roll(SOC.dice) + SOC.modifier;
            return upp;
        }

        private void GenerateMovement()
        {
            if (!Traits.Contains("No Land Movement"))
            {
                LandMovement = GenerateComparativeMovement();
                GenerateSpeedTraits(LandMovement);
                var stability = dice.roll(2);
                if (Symmetry != Symmetries.Bilateral)
                {
                    stability += 2;
                }
                if (LandMovement == MovementRates.Fast)
                {
                    stability--;
                }
                if (stability >= 12)
                {
                    AddTrait("Stable");
                }
            }
            if (Traits.Contains("Flyer"))
            {
                if (LimbGroupTypes.Contains("Wings"))
                {
                    FlyMovement = GenerateComparativeMovement();
                }
                else
                {
                    // Flyers with no wings use LTA methods
                    FlyMovement = MovementRates.Slow;
                }
                GenerateSpeedTraits(FlyMovement);
            }
            if (Traits.Contains("Natural Swimmer"))
            {
                if (LimbGroupTypes.Contains("Fins"))
                {
                    SwimMovement = GenerateComparativeMovement();
                }
                else
                {
                    // Swimmers with no fins just float
                    SwimMovement = MovementRates.Slow;
                }
                GenerateSpeedTraits(SwimMovement);
            }
            if (Traits.Contains("Natural Climber"))
            {
                ClimbMovement = GenerateComparativeMovement();
                GenerateSpeedTraits(ClimbMovement);
            }
            // Does the alien have more than 4 legs
            bool manyLegs = false;
            if (Symmetry == Symmetries.Radial)
            {
                // all limbs are multipurpose
                manyLegs = (LimbCount >= 4);
            }
            else if (Symmetry == Symmetries.Bilateral)
            {
                manyLegs = (LimbGroupTypes.Where(s => s.Equals("Legs")).Count() * 2) >= 4;
            }
            else
            {
                manyLegs = (LimbGroupTypes.Where(s => s.Equals("Legs")).Count() * 3) >= 4;
            }
            switch (Size)
            {
                case Sizes.Tiny:
                    switch (LandMovement)
                    {
                        case MovementRates.Slow:
                            if (manyLegs)
                            {
                                LandMovementRate = 3;
                            }
                            else
                            {
                                LandMovementRate = 1.5;
                            }
                            break;
                        case MovementRates.Average:
                            if (manyLegs)
                            {
                                LandMovementRate = 4.5;
                            }
                            else
                            {
                                LandMovementRate = 3;
                            }
                            break;
                        case MovementRates.Fast:
                            if (manyLegs)
                            {
                                LandMovementRate = 6;
                            }
                            else
                            {
                                LandMovementRate = 4.5;
                            }
                            break;
                    }
                    switch (FlyMovement)
                    {
                        case MovementRates.Slow:
                            FlyMovementRate = 4.5;
                            break;
                        case MovementRates.Average:
                            FlyMovementRate = 6;
                            break;
                        case MovementRates.Fast:
                            FlyMovementRate = 7.5;
                            break;
                    }
                    break;
                case Sizes.Small:
                    switch (LandMovement)
                    {
                        case MovementRates.Slow:
                            if (manyLegs)
                            {
                                LandMovementRate = 6;
                            }
                            else
                            {
                                LandMovementRate = 3;
                            }
                            break;
                        case MovementRates.Average:
                            if (manyLegs)
                            {
                                LandMovementRate = 7.5;
                            }
                            else
                            {
                                LandMovementRate = 4.5;
                            }
                            break;
                        case MovementRates.Fast:
                            if (manyLegs)
                            {
                                LandMovementRate = 9;
                            }
                            else
                            {
                                LandMovementRate = 6;
                            }
                            break;
                    }
                    switch (FlyMovement)
                    {
                        case MovementRates.Slow:
                            FlyMovementRate = 6;
                            break;
                        case MovementRates.Average:
                            FlyMovementRate = 9;
                            break;
                        case MovementRates.Fast:
                            FlyMovementRate = 12;
                            break;
                    }
                    break;
                case Sizes.Medium:
                    switch (LandMovement)
                    {
                        case MovementRates.Slow:
                            if (manyLegs)
                            {
                                LandMovementRate = 6;
                            }
                            else
                            {
                                LandMovementRate = 4.5;
                            }
                            break;
                        case MovementRates.Average:
                            if (manyLegs)
                            {
                                LandMovementRate = 6;
                            }
                            else
                            {
                                LandMovementRate = 9;
                            }
                            break;
                        case MovementRates.Fast:
                            if (manyLegs)
                            {
                                LandMovementRate = 9;
                            }
                            else
                            {
                                LandMovementRate = 12;
                            }
                            break;
                    }
                    switch (FlyMovement)
                    {
                        case MovementRates.Slow:
                            FlyMovementRate = 6;
                            break;
                        case MovementRates.Average:
                            FlyMovementRate = 12;
                            break;
                        case MovementRates.Fast:
                            FlyMovementRate = 18;
                            break;
                    }
                    break;
                case Sizes.Large:
                    switch (LandMovement)
                    {
                        case MovementRates.Slow:
                            if (manyLegs)
                            {
                                LandMovementRate = 9;
                            }
                            else
                            {
                                LandMovementRate = 6;
                            }
                            break;
                        case MovementRates.Average:
                            if (manyLegs)
                            {
                                LandMovementRate = 12;
                            }
                            else
                            {
                                LandMovementRate = 9;
                            }
                            break;
                        case MovementRates.Fast:
                            if (manyLegs)
                            {
                                LandMovementRate = 15;
                            }
                            else
                            {
                                LandMovementRate = 12;
                            }
                            break;
                    }
                    switch (FlyMovement)
                    {
                        case MovementRates.Slow:
                            FlyMovementRate = 12;
                            break;
                        case MovementRates.Average:
                            FlyMovementRate = 18;
                            break;
                        case MovementRates.Fast:
                            FlyMovementRate = 24;
                            break;
                    }
                    break;
                case Sizes.Huge:
                    switch (LandMovement)
                    {
                        case MovementRates.Slow:
                            if (manyLegs)
                            {
                                LandMovementRate = 9;
                            }
                            else
                            {
                                LandMovementRate = 6;
                            }
                            break;
                        case MovementRates.Average:
                            if (manyLegs)
                            {
                                LandMovementRate = 12;
                            }
                            else
                            {
                                LandMovementRate = 9;
                            }
                            break;
                        case MovementRates.Fast:
                            if (manyLegs)
                            {
                                LandMovementRate = 15;
                            }
                            else
                            {
                                LandMovementRate = 12;
                            }
                            break;
                    }
                    switch (FlyMovement)
                    {
                        case MovementRates.Slow:
                            FlyMovementRate = 15;
                            break;
                        case MovementRates.Average:
                            FlyMovementRate = 21;
                            break;
                        case MovementRates.Fast:
                            FlyMovementRate = 27;
                            break;
                    }
                    break;
            }
            switch (SwimMovement)
            {
                case MovementRates.Slow:
                    SwimMovementRate = 6;
                    break;
                case MovementRates.Average:
                    SwimMovementRate = 12;
                    break;
                case MovementRates.Fast:
                    SwimMovementRate = 18;
                    break;
            }
            switch (ClimbMovement)
            {
                case MovementRates.Slow:
                    ClimbMovementRate = 4.5;
                    break;
                case MovementRates.Average:
                    ClimbMovementRate = 6;
                    break;
                case MovementRates.Fast:
                    ClimbMovementRate = 9;
                    break;
            }
            if (LandMovementRate <= 6)
            {
                AddTrait("Slow Speed");
            }
            else if (LandMovementRate >= 9)
            {
                AddTrait("Fast Speed");
            }
        }

        private void GenerateSpeedTraits(MovementRates speed)
        {
            if (speed != MovementRates.Slow)
            {
                if (dice.roll(2) >= 12)
                {
                    AddTrait("Burst of Speed");
                }
            }
            if (speed != MovementRates.Fast)
            {
                if (dice.roll(2) >= 12)
                {
                    AddTrait("Stalwart Movement");
                }
            }
        }

        private MovementRates GenerateComparativeMovement()
        {
            var move = dice.roll(2);
            var result = MovementRates.None;
            switch (EcologicalSubtype)
            {
                case EcologicalSubtypes.Filter:
                case EcologicalSubtypes.Trapper:
                case EcologicalSubtypes.Siren:
                    move -= 2;
                    break;
                case EcologicalSubtypes.Grazer:
                case EcologicalSubtypes.Chaser:
                    move += 2;
                    break;
                case EcologicalSubtypes.Gatherer:
                case EcologicalSubtypes.Eater:
                case EcologicalSubtypes.Killer:
                    move++;
                    break;
            }
            move = move.Clamp(2, 12);
            switch (move)
            {
                case 2:
                case 3:
                case 4:
                    result = MovementRates.Slow;
                    break;
                case 11:
                case 12:
                    result = MovementRates.Fast;
                    break;
                default:
                    result = MovementRates.Average;
                    break;
            }
            return result;
        }

        private void GenerateLimbs()
        {
            var symmetry = dice.roll(2);

            switch (symmetry)
            {
                case 2:
                    Symmetry = Symmetries.Trilateral;
                    LimbPairs = 0;
                    LimbCount = (d3.roll() + 1) * 3;
                    break;
                case 9:
                    Symmetry = Symmetries.Bilateral;
                    LimbPairs = 3;
                    LimbCount = 6;
                    break;
                case 10:
                    Symmetry = Symmetries.Bilateral;
                    LimbPairs = 4;
                    LimbCount = 8;
                    break;
                case 11:
                    Symmetry = Symmetries.Bilateral;
                    LimbPairs = dice.roll(2);
                    LimbCount = LimbPairs * 2;
                    break;
                case 12:
                    Symmetry = Symmetries.Radial;
                    LimbPairs = 0;
                    LimbCount = d3.roll(2) + 1;
                    break;
                default:
                    Symmetry = Symmetries.Bilateral;
                    LimbPairs = 2;
                    LimbCount = 4;
                    break;
            }
            if (Symmetry == Symmetries.Bilateral)
            {
                LimbGroupTypes.Add("Manipulation");
                if (Traits.Contains("Flyer"))
                {
                    LimbGroupTypes.Add("Wings");
                }
                if (LimbGroupTypes.Count < LimbPairs)
                {
                    var number = LimbPairs - LimbGroupTypes.Count;
                    // Need to do remaining limb goups
                    AssignLimbGroups(number);
                }
                if (LimbGroupTypes.Where(s => s.Equals("Manipulation")).Count() > 1)
                {
                    AddTrait("Multiple Limbs");
                }
                if (!LimbGroupTypes.Contains("Legs"))
                {
                    AddTrait("No Land Movement");
                }
            }
            else if (Symmetry == Symmetries.Trilateral)
            {
                var groups = LimbCount / 3;
                LimbGroupTypes.Add("Manipulation");
                if (LimbGroupTypes.Count < groups)
                {
                    var number = groups - LimbGroupTypes.Count;
                    // Need to do remaining limb goups
                    AssignLimbGroups(number);
                }
                if (LimbGroupTypes.Where(s => s.Equals("Manipulation")).Count() > 1)
                {
                    AddTrait("Multiple Limbs");
                }
                if (!LimbGroupTypes.Contains("Legs"))
                {
                    AddTrait("No Land Movement");
                }
            }
        }

        private void AssignLimbGroups(int number)
        {
            for (var i = 0; i < number; i++)
            {
                var limbType = dice.roll(2);
                switch (limbType)
                {
                    case 2:
                    case 3:
                        if (Traits.Contains("Flyer"))
                        {
                            LimbGroupTypes.Add("Wings");
                        }
                        else if (Traits.Contains("Natural Swimmer"))
                        {
                            LimbGroupTypes.Add("Fins");
                        }
                        else
                        {
                            LimbGroupTypes.Add("Legs");
                        }
                        break;
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        if (Traits.Contains("Natural Swimmer"))
                        {
                            LimbGroupTypes.Add("Fins");
                        }
                        else
                        {
                            if (Traits.Contains("No Land Movement"))
                            {
                                LimbGroupTypes.Add("Wings");
                            }
                            else
                            {
                                LimbGroupTypes.Add("Legs");
                            }
                        }
                        break;
                    case 12:
                        LimbGroupTypes.Add("Dual-purpose");
                        break;
                    default:
                        LimbGroupTypes.Add("Manipulation");
                        break;
                }
            }
        }

        private void GenerateLocomotion(Planet homeworld)
        {
            if (homeworld.Normal.Hydro.Value != 10)
            {
                var locomotion = dice.roll(2);
                if (homeworld.Normal.Size.Value <= 3)
                {
                    locomotion += 2;
                }
                else if (homeworld.Normal.Size.Value == 4 || homeworld.Normal.Size.Value == 5)
                {
                    locomotion++;
                }
                else if (homeworld.Normal.Size.Value >= 9)
                {
                    locomotion--;
                }
                if (homeworld.Normal.Atmosphere.Value >= 8)
                {
                    locomotion++;
                }
                else if (homeworld.Normal.Atmosphere.Value <= 5)
                {
                    locomotion--;
                }
                if (homeworld.Normal.Hydro.Value >= 6)
                {
                    locomotion--;
                }
                locomotion = locomotion.Clamp(2, 12);
                switch (locomotion)
                {
                    case 2:
                        if (homeworld.Normal.Hydro.Value > 0 && homeworld.Normal.Size.Value > 0)
                        {
                            AddTrait("Natural Swimmer");
                            AddTrait("No Land Movement");
                            if (!Traits.Contains("Aquatic"))
                            {
                                AddTrait("Deep Diver");
                            }
                        }
                        break;
                    case 3:
                        if (homeworld.Normal.Hydro.Value > 0 && homeworld.Normal.Size.Value > 0)
                        {
                            AddTrait("Natural Swimmer");
                            if (!Traits.Contains("Aquatic"))
                            {
                                AddTrait("Deep Diver");
                            }
                            AddTrait("Amphibious");
                            var res = dice.roll(2);
                            if (res >= 6 && res < 10)
                            {
                                AddTrait("Water Dependent");
                            }
                        }
                        break;
                    case 10:
                        AddTrait("Natural Climber");
                        break;
                    case 11:
                        if (homeworld.Normal.Atmosphere.Value >= 4)
                        {
                            AddTrait("Flyer");
                        }
                        break;
                    case 12:
                        if (homeworld.Normal.Atmosphere.Value >= 4)
                        {
                            AddTrait("Flyer");
                            AddTrait("No Land Movement");
                        }
                        break;
                }
            }
        }

        private void GenerateSize(Planet homeworld)
        {
            var size = dice.roll(2);
            if (homeworld.Normal.Size.Value >= 8)
            {
                size--;
            }
            else if (homeworld.Normal.Size.Value <= 4)
            {
                size++;
            }
            if (Traits.Contains("Flyer"))
            {
                size -= 2;
            }
            if (Traits.Contains("Natural Swimmer"))
            {
                size += 2;
            }
            size = size.Clamp(2, 14);
            switch (size)
            {
                case 2:
                    Size = Sizes.Tiny;
                    STR.dice = 1;
                    END.dice = 1;
                    DEX.dice = 3;
                    AttackDM = -1;
                    break;
                case 3:
                case 4:
                    Size = Sizes.Small;
                    STR.dice = 1;
                    END.dice = 1;
                    DEX.dice = 3;
                    AttackDM = 0;
                    break;
                case 11:
                case 12:
                case 13:
                    Size = Sizes.Large;
                    STR.dice = 3;
                    END.dice = 3;
                    DEX.dice = 1;
                    AttackDM = 0;
                    Traits.Add("Increased Life Support");
                    break;
                case 14:
                    Size = Sizes.Huge;
                    STR.dice = 4;
                    END.dice = 4;
                    DEX.dice = 1;
                    AttackDM = 1;
                    Traits.Add("Increased Life Support");
                    break;
                default:
                    Size = Sizes.Medium;
                    STR.dice = 2;
                    END.dice = 2;
                    DEX.dice = 2;
                    AttackDM = 0;
                    break;
            }
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
