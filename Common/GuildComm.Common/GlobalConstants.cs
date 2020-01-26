namespace GuildComm.Common
{
    using System;
    using System.Reflection;

    public static class GlobalConstants
    {
        public static string currentDirectory = Assembly.GetExecutingAssembly().Location;
        public static string realmJsonLocation = currentDirectory + @"../../../../../../../Data/GuildComm.Data/Seeding/Datasets/Realms.Json";
    }
}
