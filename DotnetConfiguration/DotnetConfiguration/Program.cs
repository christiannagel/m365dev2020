using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {

    })
    .Build();

var config = host.Services.GetRequiredService<IConfiguration>();

var connectionString = config.GetConnectionString("SampleConnection");
Console.WriteLine(connectionString);
var val1 = config["MyKey1"];
Console.WriteLine(val1);
var val2 = config["MyKey2"];
Console.WriteLine(val2);