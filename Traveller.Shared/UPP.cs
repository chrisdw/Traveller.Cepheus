using org.DownesWard.Traveller.Shared.Properties;

namespace org.DownesWard.Traveller.Shared
{
    /// <summary>
    /// Universal Persona Profile
    /// </summary>
    public class UPP
    {
        public TravCode Str { get; } = new TravCode(15, Resources.UPP_Attr_Strength, "Str");
        public TravCode Dex { get; } = new TravCode(15, Resources.UPP_Attr_Dexterity, "Dex");
        public TravCode End { get; } = new TravCode(15, Resources.UPP_Attr_Endurance, "End");
        public TravCode Int { get; } = new TravCode(15, Resources.UPP_Attr_Intelligence, "Int");
        public TravCode Edu { get; } = new TravCode(15, Resources.UPP_Attr_Education, "Edu");
        public TravCode Soc { get; } = new TravCode(15, Resources.UPP_Attr_SocialStanding, "Soc");

        public virtual string PhysicalUPP()
        {
            return Str.ToString() + Dex.ToString() + End.ToString();
        }

        public virtual string SocialUPP()
        {
            return Int.ToString() + Edu.ToString() + Soc.ToString();
        }

        public string Display
        {
            get
            {
                return PhysicalUPP() + SocialUPP();
            }
        }
    }
}
