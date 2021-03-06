﻿using org.DownesWard.Utilities;

namespace org.DownesWard.Traveller.CharacterGeneration.Cepheus.Bughunters
{
    public class Navy: Cepheus.Navy
    {
        public string[] NCORanks { get; } = new string[6];

        public Navy()
        {
            NCORanks[0] = Resources.Rank_Starman;
            NCORanks[1] = Resources.Rank_AbleStarman;
            NCORanks[2] = Resources.Rank_LeadingStarman;
            NCORanks[3] = Resources.Rank_PettyOfficer;
            NCORanks[4] = Resources.Rank_ChiefPettyOfficer;
            NCORanks[5] = Resources.Rank_WarrantOfficer;
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
