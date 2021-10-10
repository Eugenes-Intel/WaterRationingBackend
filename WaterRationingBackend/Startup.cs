using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterRationingBackend.DataAccess;
using WaterRationingBackend.Entities;
using WaterRationingBackend.Services;
using WaterRationingBackend.Services.Interfaces;

namespace WaterRationingBackend
{
    public class Startup
    {
        private const string OpenPolicy = "_openPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors((cors) =>
            {
                cors.AddPolicy(OpenPolicy, (policy) =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            services.AddDbContext<ApplicationDbContext>((context) =>
            {
                context.UseSqlServer(Configuration.GetConnectionString("WaterRationingSystemDB"));
            }); //, ServiceLifetime.Scoped, ServiceLifetime.Scoped
            services.AddScoped<ISupervisor, ServiceSupervisor>();
            services.AddScoped<IEntityDecider, EntityDecider>();
            services.AddScoped<IEntities, Cities>();
            services.AddScoped<IEntities, Suburbs>();
            services.AddScoped<IEntities, Histories>();
            services.AddScoped<ICollator, Collator>();
            services.AddScoped<IScopeHelper, ScopeHelper>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(OpenPolicy);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
