using Application.Interfaces.Repositories;
using Domain.Entities;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class SettingsRepository(ApplicationDbContext context) : ISettingRepository
    {
        public List<Setting> GetSettings(string? useranme = null)
        {
            var settings = context.Settings.ToList();
            return settings;
        }
    }
}
