using Application.Interfaces.Services;
using Application.Services;


namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
