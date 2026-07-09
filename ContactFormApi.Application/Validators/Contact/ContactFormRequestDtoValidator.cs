using ContactFormApi.Application.DTOs.Contact;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactFormApi.Application.Validators.Contact
{
    public class ContactFormRequestDtoValidator : AbstractValidator<ContactFormRequestDto>
    {
        public ContactFormRequestDtoValidator()
        {
            RuleFor(x => x.AppKey)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.SenderName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.SenderEmail)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(200);

            RuleFor(x => x.Subject)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Body)
                .NotEmpty()
                .MaximumLength(5000);
        }
    }
}
