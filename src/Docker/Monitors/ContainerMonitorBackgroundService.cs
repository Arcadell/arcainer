using Docker.DotNet;
using Docker.DotNet.Models;
using Docker.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Docker.Monitors
{
    public class ContainerMonitorBackgroundService(
        ILogger<ContainerMonitorBackgroundService> logger,
        IDockerClient client,
        IServiceProvider serviceProvider,
        IOptions<DockerContainerMonitorOptions> containerMonitorServiceOptions) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var progress = new Progress<Message>(message =>
                    {
                        Task.WhenAll(
                            containerMonitorServiceOptions.Value.MessageHandlers.Select(x =>
                                x.Invoke(serviceProvider, message))).ConfigureAwait(false);
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