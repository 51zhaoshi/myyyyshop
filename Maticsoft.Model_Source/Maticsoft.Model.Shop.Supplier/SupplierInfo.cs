namespace Maticsoft.Model.Shop.Supplier
{
    using System;

    [Serializable]
    public class SupplierInfo
    {
        private string _accountbank;
        private string _accountinfo;
        private string _address;
        private int _agentid = -1;
        private string _artiperson;
        private decimal _balance;
        private string _businesslicense;
        private int _categoryid;
        private string _cellphone;
        private int? _companytype;
        private string _contact;
        private string _contactmail;
        private DateTime _createddate = DateTime.Now;
        private int _createduserid;
        private int? _establishedcity;
        private DateTime? _establisheddate;
        private DateTime? _expirationdate;
        private string _fax;
        private string _homepage;
        private string _introduction;
        private bool _issuppapprove;
        private bool _isuserapprove;
        private string _logo;
        private string _msn;
        private string _name;
        private int _photocount;
        private string _postcode;
        private int _productcount;
        private string _qq;
        private int _rank;
        private int _recomend;
        private int? _regionid;
        private int? _registeredcapital;
        private string _remark;
        private decimal _scoredesc;
        private decimal _scoreservice;
        private decimal _scorespeed;
        private int _sequence;
        private string _servicephone;
        private int _status;
        private int _supplierid;
        private string _taxnumber;
        private string _telphone;
        private int _themeid;
        private DateTime? _updateddate;
        private int? _updateduserid;
        private int _userid;
        private string _username;

        public string AccountBank
        {
            get
            {
                return this._accountbank;
            }
            set
            {
                this._accountbank = value;
            }
        }

        public string AccountInfo
        {
            get
            {
                return this._accountinfo;
            }
            set
            {
                this._accountinfo = value;
            }
        }

        public string Address
        {
            get
            {
                return this._address;
            }
            set
            {
                this._address = value;
            }
        }

        public int AgentId
        {
            get
            {
                return this._agentid;
            }
            set
            {
                this._agentid = value;
            }
        }

        public string ArtiPerson
        {
            get
            {
                return this._artiperson;
            }
            set
            {
                this._artiperson = value;
            }
        }

        public decimal Balance
        {
            get
            {
                return this._balance;
            }
            set
            {
                this._balance = value;
            }
        }

        public string BusinessLicense
        {
            get
            {
                return this._businesslicense;
            }
            set
            {
                this._businesslicense = value;
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

        public string CellPhone
        {
            get
            {
                return this._cellphone;
            }
            set
            {
                this._cellphone = value;
            }
        }

        public int? CompanyType
        {
            get
            {
                return this._companytype;
            }
            set
            {
                this._companytype = value;
            }
        }

        public string Contact
        {
            get
            {
                return this._contact;
            }
            set
            {
                this._contact = value;
            }
        }

        public string ContactMail
        {
            get
            {
                return this._contactmail;
            }
            set
            {
                this._contactmail = value;
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

        public int? EstablishedCity
        {
            get
            {
                return this._establishedcity;
            }
            set
            {
                this._establishedcity = value;
            }
        }

        public DateTime? EstablishedDate
        {
            get
            {
                return this._establisheddate;
            }
            set
            {
                this._establisheddate = value;
            }
        }

        public DateTime? ExpirationDate
        {
            get
            {
                return this._expirationdate;
            }
            set
            {
                this._expirationdate = value;
            }
        }

        public string Fax
        {
            get
            {
                return this._fax;
            }
            set
            {
                this._fax = value;
            }
        }

        public string HomePage
        {
            get
            {
                return this._homepage;
            }
            set
            {
                this._homepage = value;
            }
        }

        public string Introduction
        {
            get
            {
                return this._introduction;
            }
            set
            {
                this._introduction = value;
            }
        }

        public bool IsSuppApprove
        {
            get
            {
                return this._issuppapprove;
            }
            set
            {
                this._issuppapprove = value;
            }
        }

        public bool IsUserApprove
        {
            get
            {
                return this._isuserapprove;
            }
            set
            {
                this._isuserapprove = value;
            }
        }

        public string LOGO
        {
            get
            {
                return this._logo;
            }
            set
            {
                this._logo = value;
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

        public int PhotoCount
        {
            get
            {
                return this._photocount;
            }
            set
            {
                this._photocount = value;
            }
        }

        public string PostCode
        {
            get
            {
                return this._postcode;
            }
            set
            {
                this._postcode = value;
            }
        }

        public int ProductCount
        {
            get
            {
                return this._productcount;
            }
            set
            {
                this._productcount = value;
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

        public int Rank
        {
            get
            {
                return this._rank;
            }
            set
            {
                this._rank = value;
            }
        }

        public int Recomend
        {
            get
            {
                return this._recomend;
            }
            set
            {
                this._recomend = value;
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

        public int? RegisteredCapital
        {
            get
            {
                return this._registeredcapital;
            }
            set
            {
                this._registeredcapital = value;
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

        public decimal ScoreDesc
        {
            get
            {
                return this._scoredesc;
            }
            set
            {
                this._scoredesc = value;
            }
        }

        public decimal ScoreService
        {
            get
            {
                return this._scoreservice;
            }
            set
            {
                this._scoreservice = value;
            }
        }

        public decimal ScoreSpeed
        {
            get
            {
                return this._scorespeed;
            }
            set
            {
                this._scorespeed = value;
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

        public string ServicePhone
        {
            get
            {
                return this._servicephone;
            }
            set
            {
                this._servicephone = value;
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

        public string TaxNumber
        {
            get
            {
                return this._taxnumber;
            }
            set
            {
                this._taxnumber = value;
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

        public int? UpdatedUserId
        {
            get
            {
                return this._updateduserid;
            }
            set
            {
                this._updateduserid = value;
            }
        }

        public int UserId
        {
            get
            {
                return this._userid;
            }
            set
            {
                this._userid = value;
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

