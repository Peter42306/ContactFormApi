using Microsoft.AspNetCore.RateLimiting;

namespace ContactFormApi.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApi(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            var allowedOrigins = 
                configuration
                    .GetSection("Cors:AllowedOrigins")
                    .Get<string[]>() 
                ?? [];

            services.AddCors(options =>
            {
                options.AddPolicy("AllowedOrigins", policy =>
                {
                    policy
                        .WithOrigins(allowedOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            //services.AddHealthChecks();

            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                options.AddFixedWindowLimiter("PublicForms", limiterOptions =>
                {
                    limiterOptions.PermitLimit = 5;
                    limiterOptions.Window = TimeSpan.FromMinutes(1);
                    limiterOptions.QueueLimit = 0;
                    limiterOptions.AutoReplenishment = true;
                });
            });

            return services;
        }
    }
}
