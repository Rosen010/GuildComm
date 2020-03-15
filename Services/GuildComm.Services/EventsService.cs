﻿namespace GuildComm.Services
{
    using GuildComm.Data;
    using GuildComm.Data.Models;
    using GuildComm.Data.Models.Enums;
    using GuildComm.Services.Contracts;
    using GuildComm.Web.ViewModels.Events;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class EventsService : IEventsService
    {
        private readonly GuildCommDbContext context;

        public EventsService(GuildCommDbContext context)
        {
            this.context = context;
        }

        public async Task AddMemberToEvent(string memberId, int eventId)
        {
            var member = this.context.Members
                .SingleOrDefault(m => m.Id == memberId);

            var dbEvent = this.context.Events
                .SingleOrDefault(e => e.Id == eventId);

            if (member.GuildId != dbEvent.GuildId)
            {
                throw new InvalidOperationException("Member is not in the given guild");
            }

            var eventParticipant = new EventParticipant
            {
                Participant = member,
                Event = dbEvent
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
            var newEvent = new Event
            {
                Name = inputModel.Name,
                Date = inputModel.Date,
                EventType = (EventType)(Enum.Parse(typeof(EventType), inputModel.EventType)),
                Description = inputModel.Description,
                GuildId = inputModel.GuildId
            };

            await this.context.Events.AddAsync(newEvent);
            await this.context.SaveChangesAsync();
        }

        public async Task<List<EventsAllViewModel>> GetGuildEvents(string guildId)
        {
            var events = await this.context
                .Events
                .Include(e => e.Guild)
                .Where(e => e.GuildId == guildId)
                .Select(e => new EventsAllViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Date = e.Date,
                    EvenType = e.EventType.ToString()
                })
                .ToListAsync();

            return events;
        }
    }
}