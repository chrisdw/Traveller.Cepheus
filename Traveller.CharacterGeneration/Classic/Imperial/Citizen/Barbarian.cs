namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Imperial.Citizen
{
    public class Barbarian : Career
    {
        public Barbarian()
        {
            Name = "Barbarian";
            RankNumber = 0;
            TermSkills = 2;

            enlistment = 5;
            enlistment1attr = "END";
            enlistment1val = 9;
            enlistment2attr = "STR";
            enlistment2val = 10;
            survival = 6;
            survival2attr = "STR";
            survival2val = 8;
            position = 6;
            position1attr = "STR";
            position1val = 10;
            promotion = 9;
            promotion1attr = "INT";
            promotion1val = 6;
            reenlist = 6;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Properties.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Str2;
            skills[2] = SkillLibrary.Str;
            skills[3] = SkillLibrary.Carousing;
            skills[4] = SkillLibrary.Dex;
            skills[5] = SkillLibrary.End;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Properties.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.Brawling;
            skills[1] = SkillLibrary.BladeCombat;
            skills[2] = SkillLibrary.BladeCombat;
            skills[3] = SkillLibrary.BowCombat;
            skills[4] = SkillLibrary.BowCombat;
            skills[5] = SkillLibrary.GunCombat;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Properties.Resources.Table_Education;
            skills = table.Skills;
            skills[0] = SkillLibrary.BladeCombat;
            skills[1] = SkillLibrary.Mechanical;
            skills[2] = SkillLibrary.Survival;
            skills[3] = SkillLibrary.Recon;
            skills[4] = SkillLibrary.Streetwise;
            skills[5] = SkillLibrary.BowCombat;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Properties.Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = SkillLibrary.Medic;
            skills[1] = SkillLibrary.Interrogation;
            skills[2] = SkillLibrary.Tactics;
            skills[3] = SkillLibrary.Leader;
            skills[4] = SkillLibrary.Instruction;
            skills[5] = SkillLibrary.JackOfTrades;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Blade);
            Material.Add(BenefitLibrary.Blade);
            Material.Add(BenefitLibrary.Blade);
            Material.Add(BenefitLibrary.Nothing);
            Material.Add(BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.HighPsg);

            Cash[0] = 0;
            Cash[1] = 0;
            Cash[2] = 1000;
            Cash[3] = 2000;
            Cash[4] = 3000;
            Cash[5] = 4000;
            Cash[6] = 5000;

            Ranks[0] = string.Empty;
            Ranks[1] = string.Empty;
            Ranks[2] = "Warrior";
            Ranks[3] = string.Empty;
            Ranks[4] = string.Empty;
            Ranks[5] = "Chief";
            Ranks[6] = string.Empty;
        }
        protected override void CommsionSkill()
        {
            RankNumber = 2;
            OnSkillOffered(SkillLibrary.BladeCombat);
        }

        protected override int EnlistFactor()
        {
            return 0;
        }

        protected override void EnlistSkill()
        {
            Owner.AddSkill(SkillLibrary.Sword);
        }

        protected override void RankSkill()
        {
            RankNumber = 5;
            Owner.AddSkill(SkillLibrary.Leader);
        }

        public override Character Owner
        {
            get => base.Owner;
            set
            {
                base.Owner = value;
                if (Owner.Careers.Count == 0)
                {
                    Owner.Age = 14;
                }
            }
        }
    }
}
