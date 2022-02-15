using AutoMapper;
using BNetAPI.Characters.Models.Interfaces;
using BNetAPI.Characters.Models.RequestModels;
using GuildComm.Core.Interfaces;
using GuildComm.Web.Models.Character;
using System.Threading.Tasks;

namespace GuildComm.Core
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly IBNetCharacterClient _characterClient;

        public CharacterService(IMapper mapper, IBNetCharacterClient characterClient)
        {
            _mapper = mapper;
            _characterClient = characterClient;
        }

        public async Task<CharacterViewModel> FindCharacter(CharacterInputModel model)
        {
            var characterRequest = _mapper.Map<CharacterRequestModel>(model);
            var characterResponse = await _characterClient.RequestCharacter(characterRequest);

            var viewModel = _mapper.Map<CharacterViewModel>(characterResponse);
            return viewModel;
        }
    }
}
