namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Aslan
{
    public class Medic : Career
    {
        protected override void CommsionSkill()
        {
            Name = "Medic";
            hasRanks = false;

            enlistment = 9;

            survival = 6;
            survival2attr = string.Empty;
            survival3attr = "INT";
            survival3val = 8;

            reenlist = 4;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = "Personal Development";
            var skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Edu;
            skills[1] = CharacterGeneration.SkillLibrary.Str;
            skills[2] = CharacterGeneration.SkillLibrary.Dex;
            skills[3] = CharacterGeneration.SkillLibrary.End;
            skills[4] = CharacterGeneration.SkillLibrary.Int;
            skills[5] = CharacterGeneration.SkillLibrary.Edu;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = "Service Skills";
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Admin;
            skills[1] = CharacterGeneration.SkillLibrary.Mechanical;
            skills[2] = CharacterGeneration.SkillLibrary.Electronics;
            skills[3] = CharacterGeneration.SkillLibrary.Gravitics;
            skills[4] = CharacterGeneration.SkillLibrary.Medic;
            skills[5] = CharacterGeneration.SkillLibrary.Steward;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = "Advanced Service Skills";
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Medic;
            skills[1] = CharacterGeneration.SkillLibrary.Medic;
            skills[2] = CharacterGeneration.SkillLibrary.Medic;
            skills[3] = CharacterGeneration.SkillLibrary.Medic;
            skills[4] = CharacterGeneration.SkillLibrary.Computer;
            skills[5] = CharacterGeneration.SkillLibrary.Admin;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = "Experience";
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Computer;
            skills[1] = CharacterGeneration.SkillLibrary.Medic;
            skills[2] = CharacterGeneration.SkillLibrary.Admin;
            skills[3] = CharacterGeneration.SkillLibrary.Tolerance;
            skills[4] = CharacterGeneration.SkillLibrary.Liason;
            skills[5] = CharacterGeneration.SkillLibrary.Vehicle;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.Instruments);
            Material.Add(BenefitLibrary.MidPsg);
            Material.Add(BenefitLibrary.Corporation);

            Cash[0] = 5000;
            Cash[1] = 10000;
            Cash[2] = 10000;
            Cash[3] = 10000;
            Cash[4] = 20000;
            Cash[5] = 40000;
            Cash[6] = 70000;

            Ranks[0] = "Medic";
        }

        protected override void EnlistSkill()
        {
            Owner.AddSkill(CharacterGeneration.SkillLibrary.Medic);
        }

        protected override void RankSkill()
        {
            
        }

        public override void EndTerm()
        {
            base.EndTerm();
            TermSkills = 3;
        }

        public override string RankName
        {
            get
            {
                var medicSkill = Owner.Skills[CharacterGeneration.SkillLibrary.Medic.Name].Level;
                if (medicSkill >= 3)
                {
                    if (Owner.Profile.Dex.Value >= 8)
                    {
                        return "Surgeon";
                    }
                    else
                    {
                        return "Doctor";
                    }
                }
                return Ranks[RankNumber];
            }
        }
    }
}
