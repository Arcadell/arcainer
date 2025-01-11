using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Routes
{
    public static class SettingRoutes
    {
        public static RouteGroupBuilder MapSettingRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("/", ([FromServices] ISettingService settingService) =>
            {
                try
                {
                    var setting = settingService.GetSetting();
                    return Results.Ok(setting);
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
