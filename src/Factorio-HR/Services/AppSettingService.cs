using System.Configuration;

namespace Factorio_HR.Services
{
    public class AppSettingService : ISettingService
    {

        public T GetSetting<T>(string key) where T : class
        {
            return ConfigurationManager.AppSettings[key] as T;
        }
    }
}