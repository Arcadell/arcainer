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

            group.MapPost("/start", ([FromBody] List<string> Ids, [FromServices] IContainerService containerService) =>
            {
                containerService.StartContainers(Ids);
                return Results.Ok();
            });

            group.MapPost("/stop", ([FromBody] List<string> Ids, [FromServices] IContainerService containerService) =>
            {
                containerService.StopContainers(Ids);
                return Results.Ok();
            });

            return group;
        }
    }
}
