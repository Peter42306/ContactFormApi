using ContactFormApi.Application.DTOs.Contact;
using ContactFormApi.Application.DTOs.Email;
using ContactFormApi.Application.Interfaces.Repositories;
using ContactFormApi.Application.Interfaces.Services;
using ContactFormApi.Domain.Entities;
using ContactFormApi.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ContactFormApi.Application.Services
{
    public sealed class ContactFormService : IContactFormService
    {
        private readonly IValidator<ContactFormRequestDto> _validator;
        private readonly IContactMessageRepository _repository;
        private readonly IEmailSender _emailSender;
        private readonly IContactApplicationProvider _applicationProvider;

        public ContactFormService(
            IValidator<ContactFormRequestDto> validator,
            IContactMessageRepository repository,
            IEmailSender emailSender,
            IContactApplicationProvider applcationProvider)
        {
            _validator = validator;
            _repository = repository;
            _emailSender = emailSender;
            _applicationProvider = applcationProvider;
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
                    Message = "Invalid contact form data."
                };
            }

            var application = _applicationProvider.GetByAppKey(request.AppKey);
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

            var recipients = application.AdminEmails
                .Concat(application.ClientEmails)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            var emailMessage = new EmailMessageDto
            {
                ToEmails = recipients,

                Subject = $"[{application.AppName}] {request.Subject}",

                HtmlBody = $"""
                <h2>New contact form message</h2>

                <p><strong>Application: </strong> {WebUtility.HtmlEncode(application.AppName)}</p>
                <p><strong>Name: </strong> {WebUtility.HtmlEncode(request.SenderName)}</p>
                <p><strong>Email: </strong> {WebUtility.HtmlEncode(request.SenderEmail)}</p>
                <p><strong>Subject: </strong> {WebUtility.HtmlEncode(request.Subject)}</p>

                <hr>

                <p>{WebUtility.HtmlEncode(request.Body).Replace("\n", "<br>")}</p>
                """,

                ReplyToEmail = request.SenderEmail,
                ReplyToName = request.SenderName
            };

            try
            {
                await _emailSender.SendAsync(emailMessage, ct);

                contactMessage.Status = ContactMessageStatus.Sent;
                contactMessage.SentAtUtc = DateTime.UtcNow;
                contactMessage.ErrorMessage = null;

                await _repository.UpdateAsync(contactMessage, ct);

                return new ContactFormResponseDto
                {
                    MessageId = contactMessage.Id,
                    Success = true,
                    Message = "Contact message sent successfully."
                };
            }
            catch(OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                contactMessage.Status = ContactMessageStatus.Failed;
                contactMessage.ErrorMessage = ex.Message;

                await _repository.UpdateAsync(contactMessage, CancellationToken.None);

                return new ContactFormResponseDto
                {
                    MessageId = contactMessage.Id,
                    Success = false,
                    Message = "Contact message was saved, but email delivery failed."
                };
            }                        
        }
    }
}
