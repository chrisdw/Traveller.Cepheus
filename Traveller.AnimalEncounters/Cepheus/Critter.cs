﻿using org.DownesWard.Traveller.CharacterGeneration;
using org.DownesWard.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace org.DownesWard.Traveller.AnimalEncounters.Cepheus
{
    public class Critter
    {
        private Dice dice = new Dice(6);

        public string Name { get; set; }

        public EcologicalTypes EcologicalType { get; private set; }
        public EcologicalSubtypes EcologicalSubtype { get; private set; }
        public CritterProfile Profile { get; private set; } = new CritterProfile();
        public Dictionary<string, Skill> Skills { get; } = new Dictionary<string, Skill>();
        public int Weight { get; set; }
        public string NumberAppearing { get; set; }
        public List<string> Weapons { get; set; } = new List<string>();
        public int DamageDice { get; private set; }
        public int Armour { get; private set; }
        public int Move { get; private set; }
        public Motions Motion { get; private set; }
        public Regions Region { get; set; }
        public ArmourTypes ArmourType { get; private set; }

        public static Skill Athletics0 = new Skill("Athletics", Skill.SkillClass.Military, 1);
        public static Skill Recon0 = new Skill("Recon", Skill.SkillClass.Military, 0);
        public static Skill Survival0 = new Skill("Survival", Skill.SkillClass.Military, 0);

        public static Skill Athletics = new Skill("Athletics", Skill.SkillClass.Military, 1);
        public static Skill NaturalWeapons = new Skill("Natural Weapons", Skill.SkillClass.Military, 1);
        public static Skill Recon = new Skill("Recon", Skill.SkillClass.Military, 1);
        public static Skill Survival = new Skill("Survival", Skill.SkillClass.Military, 1);

        public Critter()
        {
            EcologicalType = EcologicalTypes.Event;
        }

        public Critter(EcologicalTypes ecologicalType, int terrainSubTypeDM, int terrainSizeDM, int motionSizeDM, Motions motion)
        {
            Generate(ecologicalType, terrainSubTypeDM, terrainSizeDM, motionSizeDM, motion);
        }

        public void Generate(EcologicalTypes ecologicalType, int terrainSubTypeDM, int terrainSizeDM, int motionSizeDM, Motions motion)
        {
            var pack = 0;
            var endurance = 0;
            var instinct = 0;
            var dexterity = 0;
            var strength = 0;

            EcologicalType = ecologicalType;
            Motion = motion;
            GenerateSubType(terrainSubTypeDM, ref pack, ref endurance, ref instinct, ref dexterity, ref strength);
            var sizeClass = GenerateSize(terrainSubTypeDM, terrainSizeDM, endurance, dexterity, strength);
            GenerateSocialProfile(pack, instinct);
            GeneratePackSize();
            GenerateWeapons();
            GenerateArmour(sizeClass);
            GenerateSkills();
        }

        private void GenerateSocialProfile(int pack, int instinct)
        {
            Profile["PAC"].Value = dice.roll(2) + pack;
            Profile["INS"].Value = dice.roll(2) + instinct;
            if (Move == 0)
            {
                Profile["INT"].Value = 0;
            }
            else
            {
                Profile["INT"].Value = 1;
            }
        }

        public void Write(TextWriter tw)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                tw.WriteLine("Name: {0}", Name);
            }
            tw.Write("{0:N0}kg ", Weight);
            tw.Write("{0} ({1}), ", EcologicalSubTypeLong, EcologicalTypeShort);
            tw.Write("{0} {1}, ", Terrain.TerrainName(Region), Motion);
            tw.Write(Profile.Display);
            tw.WriteLine(", #App: {0}", NumberAppearing);
            var sb = new List<string>();
            foreach (var s in Skills.Values.OrderBy(s => s.Name))
            {
                sb.Add(string.Format("{0}-{1}", s.Name, s.Level));
            }
            tw.WriteLine(string.Join(", ", sb));
            sb.Clear();
            foreach (var w in Weapons.OrderBy(w => w))
            {
                sb.Add(string.Format("{0} ({1}d6)", w, DamageDice));
            }
            tw.Write(string.Join(", ", sb));
            tw.Write("; {0} ({1}) ", ArmourType, Armour);
            tw.WriteLine("Speed {0}m", Move);
        }

        public string EcologicalTypeShort
        {
            get
            {
                switch (EcologicalType)
                {
                    case EcologicalTypes.Carnivore:
                        return "C";
                    case EcologicalTypes.Herbivore:
                        return "H";
                    case EcologicalTypes.Omnivore:
                        return "O";
                    case EcologicalTypes.Scavenger:
                        return "S";
                    default:
                        return "E";
                }
            }
        }

        public string EcologicalSubTypeLong
        {
            get
            {
                switch (EcologicalSubtype)
                {
                    case EcologicalSubtypes.CarrionEater:
                        return "Carrion Eater";
                    case EcologicalSubtypes.Chaser:
                        return "Chaser";
                    case EcologicalSubtypes.Eater:
                        return "Eater";
                    case EcologicalSubtypes.Filter:
                        return "Filter";
                    case EcologicalSubtypes.Gatherer:
                        return "Gatherer";
                    case EcologicalSubtypes.Grazer:
                        return "Grazer";
                    case EcologicalSubtypes.Hijacker:
                        return "Hijacker";
                    case EcologicalSubtypes.Hunter:
                        return "Hunter";
                    case EcologicalSubtypes.Intermittent:
                        return "Intermittent";
                    case EcologicalSubtypes.Intimidator:
                        return "Intimidator";
                    case EcologicalSubtypes.Killer:
                        return "Killer";
                    case EcologicalSubtypes.Pouncer:
                        return "Pouncer";
                    case EcologicalSubtypes.Reducer:
                        return "Reducer";
                    case EcologicalSubtypes.Siren:
                        return "Siren";
                    case EcologicalSubtypes.Trapper:
                        return "Trapper";
                    default:
                        return "Unknown";
                }
            }
        }

        private void GenerateSkills()
        {
            if (Weapons.Count > 0)
            {
                AddSkill(NaturalWeapons);
            }
            var skills = dice.roll();
            var maxSkill = Skills.Count;
            var dx = new Dice(maxSkill);
            for (var i = 0; i < skills; i++)
            {
                var toBoost = dx.roll() - 1;
                var j = 0;
                foreach (var s in Skills.Values)
                {
                    if (j == toBoost)
                    {
                        s.Level++;
                        break;
                    }
                    j++;
                }
            }
        }

        private void GenerateArmour(int sizeClass)
        {
            int result = dice.roll(2) - 7 + sizeClass;
            if (EcologicalType == EcologicalTypes.Herbivore)
            {
                result += 4;
            }
            else if (EcologicalType == EcologicalTypes.Scavenger)
            {
                result += 2;
            }
            else if (EcologicalType == EcologicalTypes.Carnivore)
            {
                result -= 2;
            }
            if (Motion == Motions.Flying)
            {
                result -= 2;
            }
            result = result.Clamp(1, 17);
            switch (result)
            {
                case 4:
                case 5:
                    Armour = 1;
                    break;
                case 6:
                case 7:
                    Armour = 2;
                    break;
                case 8:
                case 9:
                    Armour = 3;
                    break;
                case 10:
                case 11:
                    Armour = 4;
                    break;
                case 12:
                case 13:
                    Armour = 5;
                    break;
                case 14:
                case 15:
                    Armour = 6;
                    break;
                case 16:
                case 17:
                    Armour = 7;
                    break;
                default:
                    Armour = 0;
                    break;
            }
            if (Armour <= 3)
            {
                // can be anything except shell
                result = dice.roll();
                switch (result)
                {
                    case 1:
                    case 2:
                        ArmourType = ArmourTypes.Fur;
                        break;
                    case 3:
                    case 4:
                        ArmourType = ArmourTypes.Hide;
                        break;
                    case 5:
                    case 6:
                        ArmourType = ArmourTypes.Scales;
                        break;
                }
            }
            else if (Armour <= 5)
            {
                // Can be hide or shell
                if (dice.roll() <= 3)
                {
                    ArmourType = ArmourTypes.Hide;
                }
                else
                {
                    ArmourType = ArmourTypes.Shell;
                }
            }
            else
            {
                ArmourType = ArmourTypes.Shell;
            }
        }

        private void GenerateWeapons()
        {
            int result = dice.roll(2);
            if (EcologicalType == EcologicalTypes.Carnivore)
            {
                result += 8;
            }
            else if (EcologicalType == EcologicalTypes.Omnivore)
            {
                result += 4;
            }
            else if (EcologicalType == EcologicalTypes.Scavenger)
            {
                AddWeapon("Teeth");
            }
            else
            {
                result -= 6;
            }
            result = result.Clamp(1, 19);
            switch (result)
            {
                case 1:
                    AddWeapon("Hooves");
                    break;
                case 2:
                    AddWeapon("Hooves");
                    AddWeapon("Horns");
                    break;
                case 3:
                    AddWeapon("Horns");
                    break;
                case 4:
                    AddWeapon("Hooves");
                    AddWeapon("Teeth");
                    break;
                case 5:
                    AddWeapon("Horns");
                    AddWeapon("Teeth");
                    break;
                case 6:
                    AddWeapon("Thrasher");
                    break;
                case 7:
                    AddWeapon("Claws");
                    break;
                case 8:
                    AddWeapon("Teeth");
                    break;
                case 9:
                    AddWeapon("Claws");
                    AddWeapon("Teeth");
                    break;
                case 10:
                    AddWeapon("Claws");
                    DamageDice++;
                    break;
                case 11:
                    AddWeapon("Stinger");
                    break;
                case 12:
                    AddWeapon("Teeth");
                    DamageDice++;
                    break;
                case 13:
                    AddWeapon("Claws");
                    AddWeapon("Teeth");
                    DamageDice++;
                    break;
                case 14:
                    AddWeapon("Claws");
                    AddWeapon("Stinger");
                    DamageDice++;
                    break;
                case 15:
                    AddWeapon("Claws");
                    DamageDice += 2;
                    break;
                case 16:
                    AddWeapon("Teeth");
                    DamageDice += 2;
                    break;
                case 17:
                    AddWeapon("Claws");
                    AddWeapon("Teeth");
                    DamageDice += 2;
                    break;
                case 18:
                    AddWeapon("Claws");
                    AddWeapon("Stinger");
                    DamageDice += 2;
                    break;
                case 19:
                    AddWeapon("Projectile");
                    break;
            }
        }

        private void GenerateSubType(int terrainSubTypeDM, ref int pack, ref int endurance, ref int instinct, ref int dexterity, ref int strength)
        {
            var result = (dice.roll(2) + terrainSubTypeDM).Clamp(1, 13);

            AddSkill(Athletics0);
            AddSkill(Recon0);
            AddSkill(Survival0);

            switch (EcologicalType)
            {
                case EcologicalTypes.Herbivore:
                    switch (result)
                    {
                        case 1:
                        case 2:
                            EcologicalSubtype = EcologicalSubtypes.Filter;
                            endurance += 4;
                            Move = Math.Max(dice.roll() - 5, 0) * 6;
                            break;
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            EcologicalSubtype = EcologicalSubtypes.Intermittent;
                            pack += 4;
                            Move = Math.Max(dice.roll() - 4, 1) * 6;
                            break;
                        default:
                            EcologicalSubtype = EcologicalSubtypes.Grazer;
                            pack += 4;
                            instinct += 2;
                            Move = Math.Max(dice.roll() - 2, 2) * 6;
                            break;
                    }
                    break;
                case EcologicalTypes.Omnivore:
                    switch (result)
                    {
                        case 2:
                        case 4:
                        case 10:
                            EcologicalSubtype = EcologicalSubtypes.Eater;
                            endurance += 4;
                            pack += 2;
                            Move = Math.Max(dice.roll() - 3, 1) * 6;
                            break;
                        case 6:
                        case 7:
                        case 8:
                        case 11:
                            EcologicalSubtype = EcologicalSubtypes.Hunter;
                            instinct += 2;
                            AddSkill(Survival);
                            Move = Math.Max(dice.roll() - 4, 1) * 6;
                            break;
                        default:
                            EcologicalSubtype = EcologicalSubtypes.Gatherer;
                            pack += 2;
                            AddSkill(Recon);
                            Move = Math.Max(dice.roll() - 3, 1) * 6;
                            break;
                    }
                    break;
                case EcologicalTypes.Carnivore:
                    switch (result)
                    {
                        case 1:
                        case 3:
                        case 6:
                            EcologicalSubtype = EcologicalSubtypes.Pouncer;
                            dexterity += 4;
                            instinct += 4;
                            AddSkill(Athletics);
                            AddSkill(Recon);
                            Move = Math.Max(dice.roll() - 4, 1) * 6;
                            break;
                        case 2:
                        case 12:
                            EcologicalSubtype = EcologicalSubtypes.Siren;
                            pack -= 4;
                            Move = Math.Max(dice.roll() - 4, 0) * 6;
                            break;
                        case 4:
                        case 10:
                            EcologicalSubtype = EcologicalSubtypes.Killer;
                            AddSkill(NaturalWeapons);
                            if (dice.roll() <= 3)
                            {
                                strength += 4;
                            }
                            else
                            {
                                dexterity += 4;
                            }
                            instinct += 2;
                            pack -= 2;
                            Move = Math.Max(dice.roll() - 3, 1) * 6;
                            break;
                        case 5:
                            EcologicalSubtype = EcologicalSubtypes.Trapper;
                            pack -= 2;
                            Move = Math.Max(dice.roll() - 5, 0) * 6;
                            break;
                        default:
                            EcologicalSubtype = EcologicalSubtypes.Chaser;
                            dexterity += 4;
                            instinct += 2;
                            pack += 2;
                            AddSkill(Athletics);
                            Move = Math.Max(dice.roll() - 2, 2) * 6;
                            break;
                    }
                    break;
                case EcologicalTypes.Scavenger:
                    switch (result)
                    {
                        case 1:
                        case 4:
                        case 7:
                            EcologicalSubtype = EcologicalSubtypes.CarrionEater;
                            AddSkill(Recon);
                            instinct += 2;
                            Move = Math.Max(dice.roll() - 3, 1) * 6;
                            break;
                        case 2:
                        case 6:
                        case 8:
                        case 11:
                            EcologicalSubtype = EcologicalSubtypes.Reducer;
                            pack += 4;
                            Move = Math.Max(dice.roll() - 4, 1) * 6;
                            break;
                        case 3:
                        case 9:
                        case 12:
                            EcologicalSubtype = EcologicalSubtypes.Hijacker;
                            strength += 2;
                            pack += 2;
                            Move = Math.Max(dice.roll() - 4, 1) * 6;
                            break;
                        default:
                            EcologicalSubtype = EcologicalSubtypes.Intimidator;
                            Move = Math.Max(dice.roll() - 4, 1) * 6;
                            break;
                    }
                    break;
            }
        }

        private int GenerateSize(int terrainSubTypeDM, int terrainSizeDM, int endurance, int dexterity, int strength)
        {
            int result = (dice.roll(2) + terrainSizeDM + terrainSubTypeDM).Clamp(1, 20);
            switch (result)
            {
                case 1:
                    Weight = 1;
                    Profile["STR"].Value = 1 + strength;
                    Profile["DEX"].Value = dice.roll() + dexterity;
                    Profile["END"].Value = 1 + endurance;
                    break;
                case 2:
                    Weight = 3;
                    Profile["STR"].Value = 2 + strength;
                    Profile["DEX"].Value = dice.roll() + dexterity;
                    Profile["END"].Value = 2 + endurance;
                    break;
                case 3:
                    Weight = 6;
                    Profile["STR"].Value = dice.roll() + strength;
                    Profile["DEX"].Value = dice.roll(2) + dexterity;
                    Profile["END"].Value = dice.roll() + endurance;
                    break;
                case 4:
                    Weight = 12;
                    Profile["STR"].Value = dice.roll() + strength;
                    Profile["DEX"].Value = dice.roll(2) + dexterity;
                    Profile["END"].Value = dice.roll() + endurance;
                    break;
                case 5:
                    Weight = 25;
                    Profile["STR"].Value = dice.roll(2) + strength;
                    Profile["DEX"].Value = dice.roll(3) + dexterity;
                    Profile["END"].Value = dice.roll(2) + endurance;
                    break;
                case 6:
                    Weight = 50;
                    Profile["STR"].Value = dice.roll(2) + strength;
                    Profile["DEX"].Value = dice.roll(4) + dexterity;
                    Profile["END"].Value = dice.roll(2) + endurance;
                    break;
                case 7:
                    Weight = 100;
                    Profile["STR"].Value = dice.roll(3) + strength;
                    Profile["DEX"].Value = dice.roll(4) + dexterity;
                    Profile["END"].Value = dice.roll(3) + endurance;
                    break;
                case 8:
                    Weight = 200;
                    Profile["STR"].Value = dice.roll(3) + strength;
                    Profile["DEX"].Value = dice.roll(3) + dexterity;
                    Profile["END"].Value = dice.roll(3) + endurance;
                    break;
                case 9:
                    Weight = 400;
                    Profile["STR"].Value = dice.roll(4) + strength;
                    Profile["DEX"].Value = dice.roll(2) + dexterity;
                    Profile["END"].Value = dice.roll(4) + endurance;
                    break;
                case 10:
                    Weight = 800;
                    Profile["STR"].Value = dice.roll(4) + strength;
                    Profile["DEX"].Value = dice.roll(2) + dexterity;
                    Profile["END"].Value = dice.roll(4) + endurance;
                    break;
                case 11:
                    Weight = 1600;
                    Profile["STR"].Value = dice.roll(5) + strength;
                    Profile["DEX"].Value = dice.roll(2) + dexterity;
                    Profile["END"].Value = dice.roll(5) + endurance;
                    break;
                case 12:
                    Weight = 3200;
                    Profile["STR"].Value = dice.roll(5) + strength;
                    Profile["DEX"].Value = dice.roll(1) + dexterity;
                    Profile["END"].Value = dice.roll(5) + endurance;
                    break;
                case 13:
                    Weight = 5000;
                    Profile["STR"].Value = dice.roll(6) + strength;
                    Profile["DEX"].Value = dice.roll(1) + dexterity;
                    Profile["END"].Value = dice.roll(6) + endurance;
                    break;
                case 14:
                    Weight = 10000;
                    Profile["STR"].Value = dice.roll(6) + strength;
                    Profile["DEX"].Value = dice.roll(1) + dexterity;
                    Profile["END"].Value = dice.roll(6) + endurance;
                    break;
                case 15:
                    Weight = 15000;
                    Profile["STR"].Value = dice.roll(7) + strength;
                    Profile["DEX"].Value = dice.roll(1) + dexterity;
                    Profile["END"].Value = dice.roll(7) + endurance;
                    break;
                case 16:
                    Weight = 20000;
                    Profile["STR"].Value = dice.roll(7) + strength;
                    Profile["DEX"].Value = dice.roll(1) + dexterity;
                    Profile["END"].Value = dice.roll(7) + endurance;
                    break;
                case 17:
                    Weight = 25000;
                    Profile["STR"].Value = dice.roll(8) + strength;
                    Profile["DEX"].Value = dice.roll(1) + dexterity;
                    Profile["END"].Value = dice.roll(8) + endurance;
                    break;
                case 18:
                    Weight = 30000;
                    Profile["STR"].Value = dice.roll(8) + strength;
                    Profile["DEX"].Value = dice.roll(1) + dexterity;
                    Profile["END"].Value = dice.roll(8) + endurance;
                    break;
                case 19:
                    Weight = 35000;
                    Profile["STR"].Value = dice.roll(9) + strength;
                    Profile["DEX"].Value = dice.roll(1) + dexterity;
                    Profile["END"].Value = dice.roll(9) + endurance;
                    break;
                default:
                    Weight = 40000;
                    Profile["STR"].Value = dice.roll(9) + strength;
                    Profile["DEX"].Value = dice.roll(1) + dexterity;
                    Profile["END"].Value = dice.roll(9) + endurance;
                    break;
            }
            // Now we know the strenght we can work out the base damage dice
            DamageDice = (Profile["STR"].Value / 10) + 1;
            return result;
        }

        private void GeneratePackSize()
        {
            int pack = Profile["PAC"].Value.Clamp(0, 15);
            switch (pack)
            {
                case 0:
                    NumberAppearing = "1";
                    break;
                case 1:
                case 2:
                    NumberAppearing = "1D3";
                    break;
                case 3:
                case 4:
                case 5:
                    NumberAppearing = "1D6";
                    break;
                case 6:
                case 7:
                case 8:
                    NumberAppearing = "2D6";
                    break;
                case 9:
                case 10:
                case 11:
                    NumberAppearing = "3D6";
                    break;
                case 12:
                case 13:
                case 14:
                    NumberAppearing = "4D6";
                    break;
                default:
                    NumberAppearing = "5D6";
                    break;
            }
        }

        private void AddSkill(Skill skill)
        {
            if (!Skills.ContainsKey(skill.Name))
            {
                Skills.Add(skill.Name, skill.Clone());
            }
            else
            {
                Skills[skill.Name].Level += skill.Level;
            }
        }

        private void AddWeapon(string weapon)
        {
            if (!Weapons.Contains(weapon))
            {
                Weapons.Add(weapon);
            }
        }
    }
}
