namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class StarRank
    {
        private DateTime _createddate;
        private DateTime _enddate;
        private int _id;
        private bool _isrecommend;
        private string _nickname;
        private DateTime _rankdate;
        private int _ranktype;
        private int _sequence;
        private DateTime _startdate;
        private int _status;
        private int _timeunit;
        private int _typeid;
        private string _usergravatar;
        private int _userid;

        public DateTime CreatedDate
        {
            get
            {
                return this._createddate;
            }
            set
            {
                this._createddate = value;
            }
        }

        public DateTime EndDate
        {
            get
            {
                return this._enddate;
            }
            set
            {
                this._enddate = value;
            }
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

        public bool IsRecommend
        {
            get
            {
                return this._isrecommend;
            }
            set
            {
                this._isrecommend = value;
            }
        }

        public string NickName
        {
            get
            {
                return this._nickname;
            }
            set
            {
                this._nickname = value;
            }
        }

        public DateTime RankDate
        {
            get
            {
                return this._rankdate;
            }
            set
            {
                this._rankdate = value;
            }
        }

        public int RankType
        {
            get
            {
                return this._ranktype;
            }
            set
            {
                this._ranktype = value;
            }
        }

        public int Sequence
        {
            get
            {
                return this._sequence;
            }
            set
            {
                this._sequence = value;
            }
        }

        public DateTime StartDate
        {
            get
            {
                return this._startdate;
            }
            set
            {
                this._startdate = value;
            }
        }

        public int Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }

        public int TimeUnit
        {
            get
            {
                return this._timeunit;
            }
            set
            {
                this._timeunit = value;
            }
        }

        public int TypeId
        {
            get
            {
                return this._typeid;
            }
            set
            {
                this._typeid = value;
            }
        }

        public string UserGravatar
        {
            get
            {
                return this._usergravatar;
            }
            set
            {
                this._usergravatar = value;
            }
        }

        public int UserId
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
    }
}

