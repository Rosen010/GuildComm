using GuildComm.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace GuildComm.Data.Models
{
    public class Realm
    {
        [Key]
        public int Id { get; set; }

        public int RealmId { get; set; }

        public string Name { get; set; }

        public Region Region { get; set; }
    }
}
