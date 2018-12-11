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

            Material.Add(BenefitLibrary.FiveThousandCredits);
            Material.Add(BenefitLibrary.OneThousandCredits);
            Material.Add(BenefitLibrary.StandardTicket);
            Material.Add(BenefitLibrary.StandardTicket);
            Material.Add(BenefitLibrary.OneThousandCredits);
            Material.Add(BenefitLibrary.FiveThousandCredits);

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

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = string.Empty;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Medic;
            skills[1] = CharacterGeneration.SkillLibrary.Medic;
            skills[2] = SkillLibrary.Investigate;
            skills[3] = CharacterGeneration.SkillLibrary.Electronics;
            skills[4] = CharacterGeneration.SkillLibrary.Computer;
            skills[5] = CharacterGeneration.SkillLibrary.Medic;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = string.Empty;
            skills = table.Skills;
            skills[0] = SkillLibrary.Investigate;
            skills[1] = CharacterGeneration.SkillLibrary.Computer;
            skills[2] = SkillLibrary.Investigate;
            skills[3] = CharacterGeneration.SkillLibrary.Computer;
            skills[4] = CharacterGeneration.SkillLibrary.Admin;
            skills[5] = SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[4] = table;
            table.Name = string.Empty;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Engineering;
            skills[1] = CharacterGeneration.SkillLibrary.Electronics;
            skills[2] = CharacterGeneration.SkillLibrary.Mechanical;
            skills[3] = CharacterGeneration.SkillLibrary.Communications;
            skills[4] = SkillLibrary.Security;
            skills[5] = CharacterGeneration.SkillLibrary.Computer;
        }

        public override void CheckTableAvailablity()
        {

        }

        protected override void EnlistSkill()
        {
            var terms = dice.roll() + 3;
            // Normal EndTerm will increment these by 1
            TermsServed = terms - 1;
            Term = terms - 1;
            Owner.Age = TermsServed * 4;
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
                    survival = 5;
                    survivalattr = "INT";
                    useTable = 0;
                    break;
                case 2:
                    Name = Resources.Career_MilitarySpacer;
                    Owner.AddSkill(CharacterGeneration.SkillLibrary.Pilot);
                    survival = 5;
                    survivalattr = "INT";
                    useTable = 0;
                    break;
                case 3:
                    Name = Resources.Career_SurveyScout;
                    Owner.AddSkill(CharacterGeneration.SkillLibrary.Pilot);
                    survival = 7;
                    survivalattr = "END";
                    useTable = 1;
                    break;
                case 4:
                    Name = Resources.Career_Physician;
                    Owner.AddSkill(CharacterGeneration.SkillLibrary.Medic);
                    survival = 3;
                    survivalattr = "INT";
                    useTable = 2;
                    break;
                case 5:
                    Name = Resources.Career_Scientist;
                    Owner.AddSkill(SkillLibrary.Investigate);
                    survival = 3;
                    survivalattr = "INT";
                    useTable = 3;
                    break;
                case 6:
                    Name = Resources.Career_Technician;
                    Owner.AddSkill(CharacterGeneration.SkillLibrary.Engineering);
                    survival = 4;
                    survivalattr = "INT";
                    useTable = 4;
                    break;
            }
            EnlistSkill();
            return true;
        }

        public override Renlistment CanRenlist()
        {
            return Renlistment.Cant;
        }

        public override int MaxCashRolls()
        {
            return 0;
        }

        protected override SurvivalResult ResolveMishap()
        {
            SurvivalResult survive = SurvivalResult.Survived;
            switch (dice.roll(1))
            {
                case 1:
                    Owner.Journal.Add(Resources.Mishap_Android1);
                    survive = SurvivalResult.Discharged;
                    break;
                case 2:
                    Owner.Journal.Add(Resources.Mishap_Android2);
                    survive = SurvivalResult.Discharged;
                    lostBenefits = true;
                    break;
                case 3:
                    Owner.Journal.Add(Resources.Mishap_Android3);
                    survive = SurvivalResult.Discharged;
                    break;
                case 4:
                    Owner.Journal.Add(Resources.Mishap_Android4);
                    survive = SurvivalResult.Discharged;
                    break;
                case 5:
                    Owner.Journal.Add(Resources.Mishap_InjuredOnDuty);
                    survive = SurvivalResult.Discharged;
                    ResolveInjury(0);
                    break;
                case 6:
                    Owner.Journal.Add(Resources.Mishap_Android6);
                    survive = SurvivalResult.Discharged;
                    lostBenefits = true;
                    ResolveInjury(0);
                    break;
            }
            return survive;
        }
    }
}
