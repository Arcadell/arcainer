using Application.Interfaces;
using Domain.Filters;
using Domain.Filters.SearchTypes;
using Domain.Models;
using Ductus.FluentDocker.Services;

namespace Docker.Commands
{
    public class VolumeCommands(IHostService client) : IVolumeCommands
    {
        public List<Volume> GetVolumes(VolumeFilter volumeFilter)
        {
            var volumes = client.GetVolumes();
            var list = volumes.Select(x => new Volume() { Name = x.Name }).Where(x => FilterVolume(x, volumeFilter)).ToList();

            return list;
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
