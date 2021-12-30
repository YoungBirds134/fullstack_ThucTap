using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http.Extensions;
using Serilog;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace WebApi
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            //Read Configuration from appSettingsq
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            //Initialize Logger
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();

            Serilog.Debugging.SelfLog.Enable(msg =>
            {
                Debug.Print(msg);
                Debugger.Break();
            });
            //
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    // var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    // var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    //await Infrastructure.Identity.Seeds.DefaultRoles.SeedAsync(userManager, roleManager);
                    //await Infrastructure.Identity.Seeds.DefaultSuperAdmin.SeedAsync(userManager, roleManager);
                    //await Infrastructure.Identity.Seeds.DefaultBasicUser.SeedAsync(userManager, roleManager);
            
                    // Log.Information("Application Starting");
                    host.Run();
                }
                catch (Exception ex)
                {
                    Log.Warning(ex, "An error occurred seeding the DB");
                }
                finally
                {
                    Log.CloseAndFlush();
                }
            }
           
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog((hostingContext,loggerConfiguration)=>{
                loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
            }) //Uses Serilog instead of default .NET Logger
            
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.UseUrls("http://*:80");
                    webBuilder.UseStartup<Startup>();
                });
               
    }
}
