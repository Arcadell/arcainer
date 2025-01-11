using Application.Interfaces.Commands;
using Ductus.FluentDocker.Services;
using FluentDocker.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace FluentDocker;

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