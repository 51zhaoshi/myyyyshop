namespace Maticsoft.Model.Members
{
    using Maticsoft.Accounts.Bus;
    using System;

    [Serializable]
    public class UsersExpModel : User
    {
        private int? _ablumscount = 0;
        private string _address;
        private bool _addressindexvisible;
        private int _addressvisible;
        private decimal? _balance = 0;
        private DateTime? _birthday;
        private bool _birthdayindexvisible;
        private int _birthdayvisible;
        private string _bloodtype;
        private bool _bloodtypeindexvisible;
        private int _bloodtypevisible;
        private string _bodilyform;
        private bool _bodilyformindexvisible;
        private int _bodilyformvisible;
        private string _constellation;
        private bool _constellationindexvisible;
        private int _constellationvisible;
        private int? _fanscount = 0;
        private int? _favoritedcount = 0;
        private int? _favouritescount = 0;
        private int? _favtopiccount = 0;
        private int? _fellowcount = 0;
        private int? _grade;
        private string _gravatar;
        private string _homepage;
        private bool _isfellow;
        private bool _isUserDPI;
        private string _lastaccessip = "";
        private DateTime? _lastaccesstime;
        private DateTime _lastlogintime;
        private DateTime? _lastposttime;
        private string _marriaged;
        private bool _marriagedindexvisible;
        private int _marriagedvisible;
        private string _msn;
        private string _nativeplace;
        private bool _nativeplaceindexvisible;
        private int _nativeplacevisible;
        private string _payaccount;
        private string _personaldomain;
        private string _personalstatus;
        private bool _personalstatusindexvisible;
        private int _personalstatusvisible;
        private int? _points = 0;
        private int? _productscount = 0;
        private int? _pvcount = 0;
        private string _qq;
        private int? _regionid = 0;
        private string _remark;
        private int? _replytopiccount = 0;
        private int? _sharecount = 0;
        private string _singature;
        private string _telphone;
        private int? _topiccount = 0;

        public int? AblumsCount
        {
            get
            {
                return this._ablumscount;
            }
            set
            {
                this._ablumscount = value;
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

        public bool AddressIndexVisible
        {
            get
            {
                return this._addressindexvisible;
            }
            set
            {
                this._addressindexvisible = value;
            }
        }

        public int AddressVisible
        {
            get
            {
                return this._addressvisible;
            }
            set
            {
                this._addressvisible = value;
            }
        }

        public decimal? Balance
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

        public DateTime? Birthday
        {
            get
            {
                return this._birthday;
            }
            set
            {
                this._birthday = value;
            }
        }

        public bool BirthdayIndexVisible
        {
            get
            {
                return this._birthdayindexvisible;
            }
            set
            {
                this._birthdayindexvisible = value;
            }
        }

        public int BirthdayVisible
        {
            get
            {
                return this._birthdayvisible;
            }
            set
            {
                this._birthdayvisible = value;
            }
        }

        public string BloodType
        {
            get
            {
                return this._bloodtype;
            }
            set
            {
                this._bloodtype = value;
            }
        }

        public bool BloodTypeIndexVisible
        {
            get
            {
                return this._bloodtypeindexvisible;
            }
            set
            {
                this._bloodtypeindexvisible = value;
            }
        }

        public int BloodTypeVisible
        {
            get
            {
                return this._bloodtypevisible;
            }
            set
            {
                this._bloodtypevisible = value;
            }
        }

        public string BodilyForm
        {
            get
            {
                return this._bodilyform;
            }
            set
            {
                this._bodilyform = value;
            }
        }

        public bool BodilyFormIndexVisible
        {
            get
            {
                return this._bodilyformindexvisible;
            }
            set
            {
                this._bodilyformindexvisible = value;
            }
        }

        public int BodilyFormVisible
        {
            get
            {
                return this._bodilyformvisible;
            }
            set
            {
                this._bodilyformvisible = value;
            }
        }

        public string Constellation
        {
            get
            {
                return this._constellation;
            }
            set
            {
                this._constellation = value;
            }
        }

        public bool ConstellationIndexVisible
        {
            get
            {
                return this._constellationindexvisible;
            }
            set
            {
                this._constellationindexvisible = value;
            }
        }

        public int ConstellationVisible
        {
            get
            {
                return this._constellationvisible;
            }
            set
            {
                this._constellationvisible = value;
            }
        }

        public int? FansCount
        {
            get
            {
                return this._fanscount;
            }
            set
            {
                this._fanscount = value;
            }
        }

        public int? FavoritedCount
        {
            get
            {
                return this._favoritedcount;
            }
            set
            {
                this._favoritedcount = value;
            }
        }

        public int? FavouritesCount
        {
            get
            {
                return this._favouritescount;
            }
            set
            {
                this._favouritescount = value;
            }
        }

        public int? FavTopicCount
        {
            get
            {
                return this._favtopiccount;
            }
            set
            {
                this._favtopiccount = value;
            }
        }

        public int? FellowCount
        {
            get
            {
                return this._fellowcount;
            }
            set
            {
                this._fellowcount = value;
            }
        }

        public int? Grade
        {
            get
            {
                return this._grade;
            }
            set
            {
                this._grade = value;
            }
        }

        public string Gravatar
        {
            get
            {
                return this._gravatar;
            }
            set
            {
                this._gravatar = value;
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

        public bool IsFellow
        {
            get
            {
                return this._isfellow;
            }
            set
            {
                this._isfellow = value;
            }
        }

        public bool IsUserDPI
        {
            get
            {
                return this._isUserDPI;
            }
            set
            {
                this._isUserDPI = value;
            }
        }

        public string LastAccessIP
        {
            get
            {
                return this._lastaccessip;
            }
            set
            {
                this._lastaccessip = value;
            }
        }

        public DateTime? LastAccessTime
        {
            get
            {
                return this._lastaccesstime;
            }
            set
            {
                this._lastaccesstime = value;
            }
        }

        public DateTime LastLoginTime
        {
            get
            {
                return this._lastlogintime;
            }
            set
            {
                this._lastlogintime = value;
            }
        }

        public DateTime? LastPostTime
        {
            get
            {
                return this._lastposttime;
            }
            set
            {
                this._lastposttime = value;
            }
        }

        public string Marriaged
        {
            get
            {
                return this._marriaged;
            }
            set
            {
                this._marriaged = value;
            }
        }

        public bool MarriagedIndexVisible
        {
            get
            {
                return this._marriagedindexvisible;
            }
            set
            {
                this._marriagedindexvisible = value;
            }
        }

        public int MarriagedVisible
        {
            get
            {
                return this._marriagedvisible;
            }
            set
            {
                this._marriagedvisible = value;
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

        public string NativePlace
        {
            get
            {
                return this._nativeplace;
            }
            set
            {
                this._nativeplace = value;
            }
        }

        public bool NativePlaceIndexVisible
        {
            get
            {
                return this._nativeplaceindexvisible;
            }
            set
            {
                this._nativeplaceindexvisible = value;
            }
        }

        public int NativePlaceVisible
        {
            get
            {
                return this._nativeplacevisible;
            }
            set
            {
                this._nativeplacevisible = value;
            }
        }

        public string PayAccount
        {
            get
            {
                return this._payaccount;
            }
            set
            {
                this._payaccount = value;
            }
        }

        public string PersonalDomain
        {
            get
            {
                return this._personaldomain;
            }
            set
            {
                this._personaldomain = value;
            }
        }

        public string PersonalStatus
        {
            get
            {
                return this._personalstatus;
            }
            set
            {
                this._personalstatus = value;
            }
        }

        public bool PersonalStatusIndexVisible
        {
            get
            {
                return this._personalstatusindexvisible;
            }
            set
            {
                this._personalstatusindexvisible = value;
            }
        }

        public int PersonalStatusVisible
        {
            get
            {
                return this._personalstatusvisible;
            }
            set
            {
                this._personalstatusvisible = value;
            }
        }

        public int? Points
        {
            get
            {
                return this._points;
            }
            set
            {
                this._points = value;
            }
        }

        public int? ProductsCount
        {
            get
            {
                return this._productscount;
            }
            set
            {
                this._productscount = value;
            }
        }

        public int? PvCount
        {
            get
            {
                return this._pvcount;
            }
            set
            {
                this._pvcount = value;
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

        public int? ReplyTopicCount
        {
            get
            {
                return this._replytopiccount;
            }
            set
            {
                this._replytopiccount = value;
            }
        }

        public int? ShareCount
        {
            get
            {
                return this._sharecount;
            }
            set
            {
                this._sharecount = value;
            }
        }

        public string Singature
        {
            get
            {
                return this._singature;
            }
            set
            {
                this._singature = value;
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

        public int? TopicCount
        {
            get
            {
                return this._topiccount;
            }
            set
            {
                this._topiccount = value;
            }
        }
    }
}

