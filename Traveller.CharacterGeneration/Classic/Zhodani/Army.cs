using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Zhodani
{
    public class Army : Career
    {
        public Army()
        {
            Name = "Army";
            RankNumber = 0;
            TermSkills = 2;

            enlistment = 6;
            enlistment1attr = "DEX";
            enlistment1val = 6;
            enlistment2attr = "END";
            enlistment2val = 6;
            enlistment3attr = "STR";
            enlistment3val = 8;
            survival = 5;
            survival2attr = "EDU";
            survival2val = 6;
            position = 11;
            position1attr = "INT";
            position1val = 8;
            position3attr = "SOC";
            position3val = 10;
            promotion = 8;
            promotion1attr = "SOC";
            promotion1val = 11;
            reenlist = 6;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Properties.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Dex;
            skills[2] = SkillLibrary.End;
            skills[3] = SkillLibrary.GunCombat;
            skills[4] = SkillLibrary.Edu;
            skills[5] = SkillLibrary.Brawling;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Properties.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.Vehicle;
            skills[1] = SkillLibrary.FowardObserver;
            skills[2] = SkillLibrary.Mechanical;
            skills[3] = SkillLibrary.Electronics;
            skills[4] = SkillLibrary.BladeCombat;
            skills[5] = SkillLibrary.GunCombat;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Properties.Resources.Table_Education;
            skills = table.Skills;
            skills[0] = SkillLibrary.Medic;
            skills[1] = SkillLibrary.Tactics;
            skills[2] = SkillLibrary.Tactics;
            skills[3] = SkillLibrary.Computer;
            skills[4] = SkillLibrary.Leader;
            skills[5] = SkillLibrary.Admin;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Properties.Resources.Table_NobleIntendant;
            skills = table.Skills;
            skills[0] = SkillLibrary.Telekinesis;
            skills[1] = SkillLibrary.Psi;
            skills[2] = SkillLibrary.Talent;
            skills[3] = SkillLibrary.Psi;
            skills[4] = SkillLibrary.Awareness;
            skills[5] = SkillLibrary.Telekinesis;

            Material.Add(BenefitLibrary.Soc);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.MidPsg);
            Material.Add(BenefitLibrary.Soc);

            Cash[0] = 1000;
            Cash[1] = 2000;
            Cash[2] = 5000;
            Cash[3] = 5000;
            Cash[4] = 5000;
            Cash[5] = 10000;
            Cash[6] = 20000;

            Ranks[0] = "Trooper";
            Ranks[1] = "Lieutenant";
            Ranks[2] = "Captain";
            Ranks[3] = "Major";
            Ranks[4] = "Lt. Colonel";
            Ranks[5] = "Colonel";
            Ranks[6] = "General";
        }
        protected override void CommsionSkill()
        {
            Owner.AddSkill(SkillLibrary.SubmachineGun);
        }

        protected override int EnlistFactor()
        {
            return 0;
        }

        protected override void EnlistSkill()
        {
            Owner.AddSkill(SkillLibrary.Rifle);
        }

        protected override void RankSkill()
        {
            // Nothing to do
        }

        public override Character Owner
        {
            get => base.Owner;
            set
            {
                base.Owner = value;
                if (Owner.Profile.Soc.Value >= 10)
                {
                    Ranks[0] = "Subaltern";
                }
            }
        }
    }
}
