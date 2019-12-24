using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using General.Entities;
using Microsoft.EntityFrameworkCore;
using General.Services.Category;
using General.Core;
using General.Core.Data;
using General.Core.Librs;
using General.Core.Extensions;
using General.Framework.Infrastructure;
using General.Framework;
using General.Framework.Security.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace General.Mvc
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
            services.AddMvc();

            //添加DbContext
            services.AddDbContextPool<GeneralDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //保证DbContext在一次请求中是同一个实列 默认是Scope
            //services.AddDbContext<GeneralDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //添加权限过滤
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = CookieAdminAuthInfo.AuthenticationScheme;
                o.DefaultChallengeScheme = CookieAdminAuthInfo.AuthenticationScheme;
                //o.DefaultSignInScheme = "General";
                //o.DefaultSignOutScheme = "General";
            }).AddCookie(CookieAdminAuthInfo.AuthenticationScheme, o =>
            {
                o.LoginPath = "/admin/login";
            });

            services.AddSession();

            //添加服务
            //services.AddScoped<ICategoryService, CategoryService>();

            //程序集依赖注入，注入所有Services
            services.AddAssembly("General.Services");

            //泛型注入到DI里面
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddScoped<IWorkContext, WorkContext>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IMemoryCache, MemoryCache>();

            //services.BuildServiceProvider().GetService<ICategoryService>();
            //new GeneralEngin(services.BuildServiceProvider());
            EnginContext.Initialize(new GeneralEngin(services.BuildServiceProvider()));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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

            //启用权限过滤
            app.UseAuthentication();

            //启用Session
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Login}/{action=Index}/{id?}"
                );
            });

        }
    }
}
