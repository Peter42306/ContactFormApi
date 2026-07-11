using ContactFormApi.Application.DTOs.Contact;
using ContactFormApi.Application.DTOs.Feedback;
using ContactFormApi.Application.Interfaces.Services;
using ContactFormApi.Application.Services;
using ContactFormApi.Application.Validators.Contact;
using ContactFormApi.Application.Validators.Feedback;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ContactFormApi.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IContactFormService, ContactFormService>();

            services.AddScoped<IValidator<ContactFormRequestDto>, ContactFormRequestDtoValidator>();

            services.AddScoped<IFeedbackFormService, FeedbackFormService>();

            services.AddScoped<IValidator<FeedbackFormRequestDto>, FeedbackFormRequestDtoValidator>();

            return services;
        }
    }
}
