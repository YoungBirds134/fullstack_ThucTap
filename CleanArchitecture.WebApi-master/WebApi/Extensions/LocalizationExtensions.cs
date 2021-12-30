using System;
using Microsoft.AspNetCore.Builder;

namespace WebApi.Extensions
{
    public class LocalizationExtensions
    {
        public void Configure(IApplicationBuilder app, RequestLocalizationOptions options)
        {
            app.UseRequestLocalization(options);
        }
    }
}
