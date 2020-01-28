namespace GuildComm.Services
{
    using GuildComm.Data.Models;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRealmsService
    {
        Task<IList<Realm>> GetAllRealmsAsync();

        Task<Realm> GetRealmAsync(string name);
    }
}
