using System.IO;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace KooliProjekt.IntegrationTests.Helpers
{
    public class TestApplicationFactory<TTestStartup> : WebApplicationFactory<TTestStartup> where TTestStartup : class
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureWebHost(builder =>
                {
                    builder.UseContentRoot(".");
                    builder.ConfigureAppConfiguration((c, b) =>
                    {
                        c.HostingEnvironment.ApplicationName = "KooliProjekt.WebAPI";
                    });
                    builder.UseStartup<TTestStartup>();
                })
                .ConfigureAppConfiguration((context, conf) =>
                {
                    var projectDir = Directory.GetCurrentDirectory();
                    var configPath = Path.Combine(projectDir, "appsettings.json");

                    conf.Sources.Clear();
                    conf.AddJsonFile(configPath, optional: false);

                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        var linuxConfigPath = Path.Combine(projectDir, "appsettings.Linux.json");
                        conf.AddJsonFile(linuxConfigPath, optional: false);
                    }

                    // --- DIAGNOSTIC: Add this temporarily ---
                    var tempConfig = conf.Build();
                    var connStr = tempConfig.GetConnectionString("DefaultConnection");
                    System.Console.WriteLine($"=== OS: {RuntimeInformation.OSDescription}");
                    System.Console.WriteLine($"=== IsLinux: {RuntimeInformation.IsOSPlatform(OSPlatform.Linux)}");
                    System.Console.WriteLine($"=== ProjectDir: {projectDir}");
                    System.Console.WriteLine($"=== Linux config exists: {File.Exists(Path.Combine(projectDir, "appsettings.Linux.json"))}");
                    System.Console.WriteLine($"=== ConnectionString: {connStr}");
                    System.Console.WriteLine($"=== Config sources count: {conf.Sources.Count}");
                    foreach (var source in conf.Sources)
                    {
                        System.Console.WriteLine($"===   Source: {source}");
                    }
                    // --- END DIAGNOSTIC ---
                });
            return host;
        }

    }
}