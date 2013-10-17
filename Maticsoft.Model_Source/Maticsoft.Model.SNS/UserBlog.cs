namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class UserBlog
    {
        private string _attachment;
        private int _blogid;
        private DateTime _createddate;
        private string _description;
        private string _keywords;
        private string _linkurl;
        private string _meta_description;
        private string _meta_keywords;
        private string _meta_title;
        private int _pvcount;
        private int _recomend;
        private string _remark;
        private string _seourl;
        private string _staticurl;
        private int _status = 1;
        private string _summary;
        private string _title;
        private int _totalcomment;
        private int _totalfav;
        private int _totalshare;
        private int _userid;
        private string _username;

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

        public int BlogID
        {
            get
            {
                return this._blogid;
            }
            set
            {
                this._blogid = value;
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

        public int Recomend
        {
            get
            {
                return this._recomend;
            }
            set
            {
                this._recomend = value;
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

        public int UserID
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
    }
}

