using ContactFormApi.Application.DTOs.Feedback;
using ContactFormApi.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace ContactFormApi.Api.Controllers
{
    [ApiController]
    [Route("api/feedback")]
    [EnableRateLimiting("PublicForms")]
    public sealed class FeedbackController : ControllerBase
    {
        private readonly IFeedbackFormService _feedbackFormService;

        public FeedbackController(IFeedbackFormService feedbackFormService)
        {
            _feedbackFormService = feedbackFormService;
        }


        [HttpPost]
        public async Task<ActionResult<FeedbackFormResponseDto>> SubmitAsync(
            FeedbackFormRequestDto request,
            CancellationToken ct)
        {
            var result = await _feedbackFormService.SubmitAsync(request, ct);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
