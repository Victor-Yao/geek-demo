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
using Microsoft.AspNetCore.Http;

namespace MiddlewareDemo
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MiddlewareDemo", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MiddlewareDemo v1"));
            }
            #region app use
            // app.Use(async (context, next) => {
            //     await context.Response.WriteAsync("Hello");
            //     await next();
            // });

            // Map, /abc/dfdg
            // app.Map("/abc", abcBuilder =>
            // {
            //     abcBuilder.Use(async (context, next) =>
            //     {
            //         await next();
            //         await context.Response.WriteAsync("Hello Map(abc)");
            //     });
            // });

            // Map, /123?abc&qwe
            // app.MapWhen(context =>
            // {
            //     return context.Request.Query.Keys.Contains("abc");
            // }, builder =>
            // {
            //     builder.Run(async context =>
            //     {
            //         await context.Response.WriteAsync("hello MapWhen(abc)");
            //     });
            // });

            app.UseMyMiddleware();
            #endregion


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
