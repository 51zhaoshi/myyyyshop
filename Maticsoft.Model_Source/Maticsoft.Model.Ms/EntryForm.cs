namespace Maticsoft.Model.Ms
{
    using System;

    [Serializable]
    public class EntryForm
    {
        private int? _age;
        private string _companyaddress;
        private string _description;
        private string _email;
        private string _houseaddress;
        private int _id;
        private string _msn;
        private string _phone;
        private string _qq;
        private int? _regionid;
        private string _remark;
        private string _sex;
        private int? _state;
        private string _telphone;
        private string _username;

        public int? Age
        {
            get
            {
                return this._age;
            }
            set
            {
                this._age = value;
            }
        }

        public string CompanyAddress
        {
            get
            {
                return this._companyaddress;
            }
            set
            {
                this._companyaddress = value;
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

        public string Email
        {
            get
            {
                return this._email;
            }
            set
            {
                this._email = value;
            }
        }

        public string HouseAddress
        {
            get
            {
                return this._houseaddress;
            }
            set
            {
                this._houseaddress = value;
            }
        }

        public int Id
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

        public string MSN
        {
            get
            {
                return this._msn;
            }
            set
            {
                this._msn = value;
            }
        }

        public string Phone
        {
            get
            {
                return this._phone;
            }
            set
            {
                this._phone = value;
            }
        }

        public string QQ
        {
            get
            {
                return this._qq;
            }
            set
            {
                this._qq = value;
            }
        }

        public int? RegionId
        {
            get
            {
                return this._regionid;
            }
            set
            {
                this._regionid = value;
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

        public string Sex
        {
            get
            {
                return this._sex;
            }
            set
            {
                this._sex = value;
            }
        }

        public int? State
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

        public string TelPhone
        {
            get
            {
                return this._telphone;
            }
            set
            {
                this._telphone = value;
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

