using Application.Interfaces.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Api.Routes
{
    public static class VolumeRoutes
    {
        public static RouteGroupBuilder MapVolumeRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("/", ([FromServices] IVolumeCommands volumeCommands) =>
            {
                try
                {
                    var volumes = volumeCommands.GetVolumes(new Domain.Filters.VolumeFilter());
                    return Results.Ok(volumes);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            group.MapPost("/delete", ([FromBody] List<string> ids, [FromServices] IVolumeCommands volumeCommands) =>
            {
                try
                {
                    var baseResponses = volumeCommands.DeleteVolume(ids);
                    return Results.Ok(baseResponses);
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
