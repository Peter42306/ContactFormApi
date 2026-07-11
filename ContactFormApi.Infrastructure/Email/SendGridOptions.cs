using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactFormApi.Infrastructure.Email
{
    public sealed class SendGridOptions
    {
        public const string SectionName = "SendGrid";

        public string ApiKey { get; set; } = string.Empty;
        public string FromEmail { get; set; } = string.Empty;
        public string FromName { get; set; } = "Contact Form API";

    }
}
