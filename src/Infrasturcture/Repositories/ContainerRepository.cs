using Application.Containers.Interfaces;
using Domain;
using Infrasturcture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrasturcture.Repositories
{
    public class ContainerRepository : IContainerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ContainerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Container> GetContainers()
        {
            var containers = _dbContext.Containers.ToList();

            return containers;
        }
    }
}
