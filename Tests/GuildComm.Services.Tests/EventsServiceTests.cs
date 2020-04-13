namespace GuildComm.Services.Tests
{

    using GuildComm.Data;
    using GuildComm.Services.Utilities;
    using GuildComm.Services.Tests.Mocks;
    using GuildComm.Web.ViewModels.Events;

    using Xunit;
    using AutoMapper;

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [Collection("GuildComm Tests")]
    public class EventsServiceTests
    {
        [Fact]
        public async Task ShouldCreateEvent()
        {
            var db = new MockDb();
            await db.ResetDbAsync();
            var initialCount = db.Context.Events.Count();

            EventCreateInputModel inputModel = new EventCreateInputModel
            {
                Name = "Test",
                Date = DateTime.UtcNow,
                EventType = "Raid",
                Description = "Test",
                GuildId = "1"
            };

            var fakeService = this.GetEventsService(db.Context);
            await fakeService.CreateEvent(inputModel);

            Assert.True(initialCount + 1 == db.Context.Events.Count());
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldThrowException_WhenInvalidGuildId()
        {
            var db = new MockDb();
            await db.ResetDbAsync();
            var initialCount = db.Context.Events.Count();

            EventCreateInputModel inputModel = new EventCreateInputModel
            {
                Name = "Test",
                Date = DateTime.UtcNow,
                EventType = "Raid",
                Description = "Test",
                GuildId = "1123"
            };

            var fakeService = this.GetEventsService(db.Context);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await fakeService.CreateEvent(inputModel));
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldAddMemberToEvent()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = this.GetEventsService(db.Context);

            var initialParticipantsCount = db.Context.Events
                .FirstOrDefault(x => x.Id == 1)
                .Participants.Count();

            await fakeService.AddMemberToEventAsync(2, 1);

            var result = db.Context.Events
                .FirstOrDefault(x => x.Id == 1)
                .Participants.Count();

            Assert.True(result == initialParticipantsCount + 1);
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldThrowException_IfAddMemberToEvent_CharacterIsNull()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = this.GetEventsService(db.Context);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await fakeService.AddMemberToEventAsync(12312, 1));
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldThrowException_IfAddMemberToEvent_EventIsNull()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = this.GetEventsService(db.Context);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await fakeService.AddMemberToEventAsync(2, 123123));
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldThrowException_IfAddMemberToEvent_IsAlreadySignedUp()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = this.GetEventsService(db.Context);
            await fakeService.AddMemberToEventAsync(2, 1);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await fakeService.AddMemberToEventAsync(2, 1));
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldReturnAllEvents()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = this.GetEventsService(db.Context);

            var events = await fakeService.GetGuildEvents("1");

            Assert.True(events.Any());
        }

        private EventsService GetEventsService(GuildCommDbContext context)
        {
            var testiningProfile = new TestiningProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(testiningProfile));
            IMapper mapper = new Mapper(configuration);

            EventsService eventsService = new EventsService(context, mapper);

            return eventsService;
        }
    }
}
