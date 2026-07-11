using ContactFormApi.Application.DTOs.Feedback;
using ContactFormApi.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactFormApi.Application.Validators.Feedback
{
    public sealed class FeedbackFormRequestDtoValidator:AbstractValidator<FeedbackFormRequestDto>
    {
        public FeedbackFormRequestDtoValidator()
        {
            RuleFor(x => x.AppKey)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.UserId)
                .MaximumLength(200)
                .When(x => !string.IsNullOrWhiteSpace(x.UserId));

            RuleFor(x => x.SenderEmail)
                .EmailAddress()
                .MaximumLength(200)
                .When(x => !string.IsNullOrWhiteSpace(x.SenderEmail));

            RuleFor(x => x.Type)
                .IsInEnum()
                .NotEqual(FeedbackType.Unknown);

            RuleFor(x => x.Subject)
                .MaximumLength(200);

            RuleFor(x => x.Body)
                .NotEmpty()
                .MaximumLength(5000);
        }
    }
}
