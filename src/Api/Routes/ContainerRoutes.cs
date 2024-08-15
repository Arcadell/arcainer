using Application.Containers;

namespace Api.Routes
{
    public static class ContainerRoutes
    {
        public static RouteGroupBuilder MapContainerRoutes(this RouteGroupBuilder group)
        {

            group.MapGet("/", () =>
            {
                return Results.Ok();
            });

            return group;
        }
    }
}
