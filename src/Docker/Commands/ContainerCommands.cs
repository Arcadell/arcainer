using Application.Interfaces;
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
    public class ContainerCommands(IDockerClient client) : IContainerCommands
    {
        public void CreateStack(CreateContainerDto createContainerDto)
        {
            Console.WriteLine(createContainerDto);
        }

        public List<Container> GetContainers(ContainerFilter containerFilter)
        {
            var containers = client.Containers.ListContainersAsync(new ContainersListParameters() { All = true }).Result;
            var list = containers.Select(x => new Container() { Id = x.ID, Name = x.Names[0], State = x.State }).Where(x => FilterContainer(x, containerFilter)).ToList();

            return list;
        }

        public List<BaseResponse> StartContainers(List<string> ids)
        {
            throw new NotImplementedException();
        }

        public List<BaseResponse> StopContainers(List<string> ids)
        {
            throw new NotImplementedException();
        }

        public List<BaseResponse> DeleteContainers(List<string> ids)
        {
            throw new NotImplementedException();
        }

        public List<ContainersStack> GetStacks(string? stackName = null)
        {
            throw new NotImplementedException();
        }

        #region PRIVATE FUNCTION
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
