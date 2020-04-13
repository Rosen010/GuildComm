namespace GuildComm.Services.Tests
{
    using GuildComm.Data;
    using GuildComm.Services.Utilities;
    using GuildComm.Services.Tests.Mocks;

    using Xunit;
    using AutoMapper;

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [Collection("GuildComm Tests")]
    public class RealmsServiceTests
    {
        [Fact]
        public async Task ShouldReturnAllRealmsViewModels()
        {
            var db = new MockDb();
            var fakeService = this.GetRealmsService(db.Context);
            await db.ResetDbAsync();

            var fakeRealmsCount = db.Context.Realms.Count();

            var fakeRealms = await fakeService.GetAllRealmViewModelsAsync();

            Assert.True(fakeRealms.Count() == fakeRealmsCount);
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldReturnRealmByName()
        {
            var db = new MockDb();
            var fakeService = this.GetRealmsService(db.Context);
            await db.ResetDbAsync();

            var realm = await fakeService.GetRealmByNameAsync("Draenor");
            Assert.Equal("Draenor", realm.Name);
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldReturnRealmById()
        {
            var db = new MockDb();
            var fakeService = this.GetRealmsService(db.Context);
            await db.ResetDbAsync();

            var realm = await fakeService.GetRealmByIdAsync(1);
            Assert.Equal("Draenor", realm.Name);
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldThrowExceptionIf_ReturnRealmByName_IsNull()
        {
            var db = new MockDb();
            var fakeService = this.GetRealmsService(db.Context);
            await db.ResetDbAsync();

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await fakeService.GetRealmByNameAsync("Test"));
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldThrowExceptionIf_ReturnRealmById_IsNull()
        {
            var db = new MockDb();
            var fakeService = this.GetRealmsService(db.Context);
            await db.ResetDbAsync();

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await fakeService.GetRealmByIdAsync(123123));
            db.Context.Dispose();
        }

        private RealmsService GetRealmsService(GuildCommDbContext context)
        {
            var testiningProfile = new TestiningProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(testiningProfile));
            IMapper mapper = new Mapper(configuration);

            RealmsService realmsService = new RealmsService(context, mapper);

            return realmsService;
        }
    }
}
