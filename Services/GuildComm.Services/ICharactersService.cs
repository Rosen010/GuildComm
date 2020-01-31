namespace GuildComm.Services
{
    using GuildComm.Web.ViewModels.Characters;
    using System.Threading.Tasks;

    public interface ICharactersService
    {
        Task CreateCharacterAsync(CharacterRegisterInputModel inputModel);
    }
}
