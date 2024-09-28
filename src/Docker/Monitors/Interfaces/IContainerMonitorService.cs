using Docker.DotNet.Models;
using Docker.Models;

namespace Docker.Monitors.Interfaces
{
    public interface IContainerMonitorService
    {
        public Task MonitorMessageReceived(Message message);
        public void AddMessageHandler(Func<IServiceProvider, Message, Task> handler);
    }
}