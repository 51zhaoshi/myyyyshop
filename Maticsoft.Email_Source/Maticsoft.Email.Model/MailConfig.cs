namespace Maticsoft.Email.Model
{
    using System;

    [Serializable]
    public class MailConfig
    {
        private int _id;
        private string _mailaddress;
        private string _password;
        private int _popport = 110;
        private string _popserver;
        private bool _popssl;
        private int _smtpport = 0x19;
        private string _smtpserver;
        private bool _smtpssl;
        private int _userid;
        private string _username;

        public int ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public string Mailaddress
        {
            get
            {
                return this._mailaddress;
            }
            set
            {
                this._mailaddress = value;
            }
        }

        public string Password
        {
            get
            {
                return this._password;
            }
            set
            {
                this._password = value;
            }
        }

        public int POPPort
        {
            get
            {
                return this._popport;
            }
            set
            {
                this._popport = value;
            }
        }

        public string POPServer
        {
            get
            {
                return this._popserver;
            }
            set
            {
                this._popserver = value;
            }
        }

        public bool POPSSL
        {
            get
            {
                return this._popssl;
            }
            set
            {
                this._popssl = value;
            }
        }

        public int SMTPPort
        {
            get
            {
                return this._smtpport;
            }
            set
            {
                this._smtpport = value;
            }
        }

        public string SMTPServer
        {
            get
            {
                return this._smtpserver;
            }
            set
            {
                this._smtpserver = value;
            }
        }

        public bool SMTPSSL
        {
            get
            {
                return this._smtpssl;
            }
            set
            {
                this._smtpssl = value;
            }
        }

        public int UserID
        {
            get
            {
                return this._userid;
            }
            set
            {
                this._userid = value;
            }
        }

        public string Username
        {
            get
            {
                return this._username;
            }
            set
            {
                this._username = value;
            }
        }
    }
}

