using Application.Interfaces;
using Docker.DotNet;
using Docker.DotNet.Models;
using Domain.Filters;
using Domain.Filters.SearchTypes;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docker.Commands
{
    public class VolumeCommands(IDockerClient client) : IVolumeCommands
    {
        public List<Volume> GetVolumes(VolumeFilter volumeFilter)
        {
            var volumes = client.Volumes.ListAsync().Result;
            var list = volumes.Volumes.Select(x => new Volume() { Name = x.Name }).Where(x => FilterVolume(x, volumeFilter)).ToList();

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
