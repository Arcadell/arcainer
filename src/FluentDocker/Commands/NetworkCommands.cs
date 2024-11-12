using Application.Interfaces;
using Domain.Filters;
using Domain.Filters.SearchTypes;
using Domain.Models;
using Ductus.FluentDocker.Services;

namespace FluentDocker.Commands
{
    class NetworkCommands(IHostService client) : INetworkCommands
    {
        public List<Network> GetNetworks(NetworkFilter networkFilter)
        {
            var networks = client.GetNetworks();
            var list = networks.Select(x => new Network() { Id = x.Id, Name = x.Name }).Where(x => FilterNetwork(x, networkFilter)).ToList();

            return list;
        }

        public List<BaseResponse> DeleteNetworks(List<string> ids)
        {
            var baseResponse = new List<BaseResponse>();
            ids.ForEach(id =>
            {
                try
                {
                    Ductus.FluentDocker.Commands.Network.NetworkRm(client.Host, client.Certificates, id);
                    baseResponse.Add(new BaseResponse() { Id = id });
                }
                catch (Exception e)
                {
                    baseResponse.Add(new BaseResponse() { Id = id, Error = true, ErrorMessage = e.Message });
                }
            });

            return baseResponse;
        }

        #region PRIVATE FUNCTION
        private bool FilterNetwork(Network network, NetworkFilter networkFilter)
        {
            bool result = true;
            foreach (var idFilter in networkFilter.Name)
            {
                result = false;
                switch (idFilter.Type)
                {
                    case SearchType.Equal:
                        if (network.Name == idFilter.Value)
                            return true;
                        break;

                    case SearchType.Like:
                        if (network.Name.Contains(idFilter.Value))
                            return true;
                        break;
                }
            }

            return result;
        }
        #endregion
    }
}
