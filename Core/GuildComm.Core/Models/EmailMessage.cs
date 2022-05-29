using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace GuildComm.Core.Models
{
    public class EmailMessage
    {
        public EmailMessage(IEnumerable<string> receivers, string subject, string content)
        {
            this.Receivers = new List<MailboxAddress>();

            this.Subject = subject;
            this.Content = content;

            this.Receivers.AddRange(receivers.Select(r => new MailboxAddress("email", r)));
        }

        public string Subject { get; set; }

        public string Content { get; set; }

        public List<MailboxAddress> Receivers { get; set; }
    }
}
