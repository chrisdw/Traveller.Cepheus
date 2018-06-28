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
            if (Owner.Profile.Edu.Value >= 8)
            {
                SkillTables[3].Available = true;
            }
            else
            {
                SkillTables[3].Available = false;
            }
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
    }
}
