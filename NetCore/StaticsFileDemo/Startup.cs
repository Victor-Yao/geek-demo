using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;

namespace StaticsFileDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        const int BufferSize = 6 * 1024;
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StaticsFileDemo", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StaticsFileDemo v1"));
            }

            app.UseDirectoryBrowser();
            app.UseStaticFiles();

            // app.UseDefaultFiles();

            /// remarks:
            ///      server my own default page
            // var options = new DefaultFilesOptions();
            // options.DefaultFileNames.Clear();
            // options.DefaultFileNames.Add("MyDefault.html");
            // app.UseDefaultFiles(options);


            /// remarks:
            ///      add other physical folder
            // app.UseStaticFiles(new StaticFileOptions{
            //     RequestPath = "/files",
            //     FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"file"))
            // });


            /// remarks:
            ///     Enable all static file middleware (except directory browsing) 
            ///     By default, it's for wwwroot
            // app.UseFileServer();


            /// remarks:
            ///     Rewrite the request matches spcific rule 
            // app.MapWhen(context =>
            // {
            //     return !context.Request.Path.Value.StartsWith("/api");
            // }, appBuilder =>
            // {
            //     /// 1.Implement rewrite via rewrite middleware (prefer) 
            //     // var option = new RewriteOptions();
            //     // option.AddRewrite(".*", "/index.html", true);
            //     // appBuilder.UseRewriter(option);
            //     // appBuilder.UseStaticFiles();

            //     /// 2.Implement rewrite via 断路器
            //     /// Disadvantage---cannot keep http resonse header
            //     appBuilder.Run(async c =>
            //     {
            //         var file = env.WebRootFileProvider.GetFileInfo("index.html");

            //         c.Response.ContentType = "text/html";
            //         using (var fileStream = new FileStream(file.PhysicalPath, FileMode.Open, FileAccess.Read))
            //         {
            //             await StreamCopyOperation.CopyToAsync(fileStream, c.Response.Body, null, BufferSize, c.RequestAborted);
            //         }
            //     });
            // });

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
