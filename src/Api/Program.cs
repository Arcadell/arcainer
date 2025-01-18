using Api.Routes;
using Microsoft.AspNetCore.Identity;
using FluentDocker;
using Api.Models;

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
            }
            else
            {
                //TODO set trusted origins in configuration 
                // app.UseCors(_=> _.WithOrigins())
                app.UseHttpsRedirection();
            }

            // TODO FIX THIS
            app.UseCors(_ => _.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost").AllowAnyHeader().AllowAnyMethod());

            app.MapGroup("/api/identity")
                .MapIdentityApiCustom<IdentityUser>(new IdentityApiEndpointRouteBuilderOptions() {
                    ExcludeConfirmEmailGet = true,
                    ExcludeResendConfirmationPost = true,
                    ExcludeForgotPasswordPost = true,
                    ExcludeResetPasswordPost = true,
                    Exclude2faPost = true,
                    ExcludeInfoGet = true,
                    ExcludeInfoPost = true
                })
                .WithTags("Identity");

            app.UseAuthorization();

            app.MapGroup("/api/user")
                .MapUserRoutes()
                .RequireAuthorization()
                .WithTags("User");

            app.MapGroup("/api/container")
                .MapContainerRoutes()
                .RequireAuthorization()
                .WithTags("Container");

            app.MapGroup("/api/image")
                .MapImageRoutes()
                .RequireAuthorization()
                .WithTags("Image");

            app.MapGroup("/api/network")
                .MapNetworkRoutes()
                .RequireAuthorization()
                .WithTags("Network");

            app.MapGroup("/api/volume")
                .MapVolumeRoutes()
                .RequireAuthorization()
                .WithTags("Volume");

            app.MapGroup("/api/setting")
                .MapSettingRoutes()
                .RequireAuthorization()
                .WithTags("Setting");

            // Single page application provider

            app.UseStaticFiles();

            app.Use(async (ctx, next) =>
            {
                // Handle single page application routing
                if (
                ctx.Request.Path.Value != null &&
                !ctx.Request.Path.Value.StartsWith("/api") &&
                !File.Exists(Path.Combine(app.Environment.WebRootPath, ctx.Request.Path.Value.TrimStart('/')))
                )
                {
                    await ctx.Response.SendFileAsync(Path.Combine(app.Environment.WebRootPath, "index.html"));
                }
                else
                {
                    await next();
                }

            });


            app.HandleDbMigration();
            app.Run();
        }
    }
}