
using EntityFrameworkCore;
using Entitys; 
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace SyncJob.EntityFrameworkCore
{
    [DependsOn(
        typeof(DomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class EFCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            
            context.Services.AddAbpDbContext<SourceDbContext>(options =>
            {
               
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
                options.AddDefaultRepositories<ISourceDbContext>();

                //创建默认通用存储库
                options.AddDefaultRepositories();//options.AddDefaultRepositories(includeAllEntities: true);
                //覆盖默认通用存储库
                options.AddRepository<User, UserRepository>(); //Replaces IRepository<User, int>

                options.SetDefaultRepositoryClasses(
                    typeof(SourceDbRepositoryBase<,>),
                    typeof(SourceDbRepositoryBase<>));
            });


            context.Services.AddAbpDbContext<TargetDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
                options.AddDefaultRepositories<ITargetDbContext>();

                //创建默认通用存储库
                options.AddDefaultRepositories();//options.AddDefaultRepositories(includeAllEntities: true);
                //覆盖默认通用存储库
                //options.AddRepository<UserTarget, UserTargetRepository>(); //Replaces IRepository<User, int>

                //options.SetDefaultRepositoryClasses(
                //    typeof(TargetDbRepositoryBase<,>),
                //    typeof(TargetDbRepositoryBase<>));
            });
        }
    }
}