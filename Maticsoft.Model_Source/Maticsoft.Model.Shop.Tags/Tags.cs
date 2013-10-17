namespace Maticsoft.Model.Shop.Tags
{
    using System;

    [Serializable]
    public class Tags
    {
        private bool _isrecommand;
        private string _meta_description;
        private string _meta_keywords;
        private string _meta_title;
        private int _status;
        private int _tagcategoryid;
        private int _tagid;
        private string _tagname;

        public bool IsRecommand
        {
            get
            {
                return this._isrecommand;
            }
            set
            {
                this._isrecommand = value;
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

        public int TagCategoryId
        {
            get
            {
                return this._tagcategoryid;
            }
            set
            {
                this._tagcategoryid = value;
            }
        }

        public int TagID
        {
            get
            {
                return this._tagid;
            }
            set
            {
                this._tagid = value;
            }
        }

        public string TagName
        {
            get
            {
                return this._tagname;
            }
            set
            {
                this._tagname = value;
            }
        }
    }
}

