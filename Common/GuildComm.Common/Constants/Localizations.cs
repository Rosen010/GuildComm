using System.Collections.Generic;

namespace GuildComm.Common.Constants
{
    public static class Localizations
    {
        public static readonly Dictionary<string, string> MappedLocalizations = new Dictionary<string, string>
        {
            { Namespace.ProfileEU, Locale.GB },
            { Namespace.ProfileUS, Locale.US },
            { Namespace.ProfileKR, Locale.KR },
            { Namespace.ProfileTW, Locale.TW },
        };

        public static class Namespace
        {
            public const string ProfileEU = "profile-eu";

            public const string ProfileUS = "profile-us";

            public const string ProfileKR = "profile-kr";

            public const string ProfileTW = "profile-tw";
        }

        public static class Locale
        {
            public const string GB = "en_GB";

            public const string ES = "es_ES";

            public const string RU = "ru_RU";

            public const string DE = "de_DE";

            public const string US = "en_US";

            public const string KR = "ko_KR";

            public const string TW = "zh_TW";
        }
    }
}
