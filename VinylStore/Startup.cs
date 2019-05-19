using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VinylStore.Abstract;
using VinylStore.Common.Auth;
using VinylStore.Common.Contracts;
using VinylStore.DAL.ExternalServices;
using VinylStore.DAL.DataAccess;

using VinylStore.Models;

using VinylStore.Common.MTO;

namespace VinylStore
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContext<VinylStoreDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<VinylStoreDbContext>();

            services.AddAuthentication().AddGoogle(o =>
            {
                o.ClientId = "518688407704-27jgqps27u3knu06mr0cke94u3cuv2t6.apps.googleusercontent.com";
                o.ClientSecret = "DLuuf96HS9gPwz1wmFBaRtke";
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //todo : a remplacer par du générique
            services.AddTransient<IVinylRepository, VinylRepository>();

            //services.AddScoped<IRepository<UserVinyl>, Repository<UserVinyl>>();
            //services.AddScoped<IRepository<VinylDTO>, Repository<VinylDTO>>();

            //services.AddScoped<IUserService, UserService>();
            services.AddScoped<VinylForSaleRepository>();
            services.AddScoped<WantlistRepository>();
            services.AddScoped<ISpotifyService, SpotifyService>();

            services.AddTransient<Func<string, IListRepository>>(serviceProvider => key =>
            {
                switch (key)
                {
                    case "VinylForSale":
                        return serviceProvider.GetService<VinylForSaleRepository>();
                    case "Wantlist":
                        return serviceProvider.GetService<WantlistRepository>();
                    default:
                        throw new KeyNotFoundException(); // or maybe return null
                }
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
    
}
