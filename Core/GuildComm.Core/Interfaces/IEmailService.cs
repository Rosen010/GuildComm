using GuildComm.Core.Models;

namespace GuildComm.Core.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string[] emails, string subject, string content);
    }
}
