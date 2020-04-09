using AutoMapper;
using GuildComm.Data;
using GuildComm.Data.Models;
using GuildComm.Services.Tests.Mocks;
using GuildComm.Services.Utilities;
using GuildComm.Web.ViewModels;
using GuildComm.Web.ViewModels.Guild;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GuildComm.Services.Tests
{
    public class GuildsServiceTests
    {
        [Fact]
        public async Task ShouldCreateGuild()
        {
            var db = new MockDb();

            var fakeCharacter = FakeObjects.CreateFakeCharacter();
            var fakeRealm = FakeObjects.CreateFakeRealm(1);

            using (db.Context)
            {
                db.Context.Characters.Add(fakeCharacter);
                db.Context.Realms.Add(fakeRealm);
                db.Context.SaveChanges();

                var fakeGuildInputModel = new GuildCreateInputModel
                {
                    Name = "Pieces",
                    RealmName = "Draenor",
                    Information = "Random Info",
                    MasterCharacter = fakeCharacter.Name
                };

                var mockService = this.GetGuildsService(db.Context);
                await mockService.CreateGuildAsync(fakeGuildInputModel);

                Assert.True(db.Context.Guilds.Any());
            }
        }

        [Fact]
        public async Task ShouldReturnGuildById()
        {
            var fakeGuild = FakeObjects.CreateFakeGuild();
            var db = new MockDb();

            using (db.Context)
            {
                await db.Context.Guilds.AddAsync(fakeGuild);
                await db.Context.SaveChangesAsync();

                var fakeService = this.GetGuildsService(db.Context);

                var result = await fakeService.GetGuildByIdAsync("1");
                Assert.Equal(typeof(Guild), result.GetType());
            }       
        }

        [Fact]
        public async Task ShouldReturnGuildDetailsViewModelById()
        {
            var db = new MockDb();
            var fakeService = this.GetGuildsService(db.Context);

            var result = await fakeService.GetGuildViewModelByIdAsync("1");

            Assert.Equal("1", result.Id);
        }


        [Fact]
        public async Task ShouldReturnGuildManageViewModelById()
        {
            var db = new MockDb();
            var fakeService = this.GetGuildsService(db.Context);

            var result = await fakeService.GetGuildManageViewModelByIdAsync("1");

            Assert.Equal("1", result.Id);

        }

        [Fact]
        public async Task ShouldReturnAllGuilds()
        {
            var db = new MockDb();
            var fakeService = this.GetGuildsService(db.Context);

            var fakeGuild = new Guild
            {
                Id = "2",
                Name = "Method",
                RealmId = 1,
                Realm = FakeObjects.CreateFakeRealm(2),
                GuildMaster = "Nexxus",
                Members = new List<Member>(),              
            };

            var secondFakeGuild = new Guild
            {
                Id = "3",
                Name = "asd",
                RealmId = 1,
                Realm = FakeObjects.CreateFakeRealm(3),
                GuildMaster = "Nexxus"
            };

            using (db.Context)
            {
                await db.Context.Guilds.AddAsync(fakeGuild);
                await db.Context.Guilds.AddAsync(secondFakeGuild);
                await db.Context.SaveChangesAsync();

                var result = await fakeService.GetAllGuildsAsync();
                var gulds = db.Context.Guilds.ToList();

                Assert.True(result.Any());
            }      
        }

        [Fact]
        public async Task AddMember_ShouldThrowException_IfCharacterIdIsNull()
        {
            var db = new MockDb();
            var fakeService = this.GetGuildsService(db.Context);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await fakeService.AddMemberAsync(123, "Trial", "1"));
        }

        [Fact]
        public async Task AddMember_ShouldThrowException_IfGuildIdInNull()
        {
            var db = new MockDb();
            var fakeService = this.GetGuildsService(db.Context);

            var fakeCharacter = FakeObjects.CreateFakeCharacter();

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await fakeService.AddMemberAsync(1, "Trial", "132"));
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
                }));

            var guildProfile = new GuildProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(guildProfile));
            IMapper mapper = new Mapper(configuration);

            GuildsService guildsService = new GuildsService(context, usersService.Object, mapper);

            return guildsService;
        }
    }
}
