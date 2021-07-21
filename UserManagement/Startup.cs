using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserManagement.AuthManager;
using UserManagement.Persistence.DBConfiguration;
using UserManagement.Extensions;
using UserManagement.Middlewares;
using UserManagement.Services.Services;
using UserManagement.Persistence.Repository;
using Microsoft.Extensions.Logging;
using UserManagement.Services.IRepository;
using UserManagement.Services.EmailService.SendGridConfigs;
using UserManagement.Services.EmailService.Services;

namespace UserManagement
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod();

                });
            });
            services.AddControllers();

            string tokenKey = Configuration.GetValue<string>("TokenKey");
            services.ConfigureAuthentication(tokenKey);

            services.AddSingleton<IJWTAuthenticationManager, JWTAuthenticationManager>();
            services.AddSingleton<IDbClient, DbClient>();
            services.Configure<DbConfig>(Configuration);
            
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IEncryptionServices, EncryptionServices>();
            services.AddTransient<ExceptionHandlingMiddleware>();

            services.AddLogging();
            services.AddSingleton(typeof(ILogger), typeof(Logger<Startup>));

            services.AddSwaggerDocumentation();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerDocumentation();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseStatusCodePages();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
