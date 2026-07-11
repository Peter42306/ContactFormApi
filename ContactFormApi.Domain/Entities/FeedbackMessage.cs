using ContactFormApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactFormApi.Domain.Entities
{
    public class FeedbackMessage
    {
        public Guid Id { get; set; }

        public string AppKey { get; set; } = string.Empty;
        public string AppName { get; set; } = string.Empty;

        public string? UserId { get; set; }
        public string? SenderEmail { get; set; }

        public FeedbackType Type { get; set; } = FeedbackType.General;
        public FeedbackStatus Status { get; set; } = FeedbackStatus.New;

        public string? Subject { get; set; }
        public string Body { get; set; } = string.Empty;

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? ReviewedAtUtc { get; set; }

        public string? AdminNote { get; set; }

        // Notification on sending Feedback Message to admin via email
        public DateTime? NotificationSentAtUtc { get; set; }
        public string? NotificationError { get; set; }
    }
}
