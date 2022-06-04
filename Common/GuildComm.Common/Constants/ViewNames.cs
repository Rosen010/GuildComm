namespace GuildComm.Common
{
    public static class ViewNames
    {
        public const string ErrorPage = "/Error/HandleError/{0}";

        public static class Shared
        {
            public const string Error = "~/Views/Shared/_Error.cshtml";

            public const string NotFound = "~/Views/Shared/_NotFound.cshtml";
        }

        public static class Partial
        {
            public const string GuildForm = "~/Views/Home/GuildForm.cshtml";

            public const string CharacterForm = "~/Views/Home/CharacterForm.cshtml";
        }  
    }
}
