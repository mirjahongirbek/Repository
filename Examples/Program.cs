using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using RepositoryRule.Entity;

namespace Examples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseUrls("http://*:9524")
                .UseStartup<Startup>();
    }
}
