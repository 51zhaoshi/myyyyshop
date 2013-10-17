namespace Maticsoft.Model.Shop.Tags
{
    using System;

    [Serializable]
    public class TagCategories
    {
        private string _categoryname;
        private int _depth;
        private int _displaysequence;
        private bool _haschildren;
        private int _id;
        private string _meta_description;
        private string _meta_keywords;
        private string _meta_title;
        private int? _parentcategoryid;
        private string _path;
        private string _remark;
        private int? _status;

        public string CategoryName
        {
            get
            {
                return this._categoryname;
            }
            set
            {
                this._categoryname = value;
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

        public int? Status
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
    }
}

