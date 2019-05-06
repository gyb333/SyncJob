using Hangfire.Console;
using Hangfire.RecurringJobExtensions;
using Hangfire.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hangfire.Samples
{
    public class MyJob1 : IRecurringJob
    {
        static int count = 0;
        public void Execute(PerformContext context)
        {
            count++;
            context.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} MyJob1 Running ..."+count);
            Thread.Sleep(1000 * 5);
        }
    }
}
