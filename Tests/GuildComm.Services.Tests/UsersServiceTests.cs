using GuildComm.Data.Models;
using GuildComm.Services.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace GuildComm.Services.Tests
{
    public class UsersServiceTests
    {
        [Fact]
        public void ShouldReturnUserById()
        {
            var db = new MockDb();

            var mockUser = new GuildCommUser
            {
                Id = "1",
                UserName = "Gosho",
                Email = "Gosho@asd.bg"
            };

            db.Context.Users.Add(mockUser);

            using (db.Context)
            {
                var usersService = new UsersService(db.Context);

                var user = db.Context.Users.SingleOrDefaultAsync(u => u.Id == "1");
                Assert.Equal("1", user.Id.ToString());
            }
        }
    }
}
