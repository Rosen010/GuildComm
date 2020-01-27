namespace GuildComm.Services
{
    using GuildComm.Data.Models;
    using System.Threading.Tasks;

    public interface IGuildsService
    {
        void CreateGuild(Guild guild);

        Guild GetGuild(string name);
    }
}
