using GuildComm.Services.Contracts;

namespace GuildComm.Services.Settings.Contracts
{
    public interface ISettingsManager
    {
        T LoadSection<T>() where T : class, ISettings;

        void UpdateSection<T>(ISettings settings) where T : class, ISettings;
    }
}
