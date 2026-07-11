using ContactFormApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactFormApi.Application.DTOs.Feedback
{
    public sealed record FeedbackFormRequestDto
    {
        public string AppKey { get; init; } = string.Empty;

        public string? UserId {  get; init; }
        public string? SenderEmail { get; init; }

        public FeedbackType Type { get; init; } = FeedbackType.General;

        public string? Subject { get; init; }
        public string Body { get; init; } = string.Empty;
    }
}
