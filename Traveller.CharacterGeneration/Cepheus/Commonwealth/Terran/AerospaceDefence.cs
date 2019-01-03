using org.DownesWard.Utilities;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Commonwealth.Terran
{
    public class AerospaceDefence : Commonwealth.AerospaceDefence
    {
        public string[] NCORanks { get; } = new string[6];

        public AerospaceDefence()
        {
            NCORanks[0] = Resources.Rank_Aircraftman;
            NCORanks[1] = Resources.Rank_SeniorAircraftman;
            NCORanks[2] = Resources.Rank_Corporal;
            NCORanks[3] = Resources.Rank_Sergeant;
            NCORanks[4] = Resources.Rank_FlightSergeant;
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
