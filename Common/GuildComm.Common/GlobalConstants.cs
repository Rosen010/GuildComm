namespace GuildComm.Common
{
    using System.Reflection;

    public static class GlobalConstants
    {
        public static string currentDirectory = Assembly.GetExecutingAssembly().Location;

        public static string realmJsonLocation = currentDirectory + @"../../../../../../../Data/GuildComm.Data/Seeding/Datasets/Realms.Json";

        public static string userJsonLocation = currentDirectory + @"../../../../../../../Data/GuildComm.Data/Seeding/Datasets/Users.Json";
    }
}
