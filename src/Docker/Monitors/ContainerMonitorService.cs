using Docker.DotNet.Models;
using Docker.Models;
using Docker.Monitors.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Docker.Monitors;

public class ContainerMonitorService(IOptions<DockerContainerMonitorOptions> options, IServiceProvider serviceProvider) : IContainerMonitorService
{
    
    private readonly List<Func<IServiceProvider,Message, Task>> _messageHandlers = options.Value.MessageHandlers;
    public Task MonitorMessageReceived(Message message) =>
        Task.WhenAll(_messageHandlers.Select(x => x.Invoke(serviceProvider,message)));
    public void AddMessageHandler(Func<IServiceProvider,Message,Task> handler) => _messageHandlers.Add(handler);
}

