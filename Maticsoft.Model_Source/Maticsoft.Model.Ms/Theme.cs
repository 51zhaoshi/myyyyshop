namespace Maticsoft.Model.Ms
{
    using System;

    [Serializable]
    public class Theme
    {
        private string _author;
        private DateTime? _createddate = new DateTime?(DateTime.Now);
        private string _description;
        private int _id;
        private bool _iscurrent;
        private string _language;
        private string _name;
        private string _previewphotosrc;
        private string _remark;
        private int? _themesize;
        private string _zippackagesrc;

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

        public DateTime? CreatedDate
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

        public bool IsCurrent
        {
            get
            {
                return this._iscurrent;
            }
            set
            {
                this._iscurrent = value;
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

        public string PreviewPhotoSrc
        {
            get
            {
                return this._previewphotosrc;
            }
            set
            {
                this._previewphotosrc = value;
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

        public int? ThemeSize
        {
            get
            {
                return this._themesize;
            }
            set
            {
                this._themesize = value;
            }
        }

        public string ZipPackageSrc
        {
            get
            {
                return this._zippackagesrc;
            }
            set
            {
                this._zippackagesrc = value;
            }
        }
    }
}

