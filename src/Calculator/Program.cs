using Calculator;
using Common.Interfaces;
using Common.Services;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<BowlingCalculatorWorker>();
        services.AddSingleton<IBowlingScoreCalculator, BowlingScoreCalculator>();
    })
    .Build();

await host.RunAsync();
