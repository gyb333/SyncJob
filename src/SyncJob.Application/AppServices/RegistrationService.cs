
using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.BackgroundJobs;

namespace AppServices
{
    [RemoteService(IsEnabled = false)]
    public class RegistrationService : ApplicationService
    {
        private readonly IBackgroundJobManager _backgroundJobManager;

        public RegistrationService(IBackgroundJobManager backgroundJobManager)
        {
            _backgroundJobManager = backgroundJobManager;
        }

        public async Task RegisterAsync(string userName, string emailAddress, string password)
        {
            //TODO: Create new user in the database...

            await _backgroundJobManager.EnqueueAsync(
                new EmailSendingArgs
                {
                    EmailAddress = emailAddress,
                    Subject = "You've successfully registered!",
                    Body = "..."
                }
            );

            
        }
    }
}
