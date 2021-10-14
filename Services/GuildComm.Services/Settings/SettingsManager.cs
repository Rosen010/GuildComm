using GuildComm.Common;
using GuildComm.Services.Settings.Contracts;
using Newtonsoft.Json;
using System;
using System.IO;

namespace GuildComm.Services.Settings
{
    public class SettingsManager : ISettingsManager
    {
        private readonly string _configurationFilePath = GlobalConstants.ConfigLocation;

        private const string _sectionNameSuffix = "Settings";

        public T LoadSection<T>() where T : class, new() => this.LoadSection(typeof(T)) as T;

        public object LoadSection(Type type)
        {
            if (!File.Exists(_configurationFilePath))
                return null;

            var jsonFile = File.ReadAllText(_configurationFilePath);
            var section = type.Name.Replace(_sectionNameSuffix, string.Empty);
            var settingsData = JsonConvert.DeserializeObject<dynamic>(jsonFile);
            var settingsSection = settingsData[section];

            return settingsSection == null
                ? null
                : JsonConvert.DeserializeObject(JsonConvert.SerializeObject(settingsSection), type);
        }

        public void UpdateSection<T>(string section, string property, string data)
        {
            if (!File.Exists(_configurationFilePath))
                throw new InvalidOperationException("Configuration file not found");

            var jsonFile = File.ReadAllText(_configurationFilePath);
            var settingsData = JsonConvert.DeserializeObject<dynamic>(jsonFile);

            settingsData[section][property] = data;
            var output = JsonConvert.SerializeObject(jsonFile, Formatting.Indented);

            File.WriteAllText(_configurationFilePath, output);
        }
    }
}
