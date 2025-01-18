using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface ISettingRepository
    {
        List<Setting> GetSettings(string? username = null);
        Setting CreateSetting(Setting setting);
    }
}
