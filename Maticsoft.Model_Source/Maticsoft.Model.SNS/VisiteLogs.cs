namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class VisiteLogs
    {
        private string _fromnickname;
        private int _fromuserid;
        private DateTime _lastvisittime = DateTime.Now;
        private string _tonickname;
        private int _touserid;
        private int _visitid;
        private int? _visittimes = 0;

        public string FromNickName
        {
            get
            {
                return this._fromnickname;
            }
            set
            {
                this._fromnickname = value;
            }
        }

        public int FromUserID
        {
            get
            {
                return this._fromuserid;
            }
            set
            {
                this._fromuserid = value;
            }
        }

        public DateTime LastVisitTime
        {
            get
            {
                return this._lastvisittime;
            }
            set
            {
                this._lastvisittime = value;
            }
        }

        public string ToNickName
        {
            get
            {
                return this._tonickname;
            }
            set
            {
                this._tonickname = value;
            }
        }

        public int ToUserID
        {
            get
            {
                return this._touserid;
            }
            set
            {
                this._touserid = value;
            }
        }

        public int VisitID
        {
            get
            {
                return this._visitid;
            }
            set
            {
                this._visitid = value;
            }
        }

        public int? VisitTimes
        {
            get
            {
                return this._visittimes;
            }
            set
            {
                this._visittimes = value;
            }
        }
    }
}

