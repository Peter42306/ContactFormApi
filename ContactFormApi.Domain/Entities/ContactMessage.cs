using ContactFormApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactFormApi.Domain.Entities
{
    public class ContactMessage
    {
        public Guid Id { get; set; }

        public string AppKey { get; set; } = string.Empty;
        public string AppName { get; set; } = string.Empty;

        public string SenderName { get; set; } = string.Empty;
        public string SenderEmail { get; set;} = string.Empty;

        public string Subject {  get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;

        public ContactMessageStatus Status { get; set; }

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? SentAtUtc { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
