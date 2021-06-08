using Core_SatelliteBlogSitesi.Models.DAL;
using Core_SatelliteBlogSitesi.Models.DATA;
using Core_SatelliteBlogSitesi.Models.DATA.Interfaces_and_Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_SatelliteBlogSitesi
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
            services.AddDbContext<ProjectContext>(options => options.UseSqlServer(@"server=DESKTOP-F6J8MEV\SQLEXPRESS; database=SatelliteBlogDB; Trusted_Connection= True;MultipleActiveResultSets=True"));
            services.AddControllersWithViews();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IArticleRepository, ArticleRepository>();
            services.AddTransient<IUserCategoryRepository, UserCategoryRepository>();

            
            services.AddMvc();
            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromHours(1);
            });

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseRouting();

            //DB'ye verileri göndermek için.
            SeedData.Seed(app);

            app.UseAuthorization();

            app.UseSession();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Ziyaretci}/{action=AnaSayfa}/{id?}");
            });
        }
    }
}
