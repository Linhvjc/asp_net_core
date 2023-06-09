﻿

using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Assignment.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4
{
	public class Startup
	{
       
            public IConfiguration Configuration { get; }
            public Startup(IConfiguration configuration)
            {
                this.Configuration = configuration;
            }

            // This method gets called by the runtime. Use this method to add serices to the container.
            public void ConfigureServices(IServiceCollection services)
            {
            services.Configure<RouteOptions>(routeOptions =>
            {
                routeOptions.LowercaseUrls = true;
            });
                services.AddControllersWithViews().AddRazorRuntimeCompilation();
                services.AddNotyf(config =>
                {
                    config.DurationInSeconds = 5;
                    config.IsDismissable = true;
                    config.Position = NotyfPosition.TopRight;
                });
            services.AddDbContext<BookShopDbContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("BookShopConnection")));
        }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(WebApplication app, IWebHostEnvironment env)
            {
                if (!env.IsDevelopment())
                {
                    app.UseExceptionHandler("/Home/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }
                app.UseNotyf();
                app.UseHttpsRedirection();
                app.UseStaticFiles();

                app.UseRouting();
                app.UseCookiePolicy();

                app.UseAuthorization();
                app.MapControllerRoute(
                    name: "MyArea",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute("areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                });
               app.Run();
        }
    }
    
}
