using Docker.DotNet.Models;
using Docker.Models;

namespace Docker.Monitors.Interfaces
{
    public interface IContainerMonitorService
    {
        public event EventHandler<MessageRecevedEventArgs>? OnMonitorMessageReceved;
        public void MonitorMessageReceved(Message message);
    }
}