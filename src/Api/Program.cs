
using Api.Routes;
using Persistence.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Api.Hubs;
using Docker.Monitors.Interfaces;
using Docker.Monitors;
using Microsoft.AspNetCore.SignalR;
using Docker.Models;
using Docker.DotNet.Models;
using Api.EventsListener;
using Docker;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins, policy => { policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod(); });
            });

            builder.Services.AddApiServices();
            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddDockerServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapGroup("/identity")
                .MapIdentityApi<IdentityUser>()
                .WithTags("Identity");

            app.UseHttpsRedirection();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthorization();

            app.MapGroup("/container")
                .MapContainerRoutes()
                .RequireAuthorization()
                .WithTags("Container");

            app.MapGroup("/image")
                .MapImageRoutes()
                .WithTags("Image");

            app.MapGroup("/volume")
                .MapVolumeRoutes()
                .WithTags("Volume");

            app.MapHub<ContainerHub>("/containerHub");

            app.Run();
        }
    }
}
