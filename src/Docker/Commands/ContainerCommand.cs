using Application.Containers.Interfaces;
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
            throw new NotImplementedException();
        }
    }
}
