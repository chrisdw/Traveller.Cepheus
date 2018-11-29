using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus
{
    public class Athlete : Career
    {
        public Athlete()
        {
            Name = "Athlete";
            hasRanks = false;

            enlistment = 8;
            enlistmentattr = "END";
            survival = 5;
            survivalattr = "DEX";
            reenlist = 6;

            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.HighPsg);
            Material.Add(BenefitLibrary.ExplorersSociety);
            Material.Add(BenefitLibrary.HighPsg);

            Cash[0] = 2000;
            Cash[1] = 10000;
            Cash[2] = 20000;
            Cash[3] = 20000;
            Cash[4] = 50000;
            Cash[5] = 100000;
            Cash[6] = 100000;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = SkillLibrary.Dex;
            skills[1] = SkillLibrary.Int;
            skills[2] = SkillLibrary.End;
            skills[3] = SkillLibrary.Soc;
            skills[4] = SkillLibrary.Carousing;
            skills[5] = SkillLibrary.Brawling; // TODO: Make Melee Combat

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = SkillLibrary.Athletics;
            skills[1] = SkillLibrary.Admin;
            skills[2] = SkillLibrary.Carousing;
            skills[3] = SkillLibrary.Computer;
            skills[4] = SkillLibrary.Gambling;
            skills[5] = SkillLibrary.Vehicle;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Resources.Table_Education;
            skills = table.Skills;
            skills[0] = SkillLibrary.ZeroGCombat;
            skills[1] = SkillLibrary.Athletics;
            skills[2] = SkillLibrary.Athletics;
            skills[3] = SkillLibrary.Computer;
            skills[4] = SkillLibrary.Leader;
            skills[5] = SkillLibrary.Gambling;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = Resources.Table_AdvancedEducation;
            skills = table.Skills;
            skills[0] = SkillLibrary.Admin; // TODO: Make Advocate
            skills[1] = SkillLibrary.Computer;
            skills[2] = SkillLibrary.Liason;
            skills[3] = SkillLibrary.Computer; // TODO: Make linguistics
            skills[4] = SkillLibrary.Medic;
            skills[5] = SkillLibrary.JackOfTrades; // TODI: Make Sciences

            Ranks[0] ="Athlete";
        }
        protected override void CommsionSkill()
        {
            
        }

        protected override void EnlistSkill()
        {
            throw new NotImplementedException();
        }

        protected override void RankSkill()
        {
            Owner.AddSkill(SkillLibrary.Athletics);
        }
    }
}
