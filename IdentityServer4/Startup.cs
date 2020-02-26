using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Areas.Identity.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer4
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<IdentityServerDBContext>(options =>
            //       options.UseSqlServer(
            //           Configuration.GetConnectionString("IdentityServerDBContextConnection")));

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            //    .AddEntityFrameworkStores<IdentityServerDBContext>();

            //services.AddDbContext<IdentityServerDBContext>(options =>
            //  options.UseSqlServer(Configuration.GetConnectionString("IdentityServerDBContextConnection")));


            //services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
            //{
            //    opt.SignIn.RequireConfirmedAccount = false;
            //})
            //.AddEntityFrameworkStores<IdentityServerDBContext>()
            //.AddDefaultTokenProviders(); 



            var builder = services.AddIdentityServer()
                         .AddInMemoryApiResources(Config.Apis)
                         .AddInMemoryClients(Config.Clients)
                         .AddInMemoryIdentityResources(Config.GetIdentityResources())
                         .AddAspNetIdentity<ApplicationUser>()
                         .AddDeveloperSigningCredential();
           
            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:5003")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Identity/Account/Logindsfds";
            });
            //services.AddAuthentication()
            //    .AddCookie("Cookie", config => { config.LoginPath = "/dfdfd"; });
            // services.AddMvc(); 
            services.AddRazorPages();
        }

       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseCors("default");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
