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
        public static Skill Chr = new Skill("CHR", Skill.SkillClass.AttributeChange, 1);
        public static Skill ChrDrop = new Skill("CHR", Skill.SkillClass.AttributeChange, -1);

        // Simple skills
        public static Skill Admin = new Skill(Properties.Resources.Skill_Admin, Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill AirRaft = new Skill(Properties.Resources.Skill_AirRaft, Skill.SkillClass.None, 1);
        public static Skill ATV = new Skill(Properties.Resources.Skill_ATV, Skill.SkillClass.None, 1);
        public static Skill BattleDress = new Skill(Properties.Resources.Skill_BattleDress, Skill.SkillClass.Military, 1);
        public static Skill Brawling = new Skill(Properties.Resources.Skill_Brawling, Skill.SkillClass.None, 1);
        public static Skill Bribery = new Skill(Properties.Resources.Skill_Bribery, Skill.SkillClass.Civilian, 1, Skill.SkillSex.Female);
        public static Skill Broker = new Skill(Properties.Resources.Skill_Broker, Skill.SkillClass.None, 1);
        public static Skill Carousing = new Skill(Properties.Resources.Skill_Carousing, Skill.SkillClass.None, 1);
        public static Skill CombatEngineering = new Skill(Properties.Resources.Skill_CombatEngineering, Skill.SkillClass.Military, 1);
        public static Skill Communications = new Skill(Properties.Resources.Skill_Communications, Skill.SkillClass.None, 1);
        public static Skill Computer = new Skill(Properties.Resources.Skill_Computer, Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill Demolitions = new Skill(Properties.Resources.Skill_Demolitions, Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill Electronics = new Skill(Properties.Resources.Skill_Electronics, Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill Engineering = new Skill(Properties.Resources.Skill_Engineering, Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill Forgery = new Skill(Properties.Resources.Skill_Forgery, Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill FowardObserver = new Skill(Properties.Resources.Skill_ForwardObserver, Skill.SkillClass.None, 1);
        public static Skill Gambling = new Skill(Properties.Resources.Skill_Gambling, Skill.SkillClass.None, 1);
        public static Skill Gravitics = new Skill(Properties.Resources.Skill_Gravitics, Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill GravVehicle = new Skill(Properties.Resources.Skill_GravVehicle, Skill.SkillClass.None, 1);
        public static Skill Gunnery = new Skill(Properties.Resources.Skill_Gunnery, Skill.SkillClass.None, 1, Skill.SkillSex.Male);
        public static Skill Hunting = new Skill(Properties.Resources.Skill_Hunting, Skill.SkillClass.None, 1, Skill.SkillSex.Male);
        public static Skill Instruction = new Skill(Properties.Resources.Skill_Instruction, Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill Interrogation = new Skill(Properties.Resources.Skill_Interrogation, Skill.SkillClass.None, 1, Skill.SkillSex.Male);
        public static Skill JackOfTrades = new Skill(Properties.Resources.Skill_JackOfTrades, Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill Leader = new Skill(Properties.Resources.Skill_Leader, Skill.SkillClass.None, 1, Skill.SkillSex.Male);
        public static Skill Liason = new Skill(Properties.Resources.Skill_Liason, Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill Mechanical = new Skill(Properties.Resources.Skill_Mechanical, Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill Medic = new Skill(Properties.Resources.Skill_Medic, Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill Navigation = new Skill(Properties.Resources.Skill_Navigation, Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill NonVerbalComms = new Skill(Properties.Resources.Skill_NonVerbalComms, Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill Prole = new Skill(Properties.Resources.Skill_Prole, Skill.SkillClass.Prole, 1, Skill.SkillSex.DontCare);
        public static Skill Pilot = new Skill(Properties.Resources.Skill_Pilot, Skill.SkillClass.None, 1, Skill.SkillSex.Male);
        public static Skill Prospecting = new Skill(Properties.Resources.Skill_Prospecting, Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill Psychology = new Skill(Properties.Resources.Skill_Psychology, Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill Recon = new Skill(Properties.Resources.Skill_Recon, Skill.SkillClass.Military, 1);
        public static Skill Recruiting = new Skill(Properties.Resources.Skill_Recruiting, Skill.SkillClass.None, 1);
        public static Skill Tactics = new Skill(Properties.Resources.Skill_Tactics, Skill.SkillClass.None, 1, Skill.SkillSex.Male);
        public static Skill Trader = new Skill(Properties.Resources.Skill_Trader, Skill.SkillClass.None, 1, Skill.SkillSex.Female);
        public static Skill ShipsBoat = new Skill(Properties.Resources.Skill_ShipsBoat, Skill.SkillClass.None, 1);
        public static Skill ShipTactics = new Skill(Properties.Resources.Skill_ShipTactics, Skill.SkillClass.None, 1);
        public static Skill Steward = new Skill(Properties.Resources.Skill_Steward, Skill.SkillClass.Civilian, 1, Skill.SkillSex.Female);
        public static Skill Streetwise = new Skill(Properties.Resources.Skill_Streetwise, Skill.SkillClass.Civilian, 1, Skill.SkillSex.Female);
        public static Skill Survival = new Skill(Properties.Resources.Skill_Survival, Skill.SkillClass.Military, 1);
        public static Skill VaccSuit = new Skill(Properties.Resources.Skill_VaccSuit, Skill.SkillClass.None, 1);
        public static Skill ZeroGCombat = new Skill(Properties.Resources.Skill_ZeroGCombat, Skill.SkillClass.None, 1);

        public static Skill Aircraft = new Skill(Properties.Resources.Skill_Aircraft, Skill.SkillClass.None, 1)
        {
            Cascade = {
                GravVehicle,
                new Skill(Properties.Resources.Skill_PropellerAircraft, Skill.SkillClass.None, 1),
                new Skill(Properties.Resources.Skill_JetAircraft, Skill.SkillClass.None, 1),
                new Skill(Properties.Resources.Skill_Helicopters, Skill.SkillClass.None, 1),
                new Skill(Properties.Resources.Skill_LTA, Skill.SkillClass.None, 1)
            }
        };

        public static Skill Watercraft = new Skill(Properties.Resources.Skill_Watercraft, Skill.SkillClass.None, 1)
        {
            Cascade = {
                new Skill(Properties.Resources.Skill_Hovercraft, Skill.SkillClass.None, 1),
                new Skill(Properties.Resources.Skill_SmallWatercraft, Skill.SkillClass.None, 1),
                new Skill(Properties.Resources.Skill_LargeWatercraft, Skill.SkillClass.None, 1),
                new Skill(Properties.Resources.Skill_Submersibles, Skill.SkillClass.None, 1)
            }
        };

        public static Skill GroundVehicle = new Skill(Properties.Resources.Skill_GroundVehicle, Skill.SkillClass.None, 1)
        {
            Cascade = {
                GravVehicle,
                new Skill(Properties.Resources.Skill_TrackedVehicle, Skill.SkillClass.None, 1),
                new Skill(Properties.Resources.Skill_WheeledVehicle, Skill.SkillClass.None, 1)
            }
        };

        public static Skill Vehicle = new Skill(Properties.Resources.Skill_Vehicle, Skill.SkillClass.None, 1)
        {
            Cascade = {
                Aircraft,
                Watercraft,
                GroundVehicle,
            }
        };

        public static Skill Revolver = new Skill(Properties.Resources.Skill_Revolver, Skill.SkillClass.None, 1);
        public static Skill Rifle = new Skill(Properties.Resources.Skill_Rifle, Skill.SkillClass.None, 1);
        public static Skill SubmachineGun = new Skill(Properties.Resources.Skill_SubmachineGun, Skill.SkillClass.None, 1);

        public static Skill GunCombat = new Skill(Properties.Resources.Skill_GunCombat, Skill.SkillClass.Military, 1)
        {
            Cascade =
            {
                new Skill(Properties.Resources.Skill_BodyPistol, Skill.SkillClass.None, 1),
                new Skill(Properties.Resources.Skill_AutomaticPistol, Skill.SkillClass.None, 1),
                Revolver,
                new Skill(Properties.Resources.Skill_Carbine, Skill.SkillClass.None, 1),
                Rifle,
                new Skill(Properties.Resources.Skill_LaserCarbine, Skill.SkillClass.None, 1),
                new Skill(Properties.Resources.Skill_LaserRifle, Skill.SkillClass.None, 1),
                new Skill(Properties.Resources.Skill_AutomaticRifle, Skill.SkillClass.None, 1),
                SubmachineGun,
                new Skill(Properties.Resources.Skill_Shotgun, Skill.SkillClass.None, 1)
            }
        };

        public static Skill Cutlass = new Skill(Properties.Resources.Skill_Cutlass, Skill.SkillClass.None, 1);
        public static Skill Sword = new Skill(Properties.Resources.Skill_Sword, Skill.SkillClass.None, 1);

        public static Skill BladeCombat = new Skill(Properties.Resources.Skill_BladeCombat, Skill.SkillClass.Military, 1)
        {
            Cascade =
            {
                new Skill(Properties.Resources.Skill_Dagger, Skill.SkillClass.None, 1),
                new Skill(Properties.Resources.Skill_Blade, Skill.SkillClass.None, 1),
                new Skill(Properties.Resources.Skill_Foil, Skill.SkillClass.None, 1),
                Cutlass,
                Sword,
                new Skill(Properties.Resources.Skill_Broadsword, Skill.SkillClass.None, 1),
                new Skill(Properties.Resources.Skill_Halberd, Skill.SkillClass.None, 1),
                new Skill(Properties.Resources.Skill_Pike, Skill.SkillClass.None, 1),
                new Skill(Properties.Resources.Skill_Cudgel, Skill.SkillClass.None, 1),
                new Skill(Properties.Resources.Skill_Bayonet, Skill.SkillClass.None, 1)
            }
        };

        public static Skill BowCombat = new Skill(Properties.Resources.Skill_BowCombat, Skill.SkillClass.Military, 1)
        {
            Cascade =
            {
                new Skill(Properties.Resources.Skill_Sling, Skill.SkillClass.None, 1),
                new Skill(Properties.Resources.Skill_ShortBow, Skill.SkillClass.None, 1),
                new Skill(Properties.Resources.Skill_LongBow, Skill.SkillClass.None, 1),
                new Skill(Properties.Resources.Skill_SportingCrossbow, Skill.SkillClass.None, 1),
                new Skill(Properties.Resources.Skill_MilitaryCrossbow, Skill.SkillClass.None, 1),
                new Skill(Properties.Resources.Skill_RepeatingCrossbow, Skill.SkillClass.None, 1)
            }
        };

        // Psionic Skills
        public static Skill Awareness = new Skill(Properties.Resources.Skill_Awareness, Skill.SkillClass.Psionic, 1);
        public static Skill Clairvoyance = new Skill(Properties.Resources.Skill_Clairvoyance, Skill.SkillClass.Psionic, 1);
        public static Skill Talent = new Skill(Properties.Resources.Skill_Talent, Skill.SkillClass.Psionic, 1);
        public static Skill Special = new Skill(Properties.Resources.Skill_Special, Skill.SkillClass.Psionic, 1);
        public static Skill Telekinesis = new Skill(Properties.Resources.Skill_Telekinesis, Skill.SkillClass.Psionic, 1);
        public static Skill Telepathy = new Skill(Properties.Resources.Skill_Telepathy, Skill.SkillClass.Psionic, 1);
        public static Skill Teleportation = new Skill(Properties.Resources.Skill_Teleportation, Skill.SkillClass.Psionic, 1);
        public static Skill Psi = new Skill("Psi", Skill.SkillClass.Psionic, 1);

        // Dolphin Specific Skills
        public static Skill HitsU = new Skill("HitsU", Skill.SkillClass.AttributeChange, 1);
        public static Skill Herding = new Skill(Properties.Resources.Skill_Herding, Skill.SkillClass.None, 1);
        public static Skill WaldoOps = new Skill(Properties.Resources.Skill_WaldoOps, Skill.SkillClass.None, 1);

        // Aslan specific skills
        public static Skill DewClaw = new Skill(Properties.Resources.Skill_DewClaw, Skill.SkillClass.None, 1);
        public static Skill Independance = new Skill(Properties.Resources.Skill_Independance, Skill.SkillClass.None, 1, Skill.SkillSex.Male);
        public static Skill Tolerance = new Skill(Properties.Resources.Skill_Tolerance, Skill.SkillClass.None, 1, Skill.SkillSex.DontCare);

        // Vargr specific skills
        public static Skill Infighting = new Skill(Properties.Resources.Skill_Infighting, Skill.SkillClass.None, 1);
    }
}
