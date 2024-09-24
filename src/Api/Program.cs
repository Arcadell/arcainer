
using Api.Routes;
using Application.Containers.Interfaces;
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

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddIdentityApiEndpoints<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddSignalR();

            builder.Services.AddApiServices();
            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddDockerServices();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

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

            app.MapHub<ContainerHub>("/containerHub");

            using var scope = app.Services.CreateAsyncScope();
            var containerMonitorService = scope.ServiceProvider.GetRequiredService<IContainerMonitorService>();
            containerMonitorService.OnMonitorMessageReceved += ContainerMonitorService_OnMonitorMessageReceved;

            app.Run();
        }

        private static void ContainerMonitorService_OnMonitorMessageReceved(object? sender, MessageRecevedEventArgs e)
        {
            var sd = sender as ContainerMonitorService;
            var hubContext = sd.ServiceProvider.GetRequiredService<IHubContext<ContainerHub>>();
            hubContext.Clients.All.SendAsync("Test", $"Event: {e.Message?.Action} for container {e.Message?.ID}");
        }
    }
}
