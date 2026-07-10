using ContactFormApi.Application.DTOs.Contact;
using ContactFormApi.Application.Interfaces.Repositories;
using ContactFormApi.Application.Interfaces.Services;
using ContactFormApi.Domain.Entities;
using ContactFormApi.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactFormApi.Application.Services
{
    public class ContactFormService : IContactFormService
    {
        private readonly IValidator<ContactFormRequestDto> _validator;
        private readonly IContactMessageRepository _repository;
        //private readonly IEmailSender _emailSender;
        private readonly IContactApplicationProvider _applcationProvider;

        public ContactFormService(
            IValidator<ContactFormRequestDto> validator,
            IContactMessageRepository repository,
            /*IEmailSender emailSender*/
            IContactApplicationProvider applcationProvider)
        {
            _validator = validator;
            _repository = repository;
            //_emailSender = emailSender;
            _applcationProvider = applcationProvider;
        }

        public async Task<ContactFormResponseDto> SendAsync(
            ContactFormRequestDto request,
            CancellationToken ct = default)
        {
            var validationResult = await _validator.ValidateAsync(request, ct);
            if (!validationResult.IsValid)
            {
                return new ContactFormResponseDto
                {
                    Success = false,
                    Message = "Invalid contact form data"
                };
            }

            var application = _applcationProvider.GetByAppKey(request.AppKey);
            if (application is null)
            {
                return new ContactFormResponseDto
                {
                    Success = false,
                    Message = "Unknown application."
                };
            }

            var contactMessage = new ContactMessage
            {
                Id = Guid.NewGuid(),
                AppKey = application.AppKey,
                AppName = application.AppName,

                SenderName = request.SenderName,
                SenderEmail = request.SenderEmail,

                Subject = request.Subject,
                Body = request.Body,

                Status = ContactMessageStatus.Pending,
                CreatedAtUtc = DateTime.UtcNow
            };

            await _repository.AddAsync(contactMessage, ct);

            return new ContactFormResponseDto
            {
                MessageId = contactMessage.Id,
                Success = true,
                Message = "Contact message received successfully."
            };
        }
    }
}
