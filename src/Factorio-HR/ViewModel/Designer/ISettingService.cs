using System.Configuration;

namespace Factorio_HR.ViewModel.Designer
{
    public interface ISettingService
    {
        T GetSetting<T>(string key) where T : class;
    }

    public class SettingService : ISettingService
    {

        public T GetSetting<T>(string key) where T : class
        {
            return ConfigurationManager.AppSettings[key] as T;
        }
    }
}