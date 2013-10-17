namespace Maticsoft.Model.SysManage
{
    using System;

    public class UserLog
    {
        private string _OPInfo;
        private DateTime _OPTime;
        private string _Url;
        private string _UserIP;
        private string _UserName;
        private string _UserType;

        public UserLog()
        {
            this._OPTime = DateTime.Now;
        }

        public UserLog(string url, string opInfo, string userName, string userType, string userIP)
        {
            this._OPTime = DateTime.Now;
            this.Url = url;
            this.OPInfo = opInfo;
            this.UserName = userName;
            userName = this.UserType;
            this.UserIP = userIP;
        }

        public string OPInfo
        {
            get
            {
                return this._OPInfo;
            }
            set
            {
                this._OPInfo = value;
            }
        }

        public DateTime OPTime
        {
            get
            {
                return this._OPTime;
            }
            set
            {
                this._OPTime = value;
            }
        }

        public string Url
        {
            get
            {
                return this._Url;
            }
            set
            {
                this._Url = value;
            }
        }

        public string UserIP
        {
            get
            {
                return this._UserIP;
            }
            set
            {
                this._UserIP = value;
            }
        }

        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }

        public string UserType
        {
            get
            {
                return this._UserType;
            }
            set
            {
                this._UserType = value;
            }
        }
    }
}

