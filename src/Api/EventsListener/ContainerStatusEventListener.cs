using Api.Hubs;
using Docker.DotNet.Models;
using Docker.Models;
using Docker.Monitors;
using Docker.Monitors.Interfaces;
using Domain.Enums;
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
            switch (e.Message.Action)
            {
                case "start":
                    Console.WriteLine($"Event: {e.Message.Action} for container {e.Message.ID}");
                    hubContext.Clients.All.SendAsync("NewContainerStatus", e.Message.ID, ContainerStatusEvent.STARTED);
                    break;
                case "die":
                    Console.WriteLine($"Event: {e.Message.Action} for container {e.Message.ID}");
                    hubContext.Clients.All.SendAsync("NewContainerStatus", e.Message.ID, ContainerStatusEvent.DIED);
                    break;
            }
        }
    }
}
