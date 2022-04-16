using Common.Entities;

namespace Common.Interfaces
{
    public interface IPresentBowlingGameScoreService
    {
        string GetBowlingGamePresenterScore(IEnumerable<BowlingRound> rounds);
    }
}
