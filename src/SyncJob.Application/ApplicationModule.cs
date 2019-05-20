using IAppServices;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Settings;
using Volo.Abp.Validation;

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

            //注册验证
            context.Services.OnRegistred(onServiceRegistredContext =>
            {
                if (typeof(IValidateAppService).IsAssignableFrom(onServiceRegistredContext.ImplementationType))
                {
                    onServiceRegistredContext.Interceptors.TryAdd<ValidationInterceptor>();
                }
            });
            //context.Services.AddType<MyAppService>();
        }

        
    }
}
