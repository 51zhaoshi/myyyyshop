namespace Maticsoft.Model.Shop.Supplier
{
    using System;

    [Serializable]
    public class SupplierThemes
    {
        private string _author;
        private DateTime _createddate = DateTime.Now;
        private string _description;
        private string _imageurl;
        private string _language;
        private string _name;
        private string _remark;
        private int _themeid;
        private DateTime? _updateddate;
        private string _website;

        public string Author
        {
            get
            {
                return this._author;
            }
            set
            {
                this._author = value;
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

        public string Language
        {
            get
            {
                return this._language;
            }
            set
            {
                this._language = value;
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

        public int ThemeId
        {
            get
            {
                return this._themeid;
            }
            set
            {
                this._themeid = value;
            }
        }

        public DateTime? UpdatedDate
        {
            get
            {
                return this._updateddate;
            }
            set
            {
                this._updateddate = value;
            }
        }

        public string WebSite
        {
            get
            {
                return this._website;
            }
            set
            {
                this._website = value;
            }
        }
    }
}

