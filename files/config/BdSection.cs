using System.Configuration;

namespace Program;

public class BdSection : ConfigurationSection
{

    [ConfigurationProperty("Port")]
    public string Port
    {
        get { return (string)base[nameof(Port)]; }
        set { base[nameof(Port)] = value; }
    }

    [ConfigurationProperty("Host")]
    public string Host
    {
        get { return (string)base[nameof(Host)]; }
        set { base[nameof(Host)] = value; }
    }

    [ConfigurationProperty("Username")]
    public string Username
    {
        get { return (string)base[nameof(Username)]; }
        set { base[nameof(Username)] = value; }
    }

    [ConfigurationProperty("Password")]
    public string Password
    {
        get { return (string)base[nameof(Password)]; }
        set { base[nameof(Password)] = value; }
    }

    [ConfigurationProperty("DatabaseName")]
    public string DatabaseName
    {
        get { return (string)base[nameof(DatabaseName)]; }
        set { base[nameof(DatabaseName)] = value; }
    }

    [ConfigurationProperty("SchemaName")]
    public string SchemaName
    {
        get { return (string)base[nameof(SchemaName)]; }
        set { base[nameof(SchemaName)] = value; }
    }
}
