using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactFormApi.Application.DTOs.Feedback
{
    public sealed record FeedbackFormResponseDto
    {
        public Guid FeedbackId { get; init; }
        public bool Success { get; init; }
        public string Message { get; init; } = string.Empty;
    }
}
