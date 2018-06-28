namespace org.DownesWard.Traveller.Shared.Classic
{
    public class VargrUPP : UPP
    {
        public TravCode Chr { get; } = new TravCode(15, "Charisma", "Chr");

        public override string SocialUPP()
        {
            return Int.ToString() + Edu.ToString() + Soc.ToString() + Chr.ToString();
        }
    }
 }
