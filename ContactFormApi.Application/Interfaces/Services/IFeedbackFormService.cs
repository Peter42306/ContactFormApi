using ContactFormApi.Application.DTOs.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactFormApi.Application.Interfaces.Services
{
    public interface IFeedbackFormService
    {
        Task<FeedbackFormResponseDto> SubmitAsync(
            FeedbackFormRequestDto request,
            CancellationToken ct = default);
    }
}
