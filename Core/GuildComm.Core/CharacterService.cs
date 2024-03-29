﻿using AutoMapper;
using BNetAPI.Core.Components.Characters.Models.Constants;
using BNetAPI.Core.Components.Characters.Models.Interfaces;
using BNetAPI.Core.Components.Characters.Models.RequestModels;
using GuildComm.Common.Constants;
using GuildComm.Core.Interfaces;
using GuildComm.Web.Models.Character;

using System.Linq;
using System.Net;
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

        public async Task<CharacterViewModel> FindCharacterAsync(CharacterInputModel model)
        {
            var characterRequest = _mapper.Map<CharacterRequestModel>(model);
            var characterResponse = await _characterClient.RequestCharacterAsync(characterRequest);
            var characterMediaResponse = await _characterClient.RequestCharacterMediaAsync(characterRequest);

            if (characterResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                var viewModel = _mapper.Map<CharacterViewModel>(characterResponse);

                if (characterMediaResponse.Assets != null && characterMediaResponse.Assets.Any())
                {
                    viewModel.CharacterRender = characterMediaResponse.Assets.FirstOrDefault(a => a.Key.Equals(CharacterAssets.Main)).Value;
                }
                else
                {
                    viewModel.CharacterRender = MediaConstants.DefaultCharacterRender;
                }
               
                return viewModel;
            }

            return null;
        }
    }
}
