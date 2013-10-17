namespace Maticsoft.Model.Shop.Supplier
{
    using System;

    [Serializable]
    public class SupplierCategories
    {
        private int _categoryid;
        private int _createduserid;
        private int _depth;
        private string _description;
        private int _displaysequence;
        private bool _haschildren;
        private string _imageurl;
        private string _meta_description;
        private string _meta_keywords;
        private string _meta_title;
        private string _name;
        private int? _parentcategoryid;
        private string _path;
        private string _remark;
        private string _seoimagealt;
        private string _seoimagetitle;
        private string _seourl;
        private int _supplierid;
        private string _theme;

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

        public int CreatedUserId
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

        public int DisplaySequence
        {
            get
            {
                return this._displaysequence;
            }
            set
            {
                this._displaysequence = value;
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

        public int? ParentCategoryId
        {
            get
            {
                return this._parentcategoryid;
            }
            set
            {
                this._parentcategoryid = value;
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

        public int SupplierId
        {
            get
            {
                return this._supplierid;
            }
            set
            {
                this._supplierid = value;
            }
        }

        public string Theme
        {
            get
            {
                return this._theme;
            }
            set
            {
                this._theme = value;
            }
        }
    }
}

