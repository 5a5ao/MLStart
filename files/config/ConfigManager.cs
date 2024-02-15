using System.Configuration;
using System.IO;
using System.Reflection;


namespace Program;

public class ConfigManager
{
    #region Data

    private static string configFileName = "MLStart.dll.config";

    #endregion

    #region .ctor

    static ConfigManager()
    {
        string executableLocation = Assembly.GetExecutingAssembly().Location;
        string configFilePath = Path.Combine(Path.GetDirectoryName(executableLocation), configFileName);
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