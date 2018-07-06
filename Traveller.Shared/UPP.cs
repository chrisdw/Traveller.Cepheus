using org.DownesWard.Traveller.Shared.Properties;
using System.Collections.Generic;

namespace org.DownesWard.Traveller.Shared
{
    /// <summary>
    /// Universal Persona Profile
    /// </summary>
    public class UPP
    {
        public virtual TravCode Str { get; } = new TravCode(15, Resources.UPP_Attr_Strength, "Str");
        public virtual TravCode Dex { get; } = new TravCode(15, Resources.UPP_Attr_Dexterity, "Dex");
        public virtual TravCode End { get; } = new TravCode(15, Resources.UPP_Attr_Endurance, "End");
        public virtual TravCode Int { get; } = new TravCode(15, Resources.UPP_Attr_Intelligence, "Int");
        public virtual TravCode Edu { get; } = new TravCode(15, Resources.UPP_Attr_Education, "Edu");
        public virtual TravCode Soc { get; } = new TravCode(15, Resources.UPP_Attr_SocialStanding, "Soc");

        public virtual TravCode this[string index]
        {
            get
            {
                switch (index.ToUpperInvariant())
                {
                    case "STR":
                        return Str;
                    case "DEX":
                        return Dex;
                    case "END":
                        return End;
                    case "INT":
                        return Int;
                    case "EDU":
                        return Edu;
                    case "SOC":
                        return Soc;
                }
                throw new KeyNotFoundException(string.Format("attribute {0} not found", index));
            }
        }
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
