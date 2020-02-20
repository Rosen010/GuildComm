namespace GuildComm.Services
{
    using GuildComm.Data.Models;
    using GuildComm.Web.ViewModels.Characters;

    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface ICharactersService
    {
        Task CreateCharacterAsync(CharacterRegisterInputModel inputModel);

        Task<List<T>> GetUserCharactersViewModelAsync<T>();

        Task RemoveCharacterAsync(int id);

        Task<T> GetCharacterAsync<T>(int id);
    }
}
