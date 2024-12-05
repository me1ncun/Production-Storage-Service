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
            await Task.Delay(20000, stoppingToken);
            
            _logger.LogInformation("LoggingBackgroundService: Background task executed at {Time}", DateTimeOffset.Now);
        }

        _logger.LogInformation("Logging background ended.");
    }
}