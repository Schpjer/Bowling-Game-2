using System.Text.Json;
using Common.Entities;
using Common.Interfaces;
using StackExchange.Redis;

namespace Calculator
{
    public class BowlingCalculatorWorker : BackgroundService
    {
        private readonly ILogger<BowlingCalculatorWorker> _logger;
        private readonly IBowlingScoreCalculator _bowlingScoreCalculator;
        private const string RedisConnectionString = "redis";
        private const string Channel = "calculator-channel";

        public BowlingCalculatorWorker(ILogger<BowlingCalculatorWorker> logger, IBowlingScoreCalculator bowlingScoreCalculator)
        {
            _logger = logger;
            _bowlingScoreCalculator = bowlingScoreCalculator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var redis = await ConnectionMultiplexer.ConnectAsync(RedisConnectionString);
                var pubSub = redis.GetSubscriber();
                await pubSub.SubscribeAsync(Channel, (channel, message) =>
                {
                    try
                    {
                        var bowlingRoundList =
                            JsonSerializer.Deserialize<IEnumerable<BowlingRound>>(message.ToString());
                        _bowlingScoreCalculator.CalculateScore(bowlingRoundList);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Could not convert to bowlingRoundList");
                    }
                   
                });
               
                await Task.Delay(1000, stoppingToken);
            }

            
        }
    }
}