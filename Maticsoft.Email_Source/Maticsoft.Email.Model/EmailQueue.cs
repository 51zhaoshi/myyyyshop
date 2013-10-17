namespace Maticsoft.Email.Model
{
    using System;

    [Serializable]
    public class EmailQueue
    {
        private string _emailbcc;
        private string _emailbody;
        private string _emailcc;
        private string _emailfrom;
        private int _emailid;
        private int _emailpriority;
        private string _emailsubject;
        private string _emailto;
        private bool _isbodyhtml;
        private DateTime _nexttrytime;
        private int _numberoftries;

        internal string EmailBcc
        {
            get
            {
                return this._emailbcc;
            }
            set
            {
                this._emailbcc = value;
            }
        }

        public string EmailBody
        {
            get
            {
                return this._emailbody;
            }
            set
            {
                this._emailbody = value;
            }
        }

        internal string EmailCc
        {
            get
            {
                return this._emailcc;
            }
            set
            {
                this._emailcc = value;
            }
        }

        public string EmailFrom
        {
            get
            {
                return this._emailfrom;
            }
            set
            {
                this._emailfrom = value;
            }
        }

        public int EmailId
        {
            get
            {
                return this._emailid;
            }
            set
            {
                this._emailid = value;
            }
        }

        public int EmailPriority
        {
            get
            {
                return this._emailpriority;
            }
            set
            {
                this._emailpriority = value;
            }
        }

        public string EmailSubject
        {
            get
            {
                return this._emailsubject;
            }
            set
            {
                this._emailsubject = value;
            }
        }

        public string EmailTo
        {
            get
            {
                return this._emailto;
            }
            set
            {
                this._emailto = value;
            }
        }

        public bool IsBodyHtml
        {
            get
            {
                return this._isbodyhtml;
            }
            set
            {
                this._isbodyhtml = value;
            }
        }

        public DateTime NextTryTime
        {
            get
            {
                return this._nexttrytime;
            }
            set
            {
                this._nexttrytime = value;
            }
        }

        public int NumberOfTries
        {
            get
            {
                return this._numberoftries;
            }
            set
            {
                this._numberoftries = value;
            }
        }
    }
}

