using GuildComm.Core.Models;

namespace GuildComm.Core.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(EmailMessage message);
    }
}
