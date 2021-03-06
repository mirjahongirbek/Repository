﻿using EntityExample.Db;
using EntityRepository.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepositoryRule.Base;
using ServiceList;

namespace EntityExample
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
            string connection = Configuration.GetConnectionString("DefaultConnection");
            // добавляем контекст MobileContext в качестве сервиса в приложение
            services.AddEntityFrameworkSqlServer().AddDbContext<EntityDatabase>(options =>
                options.UseSqlServer(connection));

            services.AddScoped<IDataContext>(provider => provider.GetService<EntityDatabase>());
            //services.AddScoped<IEntityDataService, EntityDataService>();

            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IProductService, ProductService>();
              
            services.AddScoped<IAuthRepository<Entity.User, Entity.RoleUser, Entity.UserDevice, int>, EntityRepository.Repository.AuthRepository<Entity.User, Entity.RoleUser, Entity.UserDevice>>();
            services.AddScoped<IRoleRepository<Entity.RoleUser, int>, EntityRepository.Repository.RoleRepository<Entity.RoleUser>>();
            services.AddScoped<IUserDeviceRepository<Entity.UserDevice, Entity.RoleUser, int>, EntityRepository.Repository.DeviceRepository<Entity.UserDevice, Entity.RoleUser>>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
