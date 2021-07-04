using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserManagement.AuthManager;
using UserManagement.DBConfiguration;
using UserManagement.Extensions;
using UserManagement.Middlewares;
using UserManagement.Services;

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
            services.AddControllers();

            string tokenKey = Configuration.GetValue<string>("TokenKey");
            services.ConfigureAuthentication(tokenKey);

            services.AddSingleton<IJWTAuthenticationManager, JWTAuthenticationManager>();
            services.AddSingleton<IDbClient, DbClient>();
            services.Configure<DbConfig>(Configuration);
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<ExceptionHandlingMiddleware>();

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
            //app.UseStatusCodePages();

            app.UseRouting();
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
