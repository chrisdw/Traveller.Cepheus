using System.Linq;

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
        protected bool lostBenefits = false;
        protected int medicalBand = 1;

        public bool Mishaps { get; set; }

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
                if (Owner.Careers.Count == 0)
                {
                    // Get everything in the service skills table at level 0
                    var skills = SkillTables[1].Skills.Distinct();
                    foreach (var skill in skills)
                    {
                        var toAdd = skill.Clone();
                        toAdd.Level = 0;
                        Owner.AddSkill(toAdd);
                    }
                }
                else
                {
                    // offer the skills in the service skill table at level 0
                    var training = new Skill
                    {
                        Name = "Basic Training"
                    };
                    var skills = SkillTables[1].Skills.Distinct();
                    foreach (var skill in skills)
                    {
                        var toAdd = skill.Clone();
                        toAdd.Level = 0;
                        skill.Cascade.Add(toAdd);
                    }
                    OnSkillOffered(training);
                }
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

        public override int MusterOutRolls()
        {
            if (!lostBenefits)
            {
                return base.MusterOutRolls();
            }
            else
            {
                return 0;
            }
        }
        public override SurvivalResult Survival()
        {
            var survive = SurvivalResult.Died;

            var target = survival;
            target += Owner.Profile[survivalattr].Modifier;

            if (dice.roll(2) >= target)
            {
                survive = SurvivalResult.Survived;
            }
            else if (Mishaps)
            {
                survive = SurvivalResult.Survived;
                switch (dice.roll(1))
                {
                    case 1:
                        Owner.Journal.Add("Injured in action");
                        ResolveInjury(2);
                        break;
                    case 2:
                        Owner.Journal.Add("Honourably discharged from the service.");
                        survive = SurvivalResult.Discharged;
                        break;
                    case 3:
                        Owner.Journal.Add("Honourably discharged from the service after a legal battle.");
                        Owner.AddBenefit(
                            new Benefit()
                            {
                                Name = "Cash",
                                TypeOfBenefit = Benefit.BenefitType.Cash,
                                Value = -10000 }
                            );
                        survive = SurvivalResult.Discharged;
                        break;
                    case 4:
                        Owner.Journal.Add("Dishonourably discharged from the service.");
                        lostBenefits = true;
                        survive = SurvivalResult.Discharged;
                        break;
                    case 5:
                        Owner.Journal.Add("Dishonourably discharged from the service. Serve 4 years in prison");
                        Owner.Age += 4;
                        lostBenefits = true;
                        survive = SurvivalResult.Discharged;
                        break;
                    case 6:
                        Owner.Journal.Add("Medically discharged from the service.");
                        ResolveInjury(0);
                        survive = SurvivalResult.Discharged;
                        break;
                }
            }
            return survive;
        }

        private void ResolveInjury(int roll)
        {
            if (roll == 0)
            {
                roll = dice.roll(1);
            }
            var paid = 0;
            var loss = 0;
            var bill = 0;
            switch (roll)
            {
                case 1:
                    Owner.Journal.Add("Nearly Killed");
                    paid = MedicalBills();
                    loss = dice.roll();
                    bill = (loss + 4) * 5000;

                    if (paid != 100)
                    {
                        switch (dice.roll())
                        {
                            case 1:
                            case 2:
                                Owner.Profile.Str.Value -= loss;
                                Owner.Profile.Dex.Value -= 2;
                                Owner.Profile.End.Value -= 2;
                                break;
                            case 3:
                            case 4:
                                Owner.Profile.Dex.Value -= dice.roll();
                                Owner.Profile.Str.Value -= 2;
                                Owner.Profile.End.Value -= 2;
                                break;
                            case 5:
                            case 6:
                                Owner.Profile.End.Value -= dice.roll();
                                Owner.Profile.Str.Value -= 2;
                                Owner.Profile.Dex.Value -= 2;
                                break;
                        }
                        Owner.Journal.Add(string.Format("You incurred a bill of {0}cr, {1}% will be paid for you", bill, paid));
                    }
                    else
                    {
                        Owner.Journal.Add(string.Format("You incurred a bill of {0}cr, {1}% was paid for you", bill, paid));
                    }
                    break;
                case 2:
                    Owner.Journal.Add("Severly Injured");
                    paid = MedicalBills();
                    loss = dice.roll();
                    bill = loss * 5000;
                    if (paid != 100)
                    {
                        ReduceOneCharacteristic(loss);
                        Owner.Journal.Add(string.Format("You incurred a bill of {0}cr, {1}% will be paid for you", bill, paid));
                    }
                    else
                    {
                        Owner.Journal.Add(string.Format("You incurred a bill of {0}cr, {1}% was paid for you", bill, paid));
                    }
                    break;
                case 3:
                    Owner.Journal.Add("Missing Eye or Limb");
                    paid = MedicalBills();
                    loss = dice.roll();
                    bill = 2 * 5000;

                    if (paid != 100)
                    {
                        switch (dice.roll())
                        {
                            case 1:
                            case 2:
                            case 3:
                                Owner.Profile.Str.Value -= 2;
                                break;
                            case 4:
                            case 5:
                            case 6:
                                Owner.Profile.Dex.Value -= 2;
                                break;
                        }
                        Owner.Journal.Add(string.Format("You incurred a bill of {0}cr, {1}% will be paid for you", bill, paid));
                    }
                    else
                    {
                        Owner.Journal.Add(string.Format("You incurred a bill of {0}cr, {1}% was paid for you", bill, paid));
                    }
                    break;
                case 4:
                    Owner.Journal.Add("Scarred");
                    paid = MedicalBills();
                    loss = dice.roll();
                    bill = 2 * 5000;

                    if (paid != 100)
                    {
                        ReduceOneCharacteristic(2);
                        Owner.Journal.Add(string.Format("You incurred a bill of {0}cr, {1}% will be paid for you", bill, paid));
                    }
                    else
                    {
                        Owner.Journal.Add(string.Format("You incurred a bill of {0}cr, {1}% was paid for you", bill, paid));
                    }
                    break;
                case 5:
                    Owner.Journal.Add("Injured");
                    paid = MedicalBills();
                    loss = dice.roll();
                    bill = 5000;
                    if (paid != 100)
                    {
                        ReduceOneCharacteristic(1);
                        Owner.Journal.Add(string.Format("You incurred a bill of {0}cr, {1}% will be paid for you", bill, paid));
                    }
                    else
                    {
                        Owner.Journal.Add(string.Format("You incurred a bill of {0}cr, {1}% was paid for you", bill, paid));
                    }
                    break;
                case 6:
                    Owner.Journal.Add("Lightly Injured: No permanent effect");
                    break;
            }
        }

        private void ReduceOneCharacteristic(int by)
        {
            switch (dice.roll())
            {
                case 1:
                case 2:
                    Owner.Journal.Add(string.Format("STR reduced by {0}", by));
                    Owner.Profile.Str.Value -= by;
                    break;
                case 3:
                case 4:
                    Owner.Journal.Add(string.Format("DEX reduced by {0}", by));
                    Owner.Profile.Dex.Value -= by;
                    break;
                case 5:
                case 6:
                    Owner.Journal.Add(string.Format("END reduced by {0}", by));
                    Owner.Profile.End.Value -= by;
                    break;
            }
        }

        private int MedicalBills()
        {
            var roll = dice.roll(2) + RankNumber;
            var paid = 0;
            switch (medicalBand)
            {
                case 1:
                    if (roll >= 8)
                    {
                        paid = 100;
                    }
                    else if (roll >= 4)
                    {
                        paid = 75;
                    }
                    break;
                case 2:
                    if (roll >= 12)
                    {
                        paid = 100;
                    }
                    else if (roll >= 8)
                    {
                        paid = 75;
                    }
                    else if (roll >= 4)
                    {
                        paid = 50;
                    }
                    break;
                case 3:
                    if (roll >= 12)
                    {
                        paid = 75;
                    }
                    else if (roll >= 8)
                    {
                        paid = 50;
                    }
                    else if (roll >= 4)
                    {
                        paid = 0;
                    }
                    break;
            }

            Owner.Journal.Add(string.Format("Your employer pays {0}% of your medical bills", paid));
            return paid;
        }
    }
}
