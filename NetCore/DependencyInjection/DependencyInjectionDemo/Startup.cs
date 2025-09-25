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
using DependencyInjectionDemo.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DependencyInjectionDemo
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DependencyInjectionDemo", Version = "v1" });
            });

            #region register service
            services.AddSingleton<IMySingletonService, MySingletonService>();
            services.AddScoped<IMyScopedService, MyScopedService>();
            services.AddTransient<IMyTransientService, MyTransientService>();
            #endregion

            #region register service with different ways
            services.AddSingleton<IOrderService>(new OrderService()); // register with instance
            services.AddSingleton<IOrderService, OrderServiceEx>();
            services.AddSingleton<IOrderService>(serviceProvider =>
            {
                return new OrderService();
            });
            #endregion

            #region try register
            // services.TryAddSingleton<IOrderService, OrderService>();
            // services.TryAddSingleton<IOrderService, OrderServiceEx>();
            // services.TryAddEnumerable(ServiceDescriptor.Singleton<IOrderService, OrderService>());
            // services.TryAddEnumerable(ServiceDescriptor.Singleton<IOrderService, OrderServiceEx>());
            // services.TryAddEnumerable(ServiceDescriptor.Singleton<IOrderService>(new OrderServiceEx()));
            #endregion

            #region remove and replace registered service
            // services.Replace(ServiceDescriptor.Singleton<IOrderService,OrderServiceEx>());
            // services.RemoveAll<IOrderService>();
            #endregion

            #region register generic service template
            services.AddSingleton(typeof(IGenericService<>), typeof(GenericService<>));
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DependencyInjectionDemo v1"));
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
