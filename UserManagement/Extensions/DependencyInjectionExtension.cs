using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UserManagement.AuthManager;
using UserManagement.Middlewares;
using UserManagement.Persistence.DBConfiguration;
using UserManagement.Persistence.Repository;
using UserManagement.Services.EmailService.Services;
using UserManagement.Services.IRepository;
using UserManagement.Services.Services;

namespace UserManagement.Extensions
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

            return services;
        }
    }
}
