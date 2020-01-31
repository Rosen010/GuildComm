namespace GuildComm.Data.Models
{
    using Enums;

    using System;

    public class Character
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Role Role { get; set; }

        public Class Class { get; set; }

        public int Level { get; set; }

        public int ItemLevel { get; set; }

        public int RealmId { get; set; }

        public virtual Realm Realm { get; set; }

        public string GuildId { get; set; }

        public virtual Guild Guild { get; set; }

        public string MemberId { get; set; }

        public virtual Member Member { get; set; }

        public string UserId { get; set; }

        public virtual GuildCommUser User { get; set; }
    }
}