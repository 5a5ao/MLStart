using System;
using System.Configuration;
using System.IO;
using System.Xml;

namespace App
{
    class ConfigManager
    {
        private readonly Configuration config;
        private readonly KeyValueConfigurationCollection settings;
        private readonly string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App.config");

        public ConfigManager()
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            settings = config.AppSettings.Settings;

            if (!File.Exists(configFilePath))
            {
                CreateConfigFile(configFilePath);
                CreateSetting("N", "4");
                CreateSetting("L", "7");
                Refresh();
            }
        }

        public void CreateConfigFile(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.InsertBefore(xmlDeclaration, null);

            XmlElement configuration = doc.CreateElement("configuration");
            XmlElement appSettings = doc.CreateElement("appSettings");

            XmlElement startup = doc.CreateElement("startup");
            XmlElement supportedRuntime = doc.CreateElement("supportedRuntime");
            supportedRuntime.SetAttribute("version", "v4.0");
            supportedRuntime.SetAttribute("sku", ".NETFramework,Version=v4.8");

            startup.AppendChild(supportedRuntime);
            configuration.AppendChild(appSettings);
            configuration.AppendChild(startup);
            doc.AppendChild(configuration);

            doc.Save(filePath);
        }
        public void CreateSetting(string key, string value)
        {
            if (settings[key] == null)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(configFilePath);

                XmlNode appSettingsNode = doc.SelectSingleNode("//appSettings");

                XmlElement addElement = doc.CreateElement("add");
                addElement.SetAttribute("key", key);
                addElement.SetAttribute("value", value);

                appSettingsNode?.AppendChild(addElement);

                doc.Save(configFilePath);
                Refresh();
            }
        }
        public void UpdateSetting(string key, string value)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(configFilePath);

            XmlNode appSettingsNode = doc.SelectSingleNode("//appSettings");

            XmlNode existingNode = appSettingsNode.SelectSingleNode($"add[@key='{key}']");
            existingNode?.ParentNode?.RemoveChild(existingNode);

            XmlElement addElement = doc.CreateElement("add");
            addElement.SetAttribute("key", key);
            addElement.SetAttribute("value", value);

            appSettingsNode?.AppendChild(addElement);

            doc.Save(configFilePath);
            Refresh();
        }
        public void Refresh()
        {
            ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
        }
    }
}