using Application.Containers.Interfaces;
using Docker.DotNet;
using Docker.DotNet.Models;
using Domain.Dtos;
using Domain.Filters;
using Domain.Filters.SearchTypes;
using Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docker.Commands
{
    public class ContainerCommand : IContainerCommand
    {
        private readonly ILogger<ContainerCommand> _logger;
        DockerClient _client = new DockerClientConfiguration().CreateClient();

        public ContainerCommand(ILogger<ContainerCommand> logger)
        {
            _logger = logger;
        }

        public void CreateContainer(CreateContainerDto createContainerDto)
        {
            CreateContainerResponse container = _client.Containers.CreateContainerAsync(new CreateContainerParameters()
            {
                Image = createContainerDto.Image,
                Name = createContainerDto.Name
            }).Result;
        }

        public List<Container> GetContainers(ContainerFilter containerFilter)
        {
            IList<ContainerListResponse> containers = _client.Containers.ListContainersAsync(new ContainersListParameters() { Limit = 10 }).Result;
            List<Container> list = containers.Select(x => new Container() { Id = x.ID, Name = x.Names[0], State = x.State }).Where(x => FilterContainer(x, containerFilter)).ToList();

            return list;
        }

        public async Task StartContainers(List<string> ids)
        {
            await Task.WhenAll(ids.Select(id => _client.Containers.StartContainerAsync(id, new ContainerStartParameters())));
        }

        public async Task StopContainers(List<string> ids)
        {
            await Task.WhenAll(ids.Select(id => _client.Containers.StopContainerAsync(id, new ContainerStopParameters())));
        }

        #region PRIVATE FUNC
        private bool FilterContainer(Container container, ContainerFilter containerFilter)
        {
            bool result = true;
            foreach (var idFilter in containerFilter.Id)
            {
                result = false;
                switch (idFilter.Type)
                {
                    case SearchType.Equal:
                        if (container.Id == idFilter.Value)
                            return true;
                        break;

                    case SearchType.Like:
                        if (container.Id.Contains(idFilter.Value))
                            return true;
                        break;
                }
            }

            foreach (var nameFilter in containerFilter.Name)
            {
                result = false;
                switch (nameFilter.Type)
                {
                    case SearchType.Equal:
                        if (container.Name == nameFilter.Value)
                            return true;
                        break;

                    case SearchType.Like:
                        if (container.Name.Contains(nameFilter.Value))
                            return true;
                        break;
                }
            }

            foreach (var stateFilter in containerFilter.State)
            {
                result = false;
                switch (stateFilter.Type)
                {
                    case SearchType.Equal:
                        if (container.State == stateFilter.Value)
                            return true;
                        break;

                    case SearchType.Like:
                        if (container.State.Contains(stateFilter.Value))
                            return true;
                        break;
                }
            }

            return result;
        }
        #endregion
    }
}
