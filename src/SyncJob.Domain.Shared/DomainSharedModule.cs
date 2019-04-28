using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using SyncJob.Localization;

namespace SyncJob
{
    [DependsOn(
        typeof(AbpLocalizationModule)
        )]
    public class DomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources.Add<Resource>("en");
            });
        }
    }
}
