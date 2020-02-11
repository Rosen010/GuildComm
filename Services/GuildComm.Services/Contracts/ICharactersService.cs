namespace GuildComm.Services
{
    using GuildComm.Data.Models;
    using GuildComm.Web.ViewModels.Characters;

    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface ICharactersService
    {
        Task CreateCharacterAsync(CharacterRegisterInputModel inputModel);

        Task<List<CharacterViewModel>> GetUserCharactersViewModelAsync();

        Task<List<Character>> GetUserCharactersAsync(GuildCommUser user);

        Task RemoveCharacterAsync(int id);

        Task<CharacterDetailsViewModel> GetCharacterAsync(int id);
    }
}
