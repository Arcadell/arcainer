using Microsoft.AspNetCore.Builder;
using Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Application.Interfaces.Repositories;
using Persistence.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));

            services.AddScoped<ISettingRepository, SettingsRepository>();

            return services;
        }
        
        public static void HandleDbMigration(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var db = scope.ServiceProvider.GetService<ApplicationDbContext>()?.Database;
            if(db == null) { throw new NullReferenceException("No Database context was found."); }

            db.EnsureCreated();
        }
    }
}
