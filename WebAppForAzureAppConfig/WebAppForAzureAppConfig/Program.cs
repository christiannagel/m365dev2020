using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace WebAppForAzureAppConfig
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((context, config) =>
                    {
                        if (context.HostingEnvironment.IsDevelopment())
                        {
                            var settings = config.Build();
                            var appConfigurationConnection = settings["AzureAppConfigurationConnection"];
                            config.AddAzureAppConfiguration(options =>
                            {
                                options.Connect(appConfigurationConnection)
                                    .ConfigureRefresh(refresh =>
                                    {
                                        refresh.Register("AppConfigurationSample:Settings:Sentinel", refreshAll: true)
                                            .SetCacheExpiration(TimeSpan.FromSeconds(5));
                                    });
                            });
                        }
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
