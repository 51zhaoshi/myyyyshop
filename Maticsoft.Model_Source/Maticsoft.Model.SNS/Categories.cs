namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class Categories
    {
        private int _categoryid;
        private DateTime _createddate = DateTime.Now;
        private int _createduserid;
        private int _depth;
        private string _description;
        private string _fontcolor;
        private bool _haschildren;
        private bool _ismenu;
        private bool _menuisshow;
        private int _menusequence;
        private string _meta_description;
        private string _meta_keywords;
        private string _meta_title;
        private string _name;
        private int _parentid;
        private string _path;
        private string _seourl;
        private int _sequence;
        private int _status;
        private int _type;

        public int CategoryId
        {
            get
            {
                return this._categoryid;
            }
            set
            {
                this._categoryid = value;
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

        public int Depth
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

        public string FontColor
        {
            get
            {
                return this._fontcolor;
            }
            set
            {
                this._fontcolor = value;
            }
        }

        public bool HasChildren
        {
            get
            {
                return this._haschildren;
            }
            set
            {
                this._haschildren = value;
            }
        }

        public bool IsMenu
        {
            get
            {
                return this._ismenu;
            }
            set
            {
                this._ismenu = value;
            }
        }

        public bool MenuIsShow
        {
            get
            {
                return this._menuisshow;
            }
            set
            {
                this._menuisshow = value;
            }
        }

        public int MenuSequence
        {
            get
            {
                return this._menusequence;
            }
            set
            {
                this._menusequence = value;
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

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        public int ParentID
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

        public int Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }
    }
}

