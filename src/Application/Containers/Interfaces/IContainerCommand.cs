using Domain.Dtos;
using Domain.Filters;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Containers.Interfaces
{
    public interface IContainerCommand
    {
        List<Container> GetContainers(ContainerFilter containerFilter);
        Task StopContainers(List<String> ids);
        Task StartContainers(List<String> ids);
        void CreateContainer(CreateContainerDto createContainerDto);
    }
}
