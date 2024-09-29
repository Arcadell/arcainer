using Domain.Filters;
using Domain.Models;

namespace Docker.Commands.Interfaces
{
    public interface INetworkCommands
    {
        List<Network> GetNetworks(NetworkFilter networkFilter);
    }
}