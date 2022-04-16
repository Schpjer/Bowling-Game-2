using Common.Entities;
using Common.Interfaces;

namespace Common.Services
{
    public class BowlingScoreCalculator : IBowlingScoreCalculator
    {
        public BowlingGame CalculateScore(IEnumerable<BowlingRound> rounds)
        {
            var score = 0;
            var roundIndex = 0;
            var frameScore = ConvertBowlingRoundsIntoIntegerEnumerable(rounds);
            for (var i = 0; i < frameScore.Count(); i+=2)
            {
                if (i + 1 >= frameScore.Count())
                    break;

                if (frameScore.ElementAt(i) + frameScore.ElementAt(i + 1) < 10)
                {
                    score += frameScore.ElementAt(i) + frameScore.ElementAt(i + 1);
                    rounds.ElementAt(roundIndex).RoundScore = score;
                    continue;
                }

                if (i + 2 >= frameScore.Count())
                    break;

                score += frameScore.ElementAt(i) + frameScore.ElementAt(i + 1) + frameScore.ElementAt(i + 2);

                if (frameScore.ElementAt(i) == 10)
                    i--;
                rounds.ElementAt(roundIndex).RoundScore = score;
                roundIndex++;
            }
            var bowlingGame = new BowlingGame(score, rounds);
            return bowlingGame;
        }

        private IEnumerable<int> ConvertBowlingRoundsIntoIntegerEnumerable(IEnumerable<BowlingRound> rounds)
        {
            var integerList = new List<int>();
            foreach (var round in rounds)
            {
                if (round.IsStrike() && !round.ThirdRoll.HasValue)
                {
                    integerList.Add(round.FirstRoll);
                } 
                else
                {
                    integerList.Add(round.FirstRoll);
                    integerList.Add(round.SecondRoll);
                    if (round.ThirdRoll.HasValue)
                        integerList.Add(round.ThirdRoll.Value);
                }
            }
            return integerList;
        }
    }
}
