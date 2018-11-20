﻿namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Darrian
{
    public class SpecialArm : Imperial.Citizen.Career
    {
        public SpecialArm()
        {
            Name = Resources.Career_SpecialArm;
            RankNumber = 0;
            TermSkills = 2;

            enlistment = 10;
            enlistment1attr = "DEX";
            enlistment1val = 9;
            enlistment2attr = string.Empty;
            enlistment2val = 0;
            enlistment3attr = "INT";
            enlistment3val = 10;
            survival = 6;
            survival2attr = "EDU";
            survival2val = 9;
            position = 9;
            position1attr = "INT";
            position1val = 8;
            position2attr = string.Empty;
            position2val = 0;
            promotion = 10;
            promotion1attr = "SOC";
            promotion1val = 10;
            reenlist = 5;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Properties.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Dex;
            skills[2] = SkillLibrary.End;
            skills[3] = SkillLibrary.GunCombat;
            skills[4] = SkillLibrary.Edu;
            skills[5] = SkillLibrary.BladeCombat;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Properties.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.Vehicle;
            skills[1] = SkillLibrary.VaccSuit;
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
            table.Name = Properties.Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = SkillLibrary.Medic;
            skills[1] = SkillLibrary.Navigation;
            skills[2] = SkillLibrary.Engineering;
            skills[3] = SkillLibrary.Computer;
            skills[4] = SkillLibrary.Pilot;
            skills[5] = SkillLibrary.Pilot;

            Material.Add(BenefitLibrary.Soc);
            Material.Add(BenefitLibrary.Int2);
            Material.Add(BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.Voucher);
            Material.Add(BenefitLibrary.Voucher);
            Material.Add(BenefitLibrary.Soc2);

            Cash[0] = 1000;
            Cash[1] = 2000;
            Cash[2] = 2000;
            Cash[3] = 5000;
            Cash[4] = 10000;
            Cash[5] = 20000;
            Cash[6] = 30000;

            Ranks[0] = Resources.Rank_Spacer;
            Ranks[1] = Resources.Rank_Ensign;
            Ranks[2] = Resources.Rank_Lieutenant;
            Ranks[3] = Resources.Rank_LtCommander;
            Ranks[4] = Resources.Rank_Commander;
            Ranks[5] = Resources.Rank_Captain;
            Ranks[6] = Resources.Rank_Admiral;
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
            // Nothing to do
        }

        protected override void RankSkill()
        {
            if (RankNumber == 4)
            {
                Owner.AddSkill(SkillLibrary.Leader);
            }
            else if (RankNumber == 6)
            {
                Owner.Profile.Soc.Value++;
            }
        }

        public override void CheckTableAvailablity()
        {
            SkillTables[3].Available = (Owner.Profile["SOC"].Value >= 10);
        }
    }
}
