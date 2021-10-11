namespace GuildComm.Common
{
    using System.IO;
    using System.Reflection;

    public static class GlobalConstants
    {
        public static string currentDirectory = Directory.GetCurrentDirectory();

        public static string userJsonLocation = currentDirectory + @"../../Data/GuildComm.Data/Seeding/Datasets/Users.Json";

        public static string ConfigLocation = currentDirectory + @"/config.json";

        public static string MockDatabaseName = "MockDbContext";

        public static string NoValueTemplate = "N/A";

        public const string AdminRole = "Admin";

        public const string AdministrationArea = "Administration";

        public const string TestCollectionName = "GuildComm Tests";
    }
}
