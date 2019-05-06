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
    [Route("api/Test")]
    //[Authorize]
    public class TestController : AbpController
    {

     

        [HttpGet]
        [Route("Test")]
        public ActionResult Test()
        {
             
            return Ok();
        }
       

    }
}