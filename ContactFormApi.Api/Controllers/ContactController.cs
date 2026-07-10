using ContactFormApi.Application.DTOs.Contact;
using ContactFormApi.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactFormApi.Api.Controllers
{
    [ApiController]
    [Route("api/contact")]    
    public class ContactController : ControllerBase
    {
        private readonly IContactFormService _contactFormService;

        public ContactController(IContactFormService contactFormService)
        {
            _contactFormService = contactFormService;
        }


        [HttpPost]
        public async Task<ActionResult<ContactFormResponseDto>> SendAsync(
            ContactFormRequestDto request,
            CancellationToken ct)
        {
            var result = await _contactFormService.SendAsync(request, ct);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
