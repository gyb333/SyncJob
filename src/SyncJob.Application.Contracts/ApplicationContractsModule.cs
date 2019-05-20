using Microsoft.Extensions.DependencyInjection;
using SyncJob.Localization;
using Volo.Abp.Application;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace SyncJob
{
    [DependsOn(
        typeof(DomainSharedModule),
        typeof(AbpDddApplicationModule)
        )]
    public class ApplicationContractsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<VirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<ApplicationContractsModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<Resource>()
                    .AddVirtualJson("/Localization/ApplicationContracts");
            });
        }
    }
}
