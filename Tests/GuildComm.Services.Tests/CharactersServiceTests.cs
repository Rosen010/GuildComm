namespace GuildComm.Services.Tests
{
    using GuildComm.Data;
    using GuildComm.Data.Models;
    using GuildComm.Data.Models.Enums;
    using GuildComm.Services.Utilities;
    using GuildComm.Services.Tests.Mocks;
    using GuildComm.Web.ViewModels.Characters;

    using Moq;
    using Xunit;
    using AutoMapper;

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    [Collection("GuildComm Tests")]
    public class CharactersServiceTests
    {
        [Fact]
        public async Task ShouldCreateCharacter()
        {
            var db = new MockDb();
            await db.ResetDbAsync();
            var fakeService = GetCharactersService(db.Context);

            var initialCharacterCount = db.Context.Characters.Count();

            CharacterRegisterInputModel inputModel = new CharacterRegisterInputModel
            {
                Name = "Test",
                Class = Class.DeathKnight,
                ItemLevel = 400,
                Level = 100,
                RealmName = "Draenor",
                Role = Role.DPS
            };
         
            await fakeService.CreateCharacterAsync(inputModel);

            Assert.True(db.Context.Characters.Count() == initialCharacterCount + 1);
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldThrowException_CreateCharacter_IfRealmIsInvalid()
        {
            var db = new MockDb();
            await db.ResetDbAsync();
            var fakeService = GetCharactersService(db.Context);

            var initialCharacterCount = db.Context.Characters.Count();

            CharacterRegisterInputModel inputModel = new CharacterRegisterInputModel
            {
                Name = "Test",
                Class = Class.DeathKnight,
                ItemLevel = 400,
                Level = 100,
                RealmName = "test",
                Role = Role.DPS
            };

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await fakeService.CreateCharacterAsync(inputModel));
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldReturnUserCharacters()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = GetCharactersService(db.Context);

            var characters = await fakeService.GetUserCharactersAsync<Character>();

            Assert.True(characters.Any());
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldReturnCharactersByName()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = GetCharactersService(db.Context);

            var characters = await fakeService.GetCharactersByNameAsync<Character>("Nexxuss");

            Assert.True(characters.Any());
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldReturnCharacterById()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = GetCharactersService(db.Context);

            var character = await fakeService.GetCharacterAsync<Character>(1);

            Assert.Equal("Nexxuss", character.Name);
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldThrowException_IfGetCharacter_IsNull()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = GetCharactersService(db.Context);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await fakeService.GetCharacterAsync<Character>(123123));
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldRemoveCharacter()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = GetCharactersService(db.Context);

            var initialCharacterCount = db.Context.Characters.Count();

            await fakeService.RemoveCharacterAsync(1);
            Assert.True(db.Context.Characters.Count() == initialCharacterCount - 1);
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldThrowException_IfRemoveCharacter_IsNull()
        {
            var db = new MockDb();
            await db.ResetDbAsync();

            var fakeService = GetCharactersService(db.Context);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await fakeService.RemoveCharacterAsync(123123));
            db.Context.Dispose();
        }

        private CharactersService GetCharactersService(GuildCommDbContext context)
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

            CharactersService charactersService = new CharactersService(context, usersService.Object, mapper);

            return charactersService;
        }
    }
}
