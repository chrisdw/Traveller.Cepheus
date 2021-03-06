﻿namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus
{
    public class AerospaceDefence : Career
    {
        public AerospaceDefence()
        {
            Name = Resources.Career_AerospaceDefence;
            hasRanks = true;

            enlistment = 5;
            enlistmentattr = "END";
            survival = 5;
            survivalattr = "DEX";
            position = 6;
            positionattr = "EDU";
            promotion = 7;
            promotionattr = "EDU";
            reenlist = 5;
            medicalBand = 1;

            Material.Add(CharacterGeneration.BenefitLibrary.LowPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.Edu);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(CharacterGeneration.BenefitLibrary.MidPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.Weapon);
            Material.Add(CharacterGeneration.BenefitLibrary.HighPsg);
            Material.Add(CharacterGeneration.BenefitLibrary.Soc);

            Cash[0] = 1000;
            Cash[1] = 5000;
            Cash[2] = 10000;
            Cash[3] = 10000;
            Cash[4] = 20000;
            Cash[5] = 50000;
            Cash[6] = 50000;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Str;
            skills[1] = CharacterGeneration.SkillLibrary.Dex;
            skills[2] = CharacterGeneration.SkillLibrary.End;
            skills[3] = SkillLibrary.Athletics;
            skills[4] = SkillLibrary.MeleeCombat;
            skills[5] = SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Mechanical;
            skills[1] = SkillLibrary.GunCombat;
            skills[2] = SkillLibrary.Gunnery;
            skills[3] = SkillLibrary.MeleeCombat;
            skills[4] = CharacterGeneration.SkillLibrary.Survival;
            skills[5] = SkillLibrary.Aircraft;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Resources.Table_Specialist;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Communications;
            skills[1] = CharacterGeneration.SkillLibrary.Computer;
            skills[2] = CharacterGeneration.SkillLibrary.Electronics;
            skills[3] = SkillLibrary.Gunnery;
            skills[4] = CharacterGeneration.SkillLibrary.Recon;
            skills[5] = CharacterGeneration.SkillLibrary.Pilot;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = SkillLibrary.Advocate;
            skills[1] = CharacterGeneration.SkillLibrary.Gravitics;
            skills[2] = CharacterGeneration.SkillLibrary.JackOfTrades;
            skills[3] = CharacterGeneration.SkillLibrary.Medic;
            skills[4] = CharacterGeneration.SkillLibrary.Leader;
            skills[5] = CharacterGeneration.SkillLibrary.Tactics;

            Ranks[0] = Resources.Rank_Airman;
            Ranks[1] = Resources.Rank_FlightOfficer;
            Ranks[2] = Resources.Rank_FlightLieutenant;
            Ranks[3] = Resources.Rank_SquadronLeader;
            Ranks[4] = Resources.Rank_WingCommander;
            Ranks[5] = Resources.Rank_GroupCaptain;
            Ranks[6] = Resources.Rank_AirCommodore;
        }

        protected override void EnlistSkill()
        {
            OnSkillOffered(SkillLibrary.Aircraft);
        }

        protected override void RankSkill()
        {
            if (RankNumber == 3)
            {
                Owner.AddSkill(CharacterGeneration.SkillLibrary.Leader);
            }
        }
    }
}
