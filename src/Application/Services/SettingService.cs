﻿using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Dtos;
using Domain.Entities;

namespace Application.Services
{
    public class SettingService(ISettingRepository settingRepository) : ISettingService
    {
        public Setting GetDefaultSetting()
        {
            var settings = settingRepository.GetSettings();

            Setting? setting = settings.FirstOrDefault();
            if (setting == null) setting = settingRepository.CreateSetting(new Setting());

            return setting;
        }

        public Setting UpdateSetting(UpdateSettingDto setting)
        {
            throw new NotImplementedException();
        }
    }
}
