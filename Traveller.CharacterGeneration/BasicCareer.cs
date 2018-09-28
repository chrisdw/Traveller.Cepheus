namespace org.DownesWard.Traveller.CharacterGeneration
{
    public abstract class BasicCareer : Career
    {
        public SkillTable[] SkillTables { get; } = new SkillTable[4];
        public string[] Ranks { get; } = new string[7];
        public int CurrentRank { get; set; }
        public int TermSkills { get; set; }
        public int Term { get; set; }
        public virtual bool Drafted { get; set; }

        abstract public bool Enlist();
        abstract public bool Survival();
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
            // Default check for "advanced education"
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

        public string RankName
        {
            get
            {
                return Ranks[CurrentRank];
            }
        }

        protected Renlistment BaseCanRenlist(Renlistment renlist, int target)
        {
            var roll = dice.roll(2);
            if (roll == 12)
            {
                renlist = Renlistment.Must;
                Owner.Journal.Add(string.Format("Forced to remain in {0} at end of term {1}", Name, Term));
            }
            else if (Term >= 6)
            {
                Retired = true;
                Owner.Journal.Add(string.Format("Re-enlistment refused due to age at end of term {0}", Term));
            }
            else if (roll == 2)
            {
                Retired = true;
                Owner.Journal.Add(string.Format("Dismissed from service at end of term {0}", Term));
            }
            else if (roll < target)
            {
                Retired = true;
                Owner.Journal.Add(string.Format("Re-enlistment refused at end of term {0}", Term));
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
                Owner.Journal.Add(string.Format("Enlisted in {0} at age {1}", Name, Owner.Age));
            }
            else
            {
                Owner.Journal.Add(string.Format("Enlistment in {0} refused at age {1}", Name, Owner.Age));
            }

            return enlist;
        }

        protected void BaseRenlist(bool renlisted)
        {
            if (renlisted)
            {
                Owner.Journal.Add(string.Format("Remained in {0} at end of term {1}", Name, Term));
            }
            else
            {
                Retired = true;
                Owner.Journal.Add(string.Format("Left {0} at end of term {1}", Name, Term));
            }
        }
    }
}
