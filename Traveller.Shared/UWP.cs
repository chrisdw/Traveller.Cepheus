using org.DownesWard.Traveller.Shared.Properties;

namespace org.DownesWard.Traveller.Shared
{
    /// <summary>
    /// Univeral World Profile
    /// </summary>
    public class UWP
    {
        public char Starport { get; set; }
        public TravCode Size { get; } = new TravCode(10, Resources.UWP_Attr_Size, "Siz");
        public TravCode Atmosphere { get; } = new TravCode(Resources.UWP_Attr_Atmosphere, "Atm");
        public TravCode Hydro { get; } = new TravCode(10, Resources.UWP_Attr_Hydrographics, "Hyd");
        public TravCode Pop { get; } = new TravCode(10, Resources.UWP_Attr_Population, "Pop");
        public TravCode Government { get; } = new TravCode(Resources.UWP_Attr_Government, "Gov");
        public TravCode Law { get; } = new TravCode(Resources.UWP_Attr_Lawlevel, "Law");
        public TravCode TechLevel { get; } = new TravCode(Resources.UWP_Attr_Techlevel, "Tlv");

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
