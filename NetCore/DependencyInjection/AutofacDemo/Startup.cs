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
using Autofac;
using Autofac.Extras.DynamicProxy;
using Autofac.Extensions.DependencyInjection;
using AutofacDemo.Services;

namespace AutofacDemo
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AutofacDemo", Version = "v1" });
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // builder.RegisterType<MyService>().As<IMyService>();
            #region named register
            // builder.RegisterType<MyServiceV2>().Named<IMyService>("service2");
            #endregion

            #region property register
            // builder.RegisterType<MyNameService>();
            // builder.RegisterType<MyServiceV2>().As<IMyService>().PropertiesAutowired();
            #endregion

            #region AOP
            // builder.RegisterType<MyInterceptor>();
            // builder.RegisterType<MyNameService>();
            // builder.RegisterType<MyServiceV2>().As<IMyService>()
            //     .PropertiesAutowired()
            //     .InterceptedBy(typeof(MyInterceptor))
            //     .EnableInterfaceInterceptors();
            #endregion

            #region child container
            builder.RegisterType<MyNameService>().InstancePerMatchingLifetimeScope("myscope");
            #endregion
        }

        public ILifetimeScope AutofacContainer { get; private set; }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            /// named registration
            // var service = this.AutofacContainer.ResolveNamed<IMyService>("service2");
            // service.ShowCode();

            /// "property register" & "AOP"
            // var serviceNamed = this.AutofacContainer.Resolve<IMyService>();
            // serviceNamed.ShowCode();

            #region child container
            using (var myScope = AutofacContainer.BeginLifetimeScope("myscope"))
            {
                var service0 = myScope.Resolve<MyNameService>();
                using (var scope = myScope.BeginLifetimeScope())
                {
                    var service1 = scope.Resolve<MyNameService>();
                    var service2 = scope.Resolve<MyNameService>();
                    Console.WriteLine($"service1=service2: {service1 == service2}");
                    Console.WriteLine($"service1=service0: {service1 == service0}");
                }
            }
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AutofacDemo v1"));
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
