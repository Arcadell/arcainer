using Application.Containers;
using Application.Containers.Interfaces;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Routes
{
    public static class ContainerRoutes
    {        
        public static RouteGroupBuilder MapContainerRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("/", ([FromServices] IContainerService containerService) =>
            {
                var containers = containerService.GetContainers(new Domain.Filters.ContainerFilter());
                return Results.Ok(containers);
            });

            group.MapPost("/create", ([FromBody] CreateContainerDto createContainerDto, [FromServices] IContainerService containerService) =>
            {
                containerService.CreateContainer(createContainerDto);
                return Results.Ok();
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
