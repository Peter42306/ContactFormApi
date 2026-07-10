using ContactFormApi.Application.Interfaces.Services;
using ContactFormApi.Application.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactFormApi.Infrastructure.Configuration
{
    public sealed class ContactApplicationProvider : IContactApplcationProvider
    {
        private readonly ContactApplicationsOptions _options;

        public ContactApplicationProvider(IOptions<ContactApplicationsOptions> options)
        {
            _options = options.Value;
        }


        public ContactApplicationSettings? GetByAppKey(string appKey)
        {
            return _options.Applications.FirstOrDefault(x => string.Equals(
                x.AppKey,
                appKey,
                StringComparison.OrdinalIgnoreCase));
        }
    }
}
