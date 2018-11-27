using System;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Aslan
{
    public class Wanderer : Career
    {
        protected override void CommsionSkill()
        {
            Name = "Wanderer";
            hasRanks = false;
            Array.Resize(ref skillTables, 3);

            enlistment = 8;
            survival = 9;
            survival2attr = string.Empty;
            survival3attr = "INT";
            survival3val = 8;

            reenlist = 5;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = "Personal Development";
            var skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Independance;
            skills[1] = CharacterGeneration.SkillLibrary.Str;
            skills[2] = CharacterGeneration.SkillLibrary.Dex;
            skills[3] = CharacterGeneration.SkillLibrary.End;
            skills[4] = SkillLibrary.GunCombat;
            skills[5] = null;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = "Service Skills";
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Independance;
            skills[1] = CharacterGeneration.SkillLibrary.Vehicle;
            skills[2] = CharacterGeneration.SkillLibrary.Gunnery;
            skills[3] = CharacterGeneration.SkillLibrary.Engineering;
            skills[4] = SkillLibrary.PersonalWeapons;
            skills[5] = null;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = "Service Skills (Male)";
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Pilot;
            skills[1] = CharacterGeneration.SkillLibrary.ShipsBoat;
            skills[2] = SkillLibrary.GunCombat;
            skills[3] = CharacterGeneration.SkillLibrary.VaccSuit;
            skills[4] = CharacterGeneration.SkillLibrary.Hunting;
            skills[5] = CharacterGeneration.SkillLibrary.Tolerance;

            Material.Add(BenefitLibrary.Nothing);
            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Edu2);
            Material.Add(BenefitLibrary.Independance);
            Material.Add(BenefitLibrary.Toleranace);
            Material.Add(BenefitLibrary.Scout);
            Material.Add(BenefitLibrary.Land);

            Cash[0] = 0;
            Cash[1] = 0;
            Cash[2] = 0;
            Cash[3] = 2000;
            Cash[4] = 5000;
            Cash[5] = 10000;
            Cash[6] = 20000;

            Ranks[0] = "Wanderer";
        }

        protected override void EnlistSkill()
        {
            Owner.AddSkill(CharacterGeneration.SkillLibrary.Pilot);
        }

        protected override void RankSkill()
        {
            
        }
        public override void EndTerm()
        {
            base.EndTerm();
            TermSkills = 3;
        }
    }
}
