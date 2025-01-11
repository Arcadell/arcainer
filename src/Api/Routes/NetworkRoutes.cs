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
                try
                {
                    var images = networkCommands.GetNetworks(new Domain.Filters.NetworkFilter());
                    return Results.Ok(images);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            group.MapPost("/delete", ([FromBody] List<string> ids, [FromServices] INetworkCommands networkCommands) =>
            {
                try
                {
                    var baseResponses = networkCommands.DeleteNetworks(ids);
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
