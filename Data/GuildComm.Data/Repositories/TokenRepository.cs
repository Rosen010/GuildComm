using AutoMapper;
using GuildComm.Data.Models;
using GuildComm.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GuildComm.Data.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IMapper _mapper;
        private readonly GuildCommDbContext _context;

        public TokenRepository(GuildCommDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<AccessToken> GetTokenAsync(string name)
        {
            return await _context.Tokens.FirstOrDefaultAsync(t => t.Name.Equals(name));
        }

        public async Task UpdateTokenAsync(AccessToken accessToken)
        {
            var token = await _context.Tokens.FirstOrDefaultAsync(t => t.Name == accessToken.Name);

            if (token == null)
            {
                token = _mapper.Map<AccessToken>(accessToken);
                await _context.Tokens.AddAsync(token);
            }
            else
            {
                token.Value = accessToken.Value;
                token.Expiration = accessToken.Expiration;
            }

            await _context.SaveChangesAsync();
        }
    }
}
