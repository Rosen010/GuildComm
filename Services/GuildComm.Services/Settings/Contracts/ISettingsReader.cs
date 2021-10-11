namespace GuildComm.Services.Settings.Contracts
{
    public interface ISettingsReader
    {
        T LoadSection<T>() where T : class, new();
    }
}
