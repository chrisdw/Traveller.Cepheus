using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Aslan
{
    public class Space : Imperial.Citizen.Career
    {
        public Space()
        {
            TermSkills = 2;

            Array.Resize(ref skillTables, 8);
            enlistment1attr = string.Empty;
            enlistment2attr = string.Empty;
            enlistment3attr = string.Empty;

            // Spacer tables
            var table = new SkillTable();
            SkillTables[0] = table;
            table.Name = "Personal Development";
            var skills = table.Skills;
            skills[0] = SkillLibrary.Independance;
            skills[1] = SkillLibrary.Str;
            skills[2] = SkillLibrary.Dex;
            skills[3] = SkillLibrary.End;
            skills[4] = SkillLibrary.Int;
            skills[5] = SkillLibrary.Admin;

            table = new SkillTable();
            SkillTables[1] = table;
            table.Name = "Service Skills";
            skills = table.Skills;
            skills[0] = SkillLibrary.Gunnery;
            skills[1] = SkillLibrary.VaccSuit;
            skills[2] = SkillLibrary.GunCombat;
            skills[3] = SkillLibrary.DewClaw;
            skills[4] = SkillLibrary.Vehicle;
            skills[5] = SkillLibrary.Steward;

            table = new SkillTable();
            SkillTables[2] = table;
            table.Name = "Service Skills (Female)";
            skills = table.Skills;
            skills[0] = SkillLibrary.Computer;
            skills[1] = SkillLibrary.Mechanical;
            skills[2] = SkillLibrary.Electronics;
            skills[3] = SkillLibrary.Medic;
            skills[4] = SkillLibrary.Engineering;
            skills[5] = SkillLibrary.JackOfTrades;

            table = new SkillTable();
            SkillTables[3] = table;
            table.Name = "Service Skills (Male)";
            skills = table.Skills;
            skills[0] = SkillLibrary.Gunnery;
            skills[1] = SkillLibrary.ShipsBoat;
            skills[2] = SkillLibrary.ShipsBoat;
            skills[3] = SkillLibrary.Pilot;
            skills[4] = SkillLibrary.Leader;
            skills[5] = SkillLibrary.VaccSuit;

            // Space officer tables
            table = new SkillTable();
            SkillTables[4] = table;
            table.Name = "Personal Development";
            skills = table.Skills;
            skills[0] = SkillLibrary.Independance;
            skills[1] = SkillLibrary.Str;
            skills[2] = SkillLibrary.Dex;
            skills[3] = SkillLibrary.End;
            skills[4] = SkillLibrary.Int;
            skills[5] = SkillLibrary.Navigation;

            table = new SkillTable();
            SkillTables[5] = table;
            table.Name = "Service Skills";
            skills = table.Skills;
            skills[0] = SkillLibrary.Pilot;
            skills[1] = SkillLibrary.VaccSuit;
            skills[2] = SkillLibrary.GunCombat;
            skills[3] = SkillLibrary.DewClaw;
            skills[4] = SkillLibrary.Vehicle;
            skills[5] = SkillLibrary.Computer;

            table = new SkillTable();
            SkillTables[6] = table;
            table.Name = "Service Skills (Female)";
            skills = table.Skills;
            skills[0] = SkillLibrary.Computer;
            skills[1] = SkillLibrary.Tolerance;
            skills[2] = SkillLibrary.Navigation;
            skills[3] = SkillLibrary.Medic;
            skills[4] = SkillLibrary.Engineering;
            skills[5] = SkillLibrary.JackOfTrades;

            table = new SkillTable();
            SkillTables[7] = table;
            table.Name = "Service Skills (Male)";
            skills = table.Skills;
            skills[0] = SkillLibrary.Gunnery;
            skills[1] = SkillLibrary.Pilot;
            skills[2] = SkillLibrary.Leader;
            skills[3] = SkillLibrary.Leader;
            skills[4] = SkillLibrary.Tolerance;
            skills[5] = SkillLibrary.Hunting;

        }
        private bool officer = false;

        public bool Officer { get; set; }

        protected override void CommsionSkill()
        {
            Officer = true;
            if (Owner.Profile.Soc.Value < 9)
            {
                Owner.Profile.Soc.Value = 9;
                Owner.Journal.Add("SOC Raised to 9 due to commission");
            }
            if (Owner.Sex.Equals(Properties.Resources.Sex_Male))
            {
                Owner.AddSkill(SkillLibrary.Leader);
            }
            else
            {
                Owner.AddSkill(SkillLibrary.Admin);
            }
            RankNumber = 1;
        }

        protected override int EnlistFactor()
        {
            var ac = Culture as Aslan.Culture;
            return ac.ROPScore * -1;
        }

        protected override void EnlistSkill()
        {
            if (Officer)
            {
                RankNumber = 1;
            }
        }

        protected override void RankSkill()
        {

        }

        public override int MaxCashRolls()
        {
            if (Owner.Sex.Equals(Properties.Resources.Sex_Female))
            {
                return MusterOutRolls();
            }
            else
            {
                if (Owner.Skills.ContainsKey(SkillLibrary.Independance.Name))
                {
                    return Owner.Skills[SkillLibrary.Independance.Name].Level;
                }
                else
                {
                    return 0;
                }
            }
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
                var ac = Culture as Aslan.Culture;
                ac.CalculateROP(Owner);

                Owner.Journal.Add(string.Format("Rite of Passage Score is {0}", ac.ROPScore));

                Ranks[0] = "Spacer";
                Ranks[1] = "Intendant";
                Ranks[2] = "Lieutenant";
                Ranks[3] = "Senior Lieutenant";

                if (Owner.Sex.Equals(Properties.Resources.Sex_Female))
                {
                    Ranks[4] = "Executive Lieutenant";
                    Ranks[5] = "Executive";
                    Ranks[6] = "Chief of Staff";
                    position = 10;
                    position1attr = "SOC";
                    position1val = 9;
                    if (officer)
                    {
                        enlistment = 10;
                        reenlist = 6;
                    }
                    else
                    {
                        enlistment = 8;
                        reenlist = 5;
                    }
                    survival = 7;
                    survival3attr = "INT";
                    survival3val = 8;
                }
                else
                {
                    Ranks[4] = "Commandant Lieutenant";
                    Ranks[5] = "Captain";
                    Ranks[6] = "Admiral";
                    position = 11;
                    position1attr = "SOC";
                    position1val = 9;

                    if (officer)
                    {
                        enlistment = 11;
                        reenlist = 6;
                        survival = 8;
                        survival3attr = "INT";
                        survival3val = 8;
                    }
                    else
                    {
                        enlistment = 8;
                        reenlist = 5;
                        survival = 7;
                        survival3attr = "INT";
                        survival3val = 7;
                    }
                }

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

        public override void MusterOut()
        {
            if (Officer)
            {
                Cash[0] = 1000;
                Cash[1] = 5000;
                Cash[2] = 5000;
                Cash[3] = 10000;
                Cash[4] = 20000;
                Cash[5] = 50000;
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
                Cash[1] = 1000;
                Cash[2] = 1000;
                Cash[3] = 5000;
                Cash[4] = 5000;
                Cash[5] = 10000;
                Cash[6] = 20000;

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

        public override void EndTerm()
        {
            Term += 1;
            TermsServed += 1;
            Owner.Age += 4;
            TermSkills = 2;
            if (dice.roll(2) < Owner.Profile.Edu.Value)
            {
                TermSkills++;
            }
        }

        public override int MusterOutRolls()
        {
            var rolls = TermsServed * 2;
            if (RankNumber == 1 || RankNumber == 2 || RankNumber == 3)
            {
                rolls++;
            }
            if (RankNumber == 4 || RankNumber == 5 || RankNumber == 6)
            {
                rolls += 2;
            }
            if (Owner.Profile.Soc.Value >= 9)
            {
                rolls++;
            }
            if (Owner.Sex.Equals(Properties.Resources.Sex_Male))
            {
                rolls++;
            }
            return rolls;
        }
    }
}
