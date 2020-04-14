namespace GuildComm.Services.Tests
{
    using AutoMapper;
    using GuildComm.Data;
    using GuildComm.Data.Models;
    using GuildComm.Services.Tests.Mocks;
    using GuildComm.Services.Utilities;
    using GuildComm.Web.ViewModels.Applications;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("GuildComm Tests")]
    public class ApplicationsServiceTests
    {
        [Fact]
        public async Task ShouldCreateApplication()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = this.GetApplicationsService(db.Context);

            var initialApllicationsCount = db.Context.Applications.Count();

            var inputModel = new ApplicationCreateInputModel
            {
                CharacterName = "Nexxuss",
                Age = 20,
                Role = "DPS",
                Experience = "Test",
                Country = "Test",
                GuildId = "1",
                CharacterId = 1,
                ArmoryLink = "test"
            };

            await fakeService.CreateApplicationAsync(inputModel);

            Assert.True(db.Context.Applications.Count() == initialApllicationsCount + 1);
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldThrowException_IfCreateApplication_CharacterIsNull()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = this.GetApplicationsService(db.Context);

            var initialApllicationsCount = db.Context.Applications.Count();

            var inputModel = new ApplicationCreateInputModel
            {
                CharacterName = "test",
                Age = 20,
                Role = "DPS",
                Experience = "Test",
                Country = "Test",
                GuildId = "1",
                CharacterId = 1,
                ArmoryLink = "test"
            };

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await fakeService.CreateApplicationAsync(inputModel));
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldThrowException_IfCreateApplication_GuildIsNull()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = this.GetApplicationsService(db.Context);

            var initialApllicationsCount = db.Context.Applications.Count();

            var inputModel = new ApplicationCreateInputModel
            {
                CharacterName = "Nexxuss",
                Age = 20,
                Role = "DPS",
                Experience = "Test",
                Country = "Test",
                GuildId = "1123123",
                CharacterId = 1,
                ArmoryLink = "test"
            };

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await fakeService.CreateApplicationAsync(inputModel));
        }

        [Fact]
        public async Task ShouldReturnApplicationById()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = this.GetApplicationsService(db.Context);

            var application = fakeService.GetApplicationByIdAsync(1);

            Assert.NotNull(application);
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldThrowExceptionIf_GetApplicationById_IsNull()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = this.GetApplicationsService(db.Context);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await fakeService.GetApplicationByIdAsync(123123));
            db.Context.Dispose();
        }
        
        [Fact]
        public async Task ShouldReturnGuildApplications()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = this.GetApplicationsService(db.Context);

            var apllications = await fakeService.GetAllGuildApplications("1");

            Assert.True(apllications.Any());
        }

        [Fact]
        public async Task ShouldDismissApplication()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var initialApplicationsCount = db.Context.Applications.Count();
            var fakeService = this.GetApplicationsService(db.Context);

            await fakeService.Dismiss(1);

            Assert.True(db.Context.Applications.Count() == initialApplicationsCount - 1);
        }

        [Fact]
        public async Task ShouldThrowException_IfDismissApplication_IsNull()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = this.GetApplicationsService(db.Context);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await fakeService.Dismiss(123123));
        }

        private ApplicationsService GetApplicationsService(GuildCommDbContext context)
        {
            Mock<IUsersService> usersService = new Mock<IUsersService>();

            usersService.Setup(x => x.GetUserAsync())
                .Returns(Task.FromResult<GuildCommUser>(new GuildCommUser()
                {
                    Id = "123",
                    UserName = "Gosho",
                    Email = "Gosho@asd.bg",
                    Characters = new List<Character>()
                }));

            var testiningProfile = new TestiningProfile();
            var configuration = new MapperConfiguration(x => x.AddProfile(testiningProfile));
            IMapper mapper = new Mapper(configuration);

            ApplicationsService applicationsService = new ApplicationsService(context, usersService.Object, mapper);

            return applicationsService;
        }
    }
}