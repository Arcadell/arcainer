using Application.Containers.Interfaces;
using Docker.Commands;
using Docker.DotNet;
using Docker.Monitors;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDockerServices(this IServiceCollection services)
        {
            services.AddSingleton<IDockerClient>(_ => new DockerClientConfiguration().CreateClient());

            services.AddScoped<IContainerCommand, ContainerCommand>();
            services.AddHostedService<ContainerMonitorBackgroundService>();
            return services;
        }
    }
}
