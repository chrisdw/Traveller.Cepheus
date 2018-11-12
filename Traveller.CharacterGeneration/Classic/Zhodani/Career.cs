using System;
using System.Linq;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Zhodani
{
    /// <summary>
    /// Looks very much like Citizen Career but with the added comlications of Psionics
    /// and Psionic games
    /// </summary>
    public abstract class Career : BasicCareer
    {
        protected int enlistment = 7;
        protected string enlistment1attr = "SOC";
        protected int enlistment1val = 7;
        protected string enlistment2attr = "END";
        protected int enlistment2val = 9;
        protected string enlistment3attr = string.Empty;
        protected int enlistment3val = 0;
        protected int survival = 6;
        protected string survival2attr = "INT";
        protected int survival2val = 8;
        protected int position = 9;
        protected string position1attr = "STR";
        protected int position1val = 10;
        protected string position2attr = string.Empty;
        protected int position2val = 0;
        protected string position3attr = string.Empty;
        protected int position3val = 0;
        protected int promotion = 8;
        protected string promotion1attr = "INT";
        protected int promotion1val = 9;
        protected int reenlist = 7;
        protected bool hasRanks = true;
        private bool doneGames = false;
        private bool doingGames = false;
        private bool trained = false;

        protected abstract void EnlistSkill();
        protected abstract void RankSkill();
        protected abstract int EnlistFactor();
        protected abstract void CommsionSkill();

        public event EventHandler PsionicGamesOffered;
        public event EventHandler PsionicTrainingOffered;

        public override Renlistment CanRenlist()
        {
            var renlist = Renlistment.Cant;
            if (doingGames)
            {
                renlist = Renlistment.Must;
            }
            else
            {
                var target = reenlist;

                renlist = BaseCanRenlist(renlist, target);
            }
            return renlist;
        }

        public override bool Commission()
        {
            if (!hasRanks)
            {
                return false;
            }
            if (RankNumber == 0)
            {
                if (Term == 0 && Drafted)
                {
                    return false;
                }
                else
                {
                    var target = position;
                    if (Owner.Profile[position1attr].Value >= position1val)
                    {
                        target--;
                    }
                    if (!string.IsNullOrEmpty(position2attr))
                    {
                        if (Owner.Profile[position2attr].Value >= position2val)
                        {
                            target -= 2;
                        }
                    }
                    if (!string.IsNullOrEmpty(position3attr))
                    {
                        if (Owner.Profile[position3attr].Value >= position3val)
                        {
                            target -= 3;
                        }
                    }
                    if (dice.roll(2) >= target)
                    {
                        RankNumber = 1;
                        CommsionSkill();
                        TermSkills += 1;
                        Owner.Journal.Add(string.Format("Commissioned as {0}", Ranks[RankNumber]));
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return true;
            }
        }

        public override bool Enlist()
        {
            var target = enlistment;
            target += EnlistFactor();

            if (Owner.Profile[enlistment1attr].Value <= enlistment1val)
            {
                target--;
            }
            if (!string.IsNullOrEmpty(enlistment2attr))
            {
                if (Owner.Profile[enlistment2attr].Value <= enlistment2val)
                {
                    target -= 2;
                }
            }
            if (!string.IsNullOrEmpty(enlistment3attr))
            {
                if (Owner.Profile[enlistment3attr].Value <= enlistment3val)
                {
                    target -= 3;
                }
            }
            var enlist = BaseEnlist(target);

            if (enlist)
            {
                EnlistSkill();
            }
            return enlist;
        }

        public override void HandleRenlist(bool renlisted)
        {
            if (doingGames)
            {
                doingGames = false;
                doneGames = true;
            }
            else
            {
                BaseRenlist(renlisted);
            }
        }

        public override bool Promotion()
        {
            var promote = false;

            if (!doingGames && RankNumber > 0 && RankNumber < 6 && hasRanks)
            {
                var target = promotion;
                if (Owner.Profile[promotion1attr].Value >= promotion1val)
                {
                    target--;
                }
                if (dice.roll(2) >= target)
                {
                    promote = true;
                    RankNumber++;
                    RankSkill();
                    TermSkills++;
                    Owner.Journal.Add(string.Format("Promoted to {0}", Ranks[RankNumber]));
                }
            }
            return promote;
        }

        public void PsionicGames()
        {
            doingGames = true;
        }


        public override bool Survival()
        {
            var survive = false;

            if (!doneGames && Owner.Profile.Soc.Value == 10)
            {
                if (dice.roll(3) <= Owner.Profile["PSI"].Value)
                {
                    // Find out if they want to attend the psionic games
                    EventArgs e = new EventArgs();
                    PsionicGamesOffered?.Invoke(this, e);
                }
            }
            if (doingGames)
            {
                ResoveGames();
                survive = true;
                TermSkills = 0;
            }
            else
            {
                var target = survival;

                if (Owner.Profile[survival2attr].Value >= survival2val)
                {
                    target -= 2;
                }
                if (dice.roll(2) >= target)
                {
                    survive = true;
                }
            }
            return survive;
        }

         public override void CheckTableAvailablity()
        {
            SkillTables[3].Available = (Owner.Profile.Soc.Value >= 10);
        }

        public override void EndTerm()
        {
            base.EndTerm();
            if (!trained)
            {
                Owner.Journal.Add("PSI reduced by 1 at end of term as not trained.");
                Owner.Profile["PSI"].Value--;
            }
        }

        public override bool Drafted
        {
            get => base.Drafted;
            set
            {
                base.Drafted = value;
                if (value)
                {
                    EnlistSkill();
                }
            }
        }

        public override Character Owner {
            get => base.Owner;
            set
            {
                base.Owner = value;
                if (Owner.Profile.Soc.Value > 10)
                {
                    for (int benefit = 0; benefit < Cash.Length; benefit++)
                    {
                        Cash[benefit] *= 2;
                    }
                }
                if (Owner.Profile.Soc.Value >= 10)
                {
                    DoPsionicTraining();
                }
            }
        }

        private void ResoveGames()
        {
            var firstRound = 0;
            // first round
            foreach (var skill in Owner.Skills.Values.Where(x => x.Class == Skill.SkillClass.Psionic))
            {
                if (skill.Level < dice.roll(3))
                {
                    firstRound++;
                }
                else
                {
                    firstRound--;
                }
            }
            if (firstRound > 0)
            {
                Owner.Profile["PSI"].Value++;
                Owner.Journal.Add("PSI Raised by one due to success in Psionic games");
            }
            // 2nd round
            if (dice.roll(2) - firstRound < 12)
            {
                Owner.Profile["PSI"].Value += 2;
                Owner.Journal.Add("PSI Raised by two due to winning the Psionic games");
                Owner.Profile.Soc.Value = 11;
                Owner.Journal.Add("Enobled due to winning the Psionic games");
            }
        }

        private void DoPsionicTraining()
        {
            if (Owner.Skills.Values.Any(x => x.Class == Skill.SkillClass.Psionic))
            {
                trained = true;
                return;
            }
            // TODO: Get the user to pick talents
            trained = true;
        }
    }
}