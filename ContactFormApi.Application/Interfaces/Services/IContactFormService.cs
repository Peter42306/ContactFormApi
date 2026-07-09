using ContactFormApi.Application.DTOs.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactFormApi.Application.Interfaces.Services
{
    public interface IContactFormService
    {
        Task<ContactFormResponseDto> SendAsync(ContactFormRequestDto request, CancellationToken ct = default);
    }
}
