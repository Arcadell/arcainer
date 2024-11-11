using Api.Routes;
using Microsoft.AspNetCore.Identity;
using FluentDocker;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            builder.Services.AddCors();

            builder.Services.AddApiServices();
            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddFluentDockerServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                // In development we don't care about CORS
                app.UseCors(_ => _.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            }
            else
            {
                //TODO set trusted origins in configuration 
                // app.UseCors(_=> _.WithOrigins())
                app.UseHttpsRedirection();
            }

            app.MapGroup("/identity")
                .MapIdentityApi<IdentityUser>()
                .WithTags("Identity");

            app.UseAuthorization();

            app.MapGroup("/container")
                .MapContainerRoutes()
                .RequireAuthorization()
                .WithTags("Container");

            app.MapGroup("/image")
                .MapImageRoutes()
                .RequireAuthorization()
                .WithTags("Image");

            app.MapGroup("/network")
                .MapNetworkRoutes()
                .RequireAuthorization()
                .WithTags("Network");

            app.MapGroup("/volume")
                .MapVolumeRoutes()
                .RequireAuthorization()
                .WithTags("Volume");

            app.HandleDbMigration();
            app.Run();
        }
    }
}