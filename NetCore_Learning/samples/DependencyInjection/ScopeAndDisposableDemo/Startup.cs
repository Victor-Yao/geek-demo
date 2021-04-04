using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ScopeAndDisposableDemo.Services;

namespace ScopeAndDisposableDemo
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
            // services.AddSingleton<IOrderService, DisposableOrderService>();
            // services.AddSingleton<IOrderService>(p =>
            // {
            //     return new DisposableOrderService();
            // });
            // services.AddSingleton<IOrderService>(new DisposableOrderService());

            services.AddTransient<IOrderService, DisposableOrderService>();

            // services.AddScoped<IOrderService, DisposableOrderService>();
            // services.AddScoped<IOrderService>(p =>
            // {
            //     return new DisposableOrderService();
            // });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ScopeAndDisposableDemo", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /// the trasient service won't be released if it's registed in the root container
            var s = app.ApplicationServices.GetService<IOrderService>();
            var s2 = app.ApplicationServices.GetService<IOrderService>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ScopeAndDisposableDemo v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
