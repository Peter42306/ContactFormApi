using ContactFormApi.Application.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactFormApi.Infrastructure.Configuration
{
    public sealed class ContactApplicationsOptions
    {
        public const string SectionName = "ContactApplications";

        public List<ContactApplicationSettings> Applications { get; set; } = [];
    }
}
