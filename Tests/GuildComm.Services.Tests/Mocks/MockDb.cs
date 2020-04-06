using GuildComm.Common;
using GuildComm.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuildComm.Services.Tests.Mocks
{
    public class MockDb
    {
        public MockDb()
        {
            var dbOptions = new DbContextOptionsBuilder<GuildCommDbContext>()
                .UseInMemoryDatabase(GlobalConstants.MockDatabaseName)
                .Options;

            this.Context = new GuildCommDbContext(dbOptions);
        }

        public GuildCommDbContext Context { get; set; }
    }
}
