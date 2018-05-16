//using EFGetStarted.AspNetCore.ExistingDb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IPTaxi.Models;

namespace IPTaxi
{
    public class Startup
    {
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    string con = "Server=LAPTOP-MP5FT709\\SQLEXPRESS;Database=Service_taxi;Trusted_Connection=True;";
        //    services.AddDbContext<Service_taxiContext>(options => options.UseSqlServer(con));
        //    services.AddMvc();
        //}
        //public void Configure(IApplicationBuilder app)
        //{ app.UseDefaultFiles();
        //    app.UseStaticFiles();
        //    app.UseMvc();
        //}

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            var connection = @"Server=LAPTOP-MP5FT709\SQLEXPRESS;Database=Service_taxi;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<Service_taxiContext>(options => options.UseSqlServer(connection));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
