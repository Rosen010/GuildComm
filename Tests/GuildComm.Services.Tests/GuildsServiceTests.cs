using GuildComm.Data.Models;
using GuildComm.Services.Tests.Mocks;
using GuildComm.Web.ViewModels.Guild;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace GuildComm.Services.Tests
{
    public class GuildsServiceTests
    {
        [Fact]
        public async Task ShouldReturnGuildById()
        {
            var db = new MockDb();
            var fakeService = new GuildsService(db.Context);

            var fakeUser = FakeObjects.CreateFakeUser();
            var fakeRealm = FakeObjects.CreateFakeRealm();
            var fakeCharacter = FakeObjects.CreateFakeCharacter();
            var fakeGuild = FakeObjects.CreateFakeGuild();

            using (db.Context)
            {
                db.Context.Add(fakeUser);
                db.Context.Add(fakeRealm);
                db.Context.Add(fakeCharacter);
                db.Context.Add(fakeGuild);
                db.Context.SaveChanges();

                var result = await fakeService.GetGuildByIdAsync("1");
                Assert.Equal(typeof(Guild), result.GetType());


            }
        }

        [Fact]
        public async Task ShouldReturnGuildDetailsViewModelById()
        {
            var db = new MockDb();
            var fakeService = new Mock<IGuildsService>();

            var fakeModel = new GuildDetailsViewModel
            {
                Id = "123",
                Name = "Pieces",
                RealmName = "Draenor",
                RealmRegion = "EU"
            };

            fakeService.Setup(x => x.GetGuildViewModelByIdAsync("123")).Returns(Task.FromResult(fakeModel));
            var result = await fakeService.Object.GetGuildViewModelByIdAsync("123");

            Assert.Equal(fakeModel.Id, result.Id);
        }
    }
}
