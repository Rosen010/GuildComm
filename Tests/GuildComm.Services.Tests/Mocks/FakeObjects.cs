using GuildComm.Data.Models;
using GuildComm.Data.Models.Enums;
using GuildComm.Web.ViewModels;
using System.Collections.Generic;

namespace GuildComm.Services.Tests.Mocks
{
    public static class FakeObjects
    {
        public static Guild CreateFakeGuild()
        {
            var fakeGuild = new Guild
            {
                Id = "1",
                Name = "Pieces",
                RealmId = 1,
                Information = "Random Info",
                GuildMaster = "Nexxus"
            };

            return fakeGuild;
        }

        public static Character CreateFakeCharacter()
        {
            var fakeCharacter = new Character
            {
                Id = 1,
                Name = "Nexxus",
                Role = Role.Healer,
                Class = Class.Paladin,
                Level = 120,
                ItemLevel = 470,
                RealmId = 1
            };

            return fakeCharacter;
        }

        public static GuildCommUser CreateFakeUser()
        {
            var fakeUser = new GuildCommUser
            {
                Id = "1",
                UserName = "Gosho",
                Email = "Gosho@asd.bg"
            };

            return fakeUser;
        }

        public static Realm CreateFakeRealm(int id)
        {
            var fakeRealm = new Realm
            {
                Id = id,
                Name = "Draenor",
                RealmType = RealmType.Normal,
                Region = Region.EU,
                Guilds = new List<Guild>()
            };

            return fakeRealm;
        }
    }
}
