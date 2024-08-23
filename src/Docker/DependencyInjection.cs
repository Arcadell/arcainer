using Application.Containers.Interfaces;
using Docker.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDockerServices(this IServiceCollection services)
        {
            services.AddScoped<IContainerCommand, ContainerCommand>();
            return services;
        }
    }
}
