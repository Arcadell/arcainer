using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Filters;
using Domain.Models;

namespace Application.Containers.Interfaces
{
    public interface IContainerService
    {
        List<Container> GetContainers(ContainerFilter containerFilter);
        void CreateContainer(CreateContainerDto createContainerDto);
        List<Container> StopContainers(List<String> ids);
        List<Container> StartContainers(List<String> ids);
    }
}
