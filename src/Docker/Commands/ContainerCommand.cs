using Application.Containers.Interfaces;
using Docker.DotNet;
using Docker.DotNet.Models;
using Domain.Dtos;
using Domain.Entities;
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
            CreateContainerResponse  container = _client.Containers.CreateContainerAsync(new CreateContainerParameters() {
                Image = createContainerDto.Image,
                Name = createContainerDto.Name
            }).Result;
        }

        public List<Container> GetContainers()
        {
            IList<ContainerListResponse> containers = _client.Containers.ListContainersAsync(new ContainersListParameters() { Limit = 10 }).Result;
            List<Container> list = containers.Select(x => new Container() { Id = x.ID, Name = x.Names[0], State = x.State }).ToList();

            return list;
        }

        public void StartContainers(List<string> ids)
        {
            ids.ForEach(id => { _client.Containers.StartContainerAsync(id, new ContainerStartParameters()); });
        }

        public void StopContainers(List<string> ids)
        {
            ids.ForEach(id => { _client.Containers.StopContainerAsync(id, new ContainerStopParameters()); });
        }
    }
}
