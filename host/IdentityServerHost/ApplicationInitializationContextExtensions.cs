using Microsoft.Extensions.DependencyInjection;
using Study.Common.IdentityServer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace IdentityServerHost
{
    public static class ApplicationInitializationContextExtensions
    {
        public static async Task SeedDataAsync(this ApplicationInitializationContext context)
        {
            using (var scope = context.ServiceProvider.CreateScope())
            {
                await scope.ServiceProvider
                    .GetRequiredService<DataSeeder>().SeedAsync();

            }
        }
    }
}
