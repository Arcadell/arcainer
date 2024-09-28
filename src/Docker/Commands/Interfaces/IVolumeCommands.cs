using Domain.Filters;
using Domain.Models;

namespace Docker.Commands.Interfaces
{
    public interface IVolumeCommands
    {
        List<Volume> GetVolumes(VolumeFilter volumeFilter);
    }
}