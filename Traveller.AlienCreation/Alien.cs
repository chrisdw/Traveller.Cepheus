using org.DownesWard.Traveller.Shared;
using org.DownesWard.Traveller.SystemGeneration;
using org.DownesWard.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace org.DownesWard.Traveller.AlienCreation
{
    public class Alien
    {
        private Dice dice = new Dice(6);
        private Dice d3 = new Dice(3);
        private Dice d36 = new Dice(36);

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
        public List<string> Weapons { get; private set; } = new List<string>();
        public bool PsionicsAllowed { get; set; }
        public int StartingAge { get; private set; }
        public int AgingBegins { get; private set; }
        public int AgingModifier { get; private set; }
        public int BaseHeight { get; private set; }
        public string HeightModifier { get; private set; }
        public int BaseWeight { get; private set; }
        public string WeightModifier { get; private set; }

        private readonly int[,] BaseHeights = new int[5, 11]
        {
            { 28, 30, 32, 36, 38, 40, 42, 44, 48, 50, 52 },
            { 55, 60, 65, 70, 75, 80, 85, 90, 100, 105, 110 },
            { 110, 120, 125, 130, 140, 145, 150, 160, 165, 170, 180 },
            { 220, 230, 245, 260, 275, 290, 305, 320, 335, 350, 360 },
            { 440, 460, 490, 520, 550, 580, 610, 640, 670, 700, 720 }
        };

        private readonly int[,] BaseWeights = new int[5, 11]
        {
            { 0, 0, 0, 1, 1, 1, 1, 1, 2, 2, 2 },
            { 6, 8, 10, 10, 12, 12, 14, 14, 16, 18, 20 },
            { 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80 },
            { 120, 140, 160, 180, 200, 220, 240, 260, 280, 300, 320 },
            { 550, 650, 750, 850, 950, 1050, 1150, 1250, 1350, 1450, 1550 }
        };

        private readonly string[,] WeightModifiers = new string[5, 11]
        {
            { "1D6", "1D6", "1D6", "1D6", "1D6", "1D6", "1D6", "1D6", "1D6", "1D6", "1D6" },
            { "2D6", "2D6", "2D6", "2D6", "2D6", "2D6", "2D6", "2D6", "2D6 (x2)", "2D6 (x2)", "2D6 (x2)" },
            { "2D6 (x4)", "2D6 (x4)", "2D6 (x4)", "2D6 (x5)", "2D6 (x5)", "2D6 (x5)", "2D6 (x5)", "2D6 (x5)", "2D6 (x6)", "2D6 (x6)", "2D6 (x6)" },
            { "4D6 (x4)", "4D6 (x4)", "4D6 (x4)", "4D6 (x5)", "4D6 (x5)", "4D6 (x5)", "4D6 (x5)", "4D6 (x5)", "4D6 (x6)", "4D6 (x6)", "4D6 (x6)" },
            { "4D6 (x8)", "4D6 (x8)", "4D6 (x8)", "4D6 (x10)", "4D6 (x10)", "4D6 (x10)", "4D6 (x5)", "4D6 (x10)", "4D6 (x15)", "4D6 (x15)", "4D6 (x15)"}
        };

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
            GenerateAttributes(homeworld);
            GenerateArmour();
            GenerateWeapons();
            GenerateVision();
            GenerateHearing();
            GenerateScent();
            GenerateSpecialSenses();
            GenerateTraits();
            GenerateAgingProfile();
            GenerateHeight(homeworld);
            GenerateWeight(homeworld);
        }

        private void GenerateWeight(Planet homeworld)
        {
            var result = dice.roll(2) - 2;
            if (homeworld.Normal.Size.Value == 0)
            {
                result += 3;
            }
            else if (homeworld.Normal.Size.Value >= 1 && homeworld.Normal.Size.Value <= 3)
            {
                result += 2;
            }
            else if (homeworld.Normal.Size.Value >= 4 && homeworld.Normal.Size.Value <= 6)
            {
                result += 1;
            }
            else if (homeworld.Normal.Size.Value >= 10)
            {
                result -= 3;
            }
            result = result.Clamp(0, 10);
            switch (Size)
            {
                case Sizes.Tiny:
                    BaseWeight = BaseWeights[0, result];
                    break;
                case Sizes.Small:
                    BaseWeight = BaseWeights[1, result];
                    break;
                case Sizes.Medium:
                    BaseWeight = BaseWeights[2, result];
                    break;
                case Sizes.Large:
                    BaseWeight = BaseWeights[3, result];
                    break;
                case Sizes.Huge:
                    BaseWeight = BaseWeights[4, result];
                    break;
            }
            result = dice.roll(2) - 2;
            switch (Size)
            {
                case Sizes.Tiny:
                    WeightModifier = WeightModifiers[0, result];
                    break;
                case Sizes.Small:
                    WeightModifier = WeightModifiers[1, result];
                    break;
                case Sizes.Medium:
                    WeightModifier = WeightModifiers[2, result];
                    break;
                case Sizes.Large:
                    WeightModifier = WeightModifiers[3, result];
                    break;
                case Sizes.Huge:
                    WeightModifier = WeightModifiers[4, result];
                    break;
            }
        }

        private void GenerateHeight(Planet homeworld)
        {
            var result = dice.roll(2) - 2;

            if (homeworld.Normal.Size.Value == 0)
            {
                result += 3;
            }
            else if (homeworld.Normal.Size.Value >= 1 && homeworld.Normal.Size.Value <= 3)
            {
                result += 2;
            }
            else if (homeworld.Normal.Size.Value >= 4 && homeworld.Normal.Size.Value <= 6)
            {
                result += 1;
            }
            else if (homeworld.Normal.Size.Value >= 10)
            {
                result -= 3;
            }
            result = result.Clamp(0, 10);
            switch (Size)
            {
                case Sizes.Tiny:
                    BaseHeight = BaseHeights[0, result];
                    break;
                case Sizes.Small:
                    BaseHeight = BaseHeights[1, result];
                    break;
                case Sizes.Medium:
                    BaseHeight = BaseHeights[2, result];
                    break;
                case Sizes.Large:
                    BaseHeight = BaseHeights[3, result];
                    break;
                case Sizes.Huge:
                    BaseHeight = BaseHeights[4, result];
                    break;
            }
            switch (Size)
            {
                case Sizes.Tiny:
                    HeightModifier = "1D6";
                    break;
                case Sizes.Small:
                    HeightModifier = "2D6 (x2)";
                    break;
                case Sizes.Medium:
                    HeightModifier = "2D6 (x5)";
                    break;
                case Sizes.Large:
                    HeightModifier = "4D6 (x5)";
                    break;
                case Sizes.Huge:
                    HeightModifier = "4D6 (x10)";
                    break;
            }
        }

        private void GenerateAgingProfile()
        {
            switch (dice.roll(2))
            {
                case 2:
                case 3:
                    StartingAge = 10;
                    break;
                case 4:
                case 5:
                    StartingAge = 14;
                    break;
                case 6:
                case 7:
                case 8:
                    StartingAge = 18;
                    break;
                case 9:
                case 10:
                    StartingAge = 22;
                    break;
                case 11:
                    StartingAge = 26;
                    break;
                case 12:
                    StartingAge = 30;
                    break;
            }
            switch (dice.roll(2))
            {
                case 2:
                case 3:
                    AgingBegins = StartingAge + 8;
                    break;
                case 4:
                case 5:
                    AgingBegins = StartingAge + 12;
                    break;
                case 6:
                case 7:
                    AgingBegins = StartingAge + 16;
                    break;
                case 8:
                case 9:
                    AgingBegins = StartingAge + 20;
                    break;
                case 10:
                    AgingBegins = StartingAge + 24;
                    break;
                case 11:
                case 12:
                    AgingBegins = StartingAge + 28;
                    break;
            }
            switch (dice.roll(2))
            {
                case 2:
                    AgingModifier = -2;
                    break;
                case 3:
                case 4:
                    AgingModifier = -1;
                    break;
                case 10:
                case 11:
                    AgingModifier = 1;
                    break;
                case 12:
                    AgingModifier = 2;
                    break;
                default:
                    AgingModifier = 0;
                    break;
            }
        }

        private void GenerateTraits()
        {
            if (dice.roll() <= 4)
            {
                GeneratePhysicalTraits();
            }
            else
            {
                GenerateCulturalTraits();
            }
        }

        private void GenerateCulturalTraits()
        {
            // Cultural
            switch (d36.roll())
            {
                case 1:
                    AddTrait(Resources.Trait_Alertness);
                    break;
                case 2:
                    AddTrait(Resources.Trait_Athletic);
                    break;
                case 3:
                    AddTrait(Resources.Trait_BadFirstImpression);
                    break;
                case 4:
                    AddTrait(Resources.Trait_ClosedBook);
                    break;
                case 5:
                    AddTrait(Resources.Trait_EideticMemory);
                    break;
                case 6:
                    AddTrait(Resources.Trait_FastTalker);
                    break;
                case 7:
                    AddTrait(Resources.Trait_Gearhead);
                    break;
                case 8:
                    AddTrait(Resources.Trait_GoodFirstImpression);
                    break;
                case 9:
                    AddTrait(Resources.Trait_Haggler);
                    break;
                case 10:
                    AddTrait(Resources.Trait_Interrogator);
                    break;
                case 11:
                case 12:
                case 13:
                    AddTrait(Resources.Trait_Intolerant);
                    break;
                case 14:
                    AddTrait(Resources.Trait_NaturalAdvocate);
                    break;
                case 15:
                    AddTrait(Resources.Trait_NaturalBornLeader);
                    break;
                case 16:
                    AddTrait(Resources.Trait_NaturalCompass);
                    break;
                case 17:
                    AddTrait(Resources.Trait_NaturalPilot);
                    break;
                case 18:
                case 19:
                    AddTrait(Resources.Trait_NaturalSurvivalist);
                    break;
                case 20:
                    AddTrait(Resources.Trait_NaturalThief);
                    break;
                case 21:
                    AddTrait(Resources.Trait_NaturallyHonest);
                    break;
                case 22:
                    AddTrait(Resources.Trait_OverlyAggressive);
                    break;
                case 23:
                    if (PsionicsAllowed)
                    {
                        AddTrait(Resources.Trait_Psionic);
                    }
                    break;
                case 24:
                    AddTrait(Resources.Trait_RacialDefence);
                    break;
                case 25:
                    AddTrait(Resources.Trait_RacialEnemy);
                    break;
                case 26:
                case 27:
                case 28:
                    AddTrait(Resources.Trait_RacialPhobia);
                    break;
                case 29:
                    AddTrait(Resources.Trait_RacialWeapon);
                    break;
                case 30:
                case 31:
                    AddTrait(Resources.Trait_Stealthy);
                    break;
                case 32:
                    AddTrait(Resources.Trait_Trustworthy);
                    break;
                case 33:
                case 34:
                    AddTrait(Resources.Trait_WellTravelled);
                    break;
                case 35:
                    AddTrait(Resources.Trait_XenoEmpathy);
                    break;
                case 36:
                    AddTrait(Resources.Trait_RefereesChoice);
                    break;
            }
        }

        private void GeneratePhysicalTraits()
        {
            // Physical
            switch (d36.roll())
            {
                case 1:
                    AddTrait(Resources.Trait_AcidResistance);
                    break;
                case 2:
                    AddTrait(Resources.Trait_AcidVulnerability);
                    break;
                case 3:
                    AddTrait(Resources.Trait_AltitudeAdaption);
                    break;
                case 4:
                    if (PsionicsAllowed)
                    {
                        AddTrait(Resources.Trait_AntiPsionic);
                    }
                    break;
                case 5:
                    AddTrait(Resources.Trait_BadFirstImpression);
                    break;
                case 6:
                    AddTrait(Resources.Trait_BlindFighter);
                    break;
                case 7:
                    AddTrait(Resources.Trait_ColdEndurance);
                    break;
                case 8:
                    AddTrait(Resources.Trait_ColdResistance);
                    AddTrait(Resources.Trait_FireVulnerability);
                    break;
                case 9:
                    AddTrait(Resources.Trait_ElectricityResistance);
                    break;
                case 10:
                    AddTrait(Resources.Trait_Engineered);
                    break;
                case 11:
                    AddTrait(Resources.Trait_FastHealing);
                    break;
                case 12:
                    AddTrait(Resources.Trait_FireResistance);
                    AddTrait(Resources.Trait_ColdVulnerability);
                    break;
                case 13:
                    AddTrait(Resources.Trait_FrightfulPresence);
                    break;
                case 14:
                    AddTrait(Resources.Trait_GoodFirstImpression);
                    break;
                case 15:
                    AddTrait(Resources.Trait_HeatEndurance);
                    break;
                case 16:
                    AddTrait(Resources.Trait_Hibernation);
                    break;
                case 17:
                    AddTrait(Resources.Trait_ImprovedGrab);
                    break;
                case 18:
                    AddTrait(Resources.Trait_ImprovedGrabConstrict);
                    break;
                case 19:
                    AddTrait(Resources.Trait_ImprovedGrabEntangle);
                    break;
                case 20:
                    AddTrait(Resources.Trait_ImprovedReach);
                    break;
                case 21:
                    AddTrait(Resources.Trait_NoFineManipulators);
                    break;
                case 22:
                    AddTrait(Resources.Trait_PleasantOdor);
                    break;
                case 23:
                    if (PsionicsAllowed)
                    {
                        AddTrait(Resources.Trait_Psionic);
                    }
                    break;
                case 24:
                    AddTrait(Resources.Trait_RadiatonResistance);
                    break;
                case 25:
                    AddTrait(Resources.Trait_Regeneration);
                    break;
                case 26:
                    AddTrait(Resources.Trait_ResistantToDisease);
                    break;
                case 27:
                    AddTrait(Resources.Trait_ResistantToFear);
                    break;
                case 28:
                    AddTrait(Resources.Trait_ResistantToPoisons);
                    break;
                case 29:
                    if (PsionicsAllowed)
                    {
                        AddTrait(Resources.Trait_ResistantToPsionics);
                    }
                    break;
                case 30:
                    AddTrait(Resources.Trait_SpittingAttack);
                    break;
                case 31:
                    AddTrait(Resources.Trait_UnusualHandStructure);
                    break;
                case 32:
                    AddTrait(Resources.Trait_UnusualLifeSupportRequirementsMajor);
                    break;
                case 33:
                    AddTrait(Resources.Trait_UnusualLifeSupportRequirementsMinor);
                    break;
                case 34:
                    AddTrait(Resources.Trait_UnusualSleepCycle);
                    break;
                case 35:
                    AddTrait(Resources.Trait_Uplifited);
                    break;
                case 36:
                    AddTrait(Resources.Trait_RefereesChoice);
                    break;
            }
        }

        private void GenerateSpecialSenses()
        {
            if (dice.roll(2) >= 12)
            {
                switch (dice.roll(2))
                {
                    case 2:
                        AddTrait(Resources.Sense_OrganicRadioCommunications);
                        break;
                    case 3:
                    case 4:
                        AddTrait(Resources.Sense_VibrationSense);
                        break;
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                        AddTrait(Resources.Trait_Alertness);
                        break;
                    case 9:
                    case 10:
                        AddTrait(Resources.Trait_BlindFighter);
                        break;
                    case 11:
                        if (PsionicsAllowed)
                        {
                            AddTrait(Resources.Sense_TelepathyLimited);
                        }
                        else
                        {
                            AddTrait(Resources.Trait_NaturalCompass);
                        }
                        break;
                    case 12:
                        if (PsionicsAllowed)
                        {
                            AddTrait(Resources.Sense_Telepathy);
                        }
                        else
                        {
                            AddTrait(Resources.Trait_BlindFighter);
                        }
                        break;
                }
            }
        }

        private void GenerateScent()
        {
            if (dice.roll(2) >= 10)
            {
                switch (dice.roll(2))
                {
                    case 2:
                        AddTrait(Resources.Scent_Anosmic);
                        break;
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        AddTrait(Resources.Scent_PoorScent);
                        break;
                    default:
                        AddTrait(Resources.Scent_Scent);
                        break;
                }
            }
        }

        private void GenerateHearing()
        {
            if (dice.roll(2) >= 9)
            {
                switch (dice.roll(2))
                {
                    case 2:
                        AddTrait(Resources.Hearing_Deaf);
                        AddTrait(Resources.Trait_NoVocalCords);
                        break;
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        AddTrait(Resources.Hearing_PoorHearing);
                        break;
                    default:
                        AddTrait(Resources.Hearing_AcuteHearing);
                        break;
                }
            }
        }

        private void GenerateVision()
        {
            if (dice.roll(2) >= 8)
            {
                switch (dice.roll(2))
                {
                    case 2:
                        AddTrait(Resources.Vision_Blind);
                        AddTrait(Resources.Vision_Blindsight);
                        AddTrait(Resources.Trait_BlindFighter);
                        break;
                    case 3:
                        AddTrait(Resources.Vision_LightBlindness);
                        break;
                    case 4:
                        AddTrait(Resources.Vision_LightSensitivity);
                        break;
                    case 5:
                        AddTrait(Resources.Vision_ColourBlindness);
                        break;
                    case 6:
                        AddTrait(Resources.Vision_PoorVision);
                        AddTrait(Resources.Vision_ColourBlindness);
                        break;
                    case 7:
                        AddTrait(Resources.Vision_PoorVision);
                        break;
                    case 8:
                        AddTrait(Resources.Vision_LowLightVision);
                        break;
                    case 9:
                        AddTrait(Resources.Vision_Darkvision);
                        AddTrait(Resources.Vision_LowLightVision);
                        break;
                    case 10:
                    case 11:
                        AddTrait(Resources.Vision_AcuteVision);
                        break;
                    case 12:
                        AddTrait(Resources.Vision_AcuteVision);
                        AddTrait(Resources.Vision_Blindsight);
                        break;
                }
            }
        }

        private void GenerateWeapons()
        {
            if (!Traits.Contains(Resources.Trait_Fragile))
            {
                var result = dice.roll(2);
                if (EcologicalType == EcologicalTypes.Carnivore)
                {
                    result += 3;
                }
                else if (EcologicalType == EcologicalTypes.Scavenger)
                {
                    result--;
                }
                else if (EcologicalType == EcologicalTypes.Herbivore)
                {
                    result -= 2;
                }
                if (result >= 9)
                {
                    AddTrait(Resources.Trait_NaturalWeapons);
                    result = dice.roll(2);
                    switch (result)
                    {
                        case 2:
                        case 6:
                        case 12:
                            Weapons.Add(Resources.Weapon_Teeth);
                            if (dice.roll(2) >= 10)
                            {
                                AddTrait(Resources.Trait_Poisonous);
                            }
                            break;
                        case 3:
                            Weapons.Add(Resources.Weapon_Horns);
                            break;
                        case 4:
                            Weapons.Add(Resources.Weapon_Hooves);
                            break;
                        case 5:
                            Weapons.Add(Resources.Weapon_Teeth);
                            if (dice.roll(2) >= 10)
                            {
                                AddTrait(Resources.Trait_Poisonous);
                            }
                            Weapons.Add(Resources.Weapon_Hooves);
                            break;
                        case 7:
                        case 11:
                            Weapons.Add(Resources.Weapon_Claws);
                            break;
                        case 8:
                            Weapons.Add(Resources.Weapon_Stinger);
                            AddTrait(Resources.Trait_Poisonous);
                            break;
                        case 9:
                            Weapons.Add(Resources.Weapon_Thrasher);
                            break;
                        case 10:
                            Weapons.Add(Resources.Weapon_Claws);
                            Weapons.Add(Resources.Weapon_Teeth);
                            if (dice.roll(2) >= 10)
                            {
                                AddTrait(Resources.Trait_Poisonous);
                            }
                            break;
                    }
                }
            }
        }

        private void GenerateArmour()
        {
            var result = dice.roll(2);
            if (Traits.Contains(Resources.Trait_Flyer))
            {
                result += 4;
            }
            if (result >= 12)
            {
                AddTrait(Resources.Trait_Fragile);
            }
            else
            {
                result = dice.roll(2);
                if (EcologicalType == EcologicalTypes.Carnivore)
                {
                    result--;
                }
                else if (EcologicalType == EcologicalTypes.Scavenger)
                {
                    result++;
                }
                else if (EcologicalType == EcologicalTypes.Herbivore)
                {
                    result += 2;
                }
                if (Size == Sizes.Tiny)
                {
                    result -= 2;
                }
                else if (Size == Sizes.Small)
                {
                    result -= 1;
                }
                else if (Size == Sizes.Large)
                {
                    result += 4;
                }
                else if (Size == Sizes.Huge)
                {
                    result += 8;
                }
                if (result >= 11)
                {
                    AddTrait(Resources.Trait_Armoured);
                }
            }
        }

        private void GenerateAttributes(Planet homeworld)
        {
            GenetateSTR(homeworld, out int strchange);
            GenerateDEX(homeworld, strchange, out int dexchange);
            GenerateEND(homeworld, strchange, dexchange, out int endchange);
            GenerateEDU(homeworld, strchange, dexchange, endchange, out int educhange);
            GenerateSOC(homeworld, strchange, dexchange, endchange, educhange, out int socchange);
            GenerateINT(strchange, dexchange, endchange, educhange, socchange, out int intchange);
        }

        private void GenerateINT(int strchange, int dexchange, int endchange, int educhange, int socchange, out int intchange)
        {
            var result = dice.roll(2);
            if (EcologicalType == EcologicalTypes.Herbivore)
            {
                result--;
            }
            else if (EcologicalType == EcologicalTypes.Carnivore)
            {
                result++;
            }
            if (result <= 2)
            {
                AddTrait(Resources.Trait_Caste);
            }
            else if (result >= 12)
            {
                AddTrait(Resources.Trait_Charisma);
            }

            result = dice.roll(2);
            result += (strchange * 2);
            result += (dexchange * 2);
            result += (endchange * 2);
            result += (educhange * 2);
            result += (socchange * 2);
            INT = Attribute.Score(2, result, 9, out intchange);
        }

        private void GenerateSOC(Planet homeworld, int strchange, int dexchange, int endchange, int educhange, out int socchange)
        {
            var result = dice.roll(2);
            if (homeworld.Normal.Starport == 'A')
            {
                result += 2;
            }
            else if (homeworld.Normal.Starport == 'E')
            {
                result -= 2;
            }
            else if (homeworld.Normal.Starport == 'X')
            {
                result -= 3;
            }
            if (homeworld.Normal.TechLevel.Value <= 5)
            {
                result -= 5;
            }
            else if (homeworld.Normal.TechLevel.Value <= 8)
            {
                result -= 3;
            }
            if (homeworld.Normal.TechLevel.Value >= 12)
            {
                result += 2;
            }
            result += (strchange * 2);
            result += (dexchange * 2);
            result += (endchange * 2);
            result += (educhange * 2);
            SOC = Attribute.Score(2, result, 6, out socchange);
        }

        private void GenerateEDU(Planet homeworld, int strchange, int dexchange, int endchange, out int educhange)
        {
            var result = dice.roll(2);
            if (homeworld.Normal.Starport == 'A')
            {
                result += 2;
            }
            else if (homeworld.Normal.Starport == 'E')
            {
                result -= 2;
            }
            else if (homeworld.Normal.Starport == 'X')
            {
                result -= 3;
            }
            result += (strchange * 2);
            result += (dexchange * 2);
            result += (endchange * 2);
            var feralcheck = dice.roll(2);
            if (homeworld.Normal.Starport == 'E' || homeworld.Normal.Starport == 'X')
            {
                feralcheck += 2;
            }
            if (feralcheck >= 12)
            {
                AddTrait(Resources.Trait_Feral);
                EDU = Attribute.Score(1, result, 9, out educhange);
            }
            else
            {
                EDU = Attribute.Score(2, result, 9, out educhange);
            }
        }

        private void GenerateEND(Planet homeworld, int strchange, int dexchange, out int endchange)
        {
            var result = dice.roll(2);
            if (homeworld.Normal.Size.Value == 0)
            {
                result -= 5;
            }
            else if (homeworld.Normal.Size.Value >= 1 && homeworld.Normal.Size.Value <= 3)
            {
                result -= 3;
            }
            else if (homeworld.Normal.Size.Value >= 10)
            {
                result += 3;
            }
            result += (strchange * 2);
            result += (dexchange * 2);
            END = Attribute.Score(END.Dice, result, 14, out endchange);
        }

        private void GenerateDEX(Planet homeworld, int strchange, out int dexchange)
        {
            var result = dice.roll(2);
            if (homeworld.Normal.Size.Value == 0)
            {
                result += 7;
            }
            else if (homeworld.Normal.Size.Value >= 1 && homeworld.Normal.Size.Value <= 3)
            {
                result += 5;
            }
            else if (homeworld.Normal.Size.Value >= 4 && homeworld.Normal.Size.Value <= 6)
            {
                result += 3;
            }
            else if (homeworld.Normal.Size.Value >= 10)
            {
                result -= 3;
            }
            if (EcologicalType == EcologicalTypes.Carnivore)
            {
                result += 3;
            }
            else if (EcologicalType == EcologicalTypes.Herbivore)
            {
                result -= 5;
            }
            result += (strchange * 2);
            DEX = Attribute.Score(DEX.Dice, result, 14, out dexchange);
        }

        private void GenetateSTR(Planet homeworld, out int strchange)
        {
            var result = dice.roll(2);
            if (homeworld.Normal.Size.Value == 0)
            {
                result -= 7;
            }
            else if (homeworld.Normal.Size.Value >= 1 && homeworld.Normal.Size.Value <= 3)
            {
                result -= 5;
            }
            else if (homeworld.Normal.Size.Value >= 4 && homeworld.Normal.Size.Value <= 6)
            {
                result -= 3;
            }
            else if (homeworld.Normal.Size.Value >= 10)
            {
                result += 3;
            }
            if (EcologicalType == EcologicalTypes.Carnivore)
            {
                result += 3;
            }
            else if (EcologicalType == EcologicalTypes.Herbivore)
            {
                result += 3;
            }
            STR = Attribute.Score(STR.Dice, result, 14, out strchange);
        }

        public UPP Generate()
        {
            var upp = new UPP();
            upp.Str.Value = STR.Generate();
            upp.Dex.Value = DEX.Generate();
            upp.End.Value = END.Generate();
            upp.Int.Value = INT.Generate();
            upp.Edu.Value = EDU.Generate();
            upp.Soc.Value = SOC.Generate();
            return upp;
        }

        private void GenerateMovement()
        {
            if (!Traits.Contains(Resources.Trait_NoLandMovement))
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
                    AddTrait(Resources.Trait_Stable);
                }
            }
            if (Traits.Contains(Resources.Trait_Flyer))
            {
                if (LimbGroupTypes.Contains(Resources.Limb_Wings))
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
            if (Traits.Contains(Resources.Trait_NaturalSwimmer))
            {
                if (LimbGroupTypes.Contains(Resources.Limb_Fins))
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
            if (Traits.Contains(Resources.Trait_NaturalClimber))
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
                manyLegs = (LimbGroupTypes.Where(s => s.Equals(Resources.Limb_Legs) || s.Equals(Resources.Limb_DualPurpose)).Count() * 2) >= 4;
            }
            else
            {
                manyLegs = (LimbGroupTypes.Where(s => s.Equals(Resources.Limb_Legs) || s.Equals(Resources.Limb_DualPurpose)).Count() * 3) >= 4;
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
                AddTrait(Resources.Trait_SlowSpeed);
            }
            else if (LandMovementRate >= 9)
            {
                AddTrait(Resources.Trait_FastSpeed);
            }
        }

        private void GenerateSpeedTraits(MovementRates speed)
        {
            if (speed != MovementRates.Slow)
            {
                if (dice.roll(2) >= 12)
                {
                    AddTrait(Resources.Trait_BurstOfSpeed);
                }
            }
            if (speed != MovementRates.Fast)
            {
                if (dice.roll(2) >= 12)
                {
                    AddTrait(Resources.Trait_StalwartMovement);
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
                LimbGroupTypes.Add(Resources.Limb_Manipulation);
                if (Traits.Contains(Resources.Trait_Flyer))
                {
                    LimbGroupTypes.Add(Resources.Limb_Wings);
                }
                if (LimbGroupTypes.Count < LimbPairs)
                {
                    var number = LimbPairs - LimbGroupTypes.Count;
                    // Need to do remaining limb goups
                    AssignLimbGroups(number);
                }
                if (LimbGroupTypes.Where(s => s.Equals(Resources.Limb_Manipulation) || s.Equals(Resources.Limb_DualPurpose)).Count() > 1)
                {
                    AddTrait(Resources.Trait_MultipleLimbs);
                }
                if (LimbGroupTypes.Where(s => s.Equals(Resources.Limb_Legs) || s.Equals(Resources.Limb_DualPurpose)).Count() == 0)
                {
                    AddTrait(Resources.Trait_NoLandMovement);
                }
            }
            else if (Symmetry == Symmetries.Trilateral)
            {
                var groups = LimbCount / 3;
                LimbGroupTypes.Add(Resources.Limb_Manipulation);
                if (LimbGroupTypes.Count < groups)
                {
                    var number = groups - LimbGroupTypes.Count;
                    // Need to do remaining limb goups
                    AssignLimbGroups(number);
                }
                if (LimbGroupTypes.Where(s => s.Equals(Resources.Limb_Manipulation) || s.Equals(Resources.Limb_DualPurpose)).Count() > 1)
                {
                    AddTrait(Resources.Trait_MultipleLimbs);
                }
                if (LimbGroupTypes.Where(s => s.Equals(Resources.Limb_Legs) || s.Equals(Resources.Limb_DualPurpose)).Count() == 0)
                {
                    AddTrait(Resources.Trait_NoLandMovement);
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
                        if (Traits.Contains(Resources.Trait_Flyer))
                        {
                            LimbGroupTypes.Add(Resources.Limb_Wings);
                        }
                        else if (Traits.Contains(Resources.Trait_NaturalSwimmer))
                        {
                            LimbGroupTypes.Add(Resources.Limb_Fins);
                        }
                        else
                        {
                            LimbGroupTypes.Add(Resources.Limb_Legs);
                        }
                        break;
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        if (Traits.Contains(Resources.Trait_NaturalSwimmer))
                        {
                            LimbGroupTypes.Add(Resources.Limb_Fins);
                        }
                        else
                        {
                            if (Traits.Contains(Resources.Trait_NoLandMovement))
                            {
                                LimbGroupTypes.Add(Resources.Limb_Wings);
                            }
                            else
                            {
                                LimbGroupTypes.Add(Resources.Limb_Legs);
                            }
                        }
                        break;
                    case 12:
                        LimbGroupTypes.Add(Resources.Limb_DualPurpose);
                        break;
                    default:
                        LimbGroupTypes.Add(Resources.Limb_Manipulation);
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
                            AddTrait(Resources.Trait_NaturalSwimmer);
                            AddTrait(Resources.Trait_NoLandMovement);
                            if (!Traits.Contains(Resources.Trait_Aquatic))
                            {
                                AddTrait(Resources.Trait_DeepDiver);
                            }
                        }
                        break;
                    case 3:
                        if (homeworld.Normal.Hydro.Value > 0 && homeworld.Normal.Size.Value > 0)
                        {
                            AddTrait(Resources.Trait_NaturalSwimmer);
                            if (!Traits.Contains(Resources.Trait_Aquatic))
                            {
                                AddTrait(Resources.Trait_DeepDiver);
                            }
                            AddTrait(Resources.Trait_Amphibious);
                            var res = dice.roll(2);
                            if (res >= 6 && res < 10)
                            {
                                AddTrait(Resources.Trait_WaterDependent);
                            }
                        }
                        break;
                    case 10:
                        AddTrait(Resources.Trait_NaturalClimber);
                        break;
                    case 11:
                        if (homeworld.Normal.Atmosphere.Value >= 4)
                        {
                            AddTrait(Resources.Trait_Flyer);
                        }
                        break;
                    case 12:
                        if (homeworld.Normal.Atmosphere.Value >= 4)
                        {
                            AddTrait(Resources.Trait_Flyer);
                            AddTrait(Resources.Trait_NoLandMovement);
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
            if (Traits.Contains(Resources.Trait_Flyer))
            {
                size -= 2;
            }
            if (Traits.Contains(Resources.Trait_NaturalSwimmer))
            {
                size += 2;
            }
            size = size.Clamp(2, 14);
            switch (size)
            {
                case 2:
                    Size = Sizes.Tiny;
                    STR.Dice = 1;
                    END.Dice = 1;
                    DEX.Dice = 3;
                    AttackDM = -1;
                    break;
                case 3:
                case 4:
                    Size = Sizes.Small;
                    STR.Dice = 1;
                    END.Dice = 1;
                    DEX.Dice = 3;
                    AttackDM = 0;
                    break;
                case 11:
                case 12:
                case 13:
                    Size = Sizes.Large;
                    STR.Dice = 3;
                    END.Dice = 3;
                    DEX.Dice = 1;
                    AttackDM = 0;
                    Traits.Add(Resources.Trait_IncreasedLifeSupport);
                    break;
                case 14:
                    Size = Sizes.Huge;
                    STR.Dice = 3;
                    END.Dice = 3;
                    DEX.Dice = 1;
                    AttackDM = 1;
                    Traits.Add(Resources.Trait_IncreasedLifeSupport);
                    break;
                default:
                    Size = Sizes.Medium;
                    STR.Dice = 2;
                    END.Dice = 2;
                    DEX.Dice = 2;
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
                AddTrait(Resources.Trait_Aquatic);
                AddTrait(Resources.Trait_NaturalSwimmer);
                AddTrait(Resources.Trait_Amphibious);
                var res = dice.roll(2);
                if (res >= 6 && res < 10)
                {
                    AddTrait(Resources.Trait_WaterDependent);
                }
                else
                {
                    RemoveTrait(Resources.Trait_Amphibious);
                    RemoveTrait(Resources.Trait_WaterDependent);
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
                    AddTrait(Resources.Trait_Gendermorphic);
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
                AddTrait(Resources.Trait_ZeroGravityAdaptation);
                AddTrait(Resources.Trait_GravityIntolerance);
            }
            else if (size.Value >= 1 && size.Value <= 3)
            {
                AddTrait(Resources.Trait_LowGravityAdaptation);
                AddTrait(Resources.Trait_GravityIntolerance);
            }
            else if (size.Value >= 4 && size.Value <= 6)
            {
                AddTrait(Resources.Trait_LowGravityAdaptation);
            }
            else if (size.Value >= 10)
            {
                AddTrait(Resources.Trait_HighGravityAdaptation);
            }
        }

        private void WorldAtmospherEffects(TravCode atmosphere)
        {
            if (atmosphere.Value == 0)
            {
                AddTrait(Resources.Trait_VaccumSurvival);
            }
            else if (atmosphere.Value == 1)
            {
                Traits.Add(Resources.Trait_TraceBreather);
                if (dice.roll(2) >= 11)
                {
                    AddTrait(Resources.Trait_VaccumSurvivalLimited);
                }
            }
            else if (atmosphere.Value == 2 || atmosphere.Value == 3)
            {
                if (dice.roll(2) >= 11)
                {
                    AddTrait(Resources.Trait_TraceBreather);
                    AddTrait(Resources.Trait_VaccumSurvivalLimited);
                }
                else
                {
                    Traits.Add(Resources.Trait_TraceBreatherLimited);
                }
            }
            else if (atmosphere.Value >= 10 && atmosphere.Value == 12)
            {
                AddTrait(Resources.Trait_AtmosphericRequirements);
            }
            if (atmosphere.Value == 2 ||
                atmosphere.Value == 4 ||
                atmosphere.Value == 7 ||
                atmosphere.Value == 9)
            {
                if (dice.roll(2) >= 9)
                {
                    AddTrait(Resources.Trait_TaintImmunity);
                }
                else
                {
                    AddTrait(Resources.Trait_TaintedBreather);
                }
            }
        }

        private void WorldHydrographicsEffects(TravCode hydro)
        {
            if (hydro.Value == 0)
            {
                AddTrait(Resources.Trait_DesertAdaptation);
            }
            else if (hydro.Value == 10)
            {
                AddTrait(Resources.Trait_Aquatic);
                AddTrait(Resources.Trait_NaturalSwimmer);
                if (dice.roll(2) >= 10)
                {
                    AddTrait(Resources.Trait_Amphibious);
                    if (dice.roll(2) >= 6)
                    {
                        AddTrait(Resources.Trait_WaterDependent);
                    }
                }

            }
        }

        private void WorldClimateEffects(Planet homeworld)
        {
            if (homeworld.Temp <= -25)
            {
                AddTrait(Resources.Trait_ColdEndurance);
                AddTrait(Resources.Trait_ColdResistance);
                AddTrait(Resources.Trait_FireVulnerability);
            }
            else if (homeworld.Temp <= 0)
            {
                AddTrait(Resources.Trait_ColdEndurance);
            }
            else if (homeworld.Temp >= 25 && homeworld.Temp < 45)
            {
                AddTrait(Resources.Trait_HeatEndurance);
            }
            else if (homeworld.Temp >= 45)
            {
                AddTrait(Resources.Trait_HeatEndurance);
                AddTrait(Resources.Trait_HeatResistance);
                AddTrait(Resources.Trait_ColdVulnerability);
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

        public void Write(TextWriter tw)
        {
            tw.WriteLine("Niche: {0} ({1})", EcologicalType, EcologicalSubtype);
            tw.WriteLine("STR: {0}", STR);
            tw.WriteLine("DEX: {0}", DEX);
            tw.WriteLine("END: {0}", END);
            tw.WriteLine("INT: {0}", INT);
            tw.WriteLine("EDU: {0}", EDU);
            tw.WriteLine("SOC: {0}", SOC);
            tw.WriteLine("Metabolism {0}", Metabolism);
            tw.WriteLine("Genders {0}:{1}", GenderModel, NumGenders);
            tw.WriteLine("Reproduction: {0}", ReproductionMethod);
            tw.WriteLine("Size: {0}, DM {1:+0;-#}", Size, AttackDM);
            tw.WriteLine("Symmetry: {0}", Symmetry);
            tw.WriteLine("Limbs: {0} ({1} Pairs)", LimbCount, LimbPairs);
            var limbs = LimbGroupTypes.GroupBy(l => l)
                .Select(l => new { Name = l.Key, Count = l.Count() });
            foreach (var lg in limbs)
            {
                tw.WriteLine("{0} x {1}", lg.Name, lg.Count);
            }
            if (LandMovementRate != 0)
            {
                tw.WriteLine("Land Movement: {0}", LandMovementRate);
            }
            if (FlyMovementRate != 0)
            {
                tw.WriteLine("Flying Movement: {0}", FlyMovementRate);
            }
            if (SwimMovementRate != 0)
            {
                tw.WriteLine("Swiming Movement: {0}", SwimMovementRate);
            }
            if (ClimbMovementRate != 0)
            {
                tw.WriteLine("Climbing Movement: {0}", ClimbMovementRate);
            }
            if (Traits.Count > 0)
            {
                tw.WriteLine("Traits: {0}", string.Join(", ", Traits.OrderBy(s => s)));
            }
            if (Weapons.Count > 0)
            {
                tw.WriteLine("Weapons: {0}", string.Join(", ", Weapons.OrderBy(s => s)));
            }
            tw.WriteLine("Matures at {0}, aging begins at {1} DM {2:+0;-#}", StartingAge, AgingBegins, AgingModifier);
            tw.WriteLine("Height {0} + {1}", BaseHeight, HeightModifier);
            tw.WriteLine("Weight {0} + {1}", BaseWeight, WeightModifier);
        }
    }
}
