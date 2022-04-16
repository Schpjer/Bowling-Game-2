using Common.Entities;
using Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presenter.Controllers
{
    [ApiController]
    [Route("present")]
    public class PresentController : ControllerBase
    {
        private readonly IPresentBowlingGameScoreService _bowlingScoreCalculator;
        public PresentController(IPresentBowlingGameScoreService bowlingScoreCalculator)
        {
            _bowlingScoreCalculator = bowlingScoreCalculator;
        }

        [HttpPost]
        [Route("score")]
        public async Task<IActionResult> CreateGamePresentStringFromRounds(IEnumerable<BowlingRound> rounds)
        {
            var bowlingGameScoreString = _bowlingScoreCalculator.GetBowlingGamePresenterScore(rounds);
            return Created(nameof(BowlingGame),bowlingGameScoreString);
        }
        
    }
}