namespace GuildComm.Services
{
    using GuildComm.Data.Models;
    using GuildComm.Web.ViewModels.Characters;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICharactersService
    {
        Task CreateCharacterAsync(CharacterRegisterInputModel inputModel);

        Task<List<CharacterViewModel>> GetUserCharactersViewModelAsync();

        Task<List<Character>> GetUserCharactersAsync(GuildCommUser user);
    }
}
