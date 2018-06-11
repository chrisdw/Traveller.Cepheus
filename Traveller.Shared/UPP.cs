namespace org.DownesWard.Traveller.Shared
{
    /// <summary>
    /// Universal Persona Profile
    /// </summary>
    public class UPP
    {
        public TravCode Str { get; } = new TravCode(15);
        public TravCode Dex { get; } = new TravCode(15);
        public TravCode End { get; } = new TravCode(15);
        public TravCode Int { get; } = new TravCode(15);
        public TravCode Edu { get; } = new TravCode(15);
        public TravCode Soc { get; } = new TravCode(15);

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
