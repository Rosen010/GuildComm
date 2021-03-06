﻿namespace GuildComm.Services
{
    using GuildComm.Data;
    using GuildComm.Data.Models;
    using GuildComm.Common.Constants;
    using GuildComm.Data.Models.Enums;

    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class MembersService : IMembersService
    {
        private readonly GuildCommDbContext context;

        public MembersService(GuildCommDbContext context)
        {
            this.context = context;
        }

        public Member CreateMember(Character character, Guild guild, Rank rank)
        {
            var member = new Member
            {
                Character = character,
                Guild = guild,
                Rank = rank,
                MemberSince = DateTime.UtcNow
            };

            return member;
        }

        public async Task<Member> GetMemberByIdAsync(string id)
        {
            var member = await this.context.Members.SingleOrDefaultAsync(m => m.Id == id);

            if (member == null)
            {
                throw new InvalidOperationException(ExceptionMessages.MemberNotFound);
            }

            return member;
        }

        //public async Task<bool> IsMemberAuthorizedAsync(string memberId, string guildId)
        //{
        //    var member = await this.GetMemberByIdAsync(memberId);
        //    var guild = await this.context.Guilds
        //        .SingleOrDefaultAsync(dbGuild => dbGuild.Id == guildId);

        //    return member.Guild == guild && (member.Rank == Rank.GuildeMaster || member.Rank == Rank.Officer);
        //}
    }
}
