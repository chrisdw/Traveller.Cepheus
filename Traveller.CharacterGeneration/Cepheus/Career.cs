using System;
using System.Collections.Generic;
using System.Text;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus
{
    public abstract class Career : BasicCareer
    {
        protected int enlistment = 7;
        protected string enlistmentattr = "SOC";
        protected int survival = 6;
        protected string survivalattr = "INT";
        protected int position = 9;
        protected string positionattr = "STR";
        protected int promotion = 8;
        protected string promotionattr = "INT";
        protected int reenlist = 7;
        protected bool hasRanks = true;
        protected int maxRank = 6;

        protected abstract void EnlistSkill();
        protected abstract void RankSkill();

        protected abstract void CommsionSkill();

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
                    target -= Owner.Profile[positionattr].Modifier;

                    if (dice.roll(2) >= target)
                    {
                        RankNumber = 1;
                        CommsionSkill();
                        TermSkills += 1;
                        Owner.Journal.Add(string.Format(Resources.Prompt_Commissioned, Ranks[RankNumber]));
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

            target -= Owner.Profile[enlistmentattr].Modifier;
            target += (Owner.Careers.Count * 2);

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

            if (RankNumber > 0 && RankNumber < maxRank && hasRanks)
            {
                var target = promotion;
                target -= Owner.Profile[promotionattr].Modifier;

                if (dice.roll(2) >= target)
                {
                    promote = true;
                    RankNumber++;
                    RankSkill();
                    TermSkills++;
                    Owner.Journal.Add(string.Format(Resources.Prompt_Promoted, Ranks[RankNumber]));
                }
            }
            return promote;
        }

        public override bool Survival()
        {
            var survive = false;

            var target = survival;
            target += Owner.Profile[survivalattr].Modifier;

            if (dice.roll(2) >= target)
            {
                survive = true;
            }
            return survive;
        }
    }
}
