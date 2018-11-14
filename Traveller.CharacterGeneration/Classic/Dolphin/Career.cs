using System;

namespace org.DownesWard.Traveller.CharacterGeneration.Classic
{
    public abstract class Career : BasicCareer
    {
        protected bool solomani;
        protected int terms;

        public Career(bool isSolomani)
        {
            solomani = isSolomani;
        }
        public override Character Owner
        {
            get => base.Owner;
            set
            {
                base.Owner = value;
                if (Owner.Careers.Count == 0)
                {
                    Owner.Age = 4;
                }
            }
        }

        public override void EndTerm()
        {
            TermSkills = 1;
            Term += 1;
            TermsServed += 1;
            Owner.Age += 2;
        }
        public override bool Commission()
        {
            return false;
        }

        public override bool Enlist()
        {
            return true;
        }

        public override bool Promotion()
        {
            return false;
        }

        public override Renlistment CanRenlist()
        {
            terms -= 1;
            if (terms <= 0)
            {
                return Renlistment.Cant;
            }
            else
            {
                return Renlistment.Must;
            }
        }

        public override void HandleRenlist(bool renlisted)
        {
            BaseRenlist(renlisted);
        }

        public override bool Survival()
        {
            return true;
        }

        public override void CheckTableAvailablity()
        {
            if (Owner.Profile.Int.Value >= 9)
            {
                SkillTables[0].Skills[0].Name = "Verbalization";
                SkillTables[0].Skills[0].Class = Skill.SkillClass.Civilian;
            }
            else
            {
                SkillTables[0].Skills[0].Name = "HitsU";
                SkillTables[0].Skills[0].Class = Skill.SkillClass.AttributeChange;
            }
        }

        public override int MusterOutRolls()
        {
            return Math.Max(TermsServed - 4, 0);
        }
    }
}
