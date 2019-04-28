using Hangfire;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
 

namespace AppServices
{
    //[RemoteService(IsEnabled = true)]
    public class JobService: ApplicationService
    {
        public void Enqueue()
        {
            BackgroundJob.Enqueue(() => Console.WriteLine("Simple Job!"));
        }

        public void Schedule()
        {
            BackgroundJob.Schedule(() => Console.WriteLine("Hello, world"), TimeSpan.FromDays(1));
        }

        public void AddOrUpdate()
        {
            RecurringJob.AddOrUpdate("RecurringJob", () => Console.WriteLine("Simple RecurringJob!"), "1 * * * * ");
        }
    }
}
