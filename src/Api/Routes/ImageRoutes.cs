using Application.Interfaces.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Api.Routes
{
    public static class ImageRoutes
    {
        public static RouteGroupBuilder MapImageRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("/", ([FromServices] IImageCommands imageCommands) =>
            {
                try
                {
                    var images = imageCommands.GetImages(new Domain.Filters.ImageFilter());
                    return Results.Ok(images);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            return group;
        }
    }
}
