using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace SyncJob
{
    
    [RemoteService]
    [Area("SyncJob")]
    [Route("api/JobTest")]
    //[Authorize]
    public class JobTestController : AbpController
    {

     

        [HttpGet]
        [Route("Enqueue")]
        public ActionResult Enqueue()
        {
            BackgroundJob.Enqueue(() => Console.WriteLine("Simple Job!"));
            return Ok();
        }
        [HttpGet]
        [Route("Schedule")]
        public ActionResult Schedule()
        {
            BackgroundJob.Schedule(() => Console.WriteLine("Hello, world"), TimeSpan.FromDays(1));
            return Ok();
        }
        [HttpGet]
        [Route("AddOrUpdate")]
        public ActionResult AddOrUpdate()
        {
            
            RecurringJob.AddOrUpdate(() => Console.Write("Powerful!"), "0 12 * */2");
            //RecurringJob.AddOrUpdate("RecurringJob", () => Console.WriteLine("Simple RecurringJob!"), "1 * * * * ");
            //每小时执行一次
            RecurringJob.AddOrUpdate("some-id", () => Console.WriteLine(), Cron.Hourly);
            //RecurringJob.RemoveIfExists("some-id");
            //RecurringJob.Trigger("some-id");
            var manager = new RecurringJobManager();
            manager.AddOrUpdate("some-id", Job.FromExpression(() => Console.WriteLine()), Cron.Yearly());
            return Ok();
        }

         
        [HttpGet]
        [Route("Cancellation")]
        public void Cancellation()
        {
            BackgroundJob.Enqueue(() => LongRunningMethod(JobCancellationToken.Null));
        }

        [AutomaticRetry(Attempts = 3)]
        private void LongRunningMethod(IJobCancellationToken cancellationToken)
        {
            for (var i = 0; i < Int32.MaxValue; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }

        //出现问题防止重复执行
        private void ReName()
        {
            //BatchJob.StartNew(x =>
            //{
            //    for (var i = 0; i < 1000; i++)
            //    {
            //        x.Enqueue(() => SendEmail(i));
            //    }
            //});
        }

    }
}