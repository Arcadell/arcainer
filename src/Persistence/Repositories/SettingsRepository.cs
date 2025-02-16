using Application.Interfaces.Repositories;
using Domain.Entities;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class SettingsRepository(ApplicationDbContext context) : ISettingRepository
    {
        // TODO: Add username filter
        public List<Setting> GetSettings(string? username = null)
        {
            var settings = context.Settings.ToList();
            return settings;
        }

        public Setting CreateSetting(Setting setting)
        {
            var settingEntity = context.Settings.Add(setting);
            context.SaveChanges();
            return settingEntity.Entity;
        }

        public Setting UpdateSetting(Setting setting)
        {
            throw new NotImplementedException();
        }
    }
}
