using GuildComm.Common;
using GuildComm.Common.Constants;
using GuildComm.Services.Contracts;
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

        public T LoadSection<T>() where T : class, ISettings => this.LoadSection(typeof(T)) as T;

        public void UpdateSection<T>(ISettings settings) where T : class, ISettings => this.UpdateSection(typeof(T), settings);

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

        public void UpdateSection(Type type, ISettings settings)
        {
            if (!File.Exists(_configurationFilePath))
                throw new InvalidOperationException(ExceptionMessages.ConfigurationNotFound);

            var jsonFile = File.ReadAllText(_configurationFilePath);
            var settingsData = JsonConvert.DeserializeObject<dynamic>(jsonFile);
            var section = type.Name.Replace(_sectionNameSuffix, string.Empty);

            foreach (var property in type.GetProperties())
            {
                settingsData[section][property.Name] = (Type)settings.GetType().GetProperty(property.Name).GetValue(settings);
            }
            
            var output = JsonConvert.SerializeObject(jsonFile, Formatting.Indented);
            File.WriteAllText(_configurationFilePath, output);
        }
    }
}
