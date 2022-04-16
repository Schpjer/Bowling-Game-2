namespace Common.Entities
{
    public class BowlingGame
    {
        public int FinalScore { get; set; }
        public IEnumerable<BowlingRound> Rounds { get; set; }

        public BowlingGame(int finalScore, IEnumerable<BowlingRound> rounds)
        {
            FinalScore = finalScore;
            Rounds = rounds;
        }
    }
}
