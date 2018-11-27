namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Aslan
{
    public abstract class Career : Imperial.Citizen.Career
    {
        public Career()
        {
            survival2attr = string.Empty;
            enlistment1attr = string.Empty;
            enlistment2attr = string.Empty;
            enlistment3attr = string.Empty;
        }

        public override Character Owner { get => base.Owner;
            set 
            {
                base.Owner = value;
                Owner.Age = 16;
                var ac = Culture as Aslan.Culture;
                if (ac.ROPScore == 0)
                {
                    ac.CalculateROP(Owner);
                    Owner.Journal.Add(string.Format(Resources.Msg_ROPScore, ac.ROPScore));
                }
            }
        }

        protected override int EnlistFactor()
        {
            var ac = Culture as Aslan.Culture;
            return ac.ROPScore * -1;
        }

        public override int MaxCashRolls()
        {
            if (Owner.Sex.Equals(Properties.Resources.Sex_Female))
            {
                return MusterOutRolls();
            }
            else
            {
                if (Owner.Skills.ContainsKey(CharacterGeneration.SkillLibrary.Independance.Name))
                {
                    return Owner.Skills[CharacterGeneration.SkillLibrary.Independance.Name].Level;
                }
                else
                {
                    return 0;
                }
            }
        }

        public override void EndTerm()
        {
            Term += 1;
            TermsServed += 1;
            Owner.Age += 8;
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
