using System;
using System.Threading;
using DictationProcessorLib.DataContracts;
using Microsoft.Extensions.Configuration;

namespace DictationProcessorLib.Services
{
    public class ConfigurationService
    {
        private Lazy<IConfiguration> _configuration;

        public ConfigurationService()
        {
            _configuration = new Lazy<IConfiguration>(BuildConfiguration, LazyThreadSafetyMode.ExecutionAndPublication);
        }
        public AppSettings GetAppSettings()
        {
            const string c_appSettingSection = "AppSettings";
            var appSettingsSection = _configuration.Value.GetSection(c_appSettingSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            return appSettings;
        }

        private IConfiguration BuildConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();

            const string c_settingsFileName = "appsettings.json";
            configurationBuilder.AddJsonFile(c_settingsFileName);

            IConfiguration config = configurationBuilder.Build();
            return config;
        }
    }
}