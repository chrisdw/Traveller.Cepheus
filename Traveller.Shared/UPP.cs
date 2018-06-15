namespace org.DownesWard.Traveller.Shared
{
    /// <summary>
    /// Universal Persona Profile
    /// </summary>
    public class UPP
    {
        public TravCode Str { get; } = new TravCode(15, "Strength", "Str");
        public TravCode Dex { get; } = new TravCode(15, "Dexterity", "Dex");
        public TravCode End { get; } = new TravCode(15, "Endurance", "End");
        public TravCode Int { get; } = new TravCode(15, "Inteligence", "Int");
        public TravCode Edu { get; } = new TravCode(15, "Education", "Edu");
        public TravCode Soc { get; } = new TravCode(15, "Social Standing", "Soc");

        public virtual string PhysicalUPP()
        {
            return Str.ToString() + Dex.ToString() + End.ToString();
        }

        public virtual string SocialUPP()
        {
            return Int.ToString() + Edu.ToString() + Soc.ToString();
        }
    }
}
