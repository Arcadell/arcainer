using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;

namespace Application.Services
{
    public class SettingService(ISettingRepository settingRepository) : ISettingService
    {
        public Setting GetSetting()
        {
            var settings = settingRepository.GetSettings();

            Setting? setting = settings.FirstOrDefault();
            if (setting == null) throw new Exception("Settings not found");

            return setting;
        }
    }
}
