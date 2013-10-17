namespace Maticsoft.Model.Settings
{
    using System;

    [Serializable]
    public class AdvertisePosition
    {
        private string _advhtml;
        private int _advpositionid;
        private string _advpositionname;
        private DateTime? _createddate;
        private int? _createduserid;
        private int? _height;
        private bool _isone;
        private int? _repeatcolumns;
        private int? _showtype;
        private int? _timeinterval;
        private int? _width;

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

        public int AdvPositionId
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

        public string AdvPositionName
        {
            get
            {
                return this._advpositionname;
            }
            set
            {
                this._advpositionname = value;
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

        public int? Height
        {
            get
            {
                return this._height;
            }
            set
            {
                this._height = value;
            }
        }

        public bool IsOne
        {
            get
            {
                return this._isone;
            }
            set
            {
                this._isone = value;
            }
        }

        public int? RepeatColumns
        {
            get
            {
                return this._repeatcolumns;
            }
            set
            {
                this._repeatcolumns = value;
            }
        }

        public int? ShowType
        {
            get
            {
                return this._showtype;
            }
            set
            {
                this._showtype = value;
            }
        }

        public int? TimeInterval
        {
            get
            {
                return this._timeinterval;
            }
            set
            {
                this._timeinterval = value;
            }
        }

        public int? Width
        {
            get
            {
                return this._width;
            }
            set
            {
                this._width = value;
            }
        }
    }
}

