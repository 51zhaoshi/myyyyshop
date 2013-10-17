namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class SearchWordTop
    {
        private DateTime _createddate;
        private DateTime? _dateend;
        private DateTime? _datestart;
        private string _hotword;
        private int _id;
        private int _searchcount;
        private int _status;
        private int _timeunit;

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

        public DateTime? DateEnd
        {
            get
            {
                return this._dateend;
            }
            set
            {
                this._dateend = value;
            }
        }

        public DateTime? DateStart
        {
            get
            {
                return this._datestart;
            }
            set
            {
                this._datestart = value;
            }
        }

        public string HotWord
        {
            get
            {
                return this._hotword;
            }
            set
            {
                this._hotword = value;
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

        public int SearchCount
        {
            get
            {
                return this._searchcount;
            }
            set
            {
                this._searchcount = value;
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
    }
}

