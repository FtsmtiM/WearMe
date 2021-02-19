using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WearMe.Data;
using WearMe.Data.Interfaces;
using WearMe.Data.Models;
using WearMe.Data.Repositories;

namespace WearMe
{
    public class Startup
    {
        private IConfigurationRoot _configurationRoot;
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _configurationRoot = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(_configurationRoot.GetConnectionString("DefaultConnection"),
                              sqlServerOptionsAction: sqlOptions =>
                              {
                                  sqlOptions.EnableRetryOnFailure(
                                  maxRetryCount: 10,
                                  maxRetryDelay: TimeSpan.FromSeconds(30),
                                  errorNumbersToAdd: null);
                              }
                            ));

            
            //services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));


            services.AddIdentity<IdentityUser, IdentityRole>(options=>
                {
                    options.Password.RequiredLength = 5;
                    options.Password.RequireNonAlphanumeric = false;
                }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});


            services.AddTransient<IClothRepository, ClothRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ISexRepository, SexRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            //to get access to session
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => ShoppingCart.GetCart(sp));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMemoryCache();
            services.AddSession();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();


            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{

            //    // app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.

            //    app.UseExceptionHandler(errorApp =>
            //    {
            //        errorApp.Run(async context =>
            //        {
            //            context.Response.StatusCode = 500;
            //            context.Response.ContentType = "text/html";

            //            await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
            //            await context.Response.WriteAsync("ERROR!<br><br>\r\n");

            //            var exceptionHandlerPathFeature =
            //                context.Features.Get<IExceptionHandlerPathFeature>();

            //            // Use exceptionHandlerPathFeature to process the exception (for example, 
            //            // logging), but do NOT expose sensitive error information directly to 
            //            // the client.

            //            if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
            //            {
            //                await context.Response.WriteAsync("File error thrown!<br><br>\r\n");
            //            }

            //            await context.Response.WriteAsync("<a href=\"/\">Home</a><br>\r\n");
            //            await context.Response.WriteAsync("</body></html>\r\n");
            //            await context.Response.WriteAsync(new string(' ', 512)); // IE padding
            //        });
            //    });

            //    app.UseHsts();
            //}

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "clothlist",
                    template: "{controller=Cloth}/{action=List}/{sexid?}/{categoryid?}");

            });
            
        }
    }
}
