using Docker.Commands;
using Docker.Commands.Interfaces;
using Docker.DotNet;
using Docker.Monitors;
using Docker.Monitors.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDockerServices(this IServiceCollection services)
        {
            services.AddSingleton<IDockerClient>(_ => new DockerClientConfiguration().CreateClient());
            services.AddSingleton<IContainerMonitorService, ContainerMonitorService>();

            services.AddScoped<IContainerCommands, ContainerCommands>();
            services.AddScoped<IImageCommands, ImageCommands>();
            services.AddScoped<INetworkCommands, NetworkCommands>();
            services.AddScoped<IVolumeCommands, VolumeCommands>();

            services.AddHostedService<ContainerMonitorBackgroundService>();
            return services;
        }
    }
}
