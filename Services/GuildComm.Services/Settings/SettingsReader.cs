using GuildComm.Common;
using GuildComm.Services.Settings.Contracts;
using Newtonsoft.Json;
using System;
using System.IO;

namespace GuildComm.Services.Settings
{
    public class SettingsReader : ISettingsReader
    {
        private readonly string _configurationFilePath = GlobalConstants.ConfigLocation;

        private const string _sectionNameSuffix = "Settings";

        public T LoadSection<T>() where T : class, new() => this.LoadSection(typeof(T)) as T;

        public object LoadSection(Type type)
        {
            if (!File.Exists(_configurationFilePath))
                return Activator.CreateInstance(type);

            var jsonFile = File.ReadAllText(_configurationFilePath);
            var section = type.Name.Replace(_sectionNameSuffix, string.Empty);
            var settingsData = JsonConvert.DeserializeObject<dynamic>(jsonFile);
            var settingsSection = settingsData[section];

            return settingsSection == null
                ? null
                : JsonConvert.DeserializeObject(JsonConvert.SerializeObject(settingsSection), type);
        }
    }
}
