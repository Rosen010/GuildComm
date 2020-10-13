namespace GuildComm.Services
{
    using GuildComm.Data;
    using GuildComm.Data.Models;
    using GuildComm.Common.Constants;
    using GuildComm.Services.Contracts;
    using GuildComm.Web.ViewModels.Events;

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    using AutoMapper;

    public class EventsService : IEventsService
    {
        private readonly GuildCommDbContext context;
        private readonly IMapper mapper;

        public EventsService(GuildCommDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task AddMemberToEventAsync(int characterId, int eventId)
        {
            var character = await this.context
                .Characters
                .Include(c => c.Member)
                .Where(c => c.Id == characterId)
                .FirstOrDefaultAsync();

            if (character == null)
            {
                throw new InvalidOperationException(ExceptionMessages.CharacterNotFound);
            }

            string memberId = character.MemberId;

            var member = await this.context.Members
                .SingleOrDefaultAsync(m => m.Id == memberId);

            var dbEvent = await this.context.Events
                .Include(x => x.Participants)
                .SingleOrDefaultAsync(e => e.Id == eventId);

            if (dbEvent == null)
            {
                throw new InvalidOperationException(ExceptionMessages.EventNotFound);
            }

            if (dbEvent.Participants.Any(x => x.ParticipantId == member.Id))
            {
                throw new InvalidOperationException(ExceptionMessages.MemberAlreadySigned);
            }

            if (member.GuildId != dbEvent.GuildId)
            {
                throw new InvalidOperationException(ExceptionMessages.MemberNotInGuild);
            }

            var eventParticipant = new EventParticipant
            {
                ParticipantId = member.Id,
                EventId = dbEvent.Id
            };

            dbEvent.Participants.Add(eventParticipant);
            member.Events.Add(eventParticipant);

            await this.context.EventParticipants.AddAsync(eventParticipant);
            this.context.Events.Update(dbEvent);
            this.context.Members.Update(member);
            await this.context.SaveChangesAsync();
        }

        public async Task CreateEvent(EventCreateInputModel inputModel)
        {
            if (!this.context.Guilds.Any(g => g.Id == inputModel.GuildId))
            {
                throw new InvalidOperationException(ExceptionMessages.GuildNotFound);
            }

            var newEvent = mapper.Map<Event>(inputModel);

            await this.context.Events.AddAsync(newEvent);
            await this.context.SaveChangesAsync();
        }

        public async Task<List<EventsAllViewModel>> GetGuildEvents(string guildId)
        {
            var events = await this.context
                .Events
                .Include(e => e.Guild)
                .Include(e => e.Participants)
                .Where(e => e.GuildId == guildId)
                .Select(e => mapper.Map<EventsAllViewModel>(e))
                .ToListAsync();

            return events;
        }
    }
}
