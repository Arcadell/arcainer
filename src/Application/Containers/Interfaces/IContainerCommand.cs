using Domain.Dtos;
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
        List<Container> GetContainers();
        void StopContainers(List<String> ids);
        void StartContainers(List<String> ids);
        void CreateContainer(CreateContainerDto createContainerDto);
    }
}
