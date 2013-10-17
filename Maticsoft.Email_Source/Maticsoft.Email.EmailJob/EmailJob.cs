namespace Maticsoft.Email.EmailJob
{
    using Maticsoft.Common;
    using Maticsoft.Common.DEncrypt;
    using Maticsoft.Email.BLL;
    using Maticsoft.Email.Model;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net.Mail;
    using System.Text;
    using System.Threading;
    using System.Xml;

    public class EmailJob : IJob
    {
        private int _failureInterval = 15;
        private int _numberOfTries = 5;

        public void Execute(XmlNode node)
        {
            if (node != null)
            {
                XmlAttribute attribute = node.Attributes["failureInterval"];
                XmlAttribute attribute2 = node.Attributes["numberOfTries"];
                if (attribute != null)
                {
                    try
                    {
                        this._failureInterval = int.Parse(attribute.Value, CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        this._failureInterval = 15;
                    }
                }
                if (attribute2 != null)
                {
                    try
                    {
                        this._numberOfTries = int.Parse(attribute2.Value, CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        this._numberOfTries = 5;
                    }
                }
                this.SendQueuedEmailJob();
            }
        }

        public void SendQueuedEmailJob()
        {
            this.SendQueuedEmails(this._failureInterval, this._numberOfTries);
        }

        private void SendQueuedEmails(int failureInterval, int maxNumberOfTries)
        {
            Maticsoft.Email.Model.MailConfig model = new Maticsoft.Email.BLL.MailConfig().GetModel();
            if (model != null)
            {
                IList<EmailTemplate> list = EmailQueueProvider.DequeueEmail();
                IList<int> list2 = new List<int>();
                int num = 0;
                int num2 = 0;
                short num3 = 0;
                foreach (EmailTemplate template in list)
                {
                    try
                    {
                        template.From = new MailAddress(model.Mailaddress);
                        template.BodyEncoding = Encoding.UTF8;
                        template.Body = template.Body.Replace("\n", "\r\n");
                        MailSender.Send(model.SMTPServer, model.Username, DESEncrypt.Decrypt(model.Password), model.Mailaddress, template.EmailTo, "", "", template.Subject, template.Body, true, Encoding.Default, true, model.SMTPSSL, null);
                        EmailQueueProvider.DeleteQueuedEmail(template.EmailID);
                        if ((num3 != -1) && (++num >= num3))
                        {
                            Thread.Sleep(new TimeSpan(0, 0, 0, 15, 0));
                            num = 0;
                        }
                    }
                    catch
                    {
                        list2.Add(template.EmailID);
                    }
                    num2++;
                    if (num2 >= 5)
                    {
                        break;
                    }
                }
                if (list2.Count > 0)
                {
                    EmailQueueProvider.QueueSendingFailure(list2, failureInterval, maxNumberOfTries);
                }
            }
        }
    }
}

