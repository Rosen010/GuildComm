namespace GuildComm.Services.Settings.Contracts
{
    public interface ISettingsManager
    {
        T LoadSection<T>() where T : class, new();

        void UpdateSection(string section, string property, string data);
    }
}
