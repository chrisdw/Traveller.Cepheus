﻿namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus
{
    public class Mercenary : Career
    {
        public Mercenary()
        {
            Name = Resources.Career_Mercenary;
            hasRanks = true;

            enlistment = 4;
            enlistmentattr = "INT";
            survival = 6;
            survivalattr = "END";
            position = 7;
            positionattr = "INT";
            promotion = 6;
            promotionattr = "INT";
            reenlist = 5;
            medicalBand = 2;

            Ranks[0] = Resources.Rank_Private;
            Ranks[1] = Resources.Rank_Lieutenant;
            Ranks[2] = Resources.Rank_Captain;
            Ranks[3] = Resources.Rank_Major;
            Ranks[4] = Resources.Rank_LtColonel;
            Ranks[5] = Resources.Rank_Colonel;
            Ranks[6] = Resources.Rank_Brigadier;

            Material.Add(CharacterGeneration.BenefitLibrary.LowPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.Int);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(CharacterGeneration.BenefitLibrary.HighPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.Soc);
            Material.Add(CharacterGeneration.BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.ShipShares);

            Cash[0] = 1000;
            Cash[1] = 5000;
            Cash[2] = 10000;
            Cash[3] = 20000;
            Cash[4] = 20000;
            Cash[5] = 50000;
            Cash[6] = 100000;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Str;
            skills[1] = CharacterGeneration.SkillLibrary.Dex;
            skills[2] = CharacterGeneration.SkillLibrary.End;
            skills[3] = CharacterGeneration.SkillLibrary.ZeroGCombat;
            skills[4] = SkillLibrary.MeleeCombat;
            skills[5] = CharacterGeneration.SkillLibrary.Gambling;
            
            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Communications;
            skills[1] = CharacterGeneration.SkillLibrary.Mechanical;
            skills[2] = SkillLibrary.GunCombat;
            skills[3] = SkillLibrary.MeleeCombat;
            skills[4] = CharacterGeneration.SkillLibrary.Gambling;
            skills[5] = CharacterGeneration.SkillLibrary.BattleDress;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Resources.Table_Specialist;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Gravitics;
            skills[1] = SkillLibrary.GunCombat;
            skills[2] = SkillLibrary.Gunnery;
            skills[3] = SkillLibrary.MeleeCombat;
            skills[4] = CharacterGeneration.SkillLibrary.Recon;
            skills[5] = SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = SkillLibrary.Advocate;
            skills[1] = CharacterGeneration.SkillLibrary.Engineering;
            skills[2] = CharacterGeneration.SkillLibrary.Medic;
            skills[3] = CharacterGeneration.SkillLibrary.Navigation;
            skills[4] = SkillLibrary.Sciences;
            skills[5] = CharacterGeneration.SkillLibrary.Tactics;
        }


        protected override void EnlistSkill()
        {
            OnSkillOffered(SkillLibrary.GunCombat);
        }

        protected override void RankSkill()
        {
            if (RankNumber == 3)
            {
                Owner.AddSkill(CharacterGeneration.SkillLibrary.Tactics);
            }
        }
    }
}
