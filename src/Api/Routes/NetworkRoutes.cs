using Application.Interfaces.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Api.Routes
{
    public static class NetworkRoutes
    {
        public static RouteGroupBuilder MapNetworkRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("/", ([FromServices] INetworkCommands networkCommands) =>
            {
                var images = networkCommands.GetNetworks(new Domain.Filters.NetworkFilter());
                return Results.Ok(images);
            });
            
            group.MapPost("/delete", ([FromBody] List<string> ids, [FromServices] INetworkCommands networkCommands) =>
            {
                var baseResponses = networkCommands.DeleteNetworks(ids);
                return Results.Ok(baseResponses);
            });

            return group;
        }
    }
}
