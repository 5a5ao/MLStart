using System.Configuration;
using System.IO;


namespace Program;

public class ConfigManager
{
    #region Data

    //Файл конфигурации и его местоположение
    private static readonly string configFileName = "MLStart.dll.config";
    private static readonly string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFileName);

    #endregion

    #region .ctor

    static ConfigManager()
    {
        if (!File.Exists(configFilePath))
        {
            SetConfig("N", "4");
            SetConfig("L", "7");
            SetConfig("Thread", "3000");
            SetBdConnection("localhost", "5432", "postgres", "admin", "MLStartUsers", "public");
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

    public static void SetBdConnection(string Host, string Port, string UserName, string Password, string DatabaseName, string SchemaName)
    {
        var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        var postgreSql = (BdSection)configFile.GetSection("PostgreSql");

        if (postgreSql == null)
        {
            postgreSql = new BdSection();
            configFile.Sections.Add("PostgreSql", postgreSql);
        }

        postgreSql.Host = Host;
        postgreSql.Port = Port;
        postgreSql.Username = UserName;
        postgreSql.Password = Password;
        postgreSql.DatabaseName = DatabaseName;
        postgreSql.SchemaName = SchemaName;

        configFile.Save(ConfigurationSaveMode.Modified);
    }

    public static string? BdConnection(string key)
    {
        return ConfigurationManager.AppSettings[key];
    }

    public static string? GetConfig(string key)
    {
        return ConfigurationManager.AppSettings[key];
    }

    #endregion

}