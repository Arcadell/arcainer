using Docker.DotNet;
using Docker.DotNet.Models;
using Docker.Monitors.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Docker.Monitors
{
    public class ContainerMonitorBackgroundService(ILogger<ContainerMonitorBackgroundService> logger, IDockerClient client, IContainerMonitorService containerMonitorService) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var progress = new Progress<Message>(message =>
                {
                    Console.WriteLine($"Event: {message.Action} for container {message.ID}");
                    containerMonitorService.MonitorMessageReceved(message);
                });

                try
                {
                    await client.System.MonitorEventsAsync(new ContainerEventsParameters(), progress, stoppingToken);
                }
                catch (Exception ex) {
                    logger.LogError(ex, "Error while trying to monitor containers");
                }

                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
