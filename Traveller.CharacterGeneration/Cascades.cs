using System;
using System.Collections.Generic;
using System.Text;

namespace Traveller.CharacterGeneration
{
    /// <summary>
    /// Holds the casecade lists for all standard skills
    /// </summary>
    public static class Cascades
    {
        public static Skill Vehicle = new Skill("Vehicle", Skill.SkillClass.None, 1)
        {
            Cascade = {
                new Skill("Aircraft", Skill.SkillClass.None, 1)
                {
                    Cascade =
                    {
                        new Skill("Grav Vehicle", Skill.SkillClass.None, 1),
                        new Skill("Rotor Aircraft", Skill.SkillClass.None, 1),
                        new Skill("Winged Aircraft", Skill.SkillClass.None, 1)
                    }
                },
                new Skill("Tracked Vehicle", Skill.SkillClass.None, 1),
                new Skill("Hovercraft", Skill.SkillClass.None, 1),
                new Skill("Wheeled Vehicle", Skill.SkillClass.None, 1)
            }
        };

        public static Skill GunCombat = new Skill("Gun Combat", Skill.SkillClass.None, 1)
        {
            Cascade =
            {
                new Skill("Body Pistol", Skill.SkillClass.None, 1),
                new Skill("Automatic Pistol", Skill.SkillClass.None, 1),
                new Skill("Revolver", Skill.SkillClass.None, 1),
                new Skill("Carbine", Skill.SkillClass.None, 1),
                new Skill("Rifle", Skill.SkillClass.None, 1),
                new Skill("Laser Carbine", Skill.SkillClass.None, 1),
                new Skill("Laser Rifle", Skill.SkillClass.None, 1),
                new Skill("Automatic Rifle", Skill.SkillClass.None, 1),
                new Skill("Submachine Gun", Skill.SkillClass.None, 1),
                new Skill("Shotgun", Skill.SkillClass.None, 1)
            }
        };
    }
}
