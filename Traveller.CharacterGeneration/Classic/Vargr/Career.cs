using org.DownesWard.Utilities;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Vargr
{
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
        protected string promotion2attr = "CHR";
        protected int promotion2val = 5;
        protected int reenlist = 7;
        protected bool hasRanks = true;
        private readonly SuccessEntry[] successTable = new SuccessEntry[10];
        private bool dismissed;
        protected int maxRank;
        protected bool doneOnce = false;

        protected abstract void EnlistSkill();
        protected abstract void RankSkill();
        protected abstract int EnlistFactor();
        protected abstract void CommsionSkill();

        public override void HandleRenlist(bool renlisted)
        {
            BaseRenlist(renlisted);
        }

        public override void CheckTableAvailablity()
        {
            SkillTables[3].Available = (Owner.Profile["CHR"].Value >= 8);
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

        public override bool Promotion()
        {
            var drm = 0;
            var target = promotion;
            if (Owner.Profile[promotion1attr].Value >= promotion1val)
            {
                drm++;
            }
            if (Owner.Profile[promotion2attr].Value >= promotion2val)
            {
                drm++;
            }
            dismissed = CheckForFailure(drm);
            if (!dismissed)
            {
                RankSkill();
            }
            return !dismissed;
        }
        public override bool Survival()
        {
            var survive = false;

            var target = survival;

            if (Owner.Profile[survival2attr].Value >= survival2val)
            {
                target -= 2;
            }
            if (dice.roll(2) >= target)
            {
                survive = true;
            }
            return survive;
        }

        public override Renlistment CanRenlist()
        {
            var renlist = Renlistment.Cant;
            if (!dismissed)
            {
                var target = reenlist;

                renlist = BaseCanRenlist(renlist, target);
            }
            else
            {
                Owner.Journal.Add(string.Format("Dismissed from {0} at end of term {1}", Name, Term));
                Retired = false;
            }
            return renlist;
        }

        private bool CheckForFailure(int drm)
        {
            var result = (dice.roll(2) + drm - 2).Clamp(0, 11);
            var success = successTable[result];
            Owner.Profile["CHR"].Value += success.CharismaChange;
            if (success.CharismaChange < 0)
            {
                Owner.Journal.Add(string.Format("Your charisma was redued by {0} due to failure", success.CharismaChange));
            }
            else if (success.CharismaChange > 0)
            {
                Owner.Journal.Add(string.Format("Your charisma was increased by {0} due to success", success.CharismaChange));
                TermSkills++;
            }
            if (hasRanks)
            {
                RankNumber = (RankNumber + success.RankChange).Clamp(0, maxRank);
                if (success.RankChange < 0)
                {
                    Owner.Journal.Add(string.Format("You were demoted to {0}", RankName));
                }
                else
                {
                    Owner.Journal.Add(string.Format("You were promted to {0}", RankName));
                }
            }
            return success.Dismissed;
        }

        public Career()
        {
            successTable[0] = new SuccessEntry(-2, 0, true);
            successTable[1] = new SuccessEntry(-1, 0, true);
            successTable[2] = new SuccessEntry(0, -1, false);
            successTable[3] = new SuccessEntry(0, 0, false);
            successTable[4] = new SuccessEntry(0, 0, false);
            successTable[5] = new SuccessEntry(0, 0, false);
            successTable[6] = new SuccessEntry(0, 1, false);
            successTable[7] = new SuccessEntry(0, 1, false);
            successTable[8] = new SuccessEntry(1, 1, false);
            successTable[9] = new SuccessEntry(1, 2, false);
            successTable[9] = new SuccessEntry(2, 2, false);
        }

        public override int MusterOutRolls()
        {
            var rolls = TermsServed;
            if (RankNumber == 1 || RankNumber == 2)
            {
                rolls++;
            }
            else if (RankNumber == 3 || RankNumber == 4)
            {
                rolls += 2;
            }
            else if (RankNumber > 5)
            {
                rolls += 3;
            }
            return rolls;
        }
    }
}
