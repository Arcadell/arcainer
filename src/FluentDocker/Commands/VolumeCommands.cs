using Application.Interfaces.Commands;
using Domain.Filters;
using Domain.Filters.SearchTypes;
using Domain.Models;
using Ductus.FluentDocker.Commands;
using Ductus.FluentDocker.Services;

namespace FluentDocker.Commands
{
    public class VolumeCommands(IHostService client) : IVolumeCommands
    {
        public List<Volume> GetVolumes(VolumeFilter volumeFilter)
        {
            var volumes = client.GetVolumes();
            var list = volumes.Select(x => new Volume() { Name = x.Name }).Where(x => FilterVolume(x, volumeFilter)).ToList();

            return list;
        }

        public List<BaseResponse> DeleteVolume(List<string> ids)
        {
            var baseResponse = new List<BaseResponse>();
            ids.ForEach(id =>
            {
                try
                {
                    var response = client.Host.VolumeRm(client.Certificates, false,id);
                    if (response.Error != null) throw new Exception(response.Error);
                    
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
        private bool FilterVolume(Volume volume, VolumeFilter volumeFilter)
        {
            bool result = true;
            foreach (var idFilter in volumeFilter.Name)
            {
                result = false;
                switch (idFilter.Type)
                {
                    case SearchType.Equal:
                        if (volume.Name == idFilter.Value)
                            return true;
                        break;

                    case SearchType.Like:
                        if (volume.Name.Contains(idFilter.Value))
                            return true;
                        break;
                }
            }

            return result;
        }
        #endregion
    }
}
