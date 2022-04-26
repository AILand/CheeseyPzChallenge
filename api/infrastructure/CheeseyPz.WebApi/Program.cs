using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CheeseyPz.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            //TODO: Sensitive config settings in user secrets json
            //TODO: Install Serilog and create logs somewhere useful.
            //TODO: Enrich with application meta data
            //TODO: Configure logger for different environments and set logging levels.

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
