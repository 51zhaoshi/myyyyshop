namespace Maticsoft.Common
{
    using System;

    public class SmtpSetting
    {
        private bool _authentication;
        private string _password;
        private string _sender;
        private string _server;
        private string _username;

        public bool Authentication
        {
            get
            {
                return this._authentication;
            }
            set
            {
                this._authentication = value;
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

        public string Sender
        {
            get
            {
                return this._sender;
            }
            set
            {
                this._sender = value;
            }
        }

        public string Server
        {
            get
            {
                return this._server;
            }
            set
            {
                this._server = value;
            }
        }

        public string UserName
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

