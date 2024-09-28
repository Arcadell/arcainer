using Docker.Commands.Interfaces;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Routes
{
    public static class ImageRoutes
    {
        public static RouteGroupBuilder MapImageRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("/", ([FromServices] IImageCommands imageCommands) =>
            {
                var containers = imageCommands.GetImages(new Domain.Filters.ImageFilter());
                return Results.Ok(containers);
            });

            return group;
        }
    }
}
