using Application.Containers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Dtos;
using Domain.Models;
using Domain.Filters;
using Domain.Filters.SearchTypes;


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

        public List<Container> GetContainers(ContainerFilter containerFilter)
        {
            return _containerCommand.GetContainers(containerFilter);
        }

        public List<Container> StartContainers(List<string> ids)
        {
            _containerCommand.StartContainers(ids).Wait();

            ContainerFilter containerFilter = new ContainerFilter();
            foreach (string id in ids)
                containerFilter.Id.Add(new SearchString(SearchType.Equal, id));

            List<Container> containers = _containerCommand.GetContainers(containerFilter);
            return containers;
        }

        public List<Container> StopContainers(List<string> ids)
        {
            _containerCommand.StopContainers(ids).Wait();

            ContainerFilter containerFilter = new ContainerFilter();
            foreach (string id in ids)
                containerFilter.Id.Add(new SearchString(SearchType.Equal, id));

            List<Container> containers = _containerCommand.GetContainers(containerFilter);
            return containers;
        }
    }
}
