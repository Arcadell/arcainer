using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Routes
{
    public static class UserRoutes
    {
        public static RouteGroupBuilder MapUserRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("/", ([FromServices] IUserService userService) =>
            {
                try
                {
                    throw new NotImplementedException();
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
