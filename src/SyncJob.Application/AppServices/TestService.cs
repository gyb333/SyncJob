using Hangfire;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
 

namespace AppServices
{
    [RemoteService(IsEnabled = true)]
    public class TestService: ApplicationService
    {
        public void Test()
        {
             Console.WriteLine("Hello world!");
        }

         
    }
}
