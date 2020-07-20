using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Code.Library.AspNetCore;
using Code.Library.AspNetCore.Extensions;
using Code.Library.AspNetCore.Middleware;
using Code.Library.AspNetCore.Middleware.RequestResponseLogging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AspNetCoreApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRequestResponseLogging(opt =>
            {
                opt.Exclude.RequestBody.Add("/secret");
                opt.Exclude.ResponseBody.Add("/secret");
                opt.Include.RequestHeaders = true;
            });
            app.UseApiExceptionHandler();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapDefaultHealthChecks();
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApiExceptionHandler();
            services.AddHealthChecks();
        }
    }
}