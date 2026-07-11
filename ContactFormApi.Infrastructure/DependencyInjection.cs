using ContactFormApi.Application.Interfaces.Repositories;
using ContactFormApi.Infrastructure.Configuration;
using ContactFormApi.Infrastructure.Data;
using ContactFormApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ContactFormApi.Application.Interfaces.Services;
using ContactFormApi.Infrastructure.Email;

namespace ContactFormApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString =
                configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped<IContactMessageRepository, ContactMessageRepository>();

            services.Configure<ContactApplicationsOptions>(
                configuration.GetSection(ContactApplicationsOptions.SectionName));

            services.AddSingleton<IContactApplicationProvider, ContactApplicationProvider>();

            services.Configure<SendGridOptions>(
                configuration.GetSection(SendGridOptions.SectionName));

            services.AddScoped<IEmailSender, SendGridEmailSender>();

            return services;
        }
    }
}
