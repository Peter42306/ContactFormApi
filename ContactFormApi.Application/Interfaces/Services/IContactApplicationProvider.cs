using ContactFormApi.Application.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactFormApi.Application.Interfaces.Services
{
    public interface IContactApplicationProvider
    {
        ContactApplicationSettings? GetByAppKey(string appKey);
    }
}
