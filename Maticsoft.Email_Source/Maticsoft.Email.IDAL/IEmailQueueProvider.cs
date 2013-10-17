namespace Maticsoft.Email.IDAL
{
    using System;
    using System.Collections.Generic;
    using System.Net.Mail;

    public interface IEmailQueueProvider
    {
        int DeleteQueuedEmail(int emailId);
        IList<EmailTemplate> DequeueEmail();
        void QueueEmail(MailMessage message);
        int QueueSendingFailure(IList<int> list, int failureInterval, int maxNumberOfTries);
    }
}

