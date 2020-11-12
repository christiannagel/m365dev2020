using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
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
                        var settings = config.Build();

                        if (context.HostingEnvironment.IsDevelopment())
                        {
                            var appConfigurationConnection = settings["AzureAppConfigurationConnection"];
                            config.AddAzureAppConfiguration(options =>
                            {
                                options.Connect(appConfigurationConnection)
                                    .Select(KeyFilter.Any, LabelFilter.Null)
                                    .Select(KeyFilter.Any, context.HostingEnvironment.EnvironmentName)
                                    .ConfigureRefresh(refresh =>
                                    {
                                        refresh.Register("AppConfigurationSample:Settings:Sentinel", refreshAll: true)
                                            .SetCacheExpiration(TimeSpan.FromSeconds(5));
                                    })
                                    .UseFeatureFlags();
                            });
                        }
                        else
                        {
                            var appConfigurationEndpoint = settings["AzureAppConfigurationEndpoint"];
                            config.AddAzureAppConfiguration(options =>
                            {
                                var credentials = new ManagedIdentityCredential();
                                options.Connect(new Uri(appConfigurationEndpoint), credentials)
                                    .Select(KeyFilter.Any, LabelFilter.Null)
                                    .Select(KeyFilter.Any, context.HostingEnvironment.EnvironmentName)
                                    .ConfigureRefresh(refresh =>
                                    {
                                        refresh.Register("AppConfigurationSample:Settings:Sentinel", refreshAll: true)
                                            .SetCacheExpiration(TimeSpan.FromSeconds(5));
                                    })
                                    .UseFeatureFlags()
                                    .ConfigureKeyVault(kv =>
                                    {
                                        kv.SetCredential(credentials);
                                    });
                            });
                        }
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
