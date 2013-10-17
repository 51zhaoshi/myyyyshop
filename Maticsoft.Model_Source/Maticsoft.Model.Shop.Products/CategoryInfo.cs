namespace Maticsoft.Model.Shop.Products
{
    using System;

    [Serializable]
    public class CategoryInfo
    {
        private int? _associatedproducttype;
        private int _categoryid;
        private int _depth;
        private string _description;
        private int _displaysequence;
        private bool _haschildren;
        private string _imageurl;
        private string _meta_description;
        private string _meta_keywords;
        private string _meta_title;
        private string _name;
        private string _namepath;
        private string _notes1;
        private string _notes2;
        private string _notes3;
        private string _notes4;
        private string _notes5;
        private int _parentcategoryid;
        private string _path;
        private string _rewritename;
        private string _seoimagealt;
        private string _seoimagetitle;
        private string _seourl;
        private string _skuprefix;
        private string _theme;

        public int? AssociatedProductType
        {
            get
            {
                return this._associatedproducttype;
            }
            set
            {
                this._associatedproducttype = value;
            }
        }

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

        public string Notes1
        {
            get
            {
                return this._notes1;
            }
            set
            {
                this._notes1 = value;
            }
        }

        public string Notes2
        {
            get
            {
                return this._notes2;
            }
            set
            {
                this._notes2 = value;
            }
        }

        public string Notes3
        {
            get
            {
                return this._notes3;
            }
            set
            {
                this._notes3 = value;
            }
        }

        public string Notes4
        {
            get
            {
                return this._notes4;
            }
            set
            {
                this._notes4 = value;
            }
        }

        public string Notes5
        {
            get
            {
                return this._notes5;
            }
            set
            {
                this._notes5 = value;
            }
        }

        public int ParentCategoryId
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

        public string RewriteName
        {
            get
            {
                return this._rewritename;
            }
            set
            {
                this._rewritename = value;
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

        public string SKUPrefix
        {
            get
            {
                return this._skuprefix;
            }
            set
            {
                this._skuprefix = value;
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

