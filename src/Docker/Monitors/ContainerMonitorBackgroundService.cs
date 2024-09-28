using Docker.DotNet;
using Docker.DotNet.Models;
using Docker.Monitors.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Docker.Monitors
{
    public class ContainerMonitorBackgroundService(
        ILogger<ContainerMonitorBackgroundService> logger,
        IDockerClient client,
        IContainerMonitorService containerMonitorService) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var progress = new Progress<Message>(message =>
                    {
                        containerMonitorService.MonitorMessageReceived(message);
                    });

                    try
                    {
                        await client.System.MonitorEventsAsync(new ContainerEventsParameters(), progress,
                            stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Error while trying to monitor containers");
                    }

                    await Task.Delay(10000, stoppingToken);
                }
            }
            catch (OperationCanceledException ex)
            {
                logger.LogWarning("A operation was stopped: {ex}", ex.Message);
            }
        }
    }
}