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

            return services;
        }
    }
}
