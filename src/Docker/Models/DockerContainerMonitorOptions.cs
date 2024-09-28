using Docker.DotNet.Models;

namespace Docker.Models;

public class DockerContainerMonitorOptions
{
    public List<Func<IServiceProvider,Message, Task>> MessageHandlers { get; set; } = [];
    public void AddMessageHandler(Func<IServiceProvider,Message,Task> handler) => MessageHandlers.Add(handler);
}