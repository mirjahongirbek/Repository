using AspectCore.Extensions.Autofac;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Entity;
using LanguageService;
using LanguageService.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RepositoryRule.Entity;
using ServiceList;
using System;
using System.Collections.Generic;

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
            RepositoryRule.State.State.NoHashPassword = true;
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                     .AddJwtBearer(options =>
                     {
                         options.RequireHttpsMetadata = false;
                         options.TokenValidationParameters = new TokenValidationParameters
                         {
                            // укзывает, будет ли валидироваться издатель при валидации токена
                            ValidateIssuer = true,
                            // строка, представляющая издателя
                            ValidIssuer = AuthOption.ISSUER,

                            // будет ли валидироваться потребитель токена
                            ValidateAudience = true,
                            // установка потребителя токена
                            ValidAudience = AuthOption.AUDINECE,
                            // будет ли валидироваться время существования
                            ValidateLifetime = true,
                            // установка ключа безопасности
                            IssuerSigningKey = EntityRepository.State.State.GetSecurityKey(),
                         // валидация ключа безопасности
                         ValidateIssuerSigningKey = true,
                         };
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

            //services.AddScoped<ICompanyService, CompanyService>();
            //services.AddScoped<IProductService, ProductService>();

           // services.AddScoped<IAuthUserService, AuthUserService>();
           // services.AddScoped<IRoleService, RoleService>();
           // services.AddScoped<IUserDeviceService, UserDeviceService>();
            //services.AddScoped<IRoleRepository<Entity.RoleUser, int>, EntityRepository.Repository.RoleRepository<Entity.RoleUser>>();
            //services.AddScoped<IUserDeviceRepository<Entity.UserDevice, Entity.RoleUser, int>, EntityRepository.Repository.DeviceRepository<Entity.UserDevice, Entity.RoleUser>>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            Entity.Start.Build(containerBuilder);
            containerBuilder.RegisterType<CompanyService>().As<ICompanyService>();
            containerBuilder.RegisterType<ProductService>().As<IProductService>();
            containerBuilder.RegisterType<CategoryService>().As<ICategoryService>();
            containerBuilder.RegisterType<AuthUserService>().As<IAuthUserService>();
            containerBuilder.RegisterType<RoleService>().As<IRoleService>();
            containerBuilder.RegisterType<UserDeviceService>().As<IUserDeviceService>();
            containerBuilder.RegisterType<LanguageService<int>>().As<ILanguageService<int>>().AutoActivate();
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
           var ss= this.ApplicationContainer.Resolve<IEnumerable<IEntity<int>>>();

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
            app.UseAuthentication();
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
