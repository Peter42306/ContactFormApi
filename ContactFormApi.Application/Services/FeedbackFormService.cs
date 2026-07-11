using ContactFormApi.Application.DTOs.Feedback;
using ContactFormApi.Application.Interfaces.Repositories;
using ContactFormApi.Application.Interfaces.Services;
using ContactFormApi.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactFormApi.Domain.Enums;
using ContactFormApi.Application.DTOs.Email;
using System.Net;

namespace ContactFormApi.Application.Services
{
    public sealed class FeedbackFormService : IFeedbackFormService
    {
        private readonly IValidator<FeedbackFormRequestDto> _validator;
        private readonly IFeedbackMessageRepository _repository;
        private readonly IEmailSender _emailSender;
        private readonly IContactApplicationProvider _applicationProvider;

        public FeedbackFormService(
            IValidator<FeedbackFormRequestDto> validator,
            IFeedbackMessageRepository repository,
            IEmailSender emailSender,
            IContactApplicationProvider applicationProvider)
        {
            _validator = validator;
            _repository = repository;
            _emailSender = emailSender;
            _applicationProvider = applicationProvider;
        }


        public async Task<FeedbackFormResponseDto> SubmitAsync(
            FeedbackFormRequestDto request, 
            CancellationToken ct = default)
        {
            var validationResult = await _validator.ValidateAsync(request, ct);
            if (!validationResult.IsValid)
            {
                return new FeedbackFormResponseDto
                {
                    Success = false,
                    Message = "Invalid feedback form data."
                };
            }

            var application = _applicationProvider.GetByAppKey(request.AppKey);
            if (application is null)
            {
                return new FeedbackFormResponseDto
                {
                    Success = false,
                    Message = "Unknown application."
                };
            }

            var feedbackMessage = new FeedbackMessage
            {
                Id = Guid.NewGuid(),
                AppKey = application.AppKey,
                AppName = application.AppName,

                UserId = request.UserId,
                SenderEmail = request.SenderEmail,

                Type = request.Type,
                Status = FeedbackStatus.New,

                Subject = request.Subject,
                Body = request.Body,

                CreatedAtUtc = DateTime.UtcNow
            };

            await _repository.AddAsync(feedbackMessage, ct);

            var recipients = application.AdminEmails
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            var emailMessage = new EmailMessageDto
            {
                ToEmails = recipients,

                Subject = $"[{application.AppName}] New feedback: {request.Type}",

                HtmlBody = $"""
                <h2>New feedback received</h2>

                <p><strong>Application:</strong> {WebUtility.HtmlEncode(application.AppName)}</p>
                <p><strong>Type:</strong> {WebUtility.HtmlEncode(request.Type.ToString())}</p>
                <p><strong>Subject:</strong> {WebUtility.HtmlEncode(request.Subject ?? "Not provided")}</p>
                <p><strong>User ID:</strong> {WebUtility.HtmlEncode(request.UserId ?? "Anonimous")}</p>
                <p><strong>Email:</strong> {WebUtility.HtmlEncode(request.SenderEmail ?? "Not provided")}</p>

                <hr>

                <p>{WebUtility.HtmlEncode(request.Body).Replace("\n", "<br>")}</p>
                """,

                ReplyToEmail = request.SenderEmail
            };

            try
            {
                await _emailSender.SendAsync(emailMessage, ct);

                feedbackMessage.NotificationSentAtUtc = DateTime.UtcNow;
                feedbackMessage.NotificationError = null;
            }
            catch(OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                feedbackMessage.NotificationError = ex.Message;
            }

            await _repository.UpdateAsync(feedbackMessage, CancellationToken.None);

            return new FeedbackFormResponseDto
            {
                FeedbackId = feedbackMessage.Id,
                Success = true,
                Message = "Feedback submitted successfully."
            };
        }
    }
}
