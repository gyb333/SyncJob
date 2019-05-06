using Hangfire.Console;
using Hangfire.RecurringJobExtensions;
using Hangfire.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hangfire.Samples
{
    public class Job : IRecurringJob
    {
         
        public void Execute(PerformContext context)
        {
            for (var i = 0; i < Int32.MaxValue; i++)
            {
                context.CancellationToken.ThrowIfCancellationRequested();
                context.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} MyJob1 Running ..."+i);
                Thread.Sleep(TimeSpan.FromSeconds(1));
            } 
        }
    

    public static void CancellExecute(IJobCancellationToken cancellationToken)
        {
            for (var i = 0; i < Int32.MaxValue; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

        }
    }
}
