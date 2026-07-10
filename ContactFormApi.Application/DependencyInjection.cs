using ContactFormApi.Application.DTOs.Contact;
using ContactFormApi.Application.Interfaces.Services;
using ContactFormApi.Application.Services;
using ContactFormApi.Application.Validators.Contact;
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

            return services;
        }
    }
}
