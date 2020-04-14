namespace GuildComm.Services.Tests.Mocks
{
    using GuildComm.Data.Models;
    using GuildComm.Data.Models.Enums;

    using System;
    using System.Collections.Generic;

    public static class FakeObjects
    {
        public static Guild CreateFakeGuild(string id, string name, string guildMaster)
        {
            var fakeGuild = new Guild
            {
                Id = id,
                Name = name,
                RealmId = 1,
                Information = "Random Info",
                GuildMaster = guildMaster
            };

            return fakeGuild;
        }

        public static Character CreateFakeCharacter(int id, string name, int realmId, string guildId = null)
        {
            var fakeCharacter = new Character
            {
                Id = id,
                Name = name,
                Role = Role.Healer,
                Class = Class.Paladin,
                Level = 120,
                ItemLevel = 470,
                RealmId = realmId
            };

            return fakeCharacter;
        }

        public static Realm CreateFakeRealm(int id, string name)
        {
            var fakeRealm = new Realm
            {
                Id = id,
                Name = name,
                RealmType = RealmType.Normal,
                Region = Region.EU,
                Guilds = new List<Guild>()
            };

            return fakeRealm;
        }

        public static Member CreateFakeMember(string id, int characterId, string guildId, Rank rank = Rank.Trial)
        {
            var fakeMember = new Member
            {
                Id = id,
                CharacterId = characterId,
                GuildId = guildId,
                Info = "Random info",
                Rank = rank,
                MemberSince = DateTime.UtcNow,
                Events = new List<EventParticipant>()
            };

            return fakeMember;
        }

        public static Event CreateFakeEvent(int id, string name, string guildId)
        {
            var fakeEvent = new Event
            {
                Id = id,
                EventType = EventType.Raid,
                Description = "Test",
                Date = DateTime.UtcNow,
                Name = name,
                GuildId = guildId,
                Participants = new List<EventParticipant>()
            };

            return fakeEvent;
        }

        public static Application CreateFakeApllication(int id, string characterName, int characterId, string guildId)
        {
            var fakeApplication = new Application
            {
                Id = id,
                CharacterName = characterName,
                Age = 20,
                Role = Role.DPS,
                Experience = "Test",
                Country = "Test",
                GuildId = guildId,
                CharacterId = characterId,
                ArmoryLink = "test"
            };

            return fakeApplication;
        }
    }
}
