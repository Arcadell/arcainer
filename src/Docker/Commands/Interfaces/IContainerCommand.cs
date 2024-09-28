using Domain.Dtos;
using Domain.Filters;
using Domain.Models;

namespace Docker.Commands.Interfaces
{
    public interface IContainerCommand
    {
        void CreateContainer(CreateContainerDto createContainerDto);
        List<Container> GetContainers(ContainerFilter containerFilter);
        Task StartContainers(List<string> ids);
        Task StopContainers(List<string> ids);
    }
}