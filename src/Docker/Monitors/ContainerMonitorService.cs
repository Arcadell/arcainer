using Docker.DotNet.Models;
using Docker.Models;
using Docker.Monitors.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docker.Monitors
{
    public class ContainerMonitorService : IContainerMonitorService
    {
        public IServiceProvider ServiceProvider { get; set; }
        public ContainerMonitorService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public event EventHandler<MessageRecevedEventArgs>? OnMonitorMessageReceved;
        public void MonitorMessageReceved(Message message) => OnMonitorMessageReceved?.Invoke(this, new MessageRecevedEventArgs(message));
    }
}
