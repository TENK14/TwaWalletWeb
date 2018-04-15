using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwaWallet.Web.Data;
using TwaWallet.Web.Models;
using TwaWallet.Web.Services;
using TwaWallet.Entity;
using TwaWallet.Model;
using TwaWallet.Web.DataLayer;

namespace TwaWallet.Web
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IDataLayer, DataLayer.DataLayer>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            InitDatabase(app);
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void InitDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                //logger.LogWarning("Turn on databases....");

                var database = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                database.Database.Migrate();
                database.EnsureSeedData();
                
                //var userManager = app.ApplicationServices.GetService<UserManager<ApplicationUser>>();
                //var roleManager = app.ApplicationServices.GetService<RoleManager<IdentityRole>>();

                //var adminUser = Configuration["ADMIN_USER"];
                //var adminPassword = Configuration["ADMIN_PASSWORD"];

                //serviceScope.ServiceProvider.GetService<ApplicationDbContext>()
                //    .EnsureSeedData(userManager, roleManager, adminUser, adminPassword);


            }
        }
    }
}
