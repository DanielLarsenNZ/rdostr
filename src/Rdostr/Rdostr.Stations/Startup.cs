using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Rdostr.Stations
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private const string CorsLocalOrigins = "CorsLocalOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                // https://login.microsoftonline.com/tfp/{Configuration["AzureAdB2C:Tenant"]}/{Configuration["AzureAdB2C:Policy"]}/v2.0/
                options.Authority = "https://login.microsoftonline.com/tfp/rdostr.onmicrosoft.com/b2c_1_signupsigninv2/v2.0/";
                options.Audience = "49df1c13-8de7-46fc-a1d9-7bf0c8b73377";
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = AuthenticationFailed
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy(CorsLocalOrigins,
                builder =>
                {
                    builder
                    .WithOrigins("http://localhost:8080")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        private static Task AuthenticationFailed(AuthenticationFailedContext arg)
        {
            Trace.TraceError(arg.Exception.Message);
            return Task.CompletedTask;
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

            app.UseCors(CorsLocalOrigins);
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
