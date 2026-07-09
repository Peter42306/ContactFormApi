using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactFormApi.Application.DTOs.Contact
{
    public record ContactFormResponseDto
    {
        public Guid MessageId { get; init; }
        public bool Success { get; init; }
        public string Message { get; init; } = string.Empty;
    }
}
