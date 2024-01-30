using System.Configuration;


namespace App
{
    public class ConfigManager
    {
        private readonly string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App.config");
        public ConfigManager()
        {
            if (!File.Exists(configFilePath))
            {
                SetConfig("N", "4");
                SetConfig("L", "7");
                SetConfig("Thread", "3000");
            }
        }
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
    }
}