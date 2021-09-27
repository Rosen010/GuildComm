namespace GuildComm.Services.Tests.Mocks
{
    using GuildComm.Data;
    using GuildComm.Common;

    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class MockDb
    {
        //public MockDb()
        //{
        //    var dbOptions = new DbContextOptionsBuilder<GuildCommDbContext>()
        //        .UseInMemoryDatabase(GlobalConstants.MockDatabaseName)
        //        .Options;

        //    this.Context = new GuildCommDbContext(dbOptions);
        //}

        //public GuildCommDbContext Context { get; set; }

        //public async Task SeedAsync()
        //{
        //    var fakeGuild = FakeObjects.CreateFakeGuild(id: "1", name: "Pieces", guildMaster: "Nexxus");
        //    var fakeRealm = FakeObjects.CreateFakeRealm(id: 1, name: "Draenor");

        //    var fakeCharacter = FakeObjects.CreateFakeCharacter(id: 1, name: "Nexxuss", realmId: 1, guildId: "1");
        //    var secondFakeCharacter = FakeObjects.CreateFakeCharacter(id: 2, name: "Flapsy", realmId: 1, guildId: "1");
        //    var thirdFakeCharacter = FakeObjects.CreateFakeCharacter(id: 4, name: "LoshTank", realmId: 1, guildId: "1");
        //    var characterToKick = FakeObjects.CreateFakeCharacter(id: 5, name: "Chillybitz", realmId: 1, guildId: "1");
        //    var characterWithoutGuild = FakeObjects.CreateFakeCharacter(id: 6, name: "Name", realmId: 1);

        //    var fakeMember = FakeObjects.CreateFakeMember(id: "1", characterId: 2, guildId: "1");
        //    var secondFakeMember = FakeObjects.CreateFakeMember(id: "2", characterId: 4, guildId: "1");
        //    var memberToKick = FakeObjects.CreateFakeMember(id: "3", characterId: 5, guildId: "1");

        //    var fakeEvent = FakeObjects.CreateFakeEvent(id: 1, name: "Test", guildId: "1");

        //    var fakeApplication = FakeObjects.CreateFakeApllication(id: 1, characterName: "Name", characterId: 1, guildId: "1");

        //    fakeCharacter.UserId = "123";
        //    secondFakeCharacter.UserId = "123";
        //    thirdFakeCharacter.UserId = "123";
        //    characterToKick.UserId = "123";

        //    fakeGuild.Realm = fakeRealm;

        //    fakeCharacter.Guild = fakeGuild;
        //    secondFakeCharacter.Guild = fakeGuild;
        //    thirdFakeCharacter.Guild = fakeGuild;
        //    characterToKick.Guild = fakeGuild;

        //    memberToKick.Character = characterToKick;
        //    characterToKick.MemberId = "3";
        //    secondFakeCharacter.MemberId = "1";

        //    await this.Context.Realms.AddAsync(fakeRealm);
        //    await this.Context.Guilds.AddAsync(fakeGuild);

        //    await this.Context.Characters.AddAsync(fakeCharacter);
        //    await this.Context.Characters.AddAsync(secondFakeCharacter);
        //    await this.Context.Characters.AddAsync(thirdFakeCharacter);
        //    await this.Context.Characters.AddAsync(characterToKick);
        //    await this.Context.Characters.AddAsync(characterWithoutGuild);

        //    await this.Context.Members.AddAsync(fakeMember);
        //    await this.Context.Members.AddAsync(secondFakeMember);
        //    await this.Context.Members.AddAsync(memberToKick);

        //    await this.Context.Events.AddAsync(fakeEvent);

        //    await this.Context.Applications.AddAsync(fakeApplication);

        //    await this.Context.SaveChangesAsync();
        //}

        //public bool IsDbSeeded()
        //{
        //    return this.Context.Characters.Any()
        //        && this.Context.Guilds.Any()
        //        && this.Context.Realms.Any();
        //}

        //public async Task ResetDbAsync()
        //{
        //    await this.Context.Database.EnsureDeletedAsync();

        //    var dbOptions = new DbContextOptionsBuilder<GuildCommDbContext>()
        //        .UseInMemoryDatabase(GlobalConstants.MockDatabaseName)
        //        .Options;

        //    this.Context = new GuildCommDbContext(dbOptions);

        //    await this.SeedAsync();
        //}
    }
}
