using Abp.Hangfire;
using Abp.Hangfire.Configuration;
using Abp.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncJob.ABPHost
{
    [DependsOn(typeof(AbpHangfireAspNetCoreModule))]
    public class AppModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.BackgroundJobs.UseHangfire();
        }

        //...
    }
}
