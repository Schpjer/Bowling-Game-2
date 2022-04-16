using Common.Entities;

namespace Common.Interfaces
{
    public interface IBowlingScoreCalculator
    {
        BowlingGame CalculateScore(IEnumerable<BowlingRound> rounds);
    }
}
