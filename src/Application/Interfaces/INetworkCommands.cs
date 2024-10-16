﻿using Domain.Filters;
using Domain.Models;

namespace Application.Interfaces
{
    public interface INetworkCommands
    {
        List<Network> GetNetworks(NetworkFilter networkFilter);
    }
}