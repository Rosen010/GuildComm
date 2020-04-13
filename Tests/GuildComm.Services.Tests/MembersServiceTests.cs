namespace GuildComm.Services.Tests
{
    using GuildComm.Data;
    using GuildComm.Data.Models.Enums;
    using GuildComm.Services.Tests.Mocks;

    using Xunit;
    using System;
    using System.Threading.Tasks;

    [Collection("GuildComm Tests")]
    public class MembersServiceTests
    {
        [Fact]
        public void ShouldCreateMember()
        {
            var db = new MockDb();

            var fakeCharacter = FakeObjects.CreateFakeCharacter(10, "Test", 1, "1");
            var fakeGuild = FakeObjects.CreateFakeGuild("10", "Test", "Test");
            var rank = Rank.Trial;

            var fakeService = this.GetMembersService(db.Context);
            var fakeMember = fakeService.CreateMember(fakeCharacter, fakeGuild, rank);

            Assert.Equal("Test", fakeMember.Character.Name);
            Assert.Equal("Test", fakeMember.Guild.Name);
        }

        [Fact]
        public async Task ShouldReturnMemberById()
        {
            var db = new MockDb();
            var fakeService = this.GetMembersService(db.Context);
            await db.ResetDbAsync();

            var fakeMember = await fakeService.GetMemberByIdAsync("1");

            Assert.NotNull(fakeMember);
            db.Context.Dispose();
        }

        [Fact]
        public async Task ShouldThrowError_IfReturnMemberBy_IdIsNull()
        {
            var db = new MockDb();
            var fakeService = this.GetMembersService(db.Context);
            await db.ResetDbAsync();

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await fakeService.GetMemberByIdAsync("123123"));
            db.Context.Dispose();
        }

        public MembersService GetMembersService(GuildCommDbContext context)
        {
            MembersService membersService = new MembersService(context);

            return membersService;
        }
    }
}
