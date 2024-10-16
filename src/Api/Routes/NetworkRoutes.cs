using Application.Interfaces;
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

            return group;
        }
    }
}
