using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration
{
    /// <summary>
    /// Holds all standard skills
    /// </summary>
    public static class SkillLibrary
    {
        // Attribute changes
        public static Skill Str = new Skill("STR", Skill.SkillClass.AttributeChange, 1);
        public static Skill Str2 = new Skill("STR", Skill.SkillClass.AttributeChange, 2);
        public static Skill Dex = new Skill("DEX", Skill.SkillClass.AttributeChange, 1);
        public static Skill End = new Skill("END", Skill.SkillClass.AttributeChange, 1);
        public static Skill Int = new Skill("INT", Skill.SkillClass.AttributeChange, 1);
        public static Skill Edu = new Skill("EDU", Skill.SkillClass.AttributeChange, 1);
        public static Skill Soc = new Skill("SOC", Skill.SkillClass.AttributeChange, 1);

        // Simple skills
        public static Skill Admin = new Skill("Admin", Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill AirRaft = new Skill("Air/Raft", Skill.SkillClass.None, 1);
        public static Skill ATV = new Skill("ATV", Skill.SkillClass.None, 1);
        public static Skill Brawling = new Skill("Brawling", Skill.SkillClass.None, 1);
        public static Skill Bribery = new Skill("Bribery", Skill.SkillClass.Civilian, 1, Skill.SkillSex.Female);
        public static Skill Carousing = new Skill("Carousing", Skill.SkillClass.None, 1);
        public static Skill Computer = new Skill("Computer", Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill Electronics = new Skill("Electronics", Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill Engineering = new Skill("Engineering", Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill Forgery = new Skill("Forgery", Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill FowardObserver = new Skill("Forward Observer", Skill.SkillClass.None, 1);
        public static Skill Gambling = new Skill("Gambling", Skill.SkillClass.None, 1);
        public static Skill Gunnery = new Skill("Gunnery", Skill.SkillClass.None, 1, Skill.SkillSex.Male);
        public static Skill Instruction = new Skill("Instruction", Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill Interrogation = new Skill("Interrogation", Skill.SkillClass.None, 1, Skill.SkillSex.Male);
        public static Skill JackOfTrades = new Skill("Jack-Of-Trades", Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill Leader = new Skill("Leader", Skill.SkillClass.None, 1, Skill.SkillSex.Male);
        public static Skill Liason = new Skill("Liason", Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill Mechanical = new Skill("Mechanical", Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill Medic = new Skill("Medic", Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill Navigation = new Skill("Navigation", Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill Pilot = new Skill("Pilot", Skill.SkillClass.None, 1, Skill.SkillSex.Male);
        public static Skill Prospecting = new Skill("Prospecting", Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill Recon = new Skill("Recon", Skill.SkillClass.Military, 1);
        public static Skill Recruiting = new Skill("Recruiting", Skill.SkillClass.None, 1);
        public static Skill Tactics = new Skill("Tactics", Skill.SkillClass.None, 1, Skill.SkillSex.Male);
        public static Skill ShipsBoat = new Skill("Ships Boat", Skill.SkillClass.None, 1);
        public static Skill Steward = new Skill("Steward", Skill.SkillClass.Civilian, 1, Skill.SkillSex.Female);
        public static Skill Streetwise = new Skill("Streetwise", Skill.SkillClass.Civilian, 1, Skill.SkillSex.Female);
        public static Skill Survival = new Skill("Survival", Skill.SkillClass.Military, 1);
        public static Skill VaccSuit = new Skill("Vacc Suit", Skill.SkillClass.None, 1);

        public static Skill Aircraft = new Skill("Aircraft", Skill.SkillClass.None, 1)
        {
            Cascade = {
                new Skill("Grav Vehicle", Skill.SkillClass.None, 1),
                new Skill("Rotor Aircraft", Skill.SkillClass.None, 1),
                new Skill("Winged Aircraft", Skill.SkillClass.None, 1)
            }
        };

        public static Skill Vehicle = new Skill("Vehicle", Skill.SkillClass.None, 1)
        {
            Cascade = {
                Aircraft,
                new Skill("Tracked Vehicle", Skill.SkillClass.None, 1),
                new Skill("Hovercraft", Skill.SkillClass.None, 1),
                new Skill("Wheeled Vehicle", Skill.SkillClass.None, 1)
            }
        };

        public static Skill Revolver = new Skill("Revolver", Skill.SkillClass.None, 1);
        public static Skill Rifle = new Skill("Rifle", Skill.SkillClass.None, 1);
        public static Skill SubmachineGun = new Skill("Submachine Gun", Skill.SkillClass.None, 1);

        public static Skill GunCombat = new Skill("Gun Combat", Skill.SkillClass.Military, 1)
        {
            Cascade =
            {
                new Skill("Body Pistol", Skill.SkillClass.None, 1),
                new Skill("Automatic Pistol", Skill.SkillClass.None, 1),
                Revolver,
                new Skill("Carbine", Skill.SkillClass.None, 1),
                Rifle,
                new Skill("Laser Carbine", Skill.SkillClass.None, 1),
                new Skill("Laser Rifle", Skill.SkillClass.None, 1),
                new Skill("Automatic Rifle", Skill.SkillClass.None, 1),
                SubmachineGun,
                new Skill("Shotgun", Skill.SkillClass.None, 1)
            }
        };

        public static Skill Cutlass = new Skill("Cutlass", Skill.SkillClass.None, 1);
        public static Skill Sword = new Skill("Sword", Skill.SkillClass.None, 1);

        public static Skill BladeCombat = new Skill("Blade Combat", Skill.SkillClass.Military, 1)
        {
            Cascade =
            {
                new Skill("Dagger", Skill.SkillClass.None, 1),
                new Skill("Blade", Skill.SkillClass.None, 1),
                new Skill("Foil", Skill.SkillClass.None, 1),
                Cutlass,
                Sword,
                new Skill("Broadsword", Skill.SkillClass.None, 1),
                new Skill("Halberd", Skill.SkillClass.None, 1),
                new Skill("Pike", Skill.SkillClass.None, 1),
                new Skill("Cudgel", Skill.SkillClass.None, 1),
                new Skill("Bayonet", Skill.SkillClass.None, 1)
            }
        };

        public static Skill BowCombat = new Skill("Bow Combat", Skill.SkillClass.Military, 1)
        {
            Cascade =
            {
                new Skill("Sling", Skill.SkillClass.None, 1),
                new Skill("Short Bow", Skill.SkillClass.None, 1),
                new Skill("Long Bow", Skill.SkillClass.None, 1),
                new Skill("Sporting Crossbow", Skill.SkillClass.None, 1),
                new Skill("Military Crossbow", Skill.SkillClass.None, 1),
                new Skill("Repeating Crossbow", Skill.SkillClass.None, 1)
            }
        };
    }
}
