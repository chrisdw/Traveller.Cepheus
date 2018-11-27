namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Aslan
{
    public class Outcast : Career
    {
        public Outcast()
        {
            Name = "Outcast";
            hasRanks = false;

            RankNumber = 0;
            TermSkills = 3;

            survival = 8;
            survival2attr = string.Empty;
            survival3attr = "EDU";
            survival3val = 8;

            reenlist = 4;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = "Personal Development";
            var skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Independance;
            skills[1] = CharacterGeneration.SkillLibrary.Str;
            skills[2] = CharacterGeneration.SkillLibrary.Dex;
            skills[3] = CharacterGeneration.SkillLibrary.End;
            skills[4] = CharacterGeneration.SkillLibrary.Tolerance;
            skills[5] = CharacterGeneration.SkillLibrary.JackOfTrades;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = "Service Skills";
            skills = table.Skills;
            skills[0] = SkillLibrary.GunCombat;
            skills[1] = CharacterGeneration.SkillLibrary.DewClaw;
            skills[2] = CharacterGeneration.SkillLibrary.Vehicle;
            skills[3] = SkillLibrary.GunCombat;
            skills[4] = CharacterGeneration.SkillLibrary.Gambling;
            skills[5] = CharacterGeneration.SkillLibrary.Steward;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = "Service Skills (Female)";
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Bribery;
            skills[1] = CharacterGeneration.SkillLibrary.Mechanical;
            skills[2] = CharacterGeneration.SkillLibrary.Forgery;
            skills[3] = CharacterGeneration.SkillLibrary.Electronics;
            skills[4] = CharacterGeneration.SkillLibrary.Streetwise;
            skills[5] = CharacterGeneration.SkillLibrary.JackOfTrades;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = "Service Skills (Male)";
            skills = table.Skills;
            skills[0] = SkillLibrary.PersonalWeapons;
            skills[1] = CharacterGeneration.SkillLibrary.ShipsBoat;
            skills[2] = CharacterGeneration.SkillLibrary.Independance;
            skills[3] = SkillLibrary.GunCombat;
            skills[4] = CharacterGeneration.SkillLibrary.DewClaw;
            skills[5] = CharacterGeneration.SkillLibrary.JackOfTrades;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.Gun);
            Material.Add(BenefitLibrary.Independance);
            Material.Add(BenefitLibrary.Soc);
            Material.Add(BenefitLibrary.MidPsg);

            Cash[0] = 1000;
            Cash[1] = 1000;
            Cash[2] = 5000;
            Cash[3] = 5000;
            Cash[4] = 10000;
            Cash[5] = 20000;
            Cash[6] = 40000;

            Ranks[0] = "Outcast";
        }
        protected override void CommsionSkill()
        {
            
        }

        protected override void EnlistSkill()
        {
            if (Owner.Sex.Equals(Properties.Resources.Sex_Male))
            {
                Owner.AddSkill(CharacterGeneration.SkillLibrary.Independance);
            }
        }

        protected override void RankSkill()
        {
            
        }

        public override void CheckTableAvailablity()
        {
            SkillTables[2].Available = Owner.Sex.Equals(Properties.Resources.Sex_Female);
            SkillTables[3].Available = Owner.Sex.Equals(Properties.Resources.Sex_Male);
        }
    }
}
