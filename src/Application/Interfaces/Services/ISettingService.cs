using Domain.Dtos;
using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface ISettingService
    {
        Setting GetDefaultSetting();
        Setting UpdateSetting(UpdateSettingDto setting);
    }
}
