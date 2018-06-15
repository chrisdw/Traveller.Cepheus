namespace org.DownesWard.Traveller.Shared
{
    /// <summary>
    /// Univeral World Profile
    /// </summary>
    public class UWP
    {
        public char Starport { get; set; }
        public TravCode Size { get; } = new TravCode(10, "Size", "Siz");
        public TravCode Atmosphere { get; } = new TravCode("Atmosphere", "Atm");
        public TravCode Hydro { get; } = new TravCode(10, "Hydrographics", "Hyd");
        public TravCode Pop { get; } = new TravCode(10, "Population", "Pop");
        public TravCode Government { get; } = new TravCode("Government", "Gov");
        public TravCode Law { get; } = new TravCode("Lawlevel", "Law");
        public TravCode TechLevel { get; } = new TravCode("Techlevel", "Tlv");

        public string PhysicalUWP()
        {
            return Size.ToString() + Atmosphere.ToString() + Hydro.ToString();
        }

        public string SocialUWP()
        {
            return Pop.ToString() + Government.ToString() + Law.ToString();
        }
    }
}
