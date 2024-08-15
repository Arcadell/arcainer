using Application.Containers;
using Application.Containers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Routes
{
    public static class ContainerRoutes
    {
        public static RouteGroupBuilder MapContainerRoutes(this RouteGroupBuilder group)
        {

            group.MapGet("/", ([FromServices] IContainerService containerService) =>
            {
                var containers = containerService.GetContainers();
                return Results.Ok(containers);
            });

            return group;
        }
    }
}
