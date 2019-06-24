using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Rdostr.Configuration
{
    public class Startup
    {
        private static ILogger Logger => new LoggerFactory().CreateLogger("Startup");

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            const string AuthenticationAuthority = "AuthenticationAuthority";
            const string AuthenticationAudience = "AuthenticationAudience";

            if (string.IsNullOrEmpty(Configuration[AuthenticationAuthority]))
                throw new InvalidOperationException($"App Settings \"{AuthenticationAuthority}\" and/or \"{AuthenticationAudience}\" are missing.");

            // Add the authentication handler
            // https://docs.microsoft.com/en-us/aspnet/core/migration/1x-to-2x/identity-2x?view=aspnetcore-2.2#jwt-bearer-authentication
            // https://docs.microsoft.com/en-us/aspnet/core/security/authorization/limitingidentitybyscheme?view=aspnetcore-2.2&tabs=aspnetcore2x#use-multiple-authentication-schemes
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = Configuration[AuthenticationAuthority];
                    options.Audience = Configuration[AuthenticationAudience];
                    options.Events = new JwtBearerEvents
                    {
                        // See AuthenticationFailed delegate below
                        OnAuthenticationFailed = AuthenticationFailed
                    };
                });

            // Claims checks can be defined as policies and applied with Attributes.
            // https://docs.microsoft.com/en-us/aspnet/core/security/authorization/claims?view=aspnetcore-2.2
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Aucklanders", policy => policy.RequireClaim("city", "Auckland"));
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }

        private static Task AuthenticationFailed(AuthenticationFailedContext arg)
        {
            Logger.LogError(arg.Exception, arg.Exception.Message);
            return Task.CompletedTask;
        }
    }
}
