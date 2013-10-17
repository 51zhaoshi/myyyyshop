namespace Maticsoft.Model.Members
{
    using System;

    [Serializable]
    public class UserBind
    {
        private int _bindid;
        private bool _comment;
        private bool _grouptopic;
        private bool _ihome;
        private int _mediaid;
        private string _medianickname;
        private string _mediauserid;
        private int? _status = 1;
        private string _tokenaccess;
        private DateTime? _tokenexpiretime;
        private string _tokenrefresh;
        private int _userid;
        private string _weiboLogo;
        private string _weiboName;

        public int BindId
        {
            get
            {
                return this._bindid;
            }
            set
            {
                this._bindid = value;
            }
        }

        public bool Comment
        {
            get
            {
                return this._comment;
            }
            set
            {
                this._comment = value;
            }
        }

        public bool GroupTopic
        {
            get
            {
                return this._grouptopic;
            }
            set
            {
                this._grouptopic = value;
            }
        }

        public bool iHome
        {
            get
            {
                return this._ihome;
            }
            set
            {
                this._ihome = value;
            }
        }

        public int MediaID
        {
            get
            {
                return this._mediaid;
            }
            set
            {
                this._mediaid = value;
            }
        }

        public string MediaNickName
        {
            get
            {
                return this._medianickname;
            }
            set
            {
                this._medianickname = value;
            }
        }

        public string MediaUserID
        {
            get
            {
                return this._mediauserid;
            }
            set
            {
                this._mediauserid = value;
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

        public string TokenAccess
        {
            get
            {
                return this._tokenaccess;
            }
            set
            {
                this._tokenaccess = value;
            }
        }

        public DateTime? TokenExpireTime
        {
            get
            {
                return this._tokenexpiretime;
            }
            set
            {
                this._tokenexpiretime = value;
            }
        }

        public string TokenRefresh
        {
            get
            {
                return this._tokenrefresh;
            }
            set
            {
                this._tokenrefresh = value;
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

        public string WeiboLogo
        {
            get
            {
                return this._weiboLogo;
            }
            set
            {
                this._weiboLogo = value;
            }
        }

        public string WeiboName
        {
            get
            {
                return this._weiboName;
            }
            set
            {
                this._weiboName = value;
            }
        }
    }
}

