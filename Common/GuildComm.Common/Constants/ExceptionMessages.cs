namespace GuildComm.Common.Constants
{
    public static class ExceptionMessages
    {
        public static string GuildNotFound = "No guild with given Id was found";

        public static string ApplicationNotFound = "No application with given Id was found";

        public static string CharacterNotFound = "Character not found";

        public static string RealmNotFound = "Realm not found";

        public static string EventNotFound = "No event with given Id was found";

        public static string MemberAlreadySigned = "Member is already signed up for the event";

        public static string MemberNotInGuild = "Member is not in the given guild";

        public static string MemberNotFound = "No member with given Id was found";

        public static string MaxMemberRank = "Member cannot be promoted to a higher rank.";

        public static string MinMemberRank = "Member cannot be demoted to a lower rank.";

        public static string GuildAlreadyExistOnRealm = "A guild with the given name already exists on {0}";

        public static string CharacterNotViable = "Character must be in the same realm and not be in a guild";

        public static string UserNotFound = "No user with given Id was found";
    }
}