using System;
using System.Linq;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Hostile
{
    public class Android : Career
    {
        private int useTable = 0;

        public Android()
        {
            hasRanks = false;

            Array.Resize(ref skillTables, 5);

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = string.Empty;
            var skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Pilot;
            skills[1] = CharacterGeneration.SkillLibrary.Engineering;
            skills[2] = CharacterGeneration.SkillLibrary.Navigation;
            skills[3] = Cepheus.SkillLibrary.Gunnery;
            skills[4] = CharacterGeneration.SkillLibrary.Computer;
            skills[5] = CharacterGeneration.SkillLibrary.Communications;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = string.Empty;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Pilot;
            skills[1] = CharacterGeneration.SkillLibrary.Engineering;
            skills[2] = CharacterGeneration.SkillLibrary.Navigation;
            skills[3] = CharacterGeneration.SkillLibrary.Communications;
            skills[4] = CharacterGeneration.SkillLibrary.Survival;
            skills[5] = SkillLibrary.Vehicle;
        }

        protected override void EnlistSkill()
        {
            var terms = dice.roll() + 3;
            TermsServed = terms;
            for (var i = 0; i < terms; i++)
            {
                if (dice.roll() >= 5)
                {
                    var skill = SkillTables[useTable].Skills[dice.roll() - 1];
                    var total = Owner.Skills.Values.Sum(a => a.Level);
                    if (total < 6)
                    {
                        var current = Owner.Skills.Values.Where(a => a.Name.Equals(skill.Name)).Sum(a => a.Level);
                        if (current < 3)
                        {
                            if (skill.Cascade.Count == 0)
                            {
                                Owner.AddSkill(skill);
                            }
                            else
                            {
                                OnSkillOffered(skill);
                            }
                        }
                    }
                }
            }
        }

        protected override void RankSkill()
        {
            
        }

        public override bool Enlist()
        {
            switch (dice.roll())
            {
                case 1:
                    Name = Resources.Career_CommercialSpacer;
                    Owner.AddSkill(CharacterGeneration.SkillLibrary.Pilot);
                    useTable = 0;
                    break;
                case 2:
                    Name = Resources.Career_MilitarySpacer;
                    Owner.AddSkill(CharacterGeneration.SkillLibrary.Pilot);
                    useTable = 0;
                    break;
                case 3:
                    Name = Resources.Career_SurveyScout;
                    Owner.AddSkill(CharacterGeneration.SkillLibrary.Pilot);
                    useTable = 1;
                    break;
                case 4:
                    Name = Resources.Career_Physician;
                    Owner.AddSkill(CharacterGeneration.SkillLibrary.Medic);
                    useTable = 2;
                    break;
                case 5:
                    Name = Resources.Career_Scientist;
                    Owner.AddSkill(SkillLibrary.Investigate);
                    useTable = 3;
                    break;
                case 6:
                    Name = Resources.Career_Technician;
                    Owner.AddSkill(CharacterGeneration.SkillLibrary.Engineering);
                    useTable = 4;
                    break;
            }
            return true;
        }

        public override Renlistment CanRenlist()
        {
            return Renlistment.Cant;
        }
    }
}
