using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Routes
{
    public static class ImageRoutes
    {
        public static RouteGroupBuilder MapImageRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("/", ([FromServices] IImageCommands imageCommands) =>
            {
                var images = imageCommands.GetImages(new Domain.Filters.ImageFilter());
                return Results.Ok(images);
            });
            
            group.MapPost("/delete", ([FromBody] List<string> ids, [FromServices] IImageCommands imageCommands) =>
            {
                var baseResponses = imageCommands.DeleteImages(ids);
                return Results.Ok(baseResponses);
            });

            return group;
        }
    }
}
