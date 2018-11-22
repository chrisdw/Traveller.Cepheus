using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Imperial.Citizen
{
    public class Flyer : Career
    {
        public Flyer()
        {
            Name = Resources.Career_Flyer;
            RankNumber = 0;
            TermSkills = 2;

            enlistment = 6;
            enlistment1attr = "STR";
            enlistment1val =7;
            enlistment2attr = "DEX";
            enlistment2val = 9;
            survival = 5;
            survival2attr = "DEX";
            survival2val = 8;
            position = 5;
            position1attr = "EDU";
            position1val = 6;
            promotion = 8;
            promotion1attr = "EDU";
            promotion1val = 8;
            reenlist = 6;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Properties.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Dex;
            skills[2] = SkillLibrary.Int;
            skills[3] = SkillLibrary.Gambling;
            skills[4] = SkillLibrary.Brawling;
            skills[5] = SkillLibrary.Carousing;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Properties.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.Brawling;
            skills[1] = SkillLibrary.VaccSuit;
            skills[2] = SkillLibrary.GunCombat;
            skills[3] = SkillLibrary.Vehicle;
            skills[4] = SkillLibrary.Vehicle;
            skills[5] = SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Properties.Resources.Table_Education;
            skills = table.Skills;
            skills[0] = SkillLibrary.Aircraft;
            skills[1] = SkillLibrary.Mechanical;
            skills[2] = SkillLibrary.Electronics;
            skills[3] = SkillLibrary.Gravitics;
            skills[4] = SkillLibrary.GunCombat;
            skills[5] = SkillLibrary.Survival;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Properties.Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = SkillLibrary.Medic;
            skills[1] = SkillLibrary.Leader;
            skills[2] = SkillLibrary.Pilot;
            skills[3] = SkillLibrary.Computer;
            skills[4] = SkillLibrary.Admin;
            skills[5] = SkillLibrary.JackOfTrades;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.MidPsg);
            Material.Add(BenefitLibrary.Soc);

            Cash[0] = 2000;
            Cash[1] = 5000;
            Cash[2] = 10000;
            Cash[3] = 10000;
            Cash[4] = 10000;
            Cash[5] = 20000;
            Cash[6] = 30000;

            Ranks[0] = Resources.Rank_Flyer;
            Ranks[1] = Resources.Rank_Pilot;
            Ranks[2] = Resources.Rank_FlightLeader;
            Ranks[3] = Resources.Rank_SquadronLeader;
            Ranks[4] = Resources.Rank_StaffMajor;
            Ranks[5] = Resources.Rank_GroupLeader;
            Ranks[6] = Resources.Rank_AirMarshall;

        }
        protected override void CommsionSkill()
        {
            // Nothing to do
        }

        protected override int EnlistFactor()
        {
            var target = 0;
            if (Owner.CharacterSpecies == Character.Species.AelYael)
            {
                target += 1;
            }
            else if (Owner.CharacterSpecies == Character.Species.Aslan && Owner.Sex.Equals(Properties.Resources.Sex_Female))
            {
                target += 4;
            }
            return target;
        }

        protected override void EnlistSkill()
        {
            OnSkillOffered(SkillLibrary.Aircraft);
        }

        protected override void RankSkill()
        {
            // Nothing to do
        }
    }
}
