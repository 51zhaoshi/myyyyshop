namespace Maticsoft.Model.CMS
{
    using System;

    [Serializable]
    public class Video
    {
        private int? _albumid;
        private string _attachment;
        private DateTime _createddate;
        private int _createduserid;
        private string _createdusername;
        private string _description;
        private string _domain;
        private int _grade;
        private string _imageurl;
        private bool _isrecomend;
        private DateTime? _lastupdatedate;
        private int? _lastupdateuserid;
        private string _lastupdateusername;
        private string _normaimageurl;
        private int _privacy;
        private int _pvcount;
        private int _reference;
        private string _remark;
        private int _sequence;
        private int _state;
        private string _tags;
        private string _thumbimageurl;
        private string _title;
        private int _totalcomment;
        private int _totalfav;
        private int? _totaltime = 0;
        private int _totalup;
        private int _urltype;
        private int? _videoclassid;
        private string _videoformat;
        private int _videoid;
        private string _videourl;

        public int? AlbumID
        {
            get
            {
                return this._albumid;
            }
            set
            {
                this._albumid = value;
            }
        }

        public string Attachment
        {
            get
            {
                return this._attachment;
            }
            set
            {
                this._attachment = value;
            }
        }

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

        public int CreatedUserID
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

        public string CreatedUserName
        {
            get
            {
                return this._createdusername;
            }
            set
            {
                this._createdusername = value;
            }
        }

        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }

        public string Domain
        {
            get
            {
                return this._domain;
            }
            set
            {
                this._domain = value;
            }
        }

        public int Grade
        {
            get
            {
                return this._grade;
            }
            set
            {
                this._grade = value;
            }
        }

        public string ImageUrl
        {
            get
            {
                return this._imageurl;
            }
            set
            {
                this._imageurl = value;
            }
        }

        public bool IsRecomend
        {
            get
            {
                return this._isrecomend;
            }
            set
            {
                this._isrecomend = value;
            }
        }

        public DateTime? LastUpdateDate
        {
            get
            {
                return this._lastupdatedate;
            }
            set
            {
                this._lastupdatedate = value;
            }
        }

        public int? LastUpdateUserID
        {
            get
            {
                return this._lastupdateuserid;
            }
            set
            {
                this._lastupdateuserid = value;
            }
        }

        public string LastUpdateUserName
        {
            get
            {
                return this._lastupdateusername;
            }
            set
            {
                this._lastupdateusername = value;
            }
        }

        public string NormalImageUrl
        {
            get
            {
                return this._normaimageurl;
            }
            set
            {
                this._normaimageurl = value;
            }
        }

        public int Privacy
        {
            get
            {
                return this._privacy;
            }
            set
            {
                this._privacy = value;
            }
        }

        public int PvCount
        {
            get
            {
                return this._pvcount;
            }
            set
            {
                this._pvcount = value;
            }
        }

        public int Reference
        {
            get
            {
                return this._reference;
            }
            set
            {
                this._reference = value;
            }
        }

        public string Remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                this._remark = value;
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

        public int State
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

        public string Tags
        {
            get
            {
                return this._tags;
            }
            set
            {
                this._tags = value;
            }
        }

        public string ThumbImageUrl
        {
            get
            {
                return this._thumbimageurl;
            }
            set
            {
                this._thumbimageurl = value;
            }
        }

        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
            }
        }

        public int TotalComment
        {
            get
            {
                return this._totalcomment;
            }
            set
            {
                this._totalcomment = value;
            }
        }

        public int TotalFav
        {
            get
            {
                return this._totalfav;
            }
            set
            {
                this._totalfav = value;
            }
        }

        public int? TotalTime
        {
            get
            {
                return this._totaltime;
            }
            set
            {
                this._totaltime = value;
            }
        }

        public int TotalUp
        {
            get
            {
                return this._totalup;
            }
            set
            {
                this._totalup = value;
            }
        }

        public int UrlType
        {
            get
            {
                return this._urltype;
            }
            set
            {
                this._urltype = value;
            }
        }

        public int? VideoClassID
        {
            get
            {
                return this._videoclassid;
            }
            set
            {
                this._videoclassid = value;
            }
        }

        public string VideoFormat
        {
            get
            {
                return this._videoformat;
            }
            set
            {
                this._videoformat = value;
            }
        }

        public int VideoID
        {
            get
            {
                return this._videoid;
            }
            set
            {
                this._videoid = value;
            }
        }

        public string VideoUrl
        {
            get
            {
                return this._videourl;
            }
            set
            {
                this._videourl = value;
            }
        }
    }
}

