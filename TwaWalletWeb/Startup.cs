using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Configuration;

namespace TwaWalletWeb
{
    public class Startup
    {
        // TODO: https://www.tutorialspoint.com/asp.net_core/asp.net_core_setup_mvc.htm
        public IConfiguration Configuration { get; set; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile("AppSettings.json");            
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseIISPlatformHandler(); // kontrola windows authentikace (identity)
            //app.UseWelcomePage();            
            //app.UseRuntimeInfoPage();
            app.UseDeveloperExceptionPage();
            //app.UseDefaultFiles();
            //app.UseStaticFiles();
            app.UseFileServer(); // UseDefaultFiles + UseStaticFiles in appropriate order
            app.UseMvcWithDefaultRoute();

            app.Run(async (context) =>
            {
                //throw new InvalidOperationException("Něco se pokazilo... :(");
                var msg = Configuration["message"];
                await context.Response.WriteAsync(msg);
            });
        }

        // Entry point for the application. 
        //public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
