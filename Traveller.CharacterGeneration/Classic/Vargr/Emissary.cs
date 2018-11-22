using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Vargr
{
    public class Emissary : Career
    {
        public Emissary()
        {

            Name = Resources.Career_Emissary;

            enlistment = 8;
            enlistment1attr = "EDU";
            enlistment1val = 7;
            enlistment2attr = "CHR";
            enlistment2val = 6;
            survival = 4;
            survival2attr = "CHR";
            survival2val = 8;

            reenlist = 5;

            hasRanks = false;

            RankNumber = 0;
            TermSkills = 2;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Properties.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Dex;
            skills[2] = SkillLibrary.End;
            skills[3] = SkillLibrary.Int;
            skills[4] = SkillLibrary.Edu;
            skills[5] = SkillLibrary.Chr;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Properties.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.Vehicle;
            skills[1] = SkillLibrary.ShipsBoat;
            skills[2] = SkillLibrary.VaccSuit;
            skills[3] = SkillLibrary.Infighting;
            skills[4] = SkillLibrary.GunCombat;
            skills[5] = SkillLibrary.BladeCombat;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Properties.Resources.Table_Education;
            skills = table.Skills;
            skills[0] = SkillLibrary.Streetwise;
            skills[1] = SkillLibrary.Admin;
            skills[2] = SkillLibrary.Liason;
            skills[3] = SkillLibrary.Navigation;
            skills[4] = SkillLibrary.Computer;
            skills[5] = SkillLibrary.Medic;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Resources.Table_HighCharisma;
            skills = table.Skills;
            skills[0] = SkillLibrary.Medic;
            skills[1] = SkillLibrary.Liason;
            skills[2] = SkillLibrary.Liason;
            skills[3] = SkillLibrary.Leader;
            skills[4] = SkillLibrary.Chr;
            skills[5] = SkillLibrary.Chr;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.Blade);
            Material.Add(BenefitLibrary.Blade);
            Material.Add(BenefitLibrary.HighPsg);

            Cash[0] = 10000;
            Cash[1] = 20000;
            Cash[2] = 30000;
            Cash[3] = 40000;
            Cash[4] = 40000;
            Cash[5] = 50000;
            Cash[6] = 50000;

            Ranks[0] = Resources.Rank_Emissary;
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
            Owner.AddSkill(SkillLibrary.Liason);
        }

        protected override void RankSkill()
        {
           // Nothing to do
        }

        public override void EndTerm()
        {
            base.EndTerm();
            TermSkills = 2;
        }

        public override bool Drafted
        {
            get => base.Drafted;
            set
            {
                base.Drafted = value;
                if (value)
                {
                    Owner.AddSkill(SkillLibrary.Liason);
                }
            }
        }
    }
}
