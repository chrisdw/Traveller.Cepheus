using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Zhodani
{
    public class Navy : Career
    {
        public Navy()
        {
            Name = "Navy";
            RankNumber = 0;
            TermSkills = 2;

            enlistment = 8;
            enlistment1attr = "INT";
            enlistment1val = 8;
            enlistment2attr = "EDU";
            enlistment2val = 6;
            enlistment3attr = string.Empty;
            enlistment3val = 0;
            survival = 5;
            survival2attr = "INT";
            survival2val = 7;
            position = 12;
            position1attr = "INT";
            position1val = 10;
            position3attr = "SOC";
            position3val = 10;
            promotion = 9;
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
            skills[3] = SkillLibrary.Int;
            skills[4] = SkillLibrary.Edu;
            skills[5] = SkillLibrary.VaccSuit;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = "Service Skills";
            skills = table.Skills;
            skills[0] = SkillLibrary.Gunnery;
            skills[1] = SkillLibrary.Mechanical;
            skills[2] = SkillLibrary.Electronics;
            skills[3] = SkillLibrary.ShipsBoat;
            skills[4] = SkillLibrary.BladeCombat;
            skills[5] = SkillLibrary.GunCombat;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = "Education";
            skills = table.Skills;
            skills[0] = SkillLibrary.Medic;
            skills[1] = SkillLibrary.Engineering;
            skills[2] = SkillLibrary.Navigation;
            skills[3] = SkillLibrary.Pilot;
            skills[4] = SkillLibrary.Computer;
            skills[5] = SkillLibrary.Admin;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = "Noble/Intendant Education";
            skills = table.Skills;
            skills[0] = SkillLibrary.Telepathy;
            skills[1] = SkillLibrary.Psi;
            skills[2] = SkillLibrary.Talent;
            skills[3] = SkillLibrary.Psi;
            skills[4] = SkillLibrary.Clairvoyance;
            skills[5] = SkillLibrary.Telepathy;

            Material.Add(BenefitLibrary.Soc);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.Legion);
            Material.Add(BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.Soc);

            Cash[0] = 1000;
            Cash[1] = 1000;
            Cash[2] = 2000;
            Cash[3] = 5000;
            Cash[4] = 10000;
            Cash[5] = 20000;
            Cash[6] = 20000;

            Ranks[0] = "Spacer";
            Ranks[1] = "Ensign";
            Ranks[2] = "Lieutenant";
            Ranks[3] = "Lt Commander";
            Ranks[4] = "Commander";
            Ranks[5] = "Captain";
            Ranks[6] = "Admiral";
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
                Owner.Profile.Soc.Value += 1;
            }
        }

        public override Character Owner
        {
            get => base.Owner;
            set
            {
                base.Owner = value;
                if (Owner.Profile.Soc.Value >= 10)
                {
                    Ranks[0] = "Midshipman";
                }
            }
        }
    }
}
