namespace Maticsoft.Common
{
    using System;

    public class PopSetting
    {
        private string _password;
        private int _port;
        private string _server;
        private string _username;
        private bool _usessl;

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

        public int Port
        {
            get
            {
                return this._port;
            }
            set
            {
                this._port = value;
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

        public bool UseSSL
        {
            get
            {
                return this._usessl;
            }
            set
            {
                this._usessl = value;
            }
        }
    }
}

