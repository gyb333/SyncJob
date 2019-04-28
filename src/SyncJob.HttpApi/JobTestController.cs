using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace SyncJob.Abp
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
            RecurringJob.AddOrUpdate("RecurringJob", () => Console.WriteLine("Simple RecurringJob!"), "1 * * * * ");
            return Ok();
        }
    }
}