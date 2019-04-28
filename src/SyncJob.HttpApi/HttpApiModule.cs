using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace SyncJob
{
    [DependsOn(
        typeof(ApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class HttpApiModule : AbpModule
    {
        
    }
}
