namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Aslan
{
    public class Pirate : Career
    {
        public Pirate()
        {
            Name = "Pirate";
            TermSkills = 2;

            enlistment = 8;

            survival = 9;
            survival3attr = "INT";
            survival3val = 8;
            position = 9;
            position1attr = "STR";
            position1val = 10;
            promotion = 8;
            promotion1attr = "INT";
            promotion1val = 8;
            reenlist = 7;

            maxRank = 5;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = "Personal Development";
            var skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Independance;
            skills[1] = CharacterGeneration.SkillLibrary.Str;
            skills[2] = CharacterGeneration.SkillLibrary.Dex;
            skills[3] = CharacterGeneration.SkillLibrary.End;
            skills[4] = CharacterGeneration.SkillLibrary.ZeroGCombat;
            skills[5] = CharacterGeneration.SkillLibrary.Navigation;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = "Service Skills";
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Gunnery;
            skills[1] = CharacterGeneration.SkillLibrary.DewClaw;
            skills[2] = CharacterGeneration.SkillLibrary.Vehicle;
            skills[3] = CharacterGeneration.SkillLibrary.Leader;
            skills[4] = CharacterGeneration.SkillLibrary.Tolerance;
            skills[5] = CharacterGeneration.SkillLibrary.Trader;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = "Advanced Service Skills";
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Gunnery;
            skills[1] = CharacterGeneration.SkillLibrary.Engineering;
            skills[2] = CharacterGeneration.SkillLibrary.Medic;
            skills[3] = SkillLibrary.GunCombat;
            skills[4] = CharacterGeneration.SkillLibrary.Gambling;
            skills[5] = CharacterGeneration.SkillLibrary.Forgery;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = "Experience";
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Pilot;
            skills[1] = CharacterGeneration.SkillLibrary.ShipsBoat;
            skills[2] = CharacterGeneration.SkillLibrary.Computer;
            skills[3] = CharacterGeneration.SkillLibrary.VaccSuit;
            skills[4] = CharacterGeneration.SkillLibrary.JackOfTrades;
            skills[5] = CharacterGeneration.SkillLibrary.Streetwise;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.Independance);
            Material.Add(BenefitLibrary.Independance);
            Material.Add(BenefitLibrary.Escort);

            Cash[0] = 5000;
            Cash[1] = 10000;
            Cash[2] = 20000;
            Cash[3] = 20000;
            Cash[4] = 40000;
            Cash[5] = 70000;
            Cash[6] = 100000;

            Ranks[0] = "Pirate";
            Ranks[1] = "Soldier";
            Ranks[2] = "Warrior";
            Ranks[3] = "Veteran";
            Ranks[4] = "Lieutenant";
            Ranks[5] = "Commandant";
        }

        protected override int EnlistFactor()
        {
            return 0;
        }

        protected override void CommsionSkill()
        {
            
        }

        protected override void EnlistSkill()
        {
            Owner.AddSkill(CharacterGeneration.SkillLibrary.DewClaw);
        }

        protected override void RankSkill()
        {
            if (RankNumber == 4)
            {
                Owner.AddSkill(CharacterGeneration.SkillLibrary.Pilot);
            }
        }

        public override void CheckTableAvailablity()
        {

        }
    }
}
