using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactFormApi.Application.DTOs.Email
{
    public record EmailMessageDto
    {
        public List<string> ToEmails { get; init; } = [];

        public string Subject {  get; init; } = string.Empty;
        public string HtmlBody { get; init; } = string.Empty;

        public string? ReplyToEmail {  get; init; }
        public string? ReplyToName { get; init; }
    }
}
