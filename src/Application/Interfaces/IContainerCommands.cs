﻿using Domain.Dtos;
using Domain.Filters;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IContainerCommands
    {
        void CreateContainer(CreateContainerDto createContainerDto);
        List<Container> GetContainers(ContainerFilter containerFilter);
        Task StartContainers(List<string> ids);
        Task StopContainers(List<string> ids);
    }
}