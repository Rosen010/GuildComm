using GuildComm.Web.Models.Character;
using System.Threading.Tasks;

namespace GuildComm.Core.Interfaces
{
    public interface ICharacterService
    {
        Task<CharacterViewModel> FindCharacter(CharacterInputModel model);
    }
}
