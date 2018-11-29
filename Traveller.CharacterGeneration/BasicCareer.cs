namespace org.DownesWard.Traveller.CharacterGeneration
{
    public abstract class BasicCareer : Career
    {
        // Allow for careers with different number of skill tables
        // to redefine this if they want to
        protected SkillTable[] skillTables = new SkillTable[4];

        public SkillTable[] SkillTables { get { return skillTables; } }
        public string[] Ranks { get; } = new string[7];

        public int TermSkills { get; set; }
        public int Term { get; set; }
        public virtual bool Drafted { get; set; }

        abstract public bool Enlist();
        public enum SurvivalResult
        {
            Survived,
            Died,
            Discharged
        }
        abstract public SurvivalResult Survival();
        abstract public bool Commission();
        abstract public bool Promotion();
        public enum Renlistment
        {
            Must,
            Can,
            Cant
        }
        abstract public Renlistment CanRenlist();
        abstract public void HandleRenlist(bool renlisted);
        public virtual void EndTerm()
        {
            TermSkills = 1;
            Term += 1;
            TermsServed += 1;
            Owner.Age += 4;
        }

        public virtual void CheckTableAvailablity()
        {
            // Default check for Properties.Resources.Table_AdvancedEducation
            SkillTables[3].Available = (Owner.Profile.Edu.Value >= 8);
        }

        public override Character Owner
        {
            get
            {
                return base.Owner;
            }
            set
            {
                base.Owner = value;
                if (Owner.Careers.Count == 0)
                {
                    Owner.Age = 18;
                }
                CheckTableAvailablity();
            }
        }

        public override string RankName
        {
            get
            {
                return Ranks[RankNumber];
            }
        }

        protected Renlistment BaseCanRenlist(Renlistment renlist, int target)
        {
            var roll = dice.roll(2);
            if (roll == 12)
            {
                renlist = Renlistment.Must;
                Owner.Journal.Add(string.Format(Properties.Resources.Msg_MustRenlist, Name, Term));
            }
            else if (Term >= 6)
            {
                Retired = true;
                Owner.Journal.Add(string.Format(Properties.Resources.Msg_RenlistRefused, Term));
            }
            else if (roll == 2)
            {
                Retired = true;
                Owner.Journal.Add(string.Format(Properties.Resources.Msg_Dismissed, Term));
            }
            else if (roll < target)
            {
                Retired = true;
                Owner.Journal.Add(string.Format(Properties.Resources.Msg_Refused, Term));
            }
            else
            {
                renlist = Renlistment.Can;
            }

            return renlist;
        }

        protected bool BaseEnlist(int target)
        {
            var enlist = false;
            if (dice.roll(2) >= target)
            {
                enlist = true;
                Owner.Journal.Add(string.Format(Properties.Resources.Msg_Enlist_Success, Name, Owner.Age));
            }
            else
            {
                Owner.Journal.Add(string.Format(Properties.Resources.Msg_Enlist_Refused, Name, Owner.Age));
            }

            return enlist;
        }

        protected void BaseRenlist(bool renlisted)
        {
            if (renlisted)
            {
                Owner.Journal.Add(string.Format(Properties.Resources.Msg_Renlist_Success, Name, Term));
            }
            else
            {
                Retired = true;
                Owner.Journal.Add(string.Format(Properties.Resources.Msg_Renlist_Fail, Name, Term));
            }
        }
    }
}
