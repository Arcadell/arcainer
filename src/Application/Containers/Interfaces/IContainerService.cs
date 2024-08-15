using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Application.Containers.Interfaces
{
    public interface IContainerService
    {
        List<Container> GetContainers();
    }
}
