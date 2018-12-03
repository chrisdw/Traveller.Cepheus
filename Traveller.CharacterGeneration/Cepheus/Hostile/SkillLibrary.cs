using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Hostile
{
    public static class SkillLibrary
    {
        public static Skill Agriculture = new Skill("Agriculture", Skill.SkillClass.None, 1);
        public static Skill Aircraft = new Skill(Cepheus.Resources.Skill_Aircraft, Skill.SkillClass.None, 1);
        public static Skill GroundVehicle = new Skill(Cepheus.Resources.Skill_GroundVehicle, Skill.SkillClass.None, 1);
        public static Skill Loader = new Skill("Loader", Skill.SkillClass.None, 1);
        public static Skill Watercraft = new Skill(Cepheus.Resources.Skill_Watercraft, Skill.SkillClass.None, 1);

        public static Skill Vehicle = new Skill(Resources.Skill_Vehicle, Skill.SkillClass.None, 1)
        {
            Cascade =
            {
                Aircraft,
                GroundVehicle,
                Watercraft
            }
        };
    }
}
