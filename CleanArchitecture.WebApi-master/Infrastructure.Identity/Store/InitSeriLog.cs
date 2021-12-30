using System;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Infrastructure.Identity.Store
{
    public class InitSeriLog
    {
        public InitSeriLog()
        {
            //Read Configuration from appSettingsq
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            //Initialize Logger
            Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(config)
                    .CreateLogger();
        }
       
    }
}
