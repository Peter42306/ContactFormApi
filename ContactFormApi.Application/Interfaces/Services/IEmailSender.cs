using ContactFormApi.Application.DTOs.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactFormApi.Application.Interfaces.Services
{
    public interface IEmailSender
    {
        Task SendAsync(EmailMessageDto message, CancellationToken ct = default);
    }
}
