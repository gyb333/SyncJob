 
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Emailing;

namespace Domain
{
    public class EmailSendingJob : BackgroundJob<EmailSendingArgs>
    {
        private readonly IEmailSender _emailSender;

        public EmailSendingJob(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public override void Execute(EmailSendingArgs args)
        {
            _emailSender.Send(
                args.EmailAddress,
                args.Subject,
                args.Body
            );
        }
    }
}
