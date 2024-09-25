using Api.Hubs;
using Docker.Models;
using Docker.Monitors;
using Docker.Monitors.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Api.EventsListener
{
    public static class ContainerStatusEventListener
    {
        public static void UseContainerStatusEventListener(this IApplicationBuilder app)
        {
            var containerMonitorService = app.ApplicationServices.GetRequiredService<IContainerMonitorService>();
            containerMonitorService.OnMonitorMessageReceved += ContainerMonitorService_OnMonitorMessageReceved;
        }

        private static void ContainerMonitorService_OnMonitorMessageReceved(object? sender, MessageRecevedEventArgs e)
        {
            var sd = sender as ContainerMonitorService;
            if (e.Message == null || sd == null) { return; }

            var hubContext = sd.ServiceProvider.GetRequiredService<IHubContext<ContainerHub>>();
            hubContext.Clients.All.SendAsync("NewContainerStatus", e.Message.ID, e.Message.Status);
        }
    }
}
