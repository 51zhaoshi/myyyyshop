namespace Maticsoft.Model.CMS
{
    using System;

    [Serializable]
    public class Content
    {
        private string _attachment;
        private string _befrom;
        private int _classid;
        private string _classname;
        private int _contentid;
        private DateTime _createddate = DateTime.Now;
        private int _createduserid;
        private string _createdusername;
        private string _description;
        private string _filename;
        private string _imageurl;
        private bool _iscolor;
        private bool _ishot;
        private bool _isrecomend;
        private bool _istop;
        private string _keywords;
        private DateTime? _lasteditdate;
        private int? _lastedituserid;
        private string _linkurl;
        private string _meta_description;
        private string _meta_keywords;
        private string _meta_title;
        private string _normalimageurl;
        private int _pvcount;
        private string _remary;
        private string _seoimagealt;
        private string _seoimagetitle;
        private string _seourl;
        private int _sequence;
        private int _state;
        private string _staticurl;
        private string _subtitle;
        private string _summary;
        private string _thumbimageurl;
        private string _title;
        private int _totalcomment;
        private int _totalfav;
        private int _totalshare;
        private int _totalsupport;
        public int ComCount;

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

        public string BeFrom
        {
            get
            {
                return this._befrom;
            }
            set
            {
                this._befrom = value;
            }
        }

        public int ClassID
        {
            get
            {
                return this._classid;
            }
            set
            {
                this._classid = value;
            }
        }

        public string ClassName
        {
            get
            {
                return this._classname;
            }
            set
            {
                this._classname = value;
            }
        }

        public int ContentID
        {
            get
            {
                return this._contentid;
            }
            set
            {
                this._contentid = value;
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

        public string FileName
        {
            get
            {
                return this._filename;
            }
            set
            {
                this._filename = value;
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

        public bool IsColor
        {
            get
            {
                return this._iscolor;
            }
            set
            {
                this._iscolor = value;
            }
        }

        public bool IsHot
        {
            get
            {
                return this._ishot;
            }
            set
            {
                this._ishot = value;
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

        public bool IsTop
        {
            get
            {
                return this._istop;
            }
            set
            {
                this._istop = value;
            }
        }

        public string Keywords
        {
            get
            {
                return this._keywords;
            }
            set
            {
                this._keywords = value;
            }
        }

        public DateTime? LastEditDate
        {
            get
            {
                return this._lasteditdate;
            }
            set
            {
                this._lasteditdate = value;
            }
        }

        public int? LastEditUserID
        {
            get
            {
                return this._lastedituserid;
            }
            set
            {
                this._lastedituserid = value;
            }
        }

        public string LinkUrl
        {
            get
            {
                return this._linkurl;
            }
            set
            {
                this._linkurl = value;
            }
        }

        public string Meta_Description
        {
            get
            {
                return this._meta_description;
            }
            set
            {
                this._meta_description = value;
            }
        }

        public string Meta_Keywords
        {
            get
            {
                return this._meta_keywords;
            }
            set
            {
                this._meta_keywords = value;
            }
        }

        public string Meta_Title
        {
            get
            {
                return this._meta_title;
            }
            set
            {
                this._meta_title = value;
            }
        }

        public string NormalImageUrl
        {
            get
            {
                return this._normalimageurl;
            }
            set
            {
                this._normalimageurl = value;
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

        public string Remary
        {
            get
            {
                return this._remary;
            }
            set
            {
                this._remary = value;
            }
        }

        public string SeoImageAlt
        {
            get
            {
                return this._seoimagealt;
            }
            set
            {
                this._seoimagealt = value;
            }
        }

        public string SeoImageTitle
        {
            get
            {
                return this._seoimagetitle;
            }
            set
            {
                this._seoimagetitle = value;
            }
        }

        public string SeoUrl
        {
            get
            {
                return this._seourl;
            }
            set
            {
                this._seourl = value;
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

        public string StaticUrl
        {
            get
            {
                return this._staticurl;
            }
            set
            {
                this._staticurl = value;
            }
        }

        public string SubTitle
        {
            get
            {
                return this._subtitle;
            }
            set
            {
                this._subtitle = value;
            }
        }

        public string Summary
        {
            get
            {
                return this._summary;
            }
            set
            {
                this._summary = value;
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

        public int TotalShare
        {
            get
            {
                return this._totalshare;
            }
            set
            {
                this._totalshare = value;
            }
        }

        public int TotalSupport
        {
            get
            {
                return this._totalsupport;
            }
            set
            {
                this._totalsupport = value;
            }
        }
    }
}

