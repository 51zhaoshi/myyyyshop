namespace Maticsoft.Email.BLL
{
    using Maticsoft.Email.IDAL;
    using System;
    using System.Collections.Generic;

    internal static class EmailQueueProvider
    {
        private static IEmailQueueProvider dal = (PubConstant.IsSQLServer ? ((IEmailQueueProvider) new EmailQueueProvider()) : ((IEmailQueueProvider) new EmailQueueProvider()));

        public static int DeleteQueuedEmail(int emailId)
        {
            return dal.DeleteQueuedEmail(emailId);
        }

        public static IList<EmailTemplate> DequeueEmail()
        {
            return dal.DequeueEmail();
        }

        public static int QueueSendingFailure(IList<int> list, int failureInterval, int maxNumberOfTries)
        {
            return dal.QueueSendingFailure(list, failureInterval, maxNumberOfTries);
        }
    }
}

