namespace Maticsoft.Email.Model
{
    using System;
    using System.Net.Mail;

    public class EmailTemplate : MailMessage
    {
        private string emailBody;
        private string emailDescription;
        private int emailID;
        private string emailTo;
        private string emailType;
        private int numberOfTries;
        private string tagDescription;
        private int templetId;

        public string EmailBody
        {
            get
            {
                return this.emailBody;
            }
            set
            {
                this.emailBody = value;
            }
        }

        public string EmailDescription
        {
            get
            {
                return this.emailDescription;
            }
            set
            {
                this.emailDescription = value;
            }
        }

        public int EmailID
        {
            get
            {
                return this.emailID;
            }
            set
            {
                this.emailID = value;
            }
        }

        public string EmailTo
        {
            get
            {
                return this.emailTo;
            }
            set
            {
                this.emailTo = value;
            }
        }

        public string EmailType
        {
            get
            {
                return this.emailType;
            }
            set
            {
                this.emailType = value;
            }
        }

        public int NumberOfTries
        {
            get
            {
                return this.numberOfTries;
            }
            set
            {
                this.numberOfTries = value;
            }
        }

        public string TagDescription
        {
            get
            {
                return this.tagDescription;
            }
            set
            {
                this.tagDescription = value;
            }
        }

        public int TempletId
        {
            get
            {
                return this.templetId;
            }
            set
            {
                this.templetId = value;
            }
        }
    }
}

