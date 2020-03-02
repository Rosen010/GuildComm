namespace GuildComm.Services.Contracts
{
    using GuildComm.Web.ViewModels.Events;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEventsService
    {
        Task CreateEvent(EventCreateInputModel inputModel);

        Task<List<EventsAllViewModel>> GetGuildEvents(string guildId);
    }
}
