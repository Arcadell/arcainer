using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Containers.Interfaces
{
    public interface IContainerRepository
    {
        List<Container> GetContainers();
    }
}
