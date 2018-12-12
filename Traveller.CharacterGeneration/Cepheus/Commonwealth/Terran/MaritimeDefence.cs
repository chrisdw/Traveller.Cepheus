using org.DownesWard.Utilities;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Commonwealth.Terran
{
    public class MaritimeDefence : Cepheus.MaritimeDefence
    {
        public string[] NCORanks { get; } = new string[6];

        public MaritimeDefence()
        {
            NCORanks[0] = Resources.Rank_Seaman;
            NCORanks[1] = Resources.Rank_AbleSeaman;
            NCORanks[2] = Resources.Rank_LeadingSeaman;
            NCORanks[3] = Resources.Rank_PettyOfficer;
            NCORanks[4] = Resources.Rank_ChiefPettyOfficer;
            NCORanks[5] = Resources.Rank_WarrantOfficer;
        }

        public override string RankName
        {
            get
            {
                if (RankNumber == 0 && TermsServed > 0)
                {
                    var ncoRank = TermsServed.Clamp(0, NCORanks.Length - 1);
                    return NCORanks[ncoRank];
                }
                return Ranks[RankNumber];
            }
        }
    }
}
