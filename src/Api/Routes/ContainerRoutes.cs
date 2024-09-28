using Docker.Commands.Interfaces;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Routes
{
    public static class ContainerRoutes
    {        
        public static RouteGroupBuilder MapContainerRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("/", ([FromServices] IContainerCommand containerCommand) =>
            {
                var containers = containerCommand.GetContainers(new Domain.Filters.ContainerFilter());
                return Results.Ok(containers);
            });

            group.MapPost("/create", ([FromBody] CreateContainerDto createContainerDto, [FromServices] IContainerCommand containerCommand) =>
            {
                containerCommand.CreateContainer(createContainerDto);
                return Results.Ok();
            });

            group.MapPost("/start", ([FromBody] List<string> Ids, [FromServices] IContainerCommand containerCommand) =>
            {
                containerCommand.StartContainers(Ids);
                return Results.Ok();
            });

            group.MapPost("/stop", ([FromBody] List<string> Ids, [FromServices] IContainerCommand containerCommand) =>
            {
                containerCommand.StopContainers(Ids);
                return Results.Ok();
            });

            return group;
        }
    }
}
