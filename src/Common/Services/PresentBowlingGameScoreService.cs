using Common.Entities;
using Common.Interfaces;

namespace Common.Services
{
    public class PresentBowlingGameScoreService : IPresentBowlingGameScoreService
    {
        private readonly IBowlingScoreCalculator _bowlingScoreCalculator;

        public PresentBowlingGameScoreService(IBowlingScoreCalculator bowlingScoreCalculator)
        {
            _bowlingScoreCalculator = bowlingScoreCalculator;
        }

        public string GetBowlingGamePresenterScore(IEnumerable<BowlingRound> rounds)
        {
            var presenterString = string.Empty;
            var bowlingGame = _bowlingScoreCalculator.CalculateScore(rounds);
            var calculatedRounds = bowlingGame.Rounds;
            foreach (var round in calculatedRounds)
            {
                presenterString += AppendBowlingRoundStatsToPresentString(round);
            }
            return presenterString;
        }

        private string AppendBowlingRoundStatsToPresentString(BowlingRound round)
        {
            if (round.ThirdRoll.HasValue)
            {
                return AppendFinalBowlingRoundStatsToPresentString(round);
            }
            return round.IsStrike() ? $" [X => {round.RoundScore}]" : $" [{round.FirstRoll} | {round.SecondRoll} => {round.RoundScore}]";
        }

        private string AppendFinalBowlingRoundStatsToPresentString(BowlingRound round)
        {
            var firstRound = round.FirstRoll == 10 ? "X" : round.FirstRoll.ToString();
            var secondRound = round.FirstRoll == 10 ? "X" : round.SecondRoll.ToString();
            var thirdRound = round.FirstRoll == 10 ? "X" : round.ThirdRoll.ToString();
            return $" [{firstRound} | {secondRound} | {thirdRound} => {round.RoundScore}]";

        }
    }
}
