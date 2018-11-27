using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Aslan
{
    public class Flyer : Career
    {
        protected override void CommsionSkill()
        {
            Name = "Flyer";
            TermSkills = 2;

            enlistment = 11;

            survival = 8;
            survival3attr = "DEX";
            survival3val = 7;
            position = 10;
            position1attr = "SOC";
            position1val = 9;
            promotion = 7;
            promotion1attr = "EDU";
            promotion1val = 8;
            reenlist = 6;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = "Personal Development";
            var skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Independance;
            skills[1] = CharacterGeneration.SkillLibrary.Str;
            skills[2] = CharacterGeneration.SkillLibrary.Independance;
            skills[3] = CharacterGeneration.SkillLibrary.End;
            skills[4] = CharacterGeneration.SkillLibrary.Int;
            skills[5] = CharacterGeneration.SkillLibrary.DewClaw;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = "Service Skills";
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Dex;
            skills[1] = CharacterGeneration.SkillLibrary.Aircraft;
            skills[2] = SkillLibrary.GunCombat;
            skills[3] = CharacterGeneration.SkillLibrary.ShipsBoat;
            skills[4] = CharacterGeneration.SkillLibrary.Tactics;
            skills[5] = CharacterGeneration.SkillLibrary.Leader;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = "Advanced Service Skills";
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.ShipTactics;
            skills[1] = CharacterGeneration.SkillLibrary.Aircraft;
            skills[2] = CharacterGeneration.SkillLibrary.Aircraft;
            skills[3] = CharacterGeneration.SkillLibrary.Aircraft;
            skills[4] = CharacterGeneration.SkillLibrary.VaccSuit;
            skills[5] = CharacterGeneration.SkillLibrary.Leader;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = "Experience";
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Aircraft;
            skills[1] = CharacterGeneration.SkillLibrary.ShipsBoat;
            skills[2] = CharacterGeneration.SkillLibrary.VaccSuit;
            skills[3] = SkillLibrary.GunCombat;
            skills[4] = CharacterGeneration.SkillLibrary.Leader;
            skills[5] = CharacterGeneration.SkillLibrary.Soc;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.Independance);
            Material.Add(BenefitLibrary.MidPsg);
            Material.Add(BenefitLibrary.Soc);
            Material.Add(BenefitLibrary.Land);

            Cash[0] = 1000;
            Cash[1] = 5000;
            Cash[2] = 5000;
            Cash[3] = 5000;
            Cash[4] = 10000;
            Cash[5] = 20000;
            Cash[6] = 50000;

            Ranks[0] = "Flyer";
            Ranks[1] = "Pilot";
            Ranks[2] = "Flight Commandant";
            Ranks[3] = "Squadron Leader";
            Ranks[4] = "Wing Leader";
            Ranks[5] = "Group Leader";
            Ranks[6] = "Air Marshal";
        }

        protected override void EnlistSkill()
        {
            OnSkillOffered(CharacterGeneration.SkillLibrary.Aircraft);
        }

        protected override void RankSkill()
        {
            if (RankNumber == 4)
            {
                Owner.AddSkill(CharacterGeneration.SkillLibrary.Leader);
            }
        }

        public override void CheckTableAvailablity()
        {

        }
    }
}
