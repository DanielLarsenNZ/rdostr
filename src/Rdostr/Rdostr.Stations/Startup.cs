using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rdostr.Common;

namespace Rdostr.Stations
{
    public class Startup
    {
        const string DefaultCorsPolicy = "DefaultCorsPolicy";

        private static ILogger Logger => new LoggerFactory().CreateLogger("Startup");

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRdostrAuthentication(Configuration, Logger);
            services.AddRdostrCors(Configuration, DefaultCorsPolicy);

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Group_1", policy =>
                {
                    // Require group claim 
                    policy.RequireClaim("groups", "e67a52d3-ce5e-45b1-80e7-0bdbe49b0f0a");
                });

                options.AddPolicy("Group_2", policy =>
                {
                    // Require group claim 
                    policy.RequireClaim("groups", "20f2e65a-4a35-4e28-8309-08790897ed81");
                });
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

            app.UseCors(DefaultCorsPolicy);
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
