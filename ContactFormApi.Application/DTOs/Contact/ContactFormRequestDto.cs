using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactFormApi.Application.DTOs.Contact
{
    public sealed record ContactFormRequestDto
    {
        public string AppKey { get; init; } = string.Empty;

        public string SenderName { get; init; } = string.Empty;
        public string SenderEmail { get; init; } = string.Empty;

        public string Subject { get; init; } = string.Empty;
        public string Body { get; init; } = string.Empty;
    }
}
