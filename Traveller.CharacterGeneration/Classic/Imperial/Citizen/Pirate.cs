using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Imperial.Citizen
{
    public class Pirate : Career
    {
        public Pirate()
        {
            Name = "Pirate";
            RankNumber = 0;
            TermSkills = 2;

            enlistment = 7;
            enlistment1attr = "SOC";
            enlistment1val = 7;
            enlistment2attr = "END";
            enlistment2val = 9;
            survival = 6;
            survival2attr = "INT";
            survival2val = 8;
            position = 9;
            position1attr = "STR";
            position1val = 10;
            promotion = 8;
            promotion1attr = "INT";
            promotion1val = 9;
            reenlist = 7;

            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = "Personal Development";
            var skills = table.Skills;
            skills[0] = SkillLibrary.Str;
            skills[1] = SkillLibrary.Dex;
            skills[2] = SkillLibrary.End;
            skills[3] = SkillLibrary.Gambling;
            skills[4] = SkillLibrary.Gambling;
            skills[5] = SkillLibrary.BladeCombat;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = "Service Skills";
            skills = table.Skills;
            skills[0] = SkillLibrary.BladeCombat;
            skills[1] = SkillLibrary.VaccSuit;
            skills[2] = SkillLibrary.GunCombat;
            skills[3] = SkillLibrary.Gunnery;
            skills[4] = SkillLibrary.ZeroGCombat;
            skills[5] = SkillLibrary.GunCombat;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = "Education";
            skills = table.Skills;
            skills[0] = SkillLibrary.Streetwise;
            skills[1] = SkillLibrary.Gunnery;
            skills[2] = SkillLibrary.Engineering;
            skills[3] = SkillLibrary.ShipTactics;
            skills[4] = SkillLibrary.Tactics;
            skills[5] = SkillLibrary.GunCombat;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = "Advanced Education";
            skills = table.Skills;
            skills[0] = SkillLibrary.Navigation;
            skills[1] = SkillLibrary.Pilot;
            skills[2] = SkillLibrary.Forgery;
            skills[3] = SkillLibrary.Computer;
            skills[4] = SkillLibrary.Leader;
            skills[5] = SkillLibrary.Electronics;


            Material.Add(BenefitLibrary.LowPsg);
            Material.Add(BenefitLibrary.Int);
            Material.Add(BenefitLibrary.Weapon);
            Material.Add(BenefitLibrary.Nothing);
            Material.Add(BenefitLibrary.Soc);
            Material.Add(BenefitLibrary.MidPsg);
            Material.Add(BenefitLibrary.Corsair);

            Cash[0] = 0;
            Cash[1] = 0;
            Cash[2] = 1000;
            Cash[3] = 10000;
            Cash[4] = 50000;
            Cash[5] = 50000;
            Cash[6] = 50000;

            Ranks[0] = "Pirate";
            Ranks[1] = "Henchman";
            Ranks[2] = "Corporal";
            Ranks[3] = "Sergeant";
            Ranks[4] = "Lieutenant";
            Ranks[5] = "Leader";
        }

        protected override void CommsionSkill()
        {
            
        }

        protected override int EnlistFactor()
        {
            var target = 0;
            if (Owner.CharacterSpecies == Character.Species.AelYael)
            {
                target += 1;
            }
            else if (Owner.CharacterSpecies == Character.Species.Aslan )
            {
                target += 1;
            }
            return target;
        }

        protected override void EnlistSkill()
        {
            Owner.AddSkill(SkillLibrary.Brawling);
        }

        protected override void RankSkill()
        {
            if (RankNumber == 4)
            {
                Owner.AddSkill(SkillLibrary.Pilot);
            }
        }
    }
}
