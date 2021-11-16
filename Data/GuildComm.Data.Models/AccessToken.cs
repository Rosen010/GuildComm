using System;
using System.ComponentModel.DataAnnotations;

namespace GuildComm.Data.Models
{
    public class AccessToken
    {
        [Key]
        public string Name { get; set; }

        public string Value { get; set; }

        public DateTime Expiration { get; set; }
    }
}
