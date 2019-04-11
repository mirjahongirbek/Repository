using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using LangServer.Services.Interfaces;
using LangServer.Services.Logic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoRepository.Context;

namespace LangServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer Builder { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
           // ElasticConnection();
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            containerBuilder.RegisterType<Db.MongoDb>().As<IMongoContext>();
            containerBuilder.RegisterType<LanguageService>().As<ILanguageService>();
            containerBuilder.RegisterType<EntityService>().As<IEntityService>();
          //  containerBuilder.RegisterType<ModelService>().As<IModelService>();
            containerBuilder.RegisterType<ProjectService>().As<IProjectService>().AutoActivate();
            Builder = containerBuilder.Build();
            return new AutofacServiceProvider(this.Builder);

        }
       //  void ElasticConnection()
       // {
            
       //     var node1 = new Uri("http://localhost:9200/");
       //   //  var node2 = new Uri("http://152.30.11.192:9200/");
       //     var nodes = new Uri[]
       //     {
       //         node1
       //        //,node2
       //     };
       //     var connectionPool = new SniffingConnectionPool(nodes);
       //     var connectionSettings = new ConnectionSettings(connectionPool)
       //                         .SniffOnConnectionFault(false)
       //                         .SniffOnStartup(false)
       //                         .SniffLifeSpan(TimeSpan.FromMinutes(1));
       //   //  States.Client = new ElasticClient(connectionSettings);
       ////     _client = new ElasticClient(connectionSettings);
       // }
        //public void ElasticConnection()
        //{
        //    States.Urls = new System.Collections.Generic.List<string>() { "http://localhost:9200" };
        //    var nodes = new Uri[States.Urls.Count];
        //    for (var i = 0; i < States.Urls.Count; i++)
        //    {
        //        nodes[i] = new Uri(States.Urls[i]);
        //    }
        //    // var node1 = new Uri("http://localhost:9200/");
        //    var node = new Uri("http://localhost:9200/");
        //    //   var pool = new StaticConnectionPool(nodes);
        //    var settings = new ConnectionSettings(node);
        //    States.Client = new ElasticClient(settings);



        //}
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
            }

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
