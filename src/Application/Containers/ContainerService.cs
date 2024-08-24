using Application.Containers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;


namespace Application.Containers
{
    public class ContainerService : IContainerService
    {
        private readonly IContainerCommand _containerCommand;

        public ContainerService(IContainerCommand containerCommand)
        {
            _containerCommand = containerCommand;
        }

        public List<Container> GetContainers()
        {
            return _containerCommand.GetContainers();
        }

        public void StartContainers(List<string> Ids)
        {
            _containerCommand.StartContainers(Ids);
        }

        public void StopContainers(List<string> Ids)
        {
            _containerCommand.StopContainers(Ids);
        }
    }
}
