using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Settings;

namespace SyncJob
{
    [DependsOn(
        typeof(DomainModule),
        typeof(ApplicationContractsModule),
        typeof(AbpAutoMapperModule)
        )]
    public class ApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<AutoMapperProfile>(validate: true);
            });
        }
    }
}
