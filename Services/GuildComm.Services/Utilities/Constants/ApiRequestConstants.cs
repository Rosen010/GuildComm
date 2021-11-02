namespace GuildComm.Services.Utilities.Constants
{
    public static class ApiRequestConstants
    {
        public static class Headers
        {
            public const string Authorization = "Authorization";

            public const string GrantType = "grant_type";
        }

        public static class HeaderValues
        {
            public const string BearerTokenFormat = "Bearer {0}";
        }

        public static class AuthenticationType
        {
            public const string Bearer = "Bearer";

            public const string Basic = "Basic";
        }

        public static class GrantTypes
        {
            public const string ClientCredentials = "client_credentials";

            public const string Password = "password";
        }
    }
}
