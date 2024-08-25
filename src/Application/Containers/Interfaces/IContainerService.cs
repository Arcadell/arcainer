using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Entities;

namespace Application.Containers.Interfaces
{
    public interface IContainerService
    {
        List<Container> GetContainers();
        void CreateContainer(CreateContainerDto createContainerDto);
        void StopContainers(List<String> ids);
        void StartContainers(List<String> ids);
    }
}
