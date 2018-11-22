using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Vargr
{
    public class Corsair : Career
    {
        public enum Mode
        {
            Vargr,
            Imperial
        }

        public Corsair(Mode mode)
        {
            CareerMode = mode;

            Name = Resources.Career_Corsair;

            enlistment = 6;
            enlistment1attr = "END";
            enlistment1val = 9;
            enlistment2attr = "DEX";
            enlistment2val = 7;
            survival = 6;
            survival2attr = "END";
            survival2val = 9;
            position = 8;
            position1attr = "CHR";
            position1val = 7;
            promotion1attr = "INT";
            promotion1val = 8;
            promotion2attr = "CHR";
            promotion2val = 5;
            reenlist = 6;

            maxRank = 6;

            RankNumber = 0;
            TermSkills = 2;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Properties.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Dex;
            skills[2] = SkillLibrary.End;
            skills[3] = SkillLibrary.Bribery;
            skills[4] = SkillLibrary.Infighting;
            skills[5] = SkillLibrary.Chr;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Properties.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.Vehicle;
            skills[1] = SkillLibrary.VaccSuit;
            skills[2] = SkillLibrary.Infighting;
            skills[3] = SkillLibrary.Medic;
            skills[4] = SkillLibrary.GunCombat;
            skills[5] = SkillLibrary.GunCombat;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Properties.Resources.Table_Education;
            skills = table.Skills;
            skills[0] = SkillLibrary.ShipsBoat;
            skills[1] = SkillLibrary.VaccSuit;
            skills[2] = SkillLibrary.Electronics;
            skills[3] = SkillLibrary.Medic;
            skills[4] = SkillLibrary.Computer;
            skills[5] = SkillLibrary.Gunnery;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Resources.Table_HighCharisma;
            skills = table.Skills;
            skills[0] = SkillLibrary.Medic;
            skills[1] = SkillLibrary.Chr;
            skills[2] = SkillLibrary.Leader;
            skills[3] = SkillLibrary.FowardObserver;
            skills[4] = SkillLibrary.Pilot;
            skills[5] = SkillLibrary.Streetwise;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.Gun);
            Material.Add(BenefitLibrary.Gun);
            Material.Add(BenefitLibrary.Gun);
            Material.Add(BenefitLibrary.Corsair);

            Cash[0] = 1000;
            Cash[1] = 1000;
            Cash[2] = 10000;
            Cash[3] = 10000;
            Cash[4] = 30000;
            Cash[5] = 30000;
            Cash[6] = 50000;

            Ranks[0] = "Corsair";
            Ranks[1] = Resources.Rank_Lieutenant;
            Ranks[2] = "Force Leader";
            Ranks[3] = "Staff Major";
            Ranks[4] = "Group Leader";
            Ranks[5] = "Commodore";
            Ranks[6] = "Leader";
        }

        public Mode CareerMode { get; private set; }

        protected override void CommsionSkill()
        {
            if (!doneOnce && RankNumber == 1)
            {
                Owner.AddSkill(SkillLibrary.ShipsBoat);
                doneOnce = true;
            }
        }

        protected override int EnlistFactor()
        {
            var target = 0;
            if (CareerMode == Mode.Imperial)
            {
                target += 4;
            }
            return target;
        }

        protected override void EnlistSkill()
        {
            // Nothing to do
        }

        protected override void RankSkill()
        {
            // Nothing to do
        }
    }
}
