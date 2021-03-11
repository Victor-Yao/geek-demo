using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ExceptionDemo.Exceptions;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace ExceptionDemo
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ExceptionDemo", Version = "v1" });
            });
            services.AddMvc(mvcOptions =>
            {
                ///4.MVC全局范围的异常过滤器
                // mvcOptions.Filters.Add<MyExceptionFilter>();
                // mvcOptions.Filters.Add<MyExceptionFilterAttribute>();
            }).AddJsonOptions(jsonoptions =>
            {
                jsonoptions.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            //     app.UseSwagger();
            //     app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ExceptionDemo v1"));
            // }

            ///  route to /error path, I defined the path in ErrorController
            /// 1. 使用异常中间件
            // app.UseExceptionHandler("/error");

            /// 2. 异常中间件 委托实现
            app.UseExceptionHandler(errApp =>
            {
                errApp.Run(async context =>
                {
                    var ex = context.Features.Get<IExceptionHandlerPathFeature>();
                    var knownException = ex.Error as IKnownException;
                    if (knownException == null)
                    {
                        var logger = context.RequestServices.GetService<ILogger<MyExceptionFilter>>();
                        logger.LogError(ex.Error, ex.Error.Message);
                        knownException = KnownException.Unknown;
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    }
                    else
                    {
                        knownException = KnownException.FromKnownException(knownException);
                        context.Response.StatusCode = StatusCodes.Status200OK;
                    }
                    var jsonOptions = context.RequestServices.GetService<IOptions<JsonOptions>>();
                    context.Response.ContentType = "application/json; charset=utf-8";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(knownException, jsonOptions.Value.JsonSerializerOptions));
                });
            });

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
