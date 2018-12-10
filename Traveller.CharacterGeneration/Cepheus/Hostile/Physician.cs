namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Hostile
{
    public class Physician : Cepheus.Career
    {
        public Physician()
        {
            Name = Resources.Career_Physician;
            hasRanks = true;

            enlistment = 9;
            enlistmentattr = "INT";
            survival = 3;
            survivalattr = "INT";
            position = 6;
            positionattr = "EDU";
            promotion = 8;
            promotionattr = "EDU";
            reenlist = 4;
            medicalBand = 2;

            Ranks[0] = Resources.Rank_MedicalStudent;
            Ranks[1] = Resources.Rank_Intern;
            Ranks[2] = Resources.Rank_JuniorDoctor;
            Ranks[3] = Resources.Rank_Doctor;
            Ranks[4] = Resources.Rank_Doctor;
            Ranks[5] = Resources.Rank_Consultant;
            Ranks[6] = Resources.Rank_SeniorConsultant;

            Material.Add(BenefitLibrary.StandardTicket);
            Material.Add(CharacterGeneration.BenefitLibrary.Edu);
            Material.Add(CharacterGeneration.BenefitLibrary.Edu);
            Material.Add(BenefitLibrary.EliteTicket);
            Material.Add(BenefitLibrary.TraumaKit);
            Material.Add(BenefitLibrary.EliteTicket);
            Material.Add(CharacterGeneration.BenefitLibrary.Soc);

            Cash[0] = 500;
            Cash[1] = 1000;
            Cash[2] = 1000;
            Cash[3] = 5000;
            Cash[4] = 8000;
            Cash[5] = 10000;
            Cash[6] = 20000;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Cepheus.Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Str;
            skills[1] = CharacterGeneration.SkillLibrary.Dex;
            skills[2] = CharacterGeneration.SkillLibrary.End;
            skills[3] = CharacterGeneration.SkillLibrary.Int;
            skills[4] = CharacterGeneration.SkillLibrary.Edu;
            skills[5] = CharacterGeneration.SkillLibrary.Soc;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Cepheus.Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Dex;
            skills[1] = CharacterGeneration.SkillLibrary.Electronics;
            skills[2] = CharacterGeneration.SkillLibrary.Medic;
            skills[3] = CharacterGeneration.SkillLibrary.Streetwise;
            skills[4] = CharacterGeneration.SkillLibrary.Medic;
            skills[5] = SkillLibrary.Investigate;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Cepheus.Resources.Table_Specialist;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Liason;
            skills[1] = SkillLibrary.Investigate;
            skills[2] = CharacterGeneration.SkillLibrary.Mechanical;
            skills[3] = CharacterGeneration.SkillLibrary.Electronics;
            skills[4] = CharacterGeneration.SkillLibrary.Computer;
            skills[5] = CharacterGeneration.SkillLibrary.Admin;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Cepheus.Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Liason;
            skills[1] = CharacterGeneration.SkillLibrary.Medic;
            skills[2] = CharacterGeneration.SkillLibrary.Admin;
            skills[3] = CharacterGeneration.SkillLibrary.Computer;
            skills[4] = CharacterGeneration.SkillLibrary.Int;
            skills[5] = CharacterGeneration.SkillLibrary.Edu;
        }
        protected override void EnlistSkill()
        {
            Owner.AddSkill(CharacterGeneration.SkillLibrary.Medic);
        }

        protected override void RankSkill()
        {
            if (RankNumber == 1 || RankNumber == 2)
            {
                Owner.AddSkill(CharacterGeneration.SkillLibrary.Medic);
            }
        }

        protected override SurvivalResult ResolveMishap()
        {
            SurvivalResult survive = SurvivalResult.Survived;
            switch (dice.roll(1))
            {
                case 1:
                    Owner.Journal.Add(Resources.Mishap_Physician1);
                    lostBenefits = true;
                    survive = SurvivalResult.Discharged;
                    break;
                case 2:
                    Owner.Journal.Add(Resources.Mishap_Physician2);
                    survive = SurvivalResult.Discharged;
                    break;
                case 3:
                    Owner.Journal.Add(Resources.Mishap_Physician3);
                    Owner.Profile.Soc.Value--;
                    break;
                case 4:
                    Owner.Journal.Add(Resources.Mishap_Physician4);
                    survive = SurvivalResult.Discharged;
                    break;
                case 5:
                    Owner.Journal.Add(Resources.Mishap_Physician5);
                    survive = SurvivalResult.Discharged;
                    break;
                case 6:
                    Owner.Journal.Add(Resources.Mishap_Physician6);
                    ResolveInjury(0);
                    break;
            }
            return survive;
        }
    }
}
