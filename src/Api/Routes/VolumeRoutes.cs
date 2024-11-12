using Application.Interfaces;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Routes
{
    public static class VolumeRoutes
    {
        public static RouteGroupBuilder MapVolumeRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("/", ([FromServices] IVolumeCommands volumeCommands) =>
            {
                var volumes = volumeCommands.GetVolumes(new Domain.Filters.VolumeFilter());
                return Results.Ok(volumes);
            });
            
            group.MapPost("/delete", ([FromBody] List<string> ids, [FromServices] IVolumeCommands volumeCommands) =>
            {
                var baseResponses = volumeCommands.DeleteVolume(ids);
                return Results.Ok(baseResponses);
            });

            return group;
        }
    }
}
