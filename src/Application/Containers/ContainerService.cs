using Application.Containers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Models;


namespace Application.Containers
{
    public class ContainerService : IContainerService
    {
        private readonly IContainerCommand _containerCommand;

        public ContainerService(IContainerCommand containerCommand)
        {
            _containerCommand = containerCommand;
        }

        public void CreateContainer(CreateContainerDto createContainerDto)
        {
            _containerCommand.CreateContainer(createContainerDto);
        }

        public List<Container> GetContainers()
        {
            return _containerCommand.GetContainers();
        }

        public void StartContainers(List<string> ids)
        {
            _containerCommand.StartContainers(ids);
        }

        public void StopContainers(List<string> ids)
        {
            _containerCommand.StopContainers(ids);
        }
    }
}
