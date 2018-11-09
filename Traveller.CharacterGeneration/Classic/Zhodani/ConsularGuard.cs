using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Zhodani
{
    public class ConsularGuard : Career
    {
        public ConsularGuard()
        {
            Name = "Consular Guard";
            CurrentRank = 0;
            TermSkills = 2;

            enlistment = 14;
            enlistment1attr = "STR";
            enlistment1val = 7;
            enlistment2attr = "PSI";
            enlistment2val = 9;
            enlistment3attr = "SOC";
            enlistment3val = 10;
            survival = 6;
            survival2attr = "END";
            survival2val = 8;
            position = 10;
            position1attr = "PSI";
            position1val = 10;
            position3attr = string.Empty;
            position3val = 0;
            promotion = 10;
            promotion1attr = "SOC";
            promotion1val = 11;
            reenlist = 5;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = "Personal Development";
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Dex;
            skills[2] = SkillLibrary.End;
            skills[3] = SkillLibrary.GunCombat;
            skills[4] = SkillLibrary.Edu;
            skills[5] = SkillLibrary.BladeCombat;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = "Service Skills";
            skills = table.Skills;
            skills[0] = SkillLibrary.Vehicle;
            skills[1] = SkillLibrary.VaccSuit;
            skills[2] = SkillLibrary.Mechanical;
            skills[3] = SkillLibrary.Electronics;
            skills[4] = SkillLibrary.BladeCombat;
            skills[5] = SkillLibrary.GunCombat;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = "Noble/Intendant Education";
            skills = table.Skills;
            skills[0] = SkillLibrary.Teleportation;
            skills[1] = SkillLibrary.Psi;
            skills[2] = SkillLibrary.Talent;
            skills[3] = SkillLibrary.Psi;
            skills[4] = SkillLibrary.Telekinesis;
            skills[5] = SkillLibrary.Teleportation;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = "Education";
            skills = table.Skills;
            skills[0] = SkillLibrary.Medic;
            skills[1] = SkillLibrary.Tactics;
            skills[2] = SkillLibrary.Tactics;
            skills[3] = SkillLibrary.Computer;
            skills[4] = SkillLibrary.Leader;
            skills[5] = SkillLibrary.Admin;

            Material.Add(BenefitLibrary.Soc);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.Psi);
            Material.Add(BenefitLibrary.Legion);
            Material.Add(BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.Soc);

            Cash[0] = 1000;
            Cash[1] = 2000;
            Cash[2] = 5000;
            Cash[3] = 5000;
            Cash[4] = 10000;
            Cash[5] = 20000;
            Cash[6] = 30000;

            Ranks[0] = "Subaltern";
            Ranks[1] = "Lieutenant";
            Ranks[2] = "Captain";
            Ranks[3] = "Major";
            Ranks[4] = "Lt. Colonel";
            Ranks[5] = "Colonel";
            Ranks[6] = "General";
        }

        protected override void CommsionSkill()
        {
            // Nothing to do
        }

        protected override int EnlistFactor()
        {
            return 0;
        }

        protected override void EnlistSkill()
        {
            OnSkillOffered(SkillLibrary.BladeCombat);
            if (Owner.Profile.Soc.Value >= 11)
            {
                Owner.Profile["PSI"].Value += 1;
            }
        }

        protected override void RankSkill()
        {
            // Nothing to do
        }
    }
}
