using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace SyncJob
{
    [DependsOn(
        typeof(ApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class HttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "SyncJob";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(ApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
