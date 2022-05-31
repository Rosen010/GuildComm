using System.ComponentModel.DataAnnotations;

namespace GuildComm.Web.Models.Account
{
    public class UserForgotPasswordInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
