﻿using Application.Containers.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ContainerRepository : IContainerRepository
    {
        public List<Container> GetContainers()
        {
            throw new NotImplementedException();
        }
    }
}
