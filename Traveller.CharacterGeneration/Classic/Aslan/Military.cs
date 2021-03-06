﻿using System;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Aslan
{
    public class Military : Career
    {
        public bool Officer { get; set; }

        public Military()
        {
            Name = Resources.Career_Military;
            TermSkills = 2;

            Array.Resize(ref skillTables, 8);

            // Militiary tables
            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = Resources.Table_PersonalDevelopment;
            var skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Independance;
            skills[1] = CharacterGeneration.SkillLibrary.Str;
            skills[2] = CharacterGeneration.SkillLibrary.Dex;
            skills[3] = CharacterGeneration.SkillLibrary.End;
            skills[4] = CharacterGeneration.SkillLibrary.DewClaw;
            skills[5] = CharacterGeneration.SkillLibrary.Admin;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.GunCombat;
            skills[1] = CharacterGeneration.SkillLibrary.Vehicle;
            skills[2] = CharacterGeneration.SkillLibrary.Hunting;
            skills[3] = SkillLibrary.PersonalWeapons; 
            skills[4] = SkillLibrary.GunCombat;
            skills[5] = CharacterGeneration.SkillLibrary.FowardObserver;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = Resources.Table_ServiceSkillsFemale;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Steward;
            skills[1] = CharacterGeneration.SkillLibrary.Mechanical;
            skills[2] = CharacterGeneration.SkillLibrary.Electronics;
            skills[3] = CharacterGeneration.SkillLibrary.Medic;
            skills[4] = CharacterGeneration.SkillLibrary.Computer;
            skills[5] = SkillLibrary.GunCombat;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name =Resources.Table_ServiceSkillsMale;
            skills = table.Skills;
            skills[0] = SkillLibrary.GunCombat;
            skills[1] = SkillLibrary.GunCombat;
            skills[2] = CharacterGeneration.SkillLibrary.Vehicle;
            skills[3] = CharacterGeneration.SkillLibrary.Vehicle;
            skills[4] = CharacterGeneration.SkillLibrary.Leader;
            skills[5] = CharacterGeneration.SkillLibrary.Tactics;

            // Military officer tables
            table = new SkillTable();
            SkillTables[4] = table;
            table.Name = Resources.Table_PersonalDevelopment;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Independance;
            skills[1] = CharacterGeneration.SkillLibrary.Str;
            skills[2] = CharacterGeneration.SkillLibrary.Dex;
            skills[3] = CharacterGeneration.SkillLibrary.End;
            skills[4] = CharacterGeneration.SkillLibrary.Int;
            skills[5] = CharacterGeneration.SkillLibrary.Tolerance;

            table = new SkillTable();
            SkillTables[5] = table;
            table.Name = Resources.Table_ServiceSkills;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Tactics;
            skills[1] = CharacterGeneration.SkillLibrary.Vehicle;
            skills[2] = CharacterGeneration.SkillLibrary.DewClaw;
            skills[3] = CharacterGeneration.SkillLibrary.Tolerance;
            skills[4] = SkillLibrary.GunCombat;
            skills[5] = CharacterGeneration.SkillLibrary.JackOfTrades;

            table = new SkillTable();
            SkillTables[6] = table;
            table.Name = Resources.Table_ServiceSkillsFemale;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Admin;
            skills[1] = CharacterGeneration.SkillLibrary.FowardObserver;
            skills[2] = CharacterGeneration.SkillLibrary.Computer;
            skills[3] = CharacterGeneration.SkillLibrary.Medic;
            skills[4] = CharacterGeneration.SkillLibrary.Admin;
            skills[5] = CharacterGeneration.SkillLibrary.Streetwise;

            table = new SkillTable();
            SkillTables[7] = table;
            table.Name = Resources.Table_ServiceSkillsMale;
            skills = table.Skills;
            skills[0] = CharacterGeneration.SkillLibrary.Independance;
            skills[1] = CharacterGeneration.SkillLibrary.Leader;
            skills[2] = SkillLibrary.GunCombat;
            skills[3] = CharacterGeneration.SkillLibrary.Tolerance;
            skills[4] = CharacterGeneration.SkillLibrary.Leader;
            skills[5] = CharacterGeneration.SkillLibrary.Tactics;
        }
        protected override void CommsionSkill()
        {
            Officer = true;
            Reconfigure();

            if (Owner.Profile.Soc.Value < 9)
            {
                Owner.Profile.Soc.Value = 9;
                Owner.Journal.Add(Resources.Msg_Commision);
            }
            if (Owner.Sex.Equals(Properties.Resources.Sex_Male))
            {
                Owner.AddSkill(CharacterGeneration.SkillLibrary.Tactics);
            }
            else
            {
                Owner.AddSkill(CharacterGeneration.SkillLibrary.Admin);
            }
            RankNumber = 1;
        }

        protected override void EnlistSkill()
        {
            if (Owner.Sex.Equals(Properties.Resources.Sex_Male))
            {
                Owner.AddSkill(CharacterGeneration.SkillLibrary.DewClaw);
            }
            else
            {
                Owner.AddSkill(SkillLibrary.Uealikhe);
            }
        }

        protected override void RankSkill()
        {

        }

        public override void CheckTableAvailablity()
        {
            SkillTables[0].Available = !Officer;
            SkillTables[1].Available = !Officer;
            SkillTables[2].Available = !Officer && Owner.Sex.Equals(Properties.Resources.Sex_Female);
            SkillTables[3].Available = !Officer && Owner.Sex.Equals(Properties.Resources.Sex_Male);

            SkillTables[4].Available = Officer;
            SkillTables[5].Available = Officer;
            SkillTables[6].Available = Officer && Owner.Sex.Equals(Properties.Resources.Sex_Female);
            SkillTables[7].Available = Officer && Owner.Sex.Equals(Properties.Resources.Sex_Male);
        }

        public override Character Owner
        {
            get => base.Owner;
            set
            {
                base.Owner = value;

                Ranks[0] = Resources.Rank_Trooper;
                Ranks[1] = Resources.Rank_Cadet;
                Ranks[2] = Resources.Rank_Lieutenant;
                Ranks[3] = Resources.Rank_Captain;

                if (Owner.Sex.Equals(Properties.Resources.Sex_Female))
                {
                    Ranks[4] = Resources.Rank_Executive;
                    Ranks[5] = Resources.Rank_SeniorExecutive;
                    Ranks[6] = Resources.Rank_ChiefOfStaff;
                }
                else
                {
                    Ranks[4] = Resources.Rank_Commandant;
                    Ranks[5] = Resources.Rank_SeniorCommandant;
                    Ranks[6] = Resources.Rank_General;
                }

                Reconfigure();

                if (TermsServed == 0)
                {
                    if (dice.roll(2) < Owner.Profile.Edu.Value)
                    {
                        TermSkills++;
                    }
                    if (dice.roll(2) < Owner.Profile.Edu.Value)
                    {
                        TermSkills++;
                    }
                }
            }
        }

        private void Reconfigure()
        {
            if (Owner.Sex.Equals(Properties.Resources.Sex_Female))
            {
                position = 9;
                position1attr = "SOC";
                position1val = 9;
                if (Officer)
                {
                    enlistment = 9;
                    reenlist = 6;
                    survival3attr = "INT";
                    survival3val = 9;
                }
                else
                {
                    enlistment = 7;
                    reenlist = 5;
                    survival3attr = "EDU";
                    survival3val = 7;
                }
                survival = 8;
            }
            else
            {
                position = 11;
                position1attr = "SOC";
                position1val = 9;

                if (Officer)
                {
                    enlistment = 10;
                    reenlist = 5;
                    survival = 8;
                    survival3attr = "EDU";
                    survival3val = 6;
                }
                else
                {
                    enlistment = 7;
                    reenlist = 4;
                    survival = 7;
                    survival3attr = "END";
                    survival3val = 8;
                }
            }
        }

        public override void MusterOut()
        {
            if (Officer)
            {
                Cash[0] = 1000;
                Cash[1] = 1000;
                Cash[2] = 5000;
                Cash[3] = 5000;
                Cash[4] = 10000;
                Cash[5] = 20000;
                Cash[6] = 50000;

                Material.Add(BenefitLibrary.MidPsg);
                Material.Add(BenefitLibrary.Int);
                Material.Add(BenefitLibrary.Edu2);
                Material.Add(BenefitLibrary.Gun);
                Material.Add(BenefitLibrary.Independance);
                Material.Add(BenefitLibrary.MidPsg);
                Material.Add(BenefitLibrary.Soc);
                Material.Add(BenefitLibrary.Land);
            }
            else
            {
                Cash[0] = 500;
                Cash[1] = 500;
                Cash[2] = 1000;
                Cash[3] = 1000;
                Cash[4] = 5000;
                Cash[5] = 5000;
                Cash[6] = 10000;

                Material.Add(BenefitLibrary.LowPsg);
                Material.Add(BenefitLibrary.Int);
                Material.Add(BenefitLibrary.Edu);
                Material.Add(BenefitLibrary.Gun);
                Material.Add(BenefitLibrary.Weapon);
                Material.Add(BenefitLibrary.MidPsg);
                Material.Add(BenefitLibrary.Soc);
                Material.Add(BenefitLibrary.Land);
            }
            if (Owner.Sex.Equals(Properties.Resources.Sex_Male))
            {
                for (var i = 0; i < Cash.Length; i++)
                {
                    Cash[i] /= 2;
                }
            }
        }
    }
}
