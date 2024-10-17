using Docker.Commands;
using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Ductus.FluentDocker.Services;

namespace Docker;

public static class DependencyInjection
{
    public static IServiceCollection AddFluentDockerServices(this IServiceCollection services)
    {
        services.AddSingleton<IHostService>(_ => new Hosts().Discover().FirstOrDefault(x => x.IsNative) ?? new Hosts().Discover().FirstOrDefault(x => x.Name == "default")!);

        services.AddScoped<IContainerCommands, ContainerCommands>();
        services.AddScoped<IImageCommands, ImageCommands>();
        services.AddScoped<IVolumeCommands, VolumeCommands>();
        services.AddScoped<INetworkCommands, NetworkCommands>();
        return services;
    }
}