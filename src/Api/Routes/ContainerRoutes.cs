using Application.Interfaces;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Routes
{
    public static class ContainerRoutes
    {        
        public static RouteGroupBuilder MapContainerRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("/", ([FromServices] IContainerCommands containerCommand) =>
            {
                var containers = containerCommand.GetContainers(new Domain.Filters.ContainerFilter());
                return Results.Ok(containers);
            });

            group.MapGet("/stack/{name?}", (string? name, [FromServices] IContainerCommands containerCommand) =>
            {
                var containers = containerCommand.GetStacks(name);
                return Results.Ok(containers);
            });

            group.MapPost("/create", ([FromBody] CreateContainerDto createContainerDto, [FromServices] IContainerCommands containerCommand) =>
            {
                containerCommand.CreateContainer(createContainerDto);
                return Results.Ok();
            });

            group.MapPost("/start", ([FromBody] List<string> ids, [FromServices] IContainerCommands containerCommand) =>
            {
                containerCommand.StartContainers(ids);
                return Results.Ok();
            });

            group.MapPost("/stop", ([FromBody] List<string> ids, [FromServices] IContainerCommands containerCommand) =>
            {
                containerCommand.StopContainers(ids);
                return Results.Ok();
            });
            
            group.MapPost("/delete", ([FromBody] List<string> ids, [FromServices] IContainerCommands containerCommand) =>
            {
                containerCommand.DeleteContainers(ids);
                return Results.Ok();
            });

            return group;
        }
    }
}
