namespace Maticsoft.Email
{
    using Maticsoft.Email.BLL;
    using Maticsoft.Email.Model;
    using System;
    using System.Collections.Generic;

    public static class EmailManage
    {
        private static Maticsoft.Email.BLL.EmailQueue emailQueueManage = new Maticsoft.Email.BLL.EmailQueue();

        public static bool PushQueue(Maticsoft.Email.Model.EmailQueue model)
        {
            return emailQueueManage.Add(model);
        }

        public static void PushQueue(List<Maticsoft.Email.Model.EmailQueue> list)
        {
            if ((list != null) && (list.Count >= 1))
            {
                list.ForEach(delegate (Maticsoft.Email.Model.EmailQueue xx) {
                    emailQueueManage.Add(xx);
                });
            }
        }

        public static bool PushQueue(string userType, string emailFrom, string emailSubject, string emailBody)
        {
            return emailQueueManage.PushEmailQueur(userType, "", emailSubject, emailBody, emailFrom);
        }

        public static bool PushQueue(string userType, string userName, string emailSubject, string emailBody, string emailFrom)
        {
            return emailQueueManage.PushEmailQueur(userType, userName, emailSubject, emailBody, emailFrom);
        }
    }
}

