using System.Configuration;


namespace Program;

public class ConfigManager
{
    #region Data
    private readonly string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App.config");

    #endregion

    #region .ctor
    public ConfigManager()
    {
        if (!File.Exists(configFilePath))
        {
            SetConfig("N", "4");
            SetConfig("L", "7");
            SetConfig("Thread", "3000");
        }
    }

    #endregion

    #region Methods
    public static void SetConfig(string key, string value)
    {
        var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        var settings = configFile.AppSettings.Settings;
        if (settings[key] == null)
        {
            settings.Add(key, value);
        }
        else
        {
            settings[key].Value = value;
        }
        configFile.Save(ConfigurationSaveMode.Modified);
        ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
    }
    public static string GetConfig(string key)
    {
        return ConfigurationManager.AppSettings[key];
    }

    #endregion

}