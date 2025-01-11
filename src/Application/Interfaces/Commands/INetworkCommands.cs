using Domain.Filters;
using Domain.Models;

namespace Application.Interfaces.Commands
{
    public interface INetworkCommands
    {
        List<Network> GetNetworks(NetworkFilter networkFilter);
        List<BaseResponse> DeleteNetworks(List<string> ids);
    }
}