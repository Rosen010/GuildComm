using BNetAPI.Characters.Models.Interfaces;
using GuildComm.Core.Interfaces;

namespace GuildComm.Core
{
    public class CharacterService : ICharacterService
    {
        private readonly IBNetCharacterClient _characterClient;

        public CharacterService(IBNetCharacterClient characterClient)
        {
            _characterClient = characterClient;
        }
    }
}
