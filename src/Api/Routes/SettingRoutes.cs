using Application.Interfaces.Services;
using Domain.Dtos;
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
                    var setting = settingService.GetDefaultSetting();
                    return Results.Ok(setting);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            });

            group.MapPost("/update", ([FromBody] UpdateSettingDto updateSettingDto, [FromServices] ISettingService settingService) =>
            {
                try
                {
                    var setting = settingService.UpdateSetting(updateSettingDto);
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
