namespace Factorio_HR.Services
{
    public interface ISettingService
    {
        T GetSetting<T>(string key) where T : class;
    }
}