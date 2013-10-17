namespace Maticsoft.Model.Settings
{
    using System;

    [Serializable]
    public class Advertisement
    {
        private int _advertisementid;
        private string _advertisementname;
        private string _advhtml;
        private int? _advpositionid;
        private string _alternatetext;
        private int? _autostop = -1;
        private int? _contenttype;
        private decimal? _cpmprice = 0;
        private DateTime? _createddate;
        private int? _createduserid;
        private int? _daymaxip = 0;
        private int? _daymaxpv = 0;
        private DateTime? _enddate;
        private int? _enterpriseid;
        private string _fileurl;
        private int? _impressions;
        private string _navigateurl;
        private int _row;
        private int? _sequence;
        private DateTime? _startdate;
        private int? _state;

        public int AdvertisementId
        {
            get
            {
                return this._advertisementid;
            }
            set
            {
                this._advertisementid = value;
            }
        }

        public string AdvertisementName
        {
            get
            {
                return this._advertisementname;
            }
            set
            {
                this._advertisementname = value;
            }
        }

        public string AdvHtml
        {
            get
            {
                return this._advhtml;
            }
            set
            {
                this._advhtml = value;
            }
        }

        public int? AdvPositionId
        {
            get
            {
                return this._advpositionid;
            }
            set
            {
                this._advpositionid = value;
            }
        }

        public string AlternateText
        {
            get
            {
                return this._alternatetext;
            }
            set
            {
                this._alternatetext = value;
            }
        }

        public int? AutoStop
        {
            get
            {
                return this._autostop;
            }
            set
            {
                this._autostop = value;
            }
        }

        public int? ContentType
        {
            get
            {
                return this._contenttype;
            }
            set
            {
                this._contenttype = value;
            }
        }

        public decimal? CPMPrice
        {
            get
            {
                return this._cpmprice;
            }
            set
            {
                this._cpmprice = value;
            }
        }

        public DateTime? CreatedDate
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

        public int? CreatedUserID
        {
            get
            {
                return this._createduserid;
            }
            set
            {
                this._createduserid = value;
            }
        }

        public int? DayMaxIP
        {
            get
            {
                return this._daymaxip;
            }
            set
            {
                this._daymaxip = value;
            }
        }

        public int? DayMaxPV
        {
            get
            {
                return this._daymaxpv;
            }
            set
            {
                this._daymaxpv = value;
            }
        }

        public DateTime? EndDate
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

        public int? EnterpriseID
        {
            get
            {
                return this._enterpriseid;
            }
            set
            {
                this._enterpriseid = value;
            }
        }

        public string FileUrl
        {
            get
            {
                return this._fileurl;
            }
            set
            {
                this._fileurl = value;
            }
        }

        public int? Impressions
        {
            get
            {
                return this._impressions;
            }
            set
            {
                this._impressions = value;
            }
        }

        public string NavigateUrl
        {
            get
            {
                return this._navigateurl;
            }
            set
            {
                this._navigateurl = value;
            }
        }

        public int Row
        {
            get
            {
                return this._row;
            }
            set
            {
                this._row = value;
            }
        }

        public int? Sequence
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

        public DateTime? StartDate
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

        public int? State
        {
            get
            {
                return this._state;
            }
            set
            {
                this._state = value;
            }
        }
    }
}

