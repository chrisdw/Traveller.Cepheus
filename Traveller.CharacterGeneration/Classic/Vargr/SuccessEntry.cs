namespace org.DownesWard.Traveller.CharacterGeneration.Classic.Vargr
{
    public class SuccessEntry
    {
        public int CharismaChange { get; set; }
        public int RankChange { get; set; }
        public bool Dismissed { get; set; }

        public SuccessEntry(int charismaChange, int rankChange, bool dismissed)
        {
            CharismaChange = charismaChange;
            RankChange = rankChange;
            Dismissed = dismissed;
        }
    }
}
