using Abc.Business.Abstract;
using Abc.Business.Concrete;
using Abc.DataAccess.Abstract;
using Abc.DataAccess.Concrete.EntityFramework;
using Abc.WebUI.Entities;
using Abc.WebUI.Middlewares;
using Abc.WebUI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System.IO;

namespace Abc.WebUI
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
            #region DI
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IProductDal, EfProductDal>();

            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDal, EfCategoryDal>();

            services.AddSingleton<ICartService, CartManager>();
            services.AddSingleton<ICartSessionService, CartSessionService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #endregion
            #region Session
            services.AddSession();
            services.AddDistributedMemoryCache();//cahce de tutulur.
            #endregion
            #region Identity
            services.AddDbContext<CustomIdentityDbContext>(option => option.UseSqlServer(@"Data Source=DESKTOP-2TS36OR\SQLSERVER2017EXP;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));
            services.AddIdentity<CustomIdentityUser, CustomIdentityRole>().AddEntityFrameworkStores<CustomIdentityDbContext>().AddDefaultTokenProviders();
            #endregion
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();                       //   /css/style.css
            app.UseNodeModules(env.ContentRootPath);    //  eklenen bootstrap middleware için.
            app.UseSession();                           //Session
            app.UseIdentity();                          //Identity
            app.UseStatusCodePages();                   //404 hatası sayfaları gostermek ıcın
            app.UseDeveloperExceptionPage();
            app.UseDatabaseErrorPage();
            //app.UseMvcWithDefaultRoute();                // aşağıdakine eşittir.kendi url yapısınıa göre olusturur. 
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Product}/{action=Index}/{id?}"

                );
            });
        }
    }
}
