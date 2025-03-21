﻿using Domain.Filters;
using Domain.Models;

namespace Application.Interfaces.Commands
{
    public interface IVolumeCommands
    {
        List<Volume> GetVolumes(VolumeFilter volumeFilter);
        List<BaseResponse> DeleteVolume(List<string> ids);
    }
}