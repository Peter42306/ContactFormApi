using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactFormApi.Application.Settings
{
    public sealed record ContactApplicationSettings
    {
        public string AppKey { get; init; } = string.Empty;
        public string AppName { get; init; } = string.Empty;

        public List<string> AdminEmails { get; init; } = [];
        public List<string> ClientEmails { get; init; } = [];
    }
}
