using Domain.Dtos;
using Domain.Filters;
using Domain.Models;

namespace Application.Interfaces.Commands
{
    public interface IContainerCommands
    {
        void CreateStack(CreateContainerDto createContainerDto);
        List<ContainersStack> GetStacks(string? stackName = null);
        List<Container> GetContainers(ContainerFilter containerFilter);
        List<BaseResponse> StartContainers(List<string> ids);
        List<BaseResponse> StopContainers(List<string> ids);
        List<BaseResponse> DeleteContainers(List<string> ids);
        List<BaseResponse> DeleteStacks(List<string> stackNames);
        string GetLogContainer(string id);
    }
}