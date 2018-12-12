using org.DownesWard.Utilities;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Bughunters
{
    public class SurfaceDefence : Cepheus.SurfaceDefence
    {
        public string[] NCORanks { get; } = new string[7];

        public SurfaceDefence()
        {
            NCORanks[0] = Resources.Rank_Private;
            NCORanks[1] = Resources.Rank_LanceCorporal;
            NCORanks[2] = Resources.Rank_Corporal;
            NCORanks[3] = Resources.Rank_Sergeant;
            NCORanks[4] = Resources.Rank_StaffSergeant;
            NCORanks[5] = Resources.Rank_CompanySergeantMajor;
            NCORanks[6] = Resources.Rank_RegimentalSergeantMajor;
        }

        public override CharacterGeneration.Character Owner
        {
            get => base.Owner;
            set
            {
                base.Owner = value;
                if (Owner.CharacterSpecies == CharacterGeneration.Character.Species.Synner)
                {
                    maxRank = 3;
                }
            }
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
