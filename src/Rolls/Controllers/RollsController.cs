using System.Text.Json;
using System.Threading.Channels;
using Common.Entities;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace Rolls.Controllers
{
    [ApiController]
    [Route("rolls")]
    public class RollsController : ControllerBase
    {
        private const string RedisConnectionString = "redis";
        private static readonly ConnectionMultiplexer Connection = ConnectionMultiplexer.Connect(RedisConnectionString);
        private const string Channel = "calculator-channel";


        [HttpPost]
        public async Task<IActionResult> PublishRollsToCalculatorService(IEnumerable<BowlingRound> rounds)
        {
            var subscribers = Connection.GetSubscriber();
            await subscribers.PublishAsync(Channel, JsonSerializer.Serialize(rounds), CommandFlags.FireAndForget);
            return StatusCode(StatusCodes.Status201Created);
        }

    }
}