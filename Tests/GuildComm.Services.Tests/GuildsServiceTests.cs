namespace GuildComm.Services.Tests
{
    using GuildComm.Data;
    using GuildComm.Data.Models;
    using GuildComm.Web.ViewModels;
    using GuildComm.Data.Models.Enums;
    using GuildComm.Services.Utilities;
    using GuildComm.Services.Tests.Mocks;

    using Moq;
    using Xunit;
    using AutoMapper;

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    [Collection("GuildComm Tests")]
    public class GuildsServiceTests
    {    
        [Fact]
        public async Task ShouldCreateGuild()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeCharacter = FakeObjects.CreateFakeCharacter(3, "Velev", 1);

            var fakeGuildInputModel = new GuildCreateInputModel
            {
                Name = "FakeGuild",
                RealmName = "Draenor",
                Information = "Random Info",
                MasterCharacter = fakeCharacter.Name
            };

            using (db.Context)
            {
                db.Context.Characters.Add(fakeCharacter);
                db.Context.SaveChanges();

                var mockService = this.GetGuildsService(db.Context);
                await mockService.CreateGuildAsync(fakeGuildInputModel);

                Assert.True(db.Context.Guilds.Any());
                db.Context.Dispose();
            }
        }

        [Fact]
        public async Task ShouldReturnGuildById()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = this.GetGuildsService(db.Context);

            var result = await fakeService.GetGuildByIdAsync("1");
            Assert.Equal(typeof(Guild), result.GetType());
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldReturnGuildDetailsViewModelById()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = this.GetGuildsService(db.Context);

            var result = await fakeService.GetGuildViewModelByIdAsync("1");

            Assert.Equal("1", result.Id);
            db.Context.Dispose();
        }


        [Fact]
        public async Task ShouldReturnGuildManageViewModelById()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = this.GetGuildsService(db.Context);

            var result = await fakeService.GetGuildManageViewModelByIdAsync("1");

            Assert.Equal("1", result.Id);
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldReturnAllGuilds()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = this.GetGuildsService(db.Context);

            var result = await fakeService.GetAllGuildsAsync();

            Assert.True(result.Any());
            db.Context.Dispose();

        }

        [Fact]
        public async Task AddMember_ShouldThrowException_IfCharacterIdIsNull()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = this.GetGuildsService(db.Context);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await fakeService.AddMemberAsync(123, "Trial", "1"));
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldRemoveMember()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var initialCount = db.Context.Members.Count();

            var fakeService = this.GetGuildsService(db.Context);

            await fakeService.RemoveMemberAsync("3");
            var expectedCount = db.Context.Members.Count();

            Assert.Equal(initialCount - 1, expectedCount);
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldThrowError_IfInvalidId()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = this.GetGuildsService(db.Context);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await fakeService.RemoveMemberAsync("123123"));

            db.Context.Dispose();
        }

        [Fact]
        public async Task AddMember_ShouldThrowException_IfGuildIdInNull()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = this.GetGuildsService(db.Context);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await fakeService.AddMemberAsync(1, "Trial", "132"));
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldReturnUserGuilds()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = this.GetGuildsService(db.Context);

            var result = await fakeService.GetUserGuildsAsync();

            Assert.True(result.Any());
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldReturnIfUserIsInTargetGuild()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = this.GetGuildsService(db.Context);

            var result = await fakeService.IsUserInTargetGuild("1");

            Assert.True(result);
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldReturnIfUserIsAuthorized()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = this.GetGuildsService(db.Context);

            var result = await fakeService.IsUserAuthorized("1");
            Assert.True(result);
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldPromoteMember()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = this.GetGuildsService(db.Context);
            await fakeService.PromoteMemberAsync("1");

            var member = await db.Context.Members.FirstOrDefaultAsync(m => m.Id == "1");

            Assert.Equal(Rank.Member, member.Rank);
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldDemoteMember()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = this.GetGuildsService(db.Context);
            await fakeService.PromoteMemberAsync("2");
            await fakeService.PromoteMemberAsync("2");
            await fakeService.DemoteMemberAsync("2");

            var member = await db.Context.Members.FirstOrDefaultAsync(m => m.Id == "2");

            Assert.Equal(Rank.Member, member.Rank);
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldThrowError_WhenPromoting_FromHighestRank()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var member = await db.Context.Members.FirstOrDefaultAsync(m => m.Id == "2");
            member.Rank = Rank.Officer;
            db.Context.Update(member);
            await db.Context.SaveChangesAsync();

            var fakeService = this.GetGuildsService(db.Context);
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await fakeService.PromoteMemberAsync("2"));

            member.Rank = Rank.Trial;
            await db.Context.SaveChangesAsync();
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldThrowError_WhenDemoting_FromLowestRank()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var member = await db.Context.Members.FirstOrDefaultAsync(m => m.Id == "2");
            member.Rank = Rank.Trial;
            db.Context.Update(member);
            await db.Context.SaveChangesAsync();

            var fakeService = this.GetGuildsService(db.Context);
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await fakeService.DemoteMemberAsync("2"));

            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldRemoveGuild()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = this.GetGuildsService(db.Context);
            await fakeService.RemoveGuildAsync("1");

            Assert.True(!db.Context.Guilds.Any());

            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldThrowError_ifRemoveGuildId_IsInvalid()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = this.GetGuildsService(db.Context);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await fakeService.RemoveGuildAsync("123123"));

            db.Context.Dispose();
        }

        private GuildsService GetGuildsService(GuildCommDbContext context)
        {
            Mock<IUsersService> usersService = new Mock<IUsersService>();

            usersService.Setup(x => x.GetUserAsync())
                .Returns(Task.FromResult<GuildCommUser>(new GuildCommUser()
                {
                    Id = "123",
                    UserName = "Gosho",
                    Email = "Gosho@asd.bg",
                    Characters = new List<Character> { FakeObjects.CreateFakeCharacter(20, "Nexxus", 1, "1") }
                }));

            var testiningProfile = new TestiningProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(testiningProfile));
            IMapper mapper = new Mapper(configuration);

            GuildsService guildsService = new GuildsService(context, usersService.Object, mapper);

            return guildsService;
        }
    }
}
