using org.DownesWard.Utilities;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Commonwealth.Terran
{

    public class Marine : Cepheus.Marine
    {
        public string[] NCORanks { get; } = new string[7];

        public Marine()
        {
            Ranks[0] = Resources.Rank_Marine;

            NCORanks[0] = Resources.Rank_Marine;
            NCORanks[1] = Resources.Rank_LanceCorporal;
            NCORanks[2] = Resources.Rank_Corporal;
            NCORanks[3] = Resources.Rank_Sergeant;
            NCORanks[4] = Resources.Rank_StaffSergeant;
            NCORanks[5] = Resources.Rank_GunnerySergeant;
            NCORanks[6] = Resources.Rank_MasterSergeant;
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
