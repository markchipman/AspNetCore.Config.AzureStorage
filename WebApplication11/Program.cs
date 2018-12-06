using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

using WebApplication11.Config.AzureStorage;

namespace WebApplication11
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .ConfigureAppConfiguration((context, config) =>
                   {
                       var builtConfig = config.Build();

                       config.AddAzureTableStorage(builtConfig.GetConnectionString("StorageConnection"), builtConfig["TableName"], builtConfig["PartitionKey"]);
                   })
                   .UseStartup<Startup>();
    }
}
