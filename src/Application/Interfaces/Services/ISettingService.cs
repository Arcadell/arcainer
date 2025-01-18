using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface ISettingService
    {
        Setting GetDefaultSetting();
    }
}
