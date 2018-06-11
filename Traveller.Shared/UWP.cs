namespace org.DownesWard.Traveller.Shared
{
    /// <summary>
    /// Univeral World Profile
    /// </summary>
    public class UWP
    {
        public char Starport { get; set; }
        public TravCode Size { get; } = new TravCode(10);
        public TravCode Atmosphere { get; } = new TravCode();
        public TravCode Hydro { get; } = new TravCode(10);
        public TravCode Pop { get; } = new TravCode(10);
        public TravCode Government { get; } = new TravCode();
        public TravCode Law { get; } = new TravCode();
        public TravCode TechLevel { get; } = new TravCode();

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
