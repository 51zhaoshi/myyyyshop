namespace Maticsoft.Model.Shop.Package
{
    using System;

    [Serializable]
    public class Package
    {
        private int _categoryid;
        private DateTime? _createddate = new DateTime?(DateTime.Now);
        private string _description;
        private string _name;
        private string _normalphotourl;
        private int _packageid;
        private string _photourl;
        private string _remark;
        private int? _status = 0;
        private string _thumbphotourl;

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

        public string NormalPhotoUrl
        {
            get
            {
                return this._normalphotourl;
            }
            set
            {
                this._normalphotourl = value;
            }
        }

        public int PackageId
        {
            get
            {
                return this._packageid;
            }
            set
            {
                this._packageid = value;
            }
        }

        public string PhotoUrl
        {
            get
            {
                return this._photourl;
            }
            set
            {
                this._photourl = value;
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

        public string ThumbPhotoUrl
        {
            get
            {
                return this._thumbphotourl;
            }
            set
            {
                this._thumbphotourl = value;
            }
        }
    }
}

