namespace Maticsoft.Model.CMS
{
    using System;

    [Serializable]
    public class ContentClass
    {
        private bool _allowaddcontent;
        private bool _allowsubclass;
        private int _classid;
        private string _classindex;
        private int _classmodel;
        private string _classname;
        private int _classtypeid;
        private DateTime _createddate = DateTime.Now;
        private int _createduserid;
        private int? _depth;
        private string _description;
        private string _imageurl;
        private string _indexchar;
        private string _keywords;
        private string _meta_description;
        private string _meta_keywords;
        private string _meta_title;
        private string _namepath;
        private string _pagemodelname;
        private int? _parentid;
        private string _path;
        private string _remark;
        private string _seoimagealt;
        private string _seoimagetitle;
        private string _seourl;
        private int _sequence;
        private int _state;

        public bool AllowAddContent
        {
            get
            {
                return this._allowaddcontent;
            }
            set
            {
                this._allowaddcontent = value;
            }
        }

        public bool AllowSubclass
        {
            get
            {
                return this._allowsubclass;
            }
            set
            {
                this._allowsubclass = value;
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

        public string ClassIndex
        {
            get
            {
                return this._classindex;
            }
            set
            {
                this._classindex = value;
            }
        }

        public int ClassModel
        {
            get
            {
                return this._classmodel;
            }
            set
            {
                this._classmodel = value;
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

        public int ClassTypeID
        {
            get
            {
                return this._classtypeid;
            }
            set
            {
                this._classtypeid = value;
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

        public int? Depth
        {
            get
            {
                return this._depth;
            }
            set
            {
                this._depth = value;
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

        public string IndexChar
        {
            get
            {
                return this._indexchar;
            }
            set
            {
                this._indexchar = value;
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

        public string NamePath
        {
            get
            {
                return this._namepath;
            }
            set
            {
                this._namepath = value;
            }
        }

        public string PageModelName
        {
            get
            {
                return this._pagemodelname;
            }
            set
            {
                this._pagemodelname = value;
            }
        }

        public int? ParentId
        {
            get
            {
                return this._parentid;
            }
            set
            {
                this._parentid = value;
            }
        }

        public string Path
        {
            get
            {
                return this._path;
            }
            set
            {
                this._path = value;
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
    }
}

