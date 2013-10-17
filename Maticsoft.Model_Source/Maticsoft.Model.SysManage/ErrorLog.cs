namespace Maticsoft.Model.SysManage
{
    using System;

    public class ErrorLog
    {
        private int _id;
        private string _loginfo;
        private DateTime _optime;
        private string _stacktrace;
        private string _url;

        public ErrorLog()
        {
            this._optime = DateTime.Now;
        }

        public ErrorLog(string url, string loginfo, string stackTrace)
        {
            this._optime = DateTime.Now;
            this.Url = url;
            this.Loginfo = loginfo;
            this.StackTrace = stackTrace;
        }

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

        public string Loginfo
        {
            get
            {
                return this._loginfo;
            }
            set
            {
                this._loginfo = value;
            }
        }

        public DateTime OPTime
        {
            get
            {
                return this._optime;
            }
            set
            {
                this._optime = value;
            }
        }

        public string StackTrace
        {
            get
            {
                return this._stacktrace;
            }
            set
            {
                this._stacktrace = value;
            }
        }

        public string Url
        {
            get
            {
                return this._url;
            }
            set
            {
                this._url = value;
            }
        }
    }
}

