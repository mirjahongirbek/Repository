using AspectCore.Extensions.Autofac;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Entity;
using EntityRepository.Context;
using Examples.Db;
using LoggingRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoRepository;
using MongoRepository.Context;
using RepositoryRule.Base;
using RepositoryRule.LoggerRepository;
using Serilog;
using ServiceList;
using System;

namespace Examples
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
           // services.AddSingleton<IDataService, DataService>();
            //services.AddSingleton<IMongoContext, MongoContext>();
            //var log = new LoggerConfiguration().WriteTo.Seq("http://localhost:5341").WriteTo.Console()
            //.CreateLogger();
            //.WriteTo.Stackify()
            //.CreateLogger();
            services.AddCors();
            services.AddDbContext<DataBase>();
            services.AddScoped<EntityRepository.Context.IDataContext>(provider =>
            {
                var aa = provider.GetService<DataBase>();
               return aa;//provider.GetService<ProductContext>();
            });
            //services.AddEntityFrameworkSqlServer().AddDbContext<EntityDatabase>(options =>
            //  options.UseSqlServer(connection));

            //services.AddScoped<IDataContext>(provider => provider.GetService<EntityDatabase>());
            //services.AddScoped<IEntityDataService, EntityDataService>();

            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IAuthUserService, AuthUserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserDeviceService, UserDeviceService>();
            //services.AddScoped<IRoleRepository<Entity.RoleUser, int>, EntityRepository.Repository.RoleRepository<Entity.RoleUser>>();
            //services.AddScoped<IUserDeviceRepository<Entity.UserDevice, Entity.RoleUser, int>, EntityRepository.Repository.DeviceRepository<Entity.UserDevice, Entity.RoleUser>>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
           
            //containerBuilder.RegisterType<DataService>().As<IDataService>();
            //containerBuilder.RegisterType<SelectDataService>().As<ISelectDataService>();
            //containerBuilder.RegisterGeneric(typeof(MongoRepository<>))
            //    .As(typeof(IRepositoryBase<,>));
           // containerBuilder.RegisterType<SeilogLogger>().As<ILoggerRepository>();
            //containerBuilder.RegisterType<MongoContext>().As<IMongoContext>();

            //containerBuilder.RegisterDynamicProxy(mbox => {
            //    mbox.Interceptors.AddTyped<MethodExecuteLoggerInterceptor>(args: new object[] {log});
            //});

            this.ApplicationContainer = containerBuilder.Build();
            return new AutofacServiceProvider(this.ApplicationContainer);
            // return services.BuildAspectCoreServiceProvider();
            //return services.BuildAspectCoreServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                RepositoryRule.State.State.IsDevelopment = true;
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

           // app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseCors(builder => builder
                .WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
