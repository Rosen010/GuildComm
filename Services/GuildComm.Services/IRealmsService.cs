namespace GuildComm.Services
{
    using GuildComm.Data.Models;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRealmsService
    {
        Task<List<Realm>> GetAllRealmsAsync();

        Task<Realm> GetRealmByNameAsync(string name);

        Task<Realm> GetRealmByIdAsync(int id);
    }
}
