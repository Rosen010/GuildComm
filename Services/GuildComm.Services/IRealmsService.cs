namespace GuildComm.Services
{
    using GuildComm.Data.Models;

    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IRealmsService
    {
        Task<List<Realm>> GetAllRealmsAsync();

        Task<Realm> GetRealmByNameAsync(string name);

        Task<Realm> GetRealmByIdAsync(int id);
    }
}
