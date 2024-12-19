using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Storage.Application.Services.Background.Interfaces;

namespace Storage.Application.Services.Background;

public class LoggingBackgroundService : BackgroundService
{
    private readonly ILogger<LoggingBackgroundService> _logger;
    private readonly ILoggingQueue _loggingQueue;

    public LoggingBackgroundService(
        ILogger<LoggingBackgroundService> logger,
        ILoggingQueue loggingQueue)
    {
        _logger = logger;
        _loggingQueue = loggingQueue;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Logging background started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            await ProccessContractsAsync(stoppingToken);

            await Task.Delay(20000, stoppingToken);
        }

        _logger.LogInformation("Logging background ended.");
    }

    private async Task ProccessContractsAsync(CancellationToken stoppingToken)
    {
        while (_loggingQueue.TryDequeue(out var contract))
        {
            _logger.LogInformation($"Processing contract {contract.Id}");
            
            await Task.Delay(100, stoppingToken);
        }
    }
}