using Application.Interfaces.Commands;
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
                try
                {
                    var containers = containerCommand.GetContainers(new Domain.Filters.ContainerFilter());
                    return Results.Ok(containers);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            group.MapGet("/stack/{name?}", (string? name, [FromServices] IContainerCommands containerCommand) =>
            {
                try
                {
                    var containers = containerCommand.GetStacks(name);
                    return Results.Ok(containers);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            group.MapPost("/create", ([FromBody] CreateContainerDto createContainerDto, [FromServices] IContainerCommands containerCommand) =>
            {
                try
                {
                    containerCommand.CreateStack(createContainerDto);
                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            group.MapPost("/start", ([FromBody] List<string> ids, [FromServices] IContainerCommands containerCommand) =>
            {
                try
                {
                    var baseResponses = containerCommand.StartContainers(ids);
                    return Results.Ok(baseResponses);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            group.MapPost("/stop", ([FromBody] List<string> ids, [FromServices] IContainerCommands containerCommand) =>
            {
                try
                {
                    var baseResponses = containerCommand.StopContainers(ids);
                    return Results.Ok(baseResponses);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            group.MapPost("/delete", ([FromBody] List<string> ids, [FromServices] IContainerCommands containerCommand) =>
            {
                try
                {
                    var baseResponses = containerCommand.DeleteContainers(ids);
                    return Results.Ok(baseResponses);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            group.MapPost("/stack/delete", ([FromBody] List<string> ids, [FromServices] IContainerCommands containerCommand) =>
            {
                try
                {
                    var baseResponses = containerCommand.DeleteStacks(ids);
                    return Results.Ok(baseResponses);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            group.MapGet("/logs", ([FromQuery] string id, [FromServices] IContainerCommands containerCommand) =>
            {
                try
                {
                    var logs = containerCommand.GetLogContainer(id);
                    return Results.Ok(logs);
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
