namespace GuildComm.Services
{
    using GuildComm.Data.Models;
    using GuildComm.Web.ViewModels.Realms;

    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IRealmsService
    {
        Task CreateRealmAsync(RealmCreateInputModel inputModel);

        Task<List<RealmViewModel>> GetAllRealmViewModelsAsync();

        Task<Realm> GetRealmByNameAsync(string name);

        Task<Realm> GetRealmByIdAsync(int id);
    }
}
