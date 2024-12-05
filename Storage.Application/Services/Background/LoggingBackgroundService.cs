using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Storage.Application.Services.Background;

public class LoggingBackgroundService : BackgroundService
{
    private readonly ILogger<LoggingBackgroundService> _logger;

    public LoggingBackgroundService(ILogger<LoggingBackgroundService> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Logging background started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            await ProccessContractsAsync();

            await Task.Delay(20000, stoppingToken);
        }

        _logger.LogInformation("Logging background ended.");
    }

    private async Task ProccessContractsAsync()
    {
        await Task.Run(() =>
        {
            if (FakeQueue.Contracts.Count() > 0)
            {
                foreach (var contract in FakeQueue.Contracts.ToList())
                {
                    _logger.LogInformation($"Proccessing contract {contract.Id}\n");

                    FakeQueue.Dequeue();
                }
            }
        });
    }
}