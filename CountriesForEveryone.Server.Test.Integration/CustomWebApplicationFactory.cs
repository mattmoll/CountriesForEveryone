using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace CountriesForEveryone.Server.Test.Integration
{
    public class CustomWebApplicationFactory<TStartup> :
        WebApplicationFactory<TStartup> where TStartup : class
    {
        public IConfigurationRoot Configuration { get; private set; }

        protected override IHostBuilder CreateHostBuilder()
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.Test.json");

            var builder = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<TStartup>();
                });

            builder.ConfigureAppConfiguration((context, conf) =>
            {
                Configuration = conf.AddJsonFile(configPath)
                    .Build();
            });

            return builder;
        }
    }
}
