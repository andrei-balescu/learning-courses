namespace DesignPatterns.Creational.Singleton;

public class AppSettings
{
    private static AppSettings s_instance;

    private IDictionary<string, object> _settings = new Dictionary<string, object>();

    private AppSettings()
    {
    }

    public static AppSettings GetInstance()
    {
        if (s_instance == null)
        {
            s_instance = new AppSettings();
        }

        return s_instance;
    }

    public T GetSetting<T>(string key)
    {
        T setting;
        if (!_settings.ContainsKey(key))
        {
            setting = default;
        }
        else
        {
            setting = (T)_settings[key];
        }

        return setting;
    }

    public void SetSetting(string key, object value)
    {
        _settings[key] = value;
    }
}