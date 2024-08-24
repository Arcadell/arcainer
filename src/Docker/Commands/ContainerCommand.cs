using Application.Containers.Interfaces;
using Docker.DotNet;
using Docker.DotNet.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docker.Commands
{
    public class ContainerCommand : IContainerCommand
    {
        public List<Container> GetContainers()
        {
            DockerClient client = new DockerClientConfiguration().CreateClient();
            IList<ContainerListResponse> containers = client.Containers.ListContainersAsync(new ContainersListParameters() { Limit = 10 }).Result;
            List<Container> list = containers.Select(x => new Container() { Id = x.ID, Name = x.Names[0] }).ToList();

            return list;
        }
    }
}
