using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TwaWallet.Web.Startup))]

namespace TwaWallet.Web
{
    public partial class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddMemoryCache();
            services.AddMvc();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                //{
                //    HotModuleReplacement = true,
                //    ReactHotModuleReplacement = true
                //});
            }
            else
            {
                app.UseStatusCodePages();
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Default}/{action=Index}/{id?}");

                //routes.MapSpaFallbackRoute(
                //    name: "spa-fallback",
                //    defaults: new { controller = "Default", action = "Index" });
            });
        }


        //public void Configuration(IAppBuilder app)
        //{
        //    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

        //    app.Use((context, next) =>
        //    {
        //        System.IO.TextWriter output = context.Get<System.IO.TextWriter>("host.TraceOutput");
        //        return next().ContinueWith(result =>
        //        {
        //            output.WriteLine("Scheme {0} : Method {1} : Path {2} : MS {3}",
        //            context.Request.Scheme, context.Request.Method, context.Request.Path, getTime());
        //        });
        //    });

        //    app.Run(async context =>
        //    {
        //        await context.Response.WriteAsync(getTime() + " My First OWIN App");
        //    });
        //}

        //string getTime()
        //{
        //    return DateTime.Now.Millisecond.ToString();
        //}
    }
}
