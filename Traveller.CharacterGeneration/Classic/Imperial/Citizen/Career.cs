using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Imperial.Citizen
{
    /// <summary>
    /// All Citizen of the imperium careern follow a forumula
    /// that means a lot of code is common
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

        protected abstract void EnlistSkill();
        protected abstract void RankSkill();
        protected abstract int EnlistFactor();
        protected abstract void CommsionSkill();

        public override bool Commission()
        {
            if (!hasRanks)
            {
                return false;
            }
            if (CurrentRank == 0)
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
                        CurrentRank = 1;
                        CommsionSkill();
                        TermSkills += 1;
                        Owner.Journal.Add(string.Format("Commissioned as {0}", Ranks[CurrentRank]));
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

        public override Renlistment CanRenlist()
        {
            var renlist = Renlistment.Cant;

            var target = reenlist;

            renlist = BaseCanRenlist(renlist, target);
            return renlist;
        }

        public override void HandleRenlist(bool renlisted)
        {
            BaseRenlist(renlisted);
        }

        public override bool Promotion()
        {
            var promote = false;

            if (CurrentRank > 0 && CurrentRank < 6 && hasRanks)
            {
                var target = promotion;
                if (Owner.Profile[promotion1attr].Value >= promotion1val)
                {
                    target--;
                }
                if (dice.roll(2) >= target)
                {
                    promote = true;
                    CurrentRank++;
                    RankSkill();
                    TermSkills++;
                    Owner.Journal.Add(string.Format("Promoted to {0}", Ranks[CurrentRank]));
                }
            }
            return promote;
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
    }
}
