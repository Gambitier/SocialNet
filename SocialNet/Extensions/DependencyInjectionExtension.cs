using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SocialNet.AuthManager;
using SocialNet.Middlewares;
using SocialNet.Persistence.DBConfiguration;
using SocialNet.Persistence.Repository;
using SocialNet.Services.EmailService.Events;
using SocialNet.Services.EmailService.Services;
using SocialNet.Services.IRepository;
using SocialNet.Services.Services;

namespace SocialNet.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
        {
            // Singletons
            services.AddSingleton(typeof(ILogger), typeof(Logger<Startup>));
            services.AddSingleton<IJWTAuthenticationManager, JWTAuthenticationManager>();
            services.AddSingleton<IDbClient, DbClient>();
            services.AddTransient<IEmailSender, EmailSender>();

            // Transients
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IEncryptionServices, EncryptionServices>();
            services.AddTransient<ExceptionHandlingMiddleware>();

            // Events
            services.AddTransient<UserRegisteredEmailEvent>();

            return services;
        }
    }
}
